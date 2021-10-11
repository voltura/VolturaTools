#region Using statements

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion Using statements

namespace HardTop
{
    internal class TaskbarGui : IDisposable
    {
        #region Private variables

        private NotifyIcon _notifyIcon;
        private readonly HardTopContextMenu _contextMenu;

        #endregion Private variables

        #region Constructor

        internal TaskbarGui()
        {
            _contextMenu = new HardTopContextMenu();
            _notifyIcon = GetNotifyIcon(_contextMenu.ContextMenu);
            _notifyIcon.Click += NotifyIcon_Click;
        }

        #endregion Constructor

        #region Events

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            MouseEventArgs eobj = e as MouseEventArgs;
            if (eobj.Button == MouseButtons.Left)
            {
                ShowWindows();
            }
        }

        #endregion Events

        #region Private methods

        private void ShowWindows()
        {

        }

        #endregion Private methods

        #region Private helper property to create NotifyIcon

        private static NotifyIcon GetNotifyIcon(ContextMenu contextMenu)
        {
            return new NotifyIcon { Visible = true, ContextMenu = contextMenu, Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) };
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