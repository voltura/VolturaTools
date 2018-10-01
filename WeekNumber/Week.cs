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
                    using (Pen whitePen = new Pen(Color.White, 4f))
                    {
                        graphics.DrawRectangle(whitePen, 2, 2, 60, 60);
                    }
                    graphics.FillRectangle(Brushes.White, 10, 2, 6, 12);
                    graphics.FillRectangle(Brushes.White, 48, 2, 6, 12);
                    using (Font font = new Font(FontFamily.GenericMonospace, 36f, FontStyle.Bold))
                    {
                        graphics.DrawString(weekNumber.ToString(), font, Brushes.White, -6f, 10f);
                    }
                    IntPtr hicon = bitmap.GetHicon();
                    Icon prevIcon = notifyIcon.Icon;
                    Icon newIcon = Icon.FromHandle(hicon);
                    notifyIcon.Icon = new Icon(newIcon, SystemInformation.SmallIconSize);
                    if (prevIcon != null)
                    {
                        NativeMethods.DestroyIcon(prevIcon.Handle);
                    }
                    if (newIcon != null)
                    {
                        NativeMethods.DestroyIcon(newIcon.Handle);
                    }
                    prevIcon?.Dispose();
                }
            }
        }

        #endregion
    }
}