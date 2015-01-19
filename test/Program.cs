using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Com.Xenthrax.RegistrySettings;

namespace Settings_Test
{
	class VersionConverter : System.ComponentModel.TypeConverter
	{
		public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value == null)
				return null;

			if (!(value is string))
				throw new ArgumentException("value");

			return Version.Parse((string)value);
		}

		public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (value == null)
				return null;

			if (!(value is Version))
				throw new ArgumentException("value");

			if (destinationType != typeof(string))
				throw new ArgumentException("destinationType");

			return ((Version)value).ToString();
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			foreach (SettingsProperty property in Properties.Settings.Default.Properties)
			{
				SettingsPropertyCollection properties = new SettingsPropertyCollection();
				properties.Add(property);

				SettingsPropertyValueCollection values = property.Provider.GetPropertyValues(Properties.Settings.Default.Context, properties);
				Properties.Settings.Default.PropertyValues.Add(values[property.Name]);
			}

			foreach (SettingsPropertyValue value in Properties.Settings.Default
					.PropertyValues
					.OfType<SettingsPropertyValue>()
					.OrderBy(value => value.Name))
				Console.WriteLine("{0}: {1}", value.Name, value.PropertyValue);

			if (!Properties.Settings.Default.Bool)
			{
				Properties.Settings.Default.Bool = true;
				Properties.Settings.Default.Byte = byte.MaxValue / 2;
				Properties.Settings.Default.Char = 'R';
				Properties.Settings.Default.DateTime = DateTime.Now;
				Properties.Settings.Default.Decimal = 3.14159265359M;
				Properties.Settings.Default.Double = 3.14;
				Properties.Settings.Default.Float = 3.1F;
				Properties.Settings.Default.Guid = Guid.NewGuid();
				Properties.Settings.Default.Int = int.MinValue / 2;
				Properties.Settings.Default.Long = long.MinValue / 2;
				Properties.Settings.Default.SByte = sbyte.MinValue / 2;
				Properties.Settings.Default.Short = short.MinValue / 2;
				Properties.Settings.Default.String = "Now is the time for all good men to come to the aid of the party";
				Properties.Settings.Default.StringCollection = new System.Collections.Specialized.StringCollection();
				Properties.Settings.Default.StringCollection.Add("All your base are belong to us.");
				Properties.Settings.Default.StringCollection.Add("You have no chance to survive make your time.");
				Properties.Settings.Default.TimeSpan = new TimeSpan(1, 2, 3, 4, 5);
				Properties.Settings.Default.UInt = uint.MaxValue / 2;
				Properties.Settings.Default.ULong = ulong.MaxValue / 2;
				Properties.Settings.Default.Url = new Uri("http://example.com/");
				Properties.Settings.Default.UShort = ushort.MaxValue / 2;
				Properties.Settings.Default.Bytes = Enumerable.Range(0, 256).Select(b => (byte)b).ToArray();
				Properties.Settings.Default.Version = new Version(1, 2, 3, 4);
				Properties.Settings.Default.Enum = ConsoleColor.Green;
				Properties.Settings.Default.StringArray = new string[]
				{
					"Lorem ipsum dolor sit amet,",
					"consectetur adipiscing elit,",
					"sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
					"Ut enim ad minim veniam,",
					"quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
				};
				Properties.Settings.Default.DefaultString = "The quick brown fox jumps over the lazy dog";
				Properties.Settings.Default.SubKey1DefaultString = "New Petitions";
				Properties.Settings.Default.SubKey2DefaultString = "Building Code";
				Properties.Settings.Default.SubSubKeyDefaultString = "Etaoin shrdlu";
				Properties.Settings.Default.NewValue1 = "%AppData%\\Microsoft";
				Properties.Settings.Default.Save();
			}

			Console.ReadLine();
		}
	}
}