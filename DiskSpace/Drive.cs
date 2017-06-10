﻿namespace DiskSpace
{
    /// <summary>
    /// Drive info
    /// </summary>
    public class Drive
    {
        #region Public properties

        /// <summary>
        /// Drive name
        /// </summary>
        public string DriveName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Public constructor

        /// <summary>
        /// Drive constructor
        /// </summary>
        /// <param name="driveName"></param>
        /// <param name="description"></param>
        public Drive(string driveName, string description)
        {
            DriveName = driveName;
            Description = description;
        }

        #endregion
    }
}