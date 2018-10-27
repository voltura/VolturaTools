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
                CheckMenuItemUncheckSiblings(mi);
                Settings.UpdateSetting(Week.DayOfWeekString, mi.Text);
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Properties.Resources.FailedToUpdateDayOfWeek, ex);
            }
        }

        private static void CalendarWeekRuleClick(object o, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)o;
                mi.Enabled = false;
                CheckMenuItemUncheckSiblings(mi);
                var calendarWeekRuleSetting = mi.Text.Replace(" ", string.Empty);
                Settings.UpdateSetting(Week.CalendarWeekRuleString, calendarWeekRuleSetting);
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Properties.Resources.FailedToUpdateCalendarWeekRule, ex);
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
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Properties.Resources.FailedToUpdateRegistry, ex);
            }
        }

        private static void AboutClick(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            Message.Show(Properties.Resources.About);
            EnableMenuItem(mi);
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
            notifyIcon.Text = Properties.Resources.Week + weekNumber;
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

        #region Private static helper methods for menuitem

        private static void EnableMenuItem(MenuItem mi)
        {
            if (mi != null)
            {
                mi.Enabled = true;
            }
        }

        private static void CheckMenuItemUncheckSiblings(MenuItem mi)
        {
            foreach (MenuItem m in mi.Parent?.MenuItems)
            {
                m.Checked = false;
            }
            mi.Checked = true;
        }

        #endregion Private static helper methods for menuitem

        #region Private helper property to create NotifyIcon

        private static NotifyIcon GetNotifyIcon(ref ContextMenu contextMenu) =>
            new NotifyIcon { Visible = true, ContextMenu = contextMenu };

        #endregion Private helper property to create NotifyIcon

        #region Private method to create ContextMenu

        private static void CreateContextMenu(ref ContextMenu c)
        {
            c = new ContextMenu(new MenuItem[4]
            {
                new MenuItem(Properties.Resources.AboutMenu, AboutClick) { DefaultItem = true },
                new MenuItem(Properties.Resources.SettingsMenu, new MenuItem[3] {
                    new MenuItem(Properties.Resources.StartWithWindowsMenu, StartWithWindowsClick) { Checked = Settings.StartWithWindows },
                    new MenuItem(Properties.Resources.FirstDayOfWeekMenu, new MenuItem[7] {
                        new MenuItem(Week.Monday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Monday) },
                        new MenuItem(Week.Tuesday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Tuesday) },
                        new MenuItem(Week.Wednesday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Wednesday) },
                        new MenuItem(Week.Thursday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Thursday) },
                        new MenuItem(Week.Friday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Friday) },
                        new MenuItem(Week.Saturday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Saturday) },
                        new MenuItem(Week.Sunday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Sunday) }
                    }),
                    new MenuItem(Properties.Resources.CalendarRuleMenu,  new MenuItem[3] {
                        new MenuItem(Week.FirstDaySeparatedString, CalendarWeekRuleClick) { Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstDay) },
                        new MenuItem(Week.FirstFourDayWeekSeparatedString, CalendarWeekRuleClick){ Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstFourDayWeek) },
                        new MenuItem(Week.FirstFullWeekSeparatedString, CalendarWeekRuleClick){ Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstFullWeek) }
                    })
                }),
                new MenuItem(Properties.Resources.SeparatorMenu),
                new MenuItem(Properties.Resources.ExitMenu, ExitMenuClick)
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