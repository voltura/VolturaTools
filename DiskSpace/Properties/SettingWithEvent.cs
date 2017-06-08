using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

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
        [SuppressMessage("ReSharper", "InconsistentNaming")]
#pragma warning disable IDE1006 // Naming Styles
        public string driveLetter
#pragma warning restore IDE1006 // Naming Styles
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
