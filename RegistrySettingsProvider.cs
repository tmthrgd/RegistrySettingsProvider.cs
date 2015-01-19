using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Microsoft.Win32;

namespace Com.Xenthrax.RegistrySettings
{
	public sealed class RegistrySettingsProvider : SettingsProvider
	{
		public RegistrySettingsProvider()
		{
			this.applicationName = null;
			this.registryKey = null;
		}

		private string applicationName;
		public override string ApplicationName
		{
			get
			{
				return this.applicationName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value");

				this.applicationName = value;
			}
		}

		private string registryKey;
		public string RegistryKey
		{
			get
			{
				if (this.registryKey == null)
					return string.Format("Software\\{0}", this.ApplicationName);

				return this.registryKey;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value");

				this.registryKey = value;
			}
		}

		public override void Initialize(string name, NameValueCollection config)
		{
			if (string.IsNullOrEmpty(name))
				name = typeof(RegistrySettingsProvider).Name;

			base.Initialize(name, config);

			if (string.IsNullOrEmpty(this.ApplicationName))
				this.ApplicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
		}

		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
		{
			SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
			RegistryKey appk = null;
			RegistryKey usrk = null;

			try
			{
				foreach (SettingsProperty property in collection)
				{
					SettingsPropertyValue value = new SettingsPropertyValue(property);
					Type t = property.PropertyType;
					RegistryKey rk;
					RegistryKey bk = null;
					RegistryKey sk = null;
					string name = value.Name;
					SubKeyAttribute subKeyAttribute = (SubKeyAttribute)property.Attributes[typeof(SubKeyAttribute)];
					NameAttribute nameAttribute = (NameAttribute)property.Attributes[typeof(NameAttribute)];
					ExpandableStringAttribute expandableAttribute = (ExpandableStringAttribute)property.Attributes[typeof(ExpandableStringAttribute)];
					BaseKeyAttribute baseKeyAttribute = (BaseKeyAttribute)property.Attributes[typeof(BaseKeyAttribute)];
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)property.Attributes[typeof(TypeConverterAttribute)];

					if (baseKeyAttribute != null)
						rk = bk = Microsoft.Win32.RegistryKey.OpenBaseKey(baseKeyAttribute.Hive, baseKeyAttribute.View);
					else if (property.Attributes[typeof(UserScopedSettingAttribute)] != null)
						rk = usrk ?? (usrk = Registry.CurrentUser.OpenSubKey(this.RegistryKey, false));
					else if (property.Attributes[typeof(ApplicationScopedSettingAttribute)] != null)
						rk = appk ?? (appk = Registry.LocalMachine.OpenSubKey(this.RegistryKey, false));
					else
						throw new Exception("Property must be marked with either BaseKeyAttribute, UserScopedSettingAttribute or ApplicationScopedSettingAttribute.");

					try
					{
						if (rk != null && subKeyAttribute != null)
							rk = sk = rk.OpenSubKey(subKeyAttribute.SubKey, false);

						if (property.Attributes[typeof(DefaultKeyAttribute)] != null)
							name = string.Empty;
						else if (nameAttribute != null)
							name = nameAttribute.Name;

						if (rk != null)
						{
							if (property.Attributes[typeof(SerializeAsStringAttribute)] != null)
								value.SerializedValue = rk.GetValue(name, property.DefaultValue);
							else if (typeConverterAttribute != null && (typeConverterAttribute.SourceType == null || typeConverterAttribute.SourceType == t))
							{
								object v = rk.GetValue(name);

								if (v != null)
									value.PropertyValue = typeConverterAttribute.Converter.ConvertFrom(new RegistrySettingsProviderContext(t, typeConverterAttribute.TargetType, v, null, property), CultureInfo.InvariantCulture, v);
							}
							else if (t == typeof(string))
							{
								if (expandableAttribute == null || expandableAttribute.ExpandOnGetValue)
									value.PropertyValue = rk.GetValue(name);
								else
									value.PropertyValue = rk.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
							}
							else if (t == typeof(sbyte) || t == typeof(short) || t == typeof(int) || t == typeof(long)
								|| t == typeof(byte) || t == typeof(ushort) || t == typeof(uint) || t == typeof(ulong)
								|| (t == typeof(bool) && (property.Attributes[typeof(BoolAsStringAttribute)] == null))
								|| (t.IsEnum && (property.Attributes[typeof(EnumAsStringAttribute)] == null))
								|| t == typeof(byte[]) || t == typeof(string[]))
							{
								object v = rk.GetValue(name);

								if (v != null)
								{
									if (t.IsEnum)
										value.PropertyValue = Enum.ToObject(property.PropertyType, v);
									else
										value.PropertyValue = Convert.ChangeType(v, property.PropertyType);
								}
							}
							else if (t == typeof(StringCollection))
							{
								string[] v = rk.GetValue(name) as string[];

								if (v != null)
								{
									StringCollection strValue = new StringCollection();
									strValue.AddRange(v);
									value.PropertyValue = strValue;
								}
							}
							else
								value.SerializedValue = rk.GetValue(name, property.DefaultValue);
						}
					}
					finally
					{
						if (sk != null)
							sk.Dispose();

						if (bk != null)
							bk.Dispose();
					}

					value.IsDirty = false;
					values.Add(value);
				}
			}
			finally
			{
				if (appk != null)
					appk.Dispose();

				if (usrk != null)
					usrk.Dispose();
			}

			return values;
		}

		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
		{
			RegistryKey appk = null;
			RegistryKey usrk = null;

			try
			{
				foreach (SettingsPropertyValue value in collection)
				{
					SettingsProperty property = value.Property;
					Type t = property.PropertyType;
					RegistryKey rk;
					RegistryKey bk = null;
					RegistryKey sk = null;
					string name = value.Name;
					SubKeyAttribute subKeyAttribute = (SubKeyAttribute)property.Attributes[typeof(SubKeyAttribute)];
					NameAttribute nameAttribute = (NameAttribute)property.Attributes[typeof(NameAttribute)];
					BaseKeyAttribute baseKeyAttribute = (BaseKeyAttribute)property.Attributes[typeof(BaseKeyAttribute)];
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)property.Attributes[typeof(TypeConverterAttribute)];

					if (baseKeyAttribute != null)
						rk = bk = Microsoft.Win32.RegistryKey.OpenBaseKey(baseKeyAttribute.Hive, baseKeyAttribute.View);
					else if (property.Attributes[typeof(UserScopedSettingAttribute)] != null)
						rk = usrk ?? (usrk = Registry.CurrentUser.CreateSubKey(this.RegistryKey));
					else if (property.Attributes[typeof(ApplicationScopedSettingAttribute)] != null)
						rk = appk ?? (appk = Registry.LocalMachine.CreateSubKey(this.RegistryKey));
					else
						throw new Exception("Property must be marked with either BaseKeyAttribute, UserScopedSettingAttribute or ApplicationScopedSettingAttribute.");

					try
					{
						if (rk != null && subKeyAttribute != null)
							rk = sk = rk.CreateSubKey(subKeyAttribute.SubKey);

						if (property.Attributes[typeof(DefaultKeyAttribute)] != null)
							name = string.Empty;
						else if (nameAttribute != null)
							name = nameAttribute.Name;

						if (property.Attributes[typeof(SerializeAsStringAttribute)] != null)
							rk.SetValue(name, value.SerializedValue ?? string.Empty, RegistryValueKind.String);
						else
						{
							object v = value.PropertyValue;
							bool hasConverter = (typeConverterAttribute != null && (typeConverterAttribute.SourceType == null || typeConverterAttribute.SourceType == t));

							if (hasConverter)
							{
								v = typeConverterAttribute.Converter.ConvertTo(new RegistrySettingsProviderContext(t, typeConverterAttribute.TargetType, v, value.SerializedValue, property), CultureInfo.InvariantCulture, v, typeConverterAttribute.TargetType);
								t = typeConverterAttribute.TargetType;
							}
							
							if (t.IsEnum && (property.Attributes[typeof(EnumAsStringAttribute)] == null))
								t = Enum.GetUnderlyingType(t);

							if (t == typeof(sbyte) || t == typeof(short) || t == typeof(int)
							   || t == typeof(byte) || t == typeof(ushort) || t == typeof(uint)
							   || (t == typeof(bool) && (property.Attributes[typeof(BoolAsStringAttribute)] == null)))
								rk.SetValue(name, v, RegistryValueKind.DWord);
							else if (t == typeof(long) || t == typeof(ulong))
								rk.SetValue(name, v, RegistryValueKind.QWord);
							else if (t == typeof(byte[]))
								rk.SetValue(name, v ?? new byte[0], RegistryValueKind.Binary);
							else if (t == typeof(string[]))
								rk.SetValue(name, v ?? new string[0], RegistryValueKind.MultiString);
							else if (t == typeof(StringCollection))
								rk.SetValue(name, (v == null)
									? new string[0]
									: ((StringCollection)v).Cast<string>().ToArray(), RegistryValueKind.MultiString);
							else if (property.Attributes[typeof(ExpandableStringAttribute)] != null)
								rk.SetValue(name, v ?? string.Empty, RegistryValueKind.ExpandString);
							else if (t == typeof(string) || hasConverter)
								rk.SetValue(name, v ?? string.Empty, RegistryValueKind.String);
							else
								rk.SetValue(name, value.SerializedValue ?? string.Empty, RegistryValueKind.String);
						}
					}
					finally
					{
						if (sk != null)
							sk.Dispose();

						if (bk != null)
							bk.Dispose();
					}

					value.IsDirty = false;
				}
			}
			finally
			{
				if (appk != null)
					appk.Dispose();

				if (usrk != null)
					usrk.Dispose();
			}
		}
	}
}