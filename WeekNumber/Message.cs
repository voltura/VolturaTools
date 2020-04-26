﻿#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal static class Message
    {
        #region Private caption

        private static readonly string _caption = $"{Application.ProductName} {Resources.Version} {Application.ProductVersion}";

        #endregion Private caption

        #region Show Information or Error dialog methods

        internal static void Show(string text, Exception ex = null, Speak speak = null)
        {
            speak?.Sentence(Resources.ClearThroat + text);
            var message = ex is null ? text : $"{text}\r\n{ex}";
            MessageBoxIcon icon = ex is null ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            
            MessageBox.Show(message, _caption, MessageBoxButtons.OK, icon, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        internal static void Show(string text, Speak speak)
        {
            speak?.Sentence(Resources.ClearThroat + _caption);
            MessageBox.Show(text, _caption,
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        #endregion Show Information or Error dialog methods
    }
}