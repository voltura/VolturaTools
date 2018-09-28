using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System;

internal class Program : IDisposable
{
    NotifyIcon ni = null;
    Timer t = null;
    Bitmap b = null;
    readonly Font f = new Font(FontFamily.GenericMonospace, 26f, FontStyle.Bold);
    Icon i = null;
    Graphics g = null;
    string w = null;

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
        ni.Icon = GetTextIcon(ref w);
        ni.Text = w;
        t = new Timer() { Interval = 1000, Enabled = true };
        t.Tick += delegate
        {
            if (ni.Text != w)
            {
                ThisWeek(ref w);
                ni.Icon?.Dispose();
                ni.Icon = GetTextIcon(ref w);
                GC.Collect();
                GC.WaitForPendingFinalizers();
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
        g.DrawString(t, f, Brushes.White, 0f, 0f);
        i = Icon.FromHandle(b.GetHicon());
        b?.Dispose();
        g?.Dispose();
        return i;
    }

    public void Dispose()
    {
        t?.Stop();
        t?.Dispose();
        b.Dispose();
        ni.Icon?.Dispose();
        ni?.Dispose();
        f.Dispose();
    }
}