#region Using statements

using Microsoft.Win32;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

#endregion Using statements

namespace NetOn
{
    internal static class Settings
    {
        #region Internal static property that updates registry for application to start when Windows start

        internal static bool StartWithWindows
        {
            get => Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", Application.ProductName, null) != null;
            set
            {
                using (RegistryKey registryKey = Registry.CurrentUser)
                using (RegistryKey regRun = registryKey?.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", true))
                {
                    if (value)
                    {
                        regRun?.SetValue(Application.ProductName, Application.ExecutablePath);
                    }
                    else
                    {
                        regRun?.DeleteValue(Application.ProductName);
                    }
                    registryKey?.Flush();
                }
            }
        }

        #endregion Internal static property that updates registry for application to start when Windows start

        #region Internal static property for MuteAllSounds setting

        internal static bool MuteAllSounds
        {
            get => SettingIsValue(nameof(MuteAllSounds), true.ToString());
            set => UpdateSetting(nameof(MuteAllSounds), value.ToString());
        }

        #endregion Internal static property for MuteAllSounds setting

        #region Internal static methods

        internal static bool SettingIsValue(string setting, string value)
        {
            CreateSettings();
            return ConfigurationManager.AppSettings.Get(setting) == value;
        }

        internal static string GetSetting(string setting)
        {
            CreateSettings();
            return ConfigurationManager.AppSettings.Get(setting);
        }

        internal static void UpdateSetting(string setting, string value)
        {
            CreateSettings();
            Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;
            settings[setting].Value = value;
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        #endregion Internal static methods

        #region Private method that creates the application settings file if needed

        private static void CreateSettings()
        {
            string settingsFile = Application.ExecutablePath + ".config";
            if (!File.Exists(settingsFile))
            {
                string xml = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
    <add key=""Background"" value=""Black""/>
    <add key=""Foreground"" value=""White""/>
    <add key=""DisabledBackground"" value=""Red""/>
    <add key=""DisabledForeground"" value=""White""/>
    <add key=""ForceRedraw"" value=""False""/>
    <add key=""MuteAllSounds"" value=""False""/>
  </appSettings>
</configuration>";
                File.WriteAllText(settingsFile, xml, System.Text.Encoding.UTF8);
            }
        }

        #endregion Private method that creates the application settings file if needed
    }
}