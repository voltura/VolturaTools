namespace DiskSpace
{
    /// <summary>
    /// Drive info
    /// </summary>
    public class Drive
    {
        /// <summary>
        /// Drive name
        /// </summary>
        public string DriveName { get; set; }
        
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

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
    }
}
