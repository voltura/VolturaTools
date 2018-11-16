#region Using statements

using System.Drawing;
using System.IO;
using System.Windows.Forms;

#endregion Using statements

namespace WeekNumber
{
    internal static class WeekIcon
    {
        #region Icon Size

        private static readonly int _size = 256;

        #endregion Icon Size

        #region Internal static functions

        internal static Icon GetIcon(int weekNumber)
        {
            Icon icon = null;
            using (var bitmap = new Bitmap(_size, _size))
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
            var result = true;
            Icon icon = null;

            try
            {
                icon = GetIcon(weekNumber);
                using (FileStream fs = new FileStream(fullPath, FileMode.Create, 
                    FileAccess.Write, FileShare.None))
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
            var backgroundColor = Color.FromName(Settings.GetSetting(Resources.Background));
            var foregroundColor = Color.FromName(Settings.GetSetting(Resources.Foreground));
            using (var foregroundBrush = new SolidBrush(foregroundColor))
            using (var backgroundBrush = new SolidBrush(backgroundColor))
            {
                var inset = (float)System.Math.Abs(_size * .03125);
                graphics?.FillRectangle(backgroundBrush, inset, inset, _size - inset, _size - inset);
                using (var pen = new Pen(foregroundColor, inset * 2))
                {
                    graphics?.DrawRectangle(pen, inset, inset, _size - inset * 2, _size - inset * 2);
                }
                var leftInset = (float)System.Math.Abs(_size * .15625);
                var rightInset = (float)System.Math.Abs(_size * .75);
                graphics?.FillRectangle(foregroundBrush, leftInset, inset / 2, inset * 3, inset * 5);
                graphics?.FillRectangle(foregroundBrush, rightInset, inset / 2, inset * 3, inset * 5);
            }
        }

        private static void DrawWeekNumberOnGraphics(int weekNumber, Graphics graphics)
        {
            var fontSize = (float)System.Math.Abs(_size * .78125);
            var insetX = (float)-System.Math.Abs(fontSize * .14);
            var insetY = (float)System.Math.Abs(fontSize * .2);
            var foregroundColor = Color.FromName(Settings.GetSetting(Resources.Foreground));

            using (var font = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold, 
                GraphicsUnit.Pixel, 0, false))
            using (Brush brush = new SolidBrush(foregroundColor))
            {
                graphics?.DrawString(weekNumber.ToString().PadLeft(2, '0').Substring(0, 2), font, brush, insetX, insetY);
            }
        }

        #endregion Private static helper methods
    }
}