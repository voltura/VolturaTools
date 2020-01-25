#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal class TaskbarGui : IDisposable, IGui
    {
        #region Private variables

        private NotifyIcon _notifyIcon;
        private readonly WeekNumberContextMenu _contextMenu;
        private readonly Speak _speak;

        #endregion Private variables

        #region Constructor

        internal TaskbarGui(int week = 1)
        {
            _speak = new Speak();
            _contextMenu = new WeekNumberContextMenu(ref _speak);
            _notifyIcon = GetNotifyIcon(_contextMenu.ContextMenu);
            _notifyIcon.Click += NotifyIcon_Click;
            UpdateIcon(week, ref _notifyIcon);
            SayWeek();
        }

        #endregion Constructor

        #region Public UpdateIcon method

        /// <summary>
        /// Updates icon on GUI with given week number
        /// </summary>
        /// <param name="weekNumber"></param>
        public void UpdateIcon(int weekNumber)
        {
            UpdateIcon(weekNumber, ref _notifyIcon);
        }

        #endregion Public UpdateIcon method

        #region Private static UpdateIcon method

        private static void UpdateIcon(int weekNumber, ref NotifyIcon notifyIcon)
        {
            notifyIcon.Text = Resources.Week + weekNumber;
            System.Drawing.Icon prevIcon = notifyIcon.Icon;
            notifyIcon.Icon = WeekIcon.GetIcon(weekNumber);
            WeekIcon.CleanupIcon(ref prevIcon);
        }

        #endregion Private static UpdateIcon method

        #region Events

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            MouseEventArgs eobj = e as MouseEventArgs;
            if (eobj.Button == MouseButtons.Left)
            {
                SayWeek();
            }
        }

        #endregion Events

        #region Private method to speak week number

        private void SayWeek()
        {
            _speak?.Sentence(Resources.ClearThroat + Resources.Week + Week.Current());
        }

        #endregion Private method to speak week number

        #region Private helper property to create NotifyIcon

        private static NotifyIcon GetNotifyIcon(ContextMenu contextMenu)
        {
            return new NotifyIcon { Visible = true, ContextMenu = contextMenu };
        }

        #endregion Private helper property to create NotifyIcon

        #region IDisposable methods

        /// <summary>
        /// Disposes the GUI resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            CleanupNotifyIcon();
            _contextMenu.Dispose();
        }

        private void CleanupNotifyIcon()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                if (_notifyIcon.Icon != null)
                {
                    NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
                    _notifyIcon.Icon?.Dispose();
                }
                _notifyIcon.ContextMenu?.MenuItems.Clear();
                _notifyIcon.ContextMenu?.Dispose();
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }

        #endregion IDisposable methods
    }
}