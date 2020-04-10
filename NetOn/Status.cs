#region Using statements


#endregion Using statements

namespace NetOn
{
    internal static class Status
    {
        #region Public function that returns current status based network adapter

        /// <summary>
        /// Get current status
        /// </summary>
        /// <returns>Current status as int</returns>
        public static int Current()
        {
            int status = Network.Status();
            return status;
        }

        #endregion Public function that returns current status based on network adapter
    }
}