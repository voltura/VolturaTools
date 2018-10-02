#region Using statements

using System;
using System.Windows.Forms;

#endregion

namespace WeekNumber
{
    internal static class Message
    {
        #region Internal static methods

        internal static void ShowError(string text, Exception ex)
        {
            MessageBox.Show(text + ex.ToString(),
                Text.ApplicationNameAndVersion,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static void ShowMessage(string text)
        {
            MessageBox.Show(text, Text.ApplicationNameAndVersion,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
