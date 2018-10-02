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
        private static readonly object _lockObject = new object();

        #endregion

        #region Constructor

        public TaskbarGui(int week)
        {
            CreateContextMenu();
            CreateNotifyIcon(ref _notifyIcon, ref _contextMenu);
            SetWeekIcon(week, ref _notifyIcon);
        }

        #endregion

        #region Internal method

        internal void UpdateWeek(int week) => SetWeekIcon(week, ref _notifyIcon);

        #endregion

        #region Private static methods

        private static void SetWeekIcon(int weekNumber, ref NotifyIcon notifyIcon)
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
                    using (Font font = new Font(FontFamily.GenericMonospace, 36f, FontStyle.Bold))
                    {
                        graphics.DrawString(weekNumber.ToString(), font, Brushes.White, -6f, 10f);
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

        #region Private methods

        private void CreateContextMenu()
        {
            _contextMenu = new ContextMenu(new MenuItem[4]
            {
                    new MenuItem("&About " + Application.ProductName + "\tshift+A", delegate
                    {
                        _contextMenu.MenuItems[0].Enabled = false;
                        Message.ShowMessage(Text.About);
                        if (_contextMenu.MenuItems.Count > 0)
                        {
                            _contextMenu.MenuItems[0].Enabled = true;
                        }
                    })
                    {
                        DefaultItem = true
                    },
                    new MenuItem("&Start with Windows\tshift+S", delegate
                    {
                        try
                        {
                            lock (_lockObject)
                            {
                                _contextMenu.MenuItems[1].Enabled = false;
                                _contextMenu.MenuItems[1].Checked = !_contextMenu.MenuItems[1].Checked;
                                Settings.StartWithWindows = _contextMenu.MenuItems[1].Checked;
                                _contextMenu.MenuItems[1].Enabled = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Message.ShowError(Text.FailedToUpdateRegistry, ex);
                        }
                    })
                    {
                        Index = 1
                    },
                    new MenuItem("-"),
                    new MenuItem("E&xit " + Application.ProductName + "\tshift+X", delegate
                    {
                        Dispose();
                        Application.Exit();
                    })
            });
            _contextMenu.MenuItems[1].Checked = Settings.StartWithWindows;
        }

        private void CreateNotifyIcon(ref NotifyIcon notifyIcon, ref ContextMenu contextMenu)
        {
            notifyIcon = new NotifyIcon
            {
                Visible = true
            };
            notifyIcon.ContextMenu = contextMenu;
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
            if (disposing)
            {
                _notifyIcon.Visible = false;
                NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
                _notifyIcon?.ContextMenu?.Dispose();
                _notifyIcon?.Icon?.Dispose();
                _notifyIcon?.Dispose();
                _contextMenu.Dispose();
            }
        }

        #endregion
    }
}
