#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal class WeekNumberContextMenu : IDisposable
    {
        #region Private variables

        private readonly Speak _speak;

        #endregion Private variables

        #region Internal context menu

        internal ContextMenu ContextMenu { get; private set; }

        #endregion Internal context menu

        #region Internal contructor

        internal WeekNumberContextMenu(ref Speak speak)
        {
            _speak = speak;
            CreateContextMenu();
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
                Settings.UpdateSetting(Week.DayOfWeekString, mi.Name);
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Resources.FailedToUpdateDayOfWeek, ex);
            }
        }

        private void ColorMenuClick(object o, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)o;
                SayColorSelect(mi.Name);
                mi.Enabled = false;
                if (mi.Name == Resources.ResetColors)
                {
                    Settings.UpdateSetting(Resources.Foreground, System.Drawing.Color.White.Name);
                    Settings.UpdateSetting(Resources.Background, System.Drawing.Color.Black.Name);
                }
                else
                using (ColorDialog cd = new ColorDialog
                {
                    AllowFullOpen = false,
                    FullOpen = false,
                    SolidColorOnly = true,
                    ShowHelp = false,
                    Color = System.Drawing.Color.FromName(Settings.GetSetting(mi.Name))
                })
                {
                    cd.ShowDialog();
                    Settings.UpdateSetting(mi.Name, cd.Color.Name);
                }
                Settings.UpdateSetting(Resources.ForceRedraw, true.ToString());
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Resources.FailedToUpdateColor, ex);
            }
        }

        private static void CalendarWeekRuleClick(object o, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)o;
                mi.Enabled = false;
                CheckMenuItemUncheckSiblings(mi);
                var calendarWeekRuleSetting = mi.Name;
                Settings.UpdateSetting(Week.CalendarWeekRuleString, calendarWeekRuleSetting);
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Resources.FailedToUpdateCalendarWeekRule, ex);
            }
        }

        private void StartWithWindowsClick(object o, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)o;
                mi.Enabled = false;
                mi.Checked = !mi.Checked;
                Settings.StartWithWindows = mi.Checked;
                SayStartWithWindowsSetting();
                EnableMenuItem(mi);
            }
            catch (Exception ex)
            {
                Message.Show(Resources.FailedToUpdateRegistry, ex);
            }
        }

        private void AboutClick(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            Message.Show(Resources.About, _speak);
            EnableMenuItem(mi);
        }

        private static void SaveIconClick(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            using (var saveFileDialog = new SaveFileDialog
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
                    WeekIcon.SaveIcon(Week.Current(), saveFileDialog.FileName);
                }
            }
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
                new MenuItem(Resources.SettingsMenu, new MenuItem[5]
                {
                    new MenuItem(Resources.StartWithWindowsMenu, StartWithWindowsClick)
                    {
                        Checked = Settings.StartWithWindows
                    },
                    FirstDayOfWeekMenu(),
                    CalendarRuleMenu(),
                    ColorsMenu(),
                    new MenuItem(Resources.SaveIconMenu, SaveIconClick)
                }),
                new MenuItem(Resources.SeparatorMenu),
                new MenuItem(Resources.ExitMenu, ExitMenuClick)
            });
        }

        #endregion Private method for context menu creation

        #region Private helper methods for menu items

        private MenuItem ColorsMenu()
        {
            return new MenuItem(Resources.ColorsMenu, new MenuItem[3]
            {
                new MenuItem(Resources.ForegroundMenu, ColorMenuClick)
                {
                    Name = Resources.Foreground
                },
                new MenuItem(Resources.BackgroundMenu, ColorMenuClick)
                {
                    Name = Resources.Background
                },
                new MenuItem(Resources.ResetColorsMenu, ColorMenuClick)
                {
                    Name = Resources.ResetColors
                }
            });
        }

        private static MenuItem CalendarRuleMenu()
        {
            return new MenuItem(Resources.CalendarRuleMenu, new MenuItem[3]
            {
                new MenuItem(Resources.FirstDayMenu, CalendarWeekRuleClick)
                {
                    Name = Week.FirstDay,
                    Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstDay)
                },
                new MenuItem(Resources.FirstFourDayWeekMenu, CalendarWeekRuleClick)
                {
                    Name = Week.FirstFourDayWeek,
                    Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstFourDayWeek)
                },
                new MenuItem(Resources.FirstFullWeekMenu, CalendarWeekRuleClick)
                {
                    Name = Week.FirstFullWeek,
                    Checked = Settings.SettingIsValue(Week.CalendarWeekRuleString, Week.FirstFullWeek)
                }
            });
        }

        private static MenuItem FirstDayOfWeekMenu()
        {
            return new MenuItem(Resources.FirstDayOfWeekMenu, new MenuItem[7]
            {
                new MenuItem(Resources.MondayMenu, FirstDayOfWeekClick)
                {
                    Name = Week.Monday,
                    Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Monday)
                },
                new MenuItem(Resources.TuesdayMenu, FirstDayOfWeekClick)
                {
                    Name = Week.Tuesday,
                    Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Tuesday)
                },
                new MenuItem(Resources.WednesdayMenu, FirstDayOfWeekClick)
                {
                    Name = Week.Wednesday,
                    Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Wednesday)
                },
                new MenuItem(Resources.ThursdayMenu, FirstDayOfWeekClick)
                {
                    Name = Week.Thursday,
                    Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Thursday)
                },
                new MenuItem(Resources.FridayMenu, FirstDayOfWeekClick)
                {
                    Name = Week.Friday,
                    Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Friday)
                },
                new MenuItem(Resources.SaturdayMenu, FirstDayOfWeekClick)
                {
                    Name = Week.Saturday,
                    Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Saturday)
                },
                new MenuItem(Resources.SundayMenu, FirstDayOfWeekClick)
                {
                    Name = Week.Sunday,
                    Checked = Settings.SettingIsValue(Week.DayOfWeekString, Week.Sunday)
                }
            });
        }

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

        private void SayStartWithWindowsSetting()
        {
            if (Settings.StartWithWindows)
            {
                _speak?.Sentence(Resources.ClearThroat + Resources.StartWithWindows);
            }
            else
            {
                _speak?.Sentence(Resources.ClearThroat + Resources.NotStartWithWindows);
            }
        }

        private void SayColorSelect(string input)
        {
            if (input == Resources.Background)
            {
                _speak?.Sentence(Resources.SelectBackgroundColor);
            }
            else if (input == Resources.Foreground)
            {
                _speak?.Sentence(Resources.SelectForegroundColor);
            }
            else
            {
                _speak?.Sentence(Resources.ClearThroat + input);
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