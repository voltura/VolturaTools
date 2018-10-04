#region Using statements

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace WeekNumber
{
    internal class TaskbarGui : IDisposable
    {
        #region Private variables

        private NotifyIcon _notifyIcon;
        private ContextMenu _contextMenu;

        #endregion

        #region Constructor

        public TaskbarGui(int week = 1)
        {
            _contextMenu = GetContextMenu;
            _notifyIcon = GetNotifyIcon(ref _contextMenu);
            UpdateIcon(week, ref _notifyIcon);
        }

        #endregion

        #region Event handling

        private void StartWithWindowsClick(object o, EventArgs e)
        {
            try
            {
                MenuItem mi = (MenuItem)o;
                mi.Enabled = false;
                mi.Checked = !mi.Checked;
                Settings.StartWithWindows = mi.Checked;
                if (mi != null)
                {
                    mi.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Message.Show(Text.FailedToUpdateRegistry, ex);
            }
        }

        private void AboutClick(object o, EventArgs e)
        {
            MenuItem mi = (MenuItem)o;
            mi.Enabled = false;
            Message.Show(Text.About);
            if (mi != null)
            {
                mi.Enabled = true;
            }
        }

        #endregion

        #region Internal method

        internal void UpdateIcon(int weekNumber) => UpdateIcon(weekNumber, ref _notifyIcon);

        #endregion

        #region Private static methods

        private static void UpdateIcon(int weekNumber, ref NotifyIcon notifyIcon)
        {
            notifyIcon.Text = Text.Week + weekNumber;
            using (Bitmap bitmap = new Bitmap(64, 64))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.Black, 2, 2, 62, 62);
                    using (Pen whitePen = new Pen(Color.White, 4f))
                    {
                        graphics.DrawRectangle(whitePen, 2, 2, 60, 60);
                    }
                    graphics.FillRectangle(Brushes.White, 10, 2, 6, 12);
                    graphics.FillRectangle(Brushes.White, 48, 2, 6, 12);
                    using (Font font = new Font(FontFamily.GenericMonospace, 12f, FontStyle.Bold))
                    {
                        graphics.DrawString(weekNumber.ToString().PadLeft(2, '0').Substring(0, 2), font, Brushes.White, -6f, 10f);
                    }
                    IntPtr hicon = bitmap.GetHicon();
                    Icon prevIcon = notifyIcon.Icon;
                    Icon newIcon = Icon.FromHandle(hicon);
                    notifyIcon.Icon = new Icon(newIcon, SystemInformation.SmallIconSize);
                    if (prevIcon != null)
                    {
                        NativeMethods.DestroyIcon(prevIcon.Handle);
                    }
                    if (newIcon != null)
                    {
                        NativeMethods.DestroyIcon(newIcon.Handle);
                    }
                    prevIcon?.Dispose();
                    newIcon?.Dispose();
                }
            }
        }

        #endregion

        #region Private properties

        private ContextMenu GetContextMenu => new ContextMenu(new[]
        {
            new MenuItem(Text.AboutMenu, new EventHandler(AboutClick)) { DefaultItem = true },
            new MenuItem(Text.StartWithWindowsMenu, new EventHandler(StartWithWindowsClick)) { Checked = Settings.StartWithWindows },
            new MenuItem(Text.SeparatorMenu),
            new MenuItem(Text.ExitMenu, delegate {  Application.Exit(); })
        });

        #endregion

        #region Private function

        private NotifyIcon GetNotifyIcon(ref ContextMenu contextMenu) => new NotifyIcon { Visible = true, ContextMenu = contextMenu };

        #endregion

        #region IDisposable methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_notifyIcon != null)
                {
                    _notifyIcon.Visible = false;
                }
                if (_notifyIcon.Icon != null)
                {
                    NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
                }
                _notifyIcon?.ContextMenu?.MenuItems?.Clear();
                _notifyIcon?.ContextMenu?.Dispose();
                _notifyIcon?.Icon?.Dispose();
                _notifyIcon?.Dispose();
                if (_contextMenu != null)
                {
                    _contextMenu.Dispose();
                }
            }
        }

        #endregion
    }
}