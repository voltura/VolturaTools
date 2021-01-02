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
#pragma warning disable IDE1006 // Naming Styles
        public string driveLetter
#pragma warning restore IDE1006 // Naming Styles
        {
            get => (string)this[nameof(driveLetter)];
            set
            {
                lock (Locker)
                {
                    if (this[nameof(driveLetter)].ToString() != value)
                    {
                        this[nameof(driveLetter)] = value;
                        DriveChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}