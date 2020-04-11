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

        [STAThread]
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
        private readonly Icon _onIcon;
        private readonly Icon _offIcon;
        private readonly Timer _timer;
        private bool _proxyEnabled;
        private string _proxyServer;

        internal AppContext()
        {
            _onIcon = CreateIcon(Color.MediumSpringGreen, Color.Black);
            _offIcon = CreateIcon(Color.Crimson, Color.White);
            _proxyEnabled = ProxyEnabled();
            _proxyServer = ProxyServer();
            _notifyIcon = _proxyEnabled ? AppNotifyIcon(ref _onIcon) : AppNotifyIcon(ref _offIcon);
            _notifyIcon.Text = (_proxyEnabled ? "Proxy is ON" : "Proxy is OFF") + $"\r\nProxy: { _proxyServer}";
            _notifyIcon.Click += NotifyIcon_Click;
            _notifyIcon.ContextMenu = new ContextMenu(new MenuItem[1] { new MenuItem("E&xit ProxyToogle", ExitMenuClick) });
            _timer = new Timer
            {
                Interval = 1000,
                Enabled = true
            };
            _timer.Tick += OnTimerTick;

        }

        private static void ExitMenuClick(object o, EventArgs e)
        {
            Application.Exit();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer?.Stop();
            Application.DoEvents();
            _proxyEnabled = ProxyEnabled();
            _proxyServer = ProxyServer();
            _notifyIcon.Icon = _proxyEnabled ? _onIcon : _offIcon;
            _notifyIcon.Text = (_proxyEnabled ? "Proxy is ON" : "Proxy is OFF") + $"\r\nProxy: { _proxyServer}";
            timer?.Start();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button != MouseButtons.Left)
            {
                return;
            }

            ToogleProxy();
        }

        internal bool ProxyEnabled()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            return (int)registry.GetValue("ProxyEnable", 0, RegistryValueOptions.None) == 1 ? true : false;
        }

        internal string ProxyServer()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            return (string)registry.GetValue("ProxyServer", "No proxy server defined", RegistryValueOptions.None);
        }

        internal void ToogleProxy()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            registry.SetValue("ProxyEnable", ProxyEnabled() ? 0 : 1);
            bool settingsReturn, refreshReturn;
            settingsReturn = NativeMethods.InternetSetOption(IntPtr.Zero, NativeMethods.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            refreshReturn = NativeMethods.InternetSetOption(IntPtr.Zero, NativeMethods.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
            Console.Write($"Internet Options change = {settingsReturn}, Internet Options refreshed = {refreshReturn}");
        }

        private Icon CreateIcon(Color backColor, Color textColor)
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

        private NotifyIcon AppNotifyIcon(ref Icon icon)
        {
            return new NotifyIcon() { Visible = true, Icon = icon };
        }
    }
}
