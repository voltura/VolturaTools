#region Using statements

using System.Collections.ObjectModel;
using System.IO;

#endregion

namespace DiskSpace
{
    /// <summary>
    ///     Lists local drives
    /// </summary>
    internal static class LocalDrives
    {
        #region Internal static functions

        /// <summary>
        ///     Local drives
        /// </summary>
        /// <returns></returns>
        internal static Collection<Drive> Drives()
        {
            var drives = new Collection<Drive>();
            var space = string.Empty.PadLeft(1);
            var allDrives = DriveInfo.GetDrives();
            foreach (var d in allDrives)
                if (d.DriveType == DriveType.Fixed ||
                    d.DriveType == DriveType.Removable)
                    drives.Add(new Drive(d.Name.Substring(0, 1),
                        d.Name + space + d.VolumeLabel));
            return drives;
        }

        /// <summary>
        ///     Get next drive from system
        /// </summary>
        /// <param name="currentDriveLetter">Currently monitoried drive</param>
        /// <returns>Next drive letter after given drive</returns>
        internal static string GetNextDriveLetter(string currentDriveLetter)
        {
            var driveLetters = DriveLetters;
            if (driveLetters.Count <= 1)
                return currentDriveLetter;
            var cdIndex = driveLetters.IndexOf(currentDriveLetter);
            return cdIndex < driveLetters.Count - 1 ? driveLetters[cdIndex + 1] : driveLetters[0];
        }

        #endregion

        #region Private static properties

        /// <summary>
        ///     Returns drive letters of fixed and removable drives
        /// </summary>
        /// <returns></returns>
        private static Collection<string> DriveLetters
        {
            get
            {
                var drives = new Collection<string>();
                var allDrives = DriveInfo.GetDrives();
                foreach (var d in allDrives)
                    if (d.DriveType == DriveType.Fixed ||
                        d.DriveType == DriveType.Removable)
                        drives.Add(d.Name.Substring(0, 1));
                return drives;
            }
        }

        #endregion
    }
}