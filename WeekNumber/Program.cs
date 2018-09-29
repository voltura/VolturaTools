using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Win32;

internal class Program : IDisposable
{
    const string REGRUN = @"Software\Microsoft\Windows\CurrentVersion\Run\";
    NotifyIcon _notifyIcon = null;
    ContextMenu _contextMenu = null;
    Timer _timer = null;
    int _week, _oldWeek;
    static readonly string _about = Application.ProductName + 
        " by Voltura AB\r\rhttps://github.com/voltura/VolturaTools\r\rFree for all.";
    static readonly string _aboutTitle = Application.ProductName + " version " + Application.ProductVersion;
    readonly Font _font = new Font(FontFamily.GenericMonospace, 26f, FontStyle.Bold);

    static internal class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr handle);
    }    

    [STAThread]
    [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults")]
    static void Main()
    {
        new Program();
        Application.Run();
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "voltura")]
    [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
    public Program()
    {
        try
        {
            _contextMenu = new ContextMenu(new MenuItem[4]
            {
            new MenuItem("&About " + Application.ProductName + "\tshift+A",
                delegate
                {
                    if (_contextMenu.MenuItems.Count > 0)
                    {
                        _contextMenu.MenuItems[0].Enabled = false;
                        MessageBox.Show(_about, _aboutTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (_contextMenu.MenuItems.Count > 0) _contextMenu.MenuItems[0].Enabled = true;
                }
            ) { DefaultItem = true },
            new MenuItem("&Start with Windows\tshift+S",
                delegate
                {
                    if (_contextMenu.MenuItems.Count > 0)
                    {
                        _contextMenu.MenuItems[1].Enabled = false;
                        using (RegistryKey cu = Registry.CurrentUser)
                        {
                            if (Registry.GetValue(@"HKEY_CURRENT_USER\" + REGRUN, Application.ProductName, null) == null)
                            {
                                cu.OpenSubKey(REGRUN, true).SetValue(Application.ProductName, Application.ExecutablePath);
                                _contextMenu.MenuItems[1].Checked = true;
                            }
                            else
                            {
                                cu.OpenSubKey(REGRUN, true).DeleteValue(Application.ProductName);
                                _contextMenu.MenuItems[1].Checked = false;
                            }
                            cu.Flush();
                        }
                    }
                    if (_contextMenu.MenuItems.Count > 0) _contextMenu.MenuItems[1].Enabled = true;
                }
            ) { Index = 1 },
            new MenuItem("-"),
            new MenuItem("E&xit " + Application.ProductName + "\tshift+X",
                delegate
                {
                    Dispose();
                    Application.Exit();
                }
            )
            });
            _contextMenu.MenuItems[1].Checked = Registry.GetValue(@"HKEY_CURRENT_USER\" + REGRUN, Application.ProductName, null) != null;
            _oldWeek = _week = ThisWeek;
            _notifyIcon = new NotifyIcon() { Visible = true };
            _notifyIcon.ContextMenu = _contextMenu;
            SetWeekIcon(ref _week);
            _timer = new Timer() { Interval = 60000, Enabled = true };
            _timer.Tick += delegate
            {
                Application.DoEvents();
                lock (this)
                {
                    _week = ThisWeek;
                    if (_oldWeek != _week)
                    {
                        _oldWeek = _week;
                        SetWeekIcon(ref _week);
                    }
                }
            };
        } catch (Exception ex)
        {
            MessageBox.Show("Something went wrong. Please report to feedback@voltura.se!\r\r" + ex.ToString(), Application.ProductName + "\t" + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }

    int ThisWeek => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "voltura")]
    [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
    void SetWeekIcon(ref int t)
    {
        try
        {
            _notifyIcon.Text = "Week " + _week;
            using (Bitmap bitmap = new Bitmap(48, 48))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(Brushes.Black, 1, 1, 46, 46);
                graphics.DrawString(t.ToString(), _font, Brushes.White, -4f, 6f);
                graphics.DrawRectangle(new Pen(Color.White, 3f), 1, 1, 45, 45);
                IntPtr hicon = bitmap.GetHicon();
                Icon oldIcon = _notifyIcon.Icon;
                _notifyIcon.Icon = Icon.FromHandle(hicon);
                if (oldIcon != null) NativeMethods.DestroyIcon(oldIcon.Handle);
                oldIcon?.Dispose();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Could not set Icon. Please report to feedback@voltura.se!\r\r" + ex.ToString(), Application.ProductName + "\t" + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }

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
            _font.Dispose();
            NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
            _notifyIcon?.ContextMenu?.Dispose();
            _notifyIcon?.Icon?.Dispose();
            _notifyIcon?.Dispose();
            _contextMenu.Dispose();
        }
    }
}