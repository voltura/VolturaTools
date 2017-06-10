#region Using statements

using System.Collections.ObjectModel;
using System.IO;

#endregion

namespace DiskSpace
{
    /// <summary>
    /// Lists local drives
    /// </summary>
    public static class LocalDrives
    {
        #region Public static functions

        /// <summary>
        /// Local drives
        /// </summary>
        /// <returns></returns>
        public static Collection<Drive> Drives()
        {
            Collection<Drive> drives = new Collection<Drive>();
            string space = string.Empty.PadLeft(1);
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Fixed ||
                    d.DriveType == DriveType.Removable)
                {
                    drives.Add(new Drive(d.Name.Substring(0, 1),
                        d.Name + space + d.VolumeLabel));
                }
            }
            return drives;
        }

        /// <summary>
        /// Get next drive from system
        /// </summary>
        /// <param name="currentDriveLetter">Currently monitoried drive</param>
        /// <returns>Next drive letter after given drive</returns>
        public static string GetNextDriveLetter(string currentDriveLetter)
        {
            Collection<string> driveLetters = DriveLetters;
            if (driveLetters.Count <= 1)
            {
                return currentDriveLetter;
            }
            int cdIndex = driveLetters.IndexOf(currentDriveLetter);
            return (cdIndex < driveLetters.Count - 1) ? driveLetters[cdIndex + 1] : driveLetters[0];
        }

        #endregion

        #region Private static properties

        /// <summary>
        /// Returns drive letters of fixed and removable drives
        /// </summary>
        /// <returns></returns>
        private static Collection<string> DriveLetters
        {
            get
            {
                Collection<string> drives = new Collection<string>();
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    if (d.DriveType == DriveType.Fixed ||
                        d.DriveType == DriveType.Removable)
                    {
                        drives.Add(d.Name.Substring(0, 1));
                    }
                }
                return drives;
            }
        }

        #endregion
    }
}