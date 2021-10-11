#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace HardTop
{
    internal class HardTopContextMenu : IDisposable
    {

        #region Internal context menu

        internal ContextMenu ContextMenu { get; private set; }

        #endregion Internal context menu

        #region Internal contructor

        internal HardTopContextMenu()
        {
            CreateContextMenu();
        }

        #endregion Internal constructor

        #region Event handling

        private static void ExitMenuClick(object o, EventArgs e)
        {
            Application.Exit();
        }

        private void StartWithWindowsClick(object o, EventArgs e)
        {
            try
            {
                MenuItem mi = (MenuItem)o;
                mi.Enabled = false;
                mi.Checked = !mi.Checked;
                Settings.StartWithWindows = mi.Checked;
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Resources.FailedToUpdateRegistry, ex);
            }
        }

        private void AboutClick(object o, EventArgs e)
        {
            MenuItem mi = (MenuItem)o;
            mi.Enabled = false;
            Message.Show(Resources.About);
            EnableMenuItem(mi);
        }

        #endregion Event handling

        #region Private method for context menu creation

        private void CreateContextMenu()
        {
            ContextMenu = new ContextMenu(new MenuItem[4]
            {
                new MenuItem(Resources.AboutMenu, AboutClick)
                {
                    DefaultItem = true
                },
                new MenuItem(Resources.SettingsMenu, new MenuItem[1]
                {
                    new MenuItem(Resources.StartWithWindowsMenu, StartWithWindowsClick)
                    {
                        Checked = Settings.StartWithWindows
                    }
                }),
                new MenuItem(Resources.SeparatorMenu),
                new MenuItem(Resources.ExitMenu, ExitMenuClick)
            });
        }

        #endregion Private method for context menu creation

        #region Private helper methods for menu items

        private static void EnableMenuItem(MenuItem mi)
        {
            if (mi != null)
            {
                mi.Enabled = true;
            }
        }

        #endregion Private helper methods for menu items

        #region IDisposable methods

        /// <summary>
        /// Disposes the context menu
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
            CleanupContextMenu();
        }

        private void CleanupContextMenu()
        {
            ContextMenu?.Dispose();
            ContextMenu = null;
        }

        #endregion IDisposable methods
    }
}