using System.Collections.Generic;
using System.IO;

namespace DiskSpace
{
    /// <summary>
    /// Lists local drives
    /// </summary>
    public static class LocalDrives
    {
        /// <summary>
        /// Local drives
        /// </summary>
        /// <returns></returns>
        public static System.Collections.ObjectModel.Collection<Drive> Drives()
        {
            System.Collections.ObjectModel.Collection<Drive> drives = new System.Collections.ObjectModel.Collection<Drive>();
            string SPACE = string.Empty.PadLeft(1);
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Fixed ||
                    d.DriveType == DriveType.Removable)
                {
                    drives.Add(new Drive(d.Name.Substring(0, 1),
                        d.Name + SPACE + d.VolumeLabel));
                }
            }
            return drives;
        }
    }
}
