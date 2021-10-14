#region Using statements

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion Using statements

namespace HardTop
{
    internal class HardTopContextMenu : IDisposable
    {

        #region Internal context menu

        internal ContextMenu ContextMenu { get; private set; }
        internal const int NUMBER_OF_FIXED_ITEMS = 7;

        #endregion Internal context menu

        #region Internal contructor

        internal HardTopContextMenu()
        {
            CreateContextMenu();
            AddUpdatedWindowsInfoToContextMenu();
            ContextMenu.Collapse += ContextMenu_Collapse;
            ContextMenu.Popup += ContextMenu_Popup;
        }

        #endregion Internal constructor

        #region Event handling

        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            AddUpdatedWindowsInfoToContextMenu();
        }

        private void ContextMenu_Collapse(object sender, EventArgs e)
        {
            AddUpdatedWindowsInfoToContextMenu();
        }

        private void WindowItem_Click(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            mi.Checked = !mi.Checked;
            NativeMethods.ToogleWindowAlwaysOnTop((IntPtr)mi.Tag, mi.Checked);
            EnableMenuItem(mi);
        }

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
            ContextMenu = new ContextMenu(new MenuItem[NUMBER_OF_FIXED_ITEMS]
            {
                new MenuItem(Resources.AboutMenu, AboutClick),
                new MenuItem(Resources.SeparatorMenu),
                new MenuItem(Resources.StartWithWindowsMenu, StartWithWindowsClick) { Checked = Settings.StartWithWindows },
                new MenuItem(Resources.SeparatorMenu),
                new MenuItem(Resources.ExitMenu, ExitMenuClick),
                new MenuItem("Windows; checked equals \"always on top\" - click to toogle:") { DefaultItem = true, BarBreak = true },
                new MenuItem(Resources.SeparatorMenu)
            });
        }

        #endregion Private method for context menu creation

        #region Private helper methods for menu items

        private void AddUpdatedWindowsInfoToContextMenu()
        {
            while (ContextMenu.MenuItems.Count > NUMBER_OF_FIXED_ITEMS) ContextMenu.MenuItems.RemoveAt(NUMBER_OF_FIXED_ITEMS);
            NativeMethods.GetDesktopWindowHandlesAndTitles(out List<IntPtr> handles, out List<string> titles);
            List<string> ignoreTheseWindows = new List<string>() { "Program Manager", "MainWindow" };
            for (int i = 0; i < titles.Count; i++)
                if (!ignoreTheseWindows.Contains(titles[i]))
                    ContextMenu.MenuItems.Add(new MenuItem(titles[i], WindowItem_Click) { Name = titles[i], Tag = handles?[i], Checked = NativeMethods.AlwaysOnTopWindows().Contains((IntPtr)handles?[i]) });
        }

        private static void EnableMenuItem(MenuItem mi)
        {
            if (mi != null) mi.Enabled = true;
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