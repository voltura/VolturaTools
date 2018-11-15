#region Using statements

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal class TaskbarGui : IDisposable, IGui
    {
        #region Private variables

        private NotifyIcon _notifyIcon;
        private readonly WeekNumberContextMenu _contextMenu;

        #endregion Private variables

        #region Constructor

        internal TaskbarGui(int week = 1)
        {
            _contextMenu = new WeekNumberContextMenu();
            _notifyIcon = GetNotifyIcon(_contextMenu.ContextMenu);
            UpdateIcon(week, ref _notifyIcon);
        }

        #endregion Constructor

        #region Public UpdateIcon method

        /// <summary>
        /// Updates icon on GUI with given week number
        /// </summary>
        /// <param name="weekNumber"></param>
        public void UpdateIcon(int weekNumber) => UpdateIcon(weekNumber, ref _notifyIcon);

        #endregion Public UpdateIcon method

        #region Private static UpdateIcon method

        private static void UpdateIcon(int weekNumber, ref NotifyIcon notifyIcon)
        {
            notifyIcon.Text = Resources.Week + weekNumber;
            using (var bitmap = new Bitmap(128, 128))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                DrawBackgroundOnGraphics(graphics);
                DrawWeekNumberOnGraphics(weekNumber, graphics);
                SetIconFromBitmapToNotifyIcon(notifyIcon, bitmap);
            }
        }

        private static void SetIconFromBitmapToNotifyIcon(NotifyIcon notifyIcon, Bitmap bitmap)
        {
            var bHicon = bitmap.GetHicon();
            var prevIcon = notifyIcon.Icon;
            var newIcon = Icon.FromHandle(bHicon);
            notifyIcon.Icon = new Icon(newIcon, SystemInformation.SmallIconSize);
            CleanupIcon(ref prevIcon);
            CleanupIcon(ref newIcon);
        }

        private static void DrawBackgroundOnGraphics(Graphics graphics)
        {
            graphics?.FillRectangle(Brushes.Black, 4, 4, 124, 124);
            using (var whitePen = new Pen(Color.White, 8f))
            {
                graphics?.DrawRectangle(whitePen, 4, 4, 120, 120);
            }
            graphics?.FillRectangle(Brushes.White, 20, 2, 12, 24);
            graphics?.FillRectangle(Brushes.White, 96, 2, 12, 24);
        }

        private static void DrawWeekNumberOnGraphics(int weekNumber, Graphics graphics)
        {
            using (var font = new Font(FontFamily.GenericMonospace, 100f, FontStyle.Bold, GraphicsUnit.Pixel, 0, false))
            {
                graphics?.DrawString(weekNumber.ToString().PadLeft(2, '0').Substring(0, 2), font, Brushes.White, -14f, 20f);
            }
        }

        private static void CleanupIcon(ref Icon icon)
        {
            if (icon is null)
            {
                return;
            }
            NativeMethods.DestroyIcon(icon.Handle);
            icon.Dispose();
        }

        #endregion Private static UpdateIcon method

        #region Private helper property to create NotifyIcon

        private static NotifyIcon GetNotifyIcon(ContextMenu contextMenu) =>
            new NotifyIcon { Visible = true, ContextMenu = contextMenu };

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