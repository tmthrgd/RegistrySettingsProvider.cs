# RegistrySettingsProvider.cs

Registry Settings Provider allows the built-in `Properties.Settings` to be backed by the Windows registry instead of the file system.

To replace the default provider the attribute `[SettingsProvider(typeof(RegistrySettingsProvider))]` must be placed directly before `internal sealed class Settings : ApplicationSettingsBase` in `Settings.cs`.

Other attributes found in [`RegistrySettingsProviderAttributes.cs`](RegistrySettingsProviderAttributes.cs) may be added to individual properties to change how and where they are saved.
