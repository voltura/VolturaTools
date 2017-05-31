using System;
using System.Configuration;

namespace DiskSpace.Properties
{
    internal sealed partial class Settings
    {
        public event EventHandler DriveChanged;
        private static readonly object Locker = new object();

        /// <summary>
        /// Drive to report free space on
        /// </summary>
        [UserScopedSetting]
        [SettingsDescription("Drive to report free space on")]
        [DefaultSettingValue("C")]
        public string DriveLetter
        {
            get => (string) this["driveLetter"];
            set
            {
                lock (Locker)
                {
                    if (this["driveLetter"].ToString() != value)
                    {
                        this["driveLetter"] = value;
                        DriveChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}
