#region Using statements

using System;
using System.Configuration;
using System.Globalization;
using System.ComponentModel;

#endregion Using statements

namespace WeekNumber
{
    internal class Week
    {
        #region Private variable that holds active week

        private int _week;

        #endregion Private variable that holds active week

        #region Constructor that initiates active week

        public Week() => _week = Current;

        #endregion Constructor that initiates active week

        #region Public function to check if week has changed

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

        public int Current
        {
            get
            {
                var dayOfWeekSetting = ConfigurationManager.AppSettings.Get(@"DayOfWeek");
                Enum dayOfWeekEnum = DayOfWeek.Sunday;
                var dayOfWeek = (DayOfWeek)
                    TypeDescriptor.GetConverter(dayOfWeekEnum)
                    .ConvertFrom(dayOfWeekSetting);
                var calendarWeekRuleSetting = ConfigurationManager.AppSettings
                    .Get(@"CalendarWeekRule");
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