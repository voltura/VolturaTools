#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal static class Message
    {
        #region Private header text

        private static readonly string _caption = $"{Application.ProductName} {Resources.Version} {Application.ProductVersion}";

        #endregion Private header text

        #region Show Information or Error dialog method

        internal static void Show(string text, Exception ex = null)
        {
            MessageBox.Show(ex is null ? text : $"{text}\r\n{ex}", _caption,
                MessageBoxButtons.OK, ex is null ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        #endregion Show Information or Error dialog method
    }
}