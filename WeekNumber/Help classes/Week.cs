using System;
using System.Globalization;

namespace WeekNumber
{
    static class Week
    {
        public static string Current => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                    DateTime.Now,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday).ToString();
    }
}
