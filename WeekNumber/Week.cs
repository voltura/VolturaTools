#region Using statements

using System;
using System.Globalization;

#endregion

namespace WeekNumber
{
    internal class Week
    {
        #region Private variable

        private int _week;

        #endregion

        #region Constructor

        public Week() => _week = Current;

        #endregion

        #region Public function

        public bool WasChanged()
        {
            bool changed = _week != Week.Current;
            if (changed) _week = Week.Current;
            return changed;
        }

        #endregion

        #region Public static functions

        public static int Current => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, 
            CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        #endregion
    }
}