#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal class WeekNumberContextMenu : IDisposable
    {
        #region Internal context menu

        internal ContextMenu ContextMenu { get; private set; }

        #endregion Internal context menu

        #region Internal contructor

        internal WeekNumberContextMenu()
        {
            ContextMenu = new ContextMenu(new MenuItem[4]
            {
                new MenuItem(Resources.AboutMenu, AboutClick) { DefaultItem = true },
                new MenuItem(Resources.SettingsMenu, new MenuItem[5] {
                    new MenuItem(Resources.StartWithWindowsMenu, StartWithWindowsClick) { Checked = Settings.StartWithWindows },
                    new MenuItem(Resources.FirstDayOfWeekMenu, new MenuItem[7] {
                        new MenuItem(Week.Monday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Monday) },
                        new MenuItem(Week.Tuesday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Tuesday) },
                        new MenuItem(Week.Wednesday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Wednesday) },
                        new MenuItem(Week.Thursday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Thursday) },
                        new MenuItem(Week.Friday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Friday) },
                        new MenuItem(Week.Saturday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Saturday) },
                        new MenuItem(Week.Sunday, FirstDayOfWeekClick) { Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Sunday) }
                    }),
                    new MenuItem(Resources.CalendarRuleMenu,  new MenuItem[3] {
                        new MenuItem(Week.FirstDaySeparatedString, CalendarWeekRuleClick) { Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstDay) },
                        new MenuItem(Week.FirstFourDayWeekSeparatedString, CalendarWeekRuleClick) { Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstFourDayWeek) },
                        new MenuItem(Week.FirstFullWeekSeparatedString, CalendarWeekRuleClick) { Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstFullWeek) }
                    }),
                    new MenuItem(Resources.ColorsMenu,  ColorMenuClick),
                    new MenuItem(Resources.SaveIconMenu, SaveIconClick)
                }),
                new MenuItem(Resources.SeparatorMenu),
                new MenuItem(Resources.ExitMenu, ExitMenuClick)
            });
        }

        #endregion Internal constructor

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
                Message.Show(Resources.FailedToUpdateDayOfWeek, ex);
            }
        }

        private static void ColorMenuClick(object o, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)o;
                mi.Enabled = false;
                Message.Show(Resources.NotImplementedYet);
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Resources.FailedToSetIcon, ex);
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
                Message.Show(Resources.FailedToUpdateCalendarWeekRule, ex);
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
                Message.Show(Resources.FailedToUpdateRegistry, ex);
            }
        }

        private static void AboutClick(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            Message.Show(Resources.About);
            EnableMenuItem(mi);
        }

        private static void SaveIconClick(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            using (var saveFileDialog = new SaveFileDialog()
            {
                Title = Resources.SaveIconMenu,
                AddExtension = true,
                DefaultExt = ".ico",
                FileName = Application.ProductName + ".ico",
                SupportMultiDottedExtensions = false,
                OverwritePrompt = true,
                CheckPathExists = true
            })
            {
                if (DialogResult.OK == saveFileDialog.ShowDialog())
                {
                    var success = WeekIcon.SaveIcon(Week.Current(), saveFileDialog.FileName);
                }
            }
            EnableMenuItem(mi);
        }

        #endregion Event handling

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
            if (mi is null)
            {
                return;
            }
            foreach (MenuItem m in mi.Parent?.MenuItems)
            {
                m.Checked = false;
            }
            mi.Checked = true;
        }

        #endregion Private static helper methods for menuitem

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