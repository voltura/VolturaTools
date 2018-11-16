#region Using statements

using System.Drawing;
using System.IO;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal static class WeekIcon
    {
        #region Internal static functions

        internal static Icon GetIcon(int weekNumber)
        {
            Icon icon = null;
            using (var bitmap = new Bitmap(128, 128))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                DrawBackgroundOnGraphics(graphics);
                DrawWeekNumberOnGraphics(weekNumber, graphics);
                var bHicon = bitmap.GetHicon();
                var newIcon = Icon.FromHandle(bHicon);
                icon = new Icon(newIcon, SystemInformation.SmallIconSize);
                CleanupIcon(ref newIcon);
            }
            return icon;
        }

        internal static void CleanupIcon(ref Icon icon)
        {
            if (icon is null)
            {
                return;
            }
            NativeMethods.DestroyIcon(icon.Handle);
            icon.Dispose();
        }

        internal static bool SaveIcon(int weekNumber, string fullPath)
        {
            bool result = true;
            Icon icon = null;

            try
            {
                icon = GetIcon(weekNumber);
                using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    icon.Save(fs);
                }
            }
            catch (System.Exception ex)
            {
                Message.Show(Resources.UnhandledException, ex);
                result = false;
            }
            finally
            {
                CleanupIcon(ref icon);
            }
            return result;
        }

        #endregion Internal static functions

        #region Privare static helper methods

        private static void DrawBackgroundOnGraphics(Graphics graphics)
        {
            graphics?.FillRectangle(Brushes.Black, 4, 4, 124, 124);
            using (var whitePen = new Pen(Color.White, 8f))
            {
                graphics?.DrawRectangle(whitePen, 4, 4, 120, 120);
            }
            graphics?.FillRectangle(Brushes.White, 20, 2, 12, 24);
            graphics?.FillRectangle(Brushes.White, 96, 2, 12, 24);
        }

        private static void DrawWeekNumberOnGraphics(int weekNumber, Graphics graphics)
        {
            using (var font = new Font(FontFamily.GenericMonospace, 100f, FontStyle.Bold, GraphicsUnit.Pixel, 0, false))
            {
                var color = Color.FromName(Settings.GetSetting(Resources.Foreground));
                Brush brush = new SolidBrush(color);
                graphics?.DrawString(weekNumber.ToString().PadLeft(2, '0').Substring(0, 2), font, brush, -14f, 20f);
            }
        }

        #endregion Private static helper methods
    }
}