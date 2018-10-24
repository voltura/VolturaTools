#region Using statements

using System;
using System.Configuration;
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
                var dayOfWeekSetting = ConfigurationManager.AppSettings.Get(nameof(DayOfWeek));
                Enum dayOfWeekEnum = DayOfWeek.Sunday;
                var dayOfWeek = (DayOfWeek)
                    TypeDescriptor.GetConverter(dayOfWeekEnum)
                    .ConvertFrom(dayOfWeekSetting);
                var calendarWeekRuleSetting = ConfigurationManager.AppSettings
                    .Get(nameof(CalendarWeekRule));
                Enum calendarWeekRuleEnum = CalendarWeekRule.FirstFourDayWeek;
                var calendarWeekRule = (CalendarWeekRule)
                    TypeDescriptor.GetConverter(calendarWeekRuleEnum)
                    .ConvertFrom(calendarWeekRuleSetting);
                return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now,
                    calendarWeekRule, dayOfWeek);
            }
        }

        #endregion Public property that returns current week based on calendar rule
    }
}