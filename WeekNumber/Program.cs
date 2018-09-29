#region Using statements

using Microsoft.Win32;
using System;
using System.Windows.Forms;

#endregion

namespace WeekNumber
{
    internal class Program : IDisposable
    {
        #region Private variables

        private NotifyIcon _notifyIcon;
        private ContextMenu _contextMenu;
        private readonly Timer _timer;
        private int _week, _oldWeek;
        private static readonly string _about = Application.ProductName + " by Voltura AB\r\rhttps://github.com/voltura/VolturaTools\r\rFree for all.";
        private static readonly string _aboutTitle = Application.ProductName + " version " + Application.ProductVersion;

        #endregion

        #region Application starting point

        [STAThread]
        private static void Main()
        {
            new Program(); Application.Run();
        }

        #endregion

        #region Constructor

        public Program()
        {
            try
            {
                CreateContextMenu();
                Week.InitiateWeek(ref _oldWeek, ref _week);
                CreateNotifyIcon(ref _notifyIcon, ref _contextMenu);
                Week.SetWeekIcon(ref _week, ref _notifyIcon);
                InitiateTimer(ref _timer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong. Please report to feedback@voltura.se!\r\r" + ex.ToString(), 
                    Application.ProductName + " " + Application.ProductVersion, 
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
            }
        }

        #endregion

        #region Private methods

        private void InitiateTimer(ref Timer timer)
        {
            timer = new Timer
            {
                Interval = 60000,
                Enabled = true
            };
            timer.Tick += delegate
            {
                Application.DoEvents();
                lock (this)
                {
                    _week = Week.ThisWeek;
                    if (_oldWeek != _week)
                    {
                        _oldWeek = _week;
                        try
                        {
                            Week.SetWeekIcon(ref _week, ref _notifyIcon);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not set Icon. Please report to feedback@voltura.se!\r\r" + 
                                ex.ToString(), Application.ProductName + " " + Application.ProductVersion, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Dispose();
                            Application.Exit();
                        }
                    }
                }
            };
        }

        private void CreateContextMenu()
        {
            _contextMenu = new ContextMenu(new MenuItem[4]
            {
                    new MenuItem("&About " + Application.ProductName + "\tshift+A", delegate
                    {
                        if (_contextMenu.MenuItems.Count > 0)
                        {
                            _contextMenu.MenuItems[0].Enabled = false;
                            MessageBox.Show(_about, _aboutTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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
                            if (_contextMenu.MenuItems.Count > 0)
                            {
                                _contextMenu.MenuItems[1].Enabled = false;
                                using (RegistryKey registryKey = Registry.CurrentUser)
                                {
                                    if (Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", Application.ProductName, null) == null)
                                    {
                                        registryKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", writable: true).SetValue(Application.ProductName, Application.ExecutablePath);
                                        _contextMenu.MenuItems[1].Checked = true;
                                    }
                                    else
                                    {
                                        registryKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", writable: true).DeleteValue(Application.ProductName);
                                        _contextMenu.MenuItems[1].Checked = false;
                                    }
                                    registryKey.Flush();
                                }
                            }
                            if (_contextMenu.MenuItems.Count > 0)
                            {
                                _contextMenu.MenuItems[1].Enabled = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not Update registry. Please report to feedback@voltura.se!\r\r" +
                                ex.ToString(), Application.ProductName + " " + Application.ProductVersion,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _contextMenu.MenuItems[1].Checked = (Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", Application.ProductName, null) != null);
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
                _timer?.Stop();
                _timer?.Dispose();
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