using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;

internal class Program : IDisposable
{
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults")]
    static void Main()
    {
        new Program();
        Application.Run();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
    public Program()
    {
        _contextMenu = new ContextMenu(new MenuItem[3]
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
            new MenuItem("-"),
            new MenuItem("E&xit " + Application.ProductName + "\tshift+X", delegate { Dispose(); Application.Exit(); }) { }
        });
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
    }

    int ThisWeek => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                DateTime.Now, CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
    void SetWeekIcon(ref int t)
    {
        _notifyIcon.Text = "Week " + _week;
        Bitmap bitmap = new Bitmap(48, 48);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.FillRectangle(Brushes.Black, 1, 1, 46, 46);
        graphics.DrawString(t.ToString(), _font, Brushes.White, -4f, 5f);
        graphics.DrawRectangle(Pens.White, 0, 0, 47, 47);
        IntPtr hicon = bitmap.GetHicon();
        Icon icon = Icon.FromHandle(hicon);
        if (_notifyIcon.Icon is null == false) NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
        _notifyIcon.Icon = icon;
        bitmap.Dispose();
        graphics.Dispose();
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