#region Using statements

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#endregion

namespace WeekNumber
{
    internal static class Week
    {
        #region Public static functions

        public static void InitiateWeek(ref int oldWeek, ref int week) => oldWeek = (week = ThisWeek);

        public static int ThisWeek => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, 
            CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        public static void SetWeekIcon(ref int weekNumber, ref NotifyIcon notifyIcon)
        {
            notifyIcon.Text = "Week " + weekNumber;
            using (Bitmap bitmap = new Bitmap(64, 64))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.Black, 2, 2, 62, 62);
                    using (Font font = new Font(FontFamily.GenericMonospace, 36f, FontStyle.Bold))
                    {
                        graphics.DrawString(weekNumber.ToString(), font, Brushes.White, -6f, 6f);
                    }
                    graphics.DrawRectangle(new Pen(Color.White, 4f), 2, 2, 60, 60);
                    IntPtr hicon = bitmap.GetHicon();
                    Icon prevIcon = notifyIcon.Icon;
                    notifyIcon.Icon = Icon.FromHandle(hicon);
                    if (prevIcon != null)
                    {
                        NativeMethods.DestroyIcon(prevIcon.Handle);
                    }
                    prevIcon?.Dispose();
                }
            }
        }

        #endregion
    }
}