using System;
using System.ComponentModel;
using Microsoft.Win32;

namespace Com.Xenthrax.RegistrySettings
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class BaseKeyAttribute : Attribute
	{
		public BaseKeyAttribute(RegistryHive hive)
		{
			this.Hive = hive;
			this.View = RegistryView.Default;
		}

		public RegistryHive Hive { get; private set; }
		public RegistryView View { get; set; }
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class SubKeyAttribute : Attribute
	{
		public SubKeyAttribute(string subKey)
		{
			if (subKey == null)
				throw new ArgumentNullException("subKey");

			this.SubKey = subKey;
		}

		public string SubKey { get; private set; }
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class NameAttribute : Attribute
	{
		public NameAttribute(string name)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			this.Name = name;
		}

		public string Name { get; private set; }
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class DefaultKeyAttribute : Attribute
	{
		
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class ExpandableStringAttribute : Attribute
	{
		public ExpandableStringAttribute()
		{
			this.ExpandOnGetValue = true;
		}

		public bool ExpandOnGetValue { get; set; }
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class TypeConverterAttribute : Attribute
	{
		public TypeConverterAttribute(Type converterType)
		{
			if (converterType == null)
				throw new ArgumentNullException("converterType");

			this.Converter = (TypeConverter)Activator.CreateInstance(converterType);
			this.ConverterType = converterType;
			this.SourceType = null;
			this.TargetType = typeof(string);
		}

		public TypeConverter Converter { get; private set; }
		public Type ConverterType { get; private set; }
		public Type SourceType { get; set; }
		public Type TargetType { get; set; }
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class SerializeAsStringAttribute : Attribute
	{

	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class BoolAsStringAttribute : Attribute
	{

	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class EnumAsStringAttribute : Attribute
	{

	}
}