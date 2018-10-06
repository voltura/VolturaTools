#region Using statements

using Microsoft.Win32;
using System.Windows.Forms;

#endregion

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
                using (RegistryKey registryKey = Registry.CurrentUser)
                {
                    if (value)
                    {
                        registryKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", true).SetValue(Application.ProductName, 
                            Application.ExecutablePath);
                    }
                    else if (Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", 
                        Application.ProductName, null) != null)
                    {
                        registryKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", true).DeleteValue(Application.ProductName);
                    }
                    registryKey.Flush();
                }
            }
        }

        #endregion
    }
}