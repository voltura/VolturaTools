#region Using statement

using System.Windows.Forms;

#endregion

namespace WeekNumber
{
    internal static class Text
    {
        #region Static texts variables

        internal static readonly string About = Application.ProductName + " by Voltura AB\r\rhttps://github.com/voltura/VolturaTools\r\rFree for all.";
        internal static readonly string ApplicationNameAndVersion = Application.ProductName + " version " + Application.ProductVersion;
        internal static readonly string FailedToSetIcon = "Could not set Icon. Please report to feedback@voltura.se!\r\r";
        internal static readonly string Week = "Week ";
        internal static readonly string FailedToUpdateRegistry = "Could not update registry. Please report to feedback@voltura.se!\r\r";
        internal static readonly string UnhandledException = "Something went wrong. Please report to feedback@voltura.se!\r\r";

        #endregion
    }
}