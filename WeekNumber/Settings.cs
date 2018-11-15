#region Using statements

using Microsoft.Win32;
using System.Configuration;
using System.Globalization;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal static class Settings
    {
        #region Internal static property that updates registry for application to start when Windows start

        internal static bool StartWithWindows
        {
            get => Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", Application.ProductName, null) != null;
            set
            {
                using (var registryKey = Registry.CurrentUser)
                using (var regRun = registryKey?.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", true))
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
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings[setting].Value = value;
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        #endregion Internal static methods

        #region Private method that creates the application settings file if needed

        private static void CreateSettings()
        {
            var settingsFile = Application.ExecutablePath + ".config";
            if (!System.IO.File.Exists(settingsFile))
            {
                var currentCultureInfo = CultureInfo.CurrentCulture;
                var firstDay = currentCultureInfo.DateTimeFormat.FirstDayOfWeek;
                var calendarWeekRule = currentCultureInfo.DateTimeFormat.CalendarWeekRule;
                var xml = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
<<<<<<< HEAD
    <add key=""DayOfWeek"" value=""Monday""/>
    <add key=""CalendarWeekRule"" value=""FirstFourDayWeek""/>
    <add key=""Background"" value=""Color.Black""/>
    <add key=""Foreground"" value=""Color.White""/>
=======
    <add key=""DayOfWeek"" value=""{firstDay}""/>
    <add key=""CalendarWeekRule"" value=""{calendarWeekRule}"" />
>>>>>>> 554a7c547975b1269e94159adc5d4c07309c871f
  </appSettings>
</configuration>";
                System.IO.File.WriteAllText(settingsFile, xml, System.Text.Encoding.UTF8);
            }
        }

        #endregion Private method that creates the application settings file if needed
    }
}