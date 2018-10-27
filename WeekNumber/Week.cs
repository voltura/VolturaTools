#region Using statements

using System;
using System.ComponentModel;
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
        public Week() => _week = Current;

        #endregion Constructor that initiates active week

        #region Internal static week constants

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
        internal static readonly string FirstDaySeparatedString = "First Day";
        internal static readonly string FirstFourDayWeekSeparatedString = "First Four Day Week";
        internal static readonly string FirstFullWeekSeparatedString = "First Full Week";

        #endregion Internal static week constants

        #region Public function to check if week has changed

        /// <summary>
        /// Returns if week was changed since last check
        /// </summary>
        /// <returns>true|false</returns>
        public bool WasChanged()
        {
            var changed = _week != Current;
            if (changed)
            {
                _week = Current;
            }
            return changed;
        }

        #endregion Public function to check if week has changed

        #region Public property that returns current week based on calendar rule

        /// <summary>
        /// Returns current week based on calendar rules in application settings
        /// </summary>
        public int Current
        {
            get
            {
                var dayOfWeekSetting = Settings.GetSetting(DayOfWeekString);
                Enum dayOfWeekEnum = DayOfWeek.Sunday;
                var dayOfWeek = (DayOfWeek) TypeDescriptor.GetConverter(dayOfWeekEnum)
                    .ConvertFrom(dayOfWeekSetting);
                var calendarWeekRuleSetting = Settings.GetSetting(CalendarWeekRuleString);
                Enum calendarWeekRuleEnum = CalendarWeekRule.FirstFourDayWeek;
                var calendarWeekRule = (CalendarWeekRule) TypeDescriptor.GetConverter(calendarWeekRuleEnum)
                    .ConvertFrom(calendarWeekRuleSetting);
                return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, calendarWeekRule, dayOfWeek);
            }
        }

        #endregion Public property that returns current week based on calendar rule
    }
}