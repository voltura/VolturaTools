#region Using statements

using System;
using System.Globalization;

#endregion

namespace WeekNumber
{
    internal class Week
    {
        #region Private variable that holds active week

        private int _week;

        #endregion

        #region Constructor that initiates active week

        public Week() => _week = Current;

        #endregion

        #region Public function to check if week has changed

        public bool WasChanged()
        {
            bool changed = _week != Week.Current;
            if (changed)
            {
                _week = Week.Current;
            }
            return changed;
        }

        #endregion

        #region Public static property that returns current week based on (hardcoded) calendar rule

        public static int Current => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, 
            CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        #endregion
    }
}