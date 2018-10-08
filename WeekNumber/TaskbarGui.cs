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

        internal TaskbarGui(int week = 1)
        {
            CreateContextMenu(ref _contextMenu);
            _notifyIcon = GetNotifyIcon(ref _contextMenu);
            UpdateIcon(week, ref _notifyIcon);
        }

        #endregion

        #region Event handling

        private static void StartWithWindowsClick(object o, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)o;
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

        private static void AboutClick(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            Message.Show(Text.About);
            if (mi != null)
            {
                mi.Enabled = true;
            }
        }

        #endregion

        #region Internal UpdateIcon method

        internal void UpdateIcon(int weekNumber) => UpdateIcon(weekNumber, ref _notifyIcon);

        #endregion

        #region Private static UpdateIcon method

        private static void UpdateIcon(int weekNumber, ref NotifyIcon notifyIcon)
        {
            notifyIcon.Text = Text.Week + weekNumber;
            using (var bitmap = new Bitmap(64, 64))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(Brushes.Black, 2, 2, 62, 62);
                using (var whitePen = new Pen(Color.White, 4f))
                {
                    graphics.DrawRectangle(whitePen, 2, 2, 60, 60);
                }
                graphics.FillRectangle(Brushes.White, 10, 2, 6, 12);
                graphics.FillRectangle(Brushes.White, 48, 2, 6, 12);
                using (var font = new Font(FontFamily.GenericMonospace, 12f, FontStyle.Bold))
                {
                    graphics.DrawString(weekNumber.ToString().PadLeft(2, '0').Substring(0, 2), font, Brushes.White, -6f, 10f);
                }
                var bHicon = bitmap.GetHicon();
                var prevIcon = notifyIcon.Icon;
                var newIcon = Icon.FromHandle(bHicon);
                notifyIcon.Icon = new Icon(newIcon, SystemInformation.SmallIconSize);
                if (prevIcon != null)
                {
                    NativeMethods.DestroyIcon(prevIcon.Handle);
                    prevIcon.Dispose();
                }
                if (newIcon != null)
                {
                    NativeMethods.DestroyIcon(newIcon.Handle);
                    newIcon.Dispose();
                }
            }
        }

        #endregion

        #region Private helper property to create NotifyIcon

        private static NotifyIcon GetNotifyIcon(ref ContextMenu contextMenu) => new NotifyIcon { Visible = true, ContextMenu = contextMenu };

        #endregion

        #region Private method to create ContextMenu

        private static void CreateContextMenu(ref ContextMenu c)
        {
            c = new ContextMenu(new[]
            {
                new MenuItem(Text.AboutMenu, AboutClick) { DefaultItem = true },
                new MenuItem(Text.StartWithWindowsMenu, StartWithWindowsClick) { Checked = Settings.StartWithWindows },
                new MenuItem(Text.SeparatorMenu),
                new MenuItem(Text.ExitMenu, delegate { Application.Exit(); })
            });
        }

        #endregion

        #region IDisposable methods

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
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                if (_notifyIcon?.Icon != null)
                {
                    NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
                    _notifyIcon?.Icon?.Dispose();
                }
                _notifyIcon.ContextMenu?.MenuItems.Clear();
                _notifyIcon.ContextMenu?.Dispose();
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
            _contextMenu?.Dispose();
            _contextMenu = null;
        }

        #endregion
    }
}