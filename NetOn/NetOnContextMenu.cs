#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace NetOn
{
    internal class NetOnContextMenu : IDisposable
    {
        #region Private variables

        private readonly Speak _speak;

        #endregion Private variables

        #region Internal context menu

        internal ContextMenu ContextMenu { get; private set; }

        #endregion Internal context menu

        #region Internal contructor

        internal NetOnContextMenu(ref Speak speak)
        {
            _speak = speak;
            CreateContextMenu();
        }

        #endregion Internal constructor

        #region Event handling

        private static void ExitMenuClick(object o, EventArgs e) => Application.Exit();

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
                else using (ColorDialog cd = new ColorDialog
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
                new MenuItem(Resources.SettingsMenu, new MenuItem[3]
                {
                    new MenuItem(Resources.StartWithWindowsMenu, StartWithWindowsClick)
                    {
                        Checked = Settings.StartWithWindows
                    },
                    ColorsMenu(),
                    new MenuItem(Resources.MuteAllSoundsMenu, MuteAllSoundsClick)
                    {
                        Checked = Settings.MuteAllSounds
                    }
                }),
                new MenuItem(Resources.SeparatorMenu),
                new MenuItem(Resources.ExitMenu, ExitMenuClick)
            });
        }

        private static void MuteAllSoundsClick(object o, EventArgs e)
        {
            var mi = (MenuItem)o;
            mi.Enabled = false;
            mi.Checked = !mi.Checked;
            Settings.MuteAllSounds = mi.Checked;
            EnableMenuItem(mi);
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