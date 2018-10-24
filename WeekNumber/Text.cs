#region Using statement

using System;
using System.Windows.Forms;

#endregion Using statement

namespace WeekNumber
{
    internal static class Text
    {
        #region Static string variables

        internal static readonly string Monday = nameof(DayOfWeek.Monday);
        internal static readonly string Tuesday = nameof(DayOfWeek.Tuesday);
        internal static readonly string Wednesday = nameof(DayOfWeek.Wednesday);
        internal static readonly string Thursday = nameof(DayOfWeek.Thursday);
        internal static readonly string Friday = nameof(DayOfWeek.Friday);
        internal static readonly string Saturday = nameof(DayOfWeek.Saturday);
        internal static readonly string Sunday = nameof(DayOfWeek.Sunday);
        internal static readonly string About = Application.ProductName + " by Voltura AB\r\rhttps://github.com/voltura/VolturaTools\r\rFree for all.";
        internal static readonly string ApplicationNameAndVersion = Application.ProductName + " version " + Application.ProductVersion;
        internal static readonly string AboutMenu = "About " + Application.ProductName;
        internal static readonly string ExitMenu = "Exit " + Application.ProductName;
        internal static readonly string FailedToSetIcon = "Could not set Icon. Please report to feedback@voltura.se!\r\r";
        internal static readonly string FailedToUpdateCalendarWeekRule = "Could not calendar week rule setting. Please report to feedback@voltura.se!\r\r";
        internal static readonly string FailedToUpdateDayOfWeek = "Could not update first day of week setting. Please report to feedback@voltura.se!\r\r";
        internal static readonly string FailedToUpdateRegistry = "Could not update registry. Please report to feedback@voltura.se!\r\r";
        internal static readonly string FirstDayOfWeekMenu = "First day of week";
        internal static readonly string SettingsMenu = nameof(Settings);
        internal static readonly string SeparatorMenu = "-";
        internal static readonly string StartWithWindowsMenu = "Start with Windows";
        internal static readonly string UnhandledException = "Something went wrong. Please report to feedback@voltura.se!\r\r";
        internal static readonly string Week = "Week ";
        internal static readonly string CalendarRuleMenu = "Calendar week rule";
        internal static readonly string FirstDay = "First Day";
        internal static readonly string FirstFourDayWeek = "First Four Day Week";
        internal static readonly string FirstFullWeek = "First Full Week";

        #endregion Static string variables
    }
}