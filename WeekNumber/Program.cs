using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System;

internal class Program : IDisposable
{
    NotifyIcon ni = null;
    Timer t = null;
    Bitmap b = null;
    Icon i = null;
    Graphics g = null;
    string w = null;
    readonly Font f = new Font(FontFamily.GenericMonospace, 26f, FontStyle.Bold);
    readonly ContextMenu cm = new ContextMenu(new MenuItem[1] { new MenuItem("E&xit WeekNumber", delegate { Application.Exit(); }) { DefaultItem = true } });

    [STAThread]
    static void Main()
    {
        new Program();
        Application.Run();
    }

    public Program()
    {
        ThisWeek(ref w);
        ni = new NotifyIcon() { Visible = true };
        ni.ContextMenu = cm;
        ni.Icon = GetTextIcon(ref w);
        ni.Text = w;
        t = new Timer() { Interval = 60000, Enabled = true };
        t.Tick += delegate
        {
            lock (this)
            {
                if (ni.Text != w)
                {
                    ThisWeek(ref w);
                    ni.Icon?.Dispose();
                    ni.Icon = GetTextIcon(ref w);
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Application.DoEvents();
            }
        };
    }

    void ThisWeek(ref string w)
    {
        int iw = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            DateTime.Now, CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday);
        w = iw.ToString();
    }

    Icon GetTextIcon(ref string t)
    {
        i?.Dispose();
        b?.Dispose();
        g?.Dispose();
        b = new Bitmap(48, 48);
        g = Graphics.FromImage(b);
        g.FillRectangle(Brushes.Black, 1, 1, 46, 46);
        g.DrawString(t, f, Brushes.White, -4f, 4f);
        g.DrawRectangle(Pens.White, 0, 0, 47, 47);
        i = Icon.FromHandle(b.GetHicon());
        b?.Dispose();
        g?.Dispose();
        return i;
    }

    public void Dispose()
    {
        t?.Stop();
        t?.Dispose();
        b?.Dispose();
        f?.Dispose();
        ni?.ContextMenu?.Dispose();
        ni?.Icon?.Dispose();
        ni?.Dispose();
        cm?.Dispose();
    }
}