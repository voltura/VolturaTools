using Microsoft.Win32;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ProxyToogle
{
    internal class ProxyToogle
    {
        private static readonly Mutex Mutex = new Mutex(true, "88F5D3E0-16DC-40BC-AD02-C399DC744E14");

        private static void Main()
        {
            if (!Mutex.WaitOne(TimeSpan.Zero, true))
            {
                return;
            }
            using (AppContext context = new AppContext())
            {
                Application.Run(context);
            }
            Mutex.ReleaseMutex();
        }
    }

    internal class AppContext : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly Icon _onIcon, _offIcon;
        private readonly Timer _timer;
        private bool _proxyEnabled;
        //private string _proxyServer;
        private const string REGKEY = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
        //private readonly string PROXY_ON = "Proxy is ON";
        //private readonly string PROXY_OFF = "Proxy is OFF";

        internal AppContext()
        {
            _onIcon = CreateIcon(Color.MediumSpringGreen, Color.Black);
            _offIcon = CreateIcon(Color.Crimson, Color.White);
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGKEY, true))
            {
                _proxyEnabled = (int)key.GetValue("ProxyEnable", 0) == 1;
          //      _proxyServer = (string)key.GetValue("ProxyServer", "No proxy server defined");
            }
            //PROXY_ON += "\r\nProxy: " + _proxyServer;
            //PROXY_OFF += "\r\nProxy: " + _proxyServer;

            Size SIZE = new Size(256, 256);
            Icon iconCopy = new Icon(_proxyEnabled ? _onIcon : _offIcon, SIZE);
            _notifyIcon = new NotifyIcon() { Visible = true, Icon = iconCopy };
            _notifyIcon.Click += (s, e) => { if (((MouseEventArgs)e).Button == MouseButtons.Left) {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGKEY, true))
                    {
                        _proxyEnabled = (int)key.GetValue("ProxyEnable", 0) == 1;
                        key.SetValue("ProxyEnable", _proxyEnabled ? 0 : 1);
                    }
                    NativeMethods.InternetSetOption(IntPtr.Zero, NativeMethods.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                    NativeMethods.InternetSetOption(IntPtr.Zero, NativeMethods.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
                }
            };
            _notifyIcon.ContextMenu = new ContextMenu(new MenuItem[1] { new MenuItem(Properties.Resources.ExitProxyToogle, (o, e) => {
                _notifyIcon.Visible = false;
                if (_notifyIcon.Icon != null)
                {
                    NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
                    if (iconCopy != null)
                    {
                        NativeMethods.DestroyIcon(iconCopy.Handle);
                        iconCopy?.Dispose();
                    }
                    _notifyIcon.Icon?.Dispose();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                Application.Exit(); 
            } ) });
            _timer = new Timer { Interval = 1000, Enabled = true };
            _timer.Tick += (s, e) => {
                _timer?.Stop();
                Application.DoEvents();
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGKEY, true))
                {
                    _proxyEnabled = (int)key.GetValue("ProxyEnable", 0) == 1;
            //        _proxyServer = (string)key.GetValue("ProxyServer", "No proxy server defined");
                }
                if (_notifyIcon.Icon != null)
                {
                    NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
                    if (iconCopy != null)
                    {
                        NativeMethods.DestroyIcon(iconCopy.Handle);
                        iconCopy?.Dispose();
                    }
                    _notifyIcon.Icon?.Dispose();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                iconCopy = new Icon(_proxyEnabled ? _onIcon : _offIcon, SIZE);
                _notifyIcon.Icon = iconCopy;
                // MEMORY LEAK ON ROW BELOW! How to handle?
                //_notifyIcon.Text = _proxyEnabled ? PROXY_ON : PROXY_OFF;
                _timer?.Start();
            };
        }

        private static Icon CreateIcon(Color backColor, Color textColor)
        {
            using (Bitmap bitmap = new Bitmap(256, 256)) using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                float fontSize = (float)Math.Abs(256 * .3), insetX = (float)-Math.Abs(fontSize * .08), insetY = (float)Math.Abs(fontSize * 1.1);
                using (Font font = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold, GraphicsUnit.Pixel, 0, false))
                using (SolidBrush foregroundBrush = new SolidBrush(textColor))
                using (SolidBrush backgroundBrush = new SolidBrush(backColor))
                {
                    graphics?.FillRectangle(backgroundBrush, 0, 0, 256, 256);
                    graphics?.DrawString("Proxy", font, foregroundBrush, insetX, insetY);
                }
                return Icon.FromHandle(bitmap.GetHicon());
            }
        }
    }

    internal static class NativeMethods
    {
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr handle);
    }
}
