#region Using statements

using System;
using System.Globalization;

#endregion Using statements

namespace WeekNumber
{
    internal class Week
    {
        #region Private variable that holds active week

        private int _week;

        #endregion Private variable that holds active week

        #region Constructor that initiates active week

        /// <summary>
        /// Initiates the week to current
        /// </summary>
        public Week() => _week = Current();

        #endregion Constructor that initiates active week

        #region Internal static week strings

        internal readonly static string CalendarWeekRuleString = nameof(CalendarWeekRule);
        internal readonly static string FirstDay = nameof(CalendarWeekRule.FirstDay);
        internal readonly static string FirstFourDayWeek = nameof(CalendarWeekRule.FirstFourDayWeek);
        internal readonly static string FirstFullWeek = nameof(CalendarWeekRule.FirstFullWeek);
        internal readonly static string DayOfWeekString = nameof(DayOfWeek);
        internal readonly static string Monday = nameof(DayOfWeek.Monday);
        internal readonly static string Tuesday = nameof(DayOfWeek.Tuesday);
        internal readonly static string Wednesday = nameof(DayOfWeek.Wednesday);
        internal readonly static string Thursday = nameof(DayOfWeek.Thursday);
        internal readonly static string Friday = nameof(DayOfWeek.Friday);
        internal readonly static string Saturday = nameof(DayOfWeek.Saturday);
        internal readonly static string Sunday = nameof(DayOfWeek.Sunday);

        #endregion Internal static week strings

        #region Public function to check if week has changed

        /// <summary>
        /// Returns if week was changed since last check
        /// </summary>
        /// <returns>true|false</returns>
        public bool WasChanged()
        {
            var changed = _week != Current();
            if (changed)
            {
                _week = Current();
            }
            return changed;
        }

        #endregion Public function to check if week has changed

        #region Public function that returns current week based on calendar rule

        /// <summary>
        /// Get current week based on calendar rules in application settings
        /// </summary>
        /// <returns>Current week as int based on calendar rules in application settings</returns>
        public static int Current()
        {
            var currentCultureInfo = CultureInfo.CurrentCulture;
            var dayOfWeek = currentCultureInfo.DateTimeFormat.FirstDayOfWeek;
            var calendarWeekRule = currentCultureInfo.DateTimeFormat.CalendarWeekRule;
            dayOfWeek = Enum.TryParse(Settings.GetSetting(DayOfWeekString), true, out dayOfWeek) ?
                dayOfWeek : DayOfWeek.Monday;
            calendarWeekRule = Enum.TryParse(Settings.GetSetting(CalendarWeekRuleString), true,
                out calendarWeekRule) ? calendarWeekRule : CalendarWeekRule.FirstFourDayWeek;
            return CultureInfo.CurrentCulture.Calendar.
                GetWeekOfYear(DateTime.Now, calendarWeekRule, dayOfWeek);
        }

        #endregion Public function that returns current week based on calendar rule
    }
}