using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using Com.Xenthrax.RegistrySettings;

namespace Properties
{
	[SettingsProvider(typeof(RegistrySettingsProvider))]
	[TypeConverter(typeof(Settings_Test.VersionConverter), SourceType = typeof(Version))]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)ApplicationSettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return defaultInstance;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("False")]
		public bool Bool
		{
			get
			{
				return (bool)this["Bool"];
			}
			set
			{
				this["Bool"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public byte Byte
		{
			get
			{
				return (byte)this["Byte"];
			}
			set
			{
				this["Byte"] = value;
			}
		}

		[UserScopedSetting]
		public char Char
		{
			get
			{
				return (char)this["Char"];
			}
			set
			{
				this["Char"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public decimal Decimal
		{
			get
			{
				return (decimal)this["Decimal"];
			}
			set
			{
				this["Decimal"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public double Double
		{
			get
			{
				return (double)this["Double"];
			}
			set
			{
				this["Double"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public float Float
		{
			get
			{
				return (float)this["Float"];
			}
			set
			{
				this["Float"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public int Int
		{
			get
			{
				return (int)this["Int"];
			}
			set
			{
				this["Int"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public long Long
		{
			get
			{
				return (long)this["Long"];
			}
			set
			{
				this["Long"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public sbyte SByte
		{
			get
			{
				return (sbyte)this["SByte"];
			}
			set
			{
				this["SByte"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public short Short
		{
			get
			{
				return (short)this["Short"];
			}
			set
			{
				this["Short"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string String
		{
			get
			{
				return (string)this["String"];
			}
			set
			{
				this["String"] = value;
			}
		}

		[UserScopedSetting]
		public StringCollection StringCollection
		{
			get
			{
				return (StringCollection)this["StringCollection"];
			}
			set
			{
				this["StringCollection"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("1/01/0001 00:00:00")]
		public DateTime DateTime
		{
			get
			{
				return (DateTime)this["DateTime"];
			}
			set
			{
				this["DateTime"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("00000000-0000-0000-0000-000000000000")]
		public Guid Guid
		{
			get
			{
				return (Guid)this["Guid"];
			}
			set
			{
				this["Guid"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("00:00:00")]
		public TimeSpan TimeSpan
		{
			get
			{
				return (TimeSpan)this["TimeSpan"];
			}
			set
			{
				this["TimeSpan"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		public uint UInt
		{
			get
			{
				return (uint)this["UInt"];
			}
			set
			{
				this["UInt"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		[SubKey("SubKey2\\SubSubKey")]
		public ulong ULong
		{
			get
			{
				return (ulong)this["ULong"];
			}
			set
			{
				this["ULong"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("0")]
		[SubKey("SubKey2\\SubSubKey")]
		public ushort UShort
		{
			get
			{
				return (ushort)this["UShort"];
			}
			set
			{
				this["UShort"] = value;
			}
		}

		[UserScopedSetting]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		[DefaultSettingValue("")]
		[SubKey("SubKey2")]
		public Uri Url
		{
			get
			{
				return (Uri)this["Url"];
			}
			set
			{
				this["Url"] = value;
			}
		}

		[UserScopedSetting]
		[SubKey("SubKey2")]
		public byte[] Bytes
		{
			get
			{
				return (byte[])this["Bytes"];
			}
			set
			{
				this["Bytes"] = value;
			}
		}

		[UserScopedSetting]
		[SubKey("SubKey1")]
		public Version Version
		{
			get
			{
				return (Version)this["Version"];
			}
			set
			{
				this["Version"] = value;
			}
		}

		[UserScopedSetting]
		[SubKey("SubKey1")]
		public ConsoleColor Enum
		{
			get
			{
				return (ConsoleColor)this["Enum"];
			}
			set
			{
				this["Enum"] = value;
			}
		}

		[UserScopedSetting]
		public string[] StringArray
		{
			get
			{
				return (string[])this["StringArray"];
			}
			set
			{
				this["StringArray"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultKey]
		public string DefaultString
		{
			get
			{
				return (string)this["DefaultString"];
			}
			set
			{
				this["DefaultString"] = value;
			}
		}

		[UserScopedSetting]
		[SubKey("SubKey1")]
		[DefaultKey]
		public string SubKey1DefaultString
		{
			get
			{
				return (string)this["SubKey1DefaultString"];
			}
			set
			{
				this["SubKey1DefaultString"] = value;
			}
		}

		[UserScopedSetting]
		[SubKey("SubKey2")]
		[DefaultKey]
		public string SubKey2DefaultString
		{
			get
			{
				return (string)this["SubKey2DefaultString"];
			}
			set
			{
				this["SubKey2DefaultString"] = value;
			}
		}

		[UserScopedSetting]
		[SubKey("SubKey2\\SubSubKey")]
		[DefaultKey]
		public string SubSubKeyDefaultString
		{
			get
			{
				return (string)this["SubSubKeyDefaultString"];
			}
			set
			{
				this["SubSubKeyDefaultString"] = value;
			}
		}

		[UserScopedSetting]
		[ExpandableString]
		[Name("New Value #1")]
		public string NewValue1
		{
			get
			{
				return (string)this["NewValue1"];
			}
			set
			{
				this["NewValue1"] = value;
			}
		}

		[Setting]
		[BaseKey(Microsoft.Win32.RegistryHive.ClassesRoot)]
		[SubKey(".exe")]
		[Name("Content Type")]
		public string ExeContentType
		{
			get
			{
				return (string)this["ExeContentType"];
			}
			set
			{
				this["ExeContentType"] = value;
			}
		}

		[Setting]
		[BaseKey(Microsoft.Win32.RegistryHive.ClassesRoot)]
		[SubKey(".htm\\OpenWithList\\WinWord.exe\\shell\\edit\\command")]
		[DefaultKey]
		public string HtmCommand
		{
			get
			{
				return (string)this["HtmCommand"];
			}
			set
			{
				this["HtmCommand"] = value;
			}
		}
	}
}