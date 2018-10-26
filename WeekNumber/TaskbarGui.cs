#region Using statements

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal class TaskbarGui : IDisposable, IGui
    {
        #region Private variables

        private NotifyIcon _notifyIcon;
        private ContextMenu _contextMenu;

        #endregion Private variables

        #region Constructor

        internal TaskbarGui(int week = 1)
        {
            CreateContextMenu(ref _contextMenu);
            _notifyIcon = GetNotifyIcon(ref _contextMenu);
            UpdateIcon(week, ref _notifyIcon);
        }

        #endregion Constructor

        #region Event handling

        private static void ExitMenuClick(object o, EventArgs e) => Application.Exit();

        private static void FirstDayOfWeekClick(object o, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)o;
                mi.Enabled = false;
                var newCheckState = !mi.Checked;
                foreach (MenuItem m in mi.Parent.MenuItems)
                {
                    m.Checked = false;
                }
                mi.Checked = newCheckState;
                Settings.UpdateSetting(nameof(DayOfWeek), mi.Text);
                if (mi != null)
                {
                    mi.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Message.Show(Text.FailedToUpdateDayOfWeek, ex);
            }
        }

        private static void CalendarWeekRuleClick(object o, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)o;
                mi.Enabled = false;
                var newCheckState = !mi.Checked;
                foreach (MenuItem m in mi.Parent.MenuItems)
                {
                    m.Checked = false;
                }
                mi.Checked = newCheckState;
                Settings.UpdateSetting(nameof(CalendarWeekRule), mi.Text.Replace(" ", string.Empty));
                if (mi != null)
                {
                    mi.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Message.Show(Text.FailedToUpdateCalendarWeekRule, ex);
            }
        }

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

        #endregion Event handling

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
            notifyIcon.Text = Text.Week + weekNumber;
            using (var bitmap = new Bitmap(64, 64))
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
            graphics?.FillRectangle(Brushes.Black, 2, 2, 62, 62);
            using (var whitePen = new Pen(Color.White, 4f))
            {
                graphics?.DrawRectangle(whitePen, 2, 2, 60, 60);
            }
            graphics?.FillRectangle(Brushes.White, 10, 2, 6, 12);
            graphics?.FillRectangle(Brushes.White, 48, 2, 6, 12);
        }

        private static void DrawWeekNumberOnGraphics(int weekNumber, Graphics graphics)
        {
            using (var font = new Font(FontFamily.GenericMonospace, 12f, FontStyle.Bold))
            {
                graphics?.DrawString(weekNumber.ToString().PadLeft(2, '0').Substring(0, 2), font, Brushes.White, -6f, 10f);
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

        private static NotifyIcon GetNotifyIcon(ref ContextMenu contextMenu) => new NotifyIcon { Visible = true, ContextMenu = contextMenu };

        #endregion Private helper property to create NotifyIcon

        #region Private method to create ContextMenu

        private static void CreateContextMenu(ref ContextMenu c)
        {
            c = new ContextMenu(new[]
            {
                new MenuItem(Text.AboutMenu, AboutClick) { DefaultItem = true },
                new MenuItem(Text.SettingsMenu, new MenuItem[3] {
                    new MenuItem(Text.StartWithWindowsMenu, StartWithWindowsClick) { Checked = Settings.StartWithWindows },
                    new MenuItem(Text.FirstDayOfWeekMenu, new MenuItem[7] {
                        new MenuItem(Text.Monday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(nameof(DayOfWeek), nameof(DayOfWeek.Monday)) },
                        new MenuItem(Text.Tuesday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(nameof(DayOfWeek), nameof(DayOfWeek.Tuesday)) },
                        new MenuItem(Text.Wednesday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(nameof(DayOfWeek), nameof(DayOfWeek.Wednesday)) },
                        new MenuItem(Text.Thursday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(nameof(DayOfWeek), nameof(DayOfWeek.Thursday)) },
                        new MenuItem(Text.Friday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(nameof(DayOfWeek), nameof(DayOfWeek.Friday)) },
                        new MenuItem(Text.Saturday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(nameof(DayOfWeek), nameof(DayOfWeek.Saturday)) },
                        new MenuItem(Text.Sunday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(nameof(DayOfWeek), nameof(DayOfWeek.Sunday)) }
                    }),
                    new MenuItem(Text.CalendarRuleMenu,  new MenuItem[3] {
                        new MenuItem(Text.FirstDay, CalendarWeekRuleClick) { Checked = Settings.SettingIsValue(nameof(CalendarWeekRule), nameof(CalendarWeekRule.FirstDay)) },
                        new MenuItem(Text.FirstFourDayWeek, CalendarWeekRuleClick){ Checked = Settings.SettingIsValue(nameof(CalendarWeekRule), nameof(CalendarWeekRule.FirstFourDayWeek)) },
                        new MenuItem(Text.FirstFullWeek, CalendarWeekRuleClick){ Checked = Settings.SettingIsValue(nameof(CalendarWeekRule), nameof(CalendarWeekRule.FirstFullWeek)) }
                    })
                }),
                new MenuItem(Text.SeparatorMenu),
                new MenuItem(Text.ExitMenu, ExitMenuClick)
            });
        }

        #endregion Private method to create ContextMenu

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
            CleanupContextMenu();
        }

        private void CleanupContextMenu()
        {
            _contextMenu?.Dispose();
            _contextMenu = null;
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