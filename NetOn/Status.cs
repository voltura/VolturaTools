#region Using statements

using System;
using System.Globalization;

#endregion Using statements

namespace NetOn
{
    internal class Status
    {
        #region Private variable that holds active status

        private int _status;

        #endregion Private variable that holds active status

        #region Constructor that initiates active status

        /// <summary>
        /// Initiates the status to current
        /// </summary>
        public Status() => _status = Current();

        #endregion Constructor that initiates active status

        #region Public function to check if status has changed

        /// <summary>
        /// Returns if status was changed since last check
        /// </summary>
        /// <returns>true|false</returns>
        public bool WasChanged()
        {
            var changed = _status != Current();
            if (changed)
            {
                _status = Current();
            }
            return changed;
        }

        #endregion Public function to check if status has changed

        #region Public function that returns current status based network adapter

        /// <summary>
        /// Get current status
        /// </summary>
        /// <returns>Current status as int</returns>
        public static int Current()
        {
            var status = Network.Status();
            return status;
        }

        #endregion Public function that returns current status based on network adapter

    }
}