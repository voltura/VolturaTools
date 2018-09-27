using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

internal class Program
{
    NotifyIcon ni = new NotifyIcon() { Visible = true };
    Timer t;

    static void Main()
    {
        Program p = new Program();
        Application.Run();
    }

    public Program()
    {
        ni.Icon = GetTextIcon(ThisWeek);
        t = new Timer() { Interval = 60000, Enabled = true };
        t.Tick += delegate(object o, System.EventArgs e) {
            if (ni.Text != ThisWeek) ni.Icon = GetTextIcon(ThisWeek);
        };
    }

    static string ThisWeek => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            System.DateTime.Now, CalendarWeekRule.FirstFourDayWeek,
            System.DayOfWeek.Monday).ToString();

    static Icon GetTextIcon(string t)
    {
        Bitmap bitmap = new Bitmap(48, 48);
        Graphics.FromImage(bitmap).DrawString(t, 
            new Font(FontFamily.GenericMonospace, 26f, FontStyle.Bold) { }, Brushes.White, 0f, 0f);
        return Icon.FromHandle(bitmap.GetHicon());
    }
}