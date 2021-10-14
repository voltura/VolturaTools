#region Using statements

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            AddWindowsToContextMenu();
        }

        #endregion Constructor

        #region Events

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            AddWindowsToContextMenu();
        }

        private void WindowItem_Click(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            mi.Checked = !mi.Checked;
            MessageBox.Show(mi.Name + (mi.Checked ? "" : " not") + " topmost");
            mi.Enabled = true;
        }


        #endregion Events

        #region Private methods

        private void AddWindowsToContextMenu()
        {
            NativeMethods.GetDesktopWindowHandlesAndTitles(out List<IntPtr> handles, out List<string> titles);
            for (int i = 0; i < titles.Count; i++)
            {
                _contextMenu.ContextMenu.MenuItems.RemoveByKey(titles[i]);
                _contextMenu.ContextMenu.MenuItems.Add(new MenuItem(titles[i], WindowItem_Click) { Name = titles[i], Tag = handles?[i] });
            }
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