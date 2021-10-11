﻿#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace HardTop
{
    internal static class Message
    {
        #region Private caption

        private static readonly string _caption = $"{Application.ProductName} {Resources.Version} {Application.ProductVersion}";

        #endregion Private caption

        #region Show Information or Error dialog methods

        internal static void Show(string text, Exception ex = null)
        {
            MessageBox.Show(ex is null ? text : $"{text}\r\n{ex}", _caption,
                MessageBoxButtons.OK, ex is null ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        internal static void Show(string text)
        {
            MessageBox.Show(text, _caption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Show Information or Error dialog methods
    }
}