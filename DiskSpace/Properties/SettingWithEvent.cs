namespace DiskSpace.Properties
{
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
    {
        public event System.EventHandler DriveChanged;
        private static readonly object Locker = new object();

        /// <summary>
        /// Drive to report free space on
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Drive to report free space on")]
        [global::System.Configuration.DefaultSettingValueAttribute("C")]
        public string DriveLetter
        {
            get
            {
                return ((string)(this["driveLetter"]));
            }
            set
            {
                lock (Locker)
                {
                    if (this["driveLetter"].ToString() != value)
                    {
                        this["driveLetter"] = value;
                        DriveChanged?.Invoke(this, System.EventArgs.Empty);
                    }
                }
            }
        }
    }
}
