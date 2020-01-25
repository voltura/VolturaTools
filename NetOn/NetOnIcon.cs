#region Using statements

using System.Drawing;
using System.IO;

#endregion Using statements

namespace NetOn
{
    internal static class NetOnIcon
    {
        #region Icon Size

        private static readonly int _size = 256;

        #endregion Icon Size

        #region Internal static functions

        internal static Icon GetIcon(int netOn)
        {
            Icon icon = null;
            using (Bitmap bitmap = new Bitmap(_size, _size))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                DrawBackgroundOnGraphics(netOn, graphics);
                DrawNetOnOnGraphics(netOn, graphics);
                System.IntPtr bHicon = bitmap.GetHicon();
                Icon newIcon = Icon.FromHandle(bHicon);
                icon = new Icon(newIcon, _size, _size);
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

        internal static bool SaveIcon(int NetOn, string fullPath)
        {
            bool result = true;
            Icon icon = null;

            try
            {
                icon = GetIcon(NetOn);
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

        private static void DrawBackgroundOnGraphics(int netOn, Graphics graphics)
        {
            Color backgroundColor = Color.FromName(Settings.GetSetting(Resources.Background));
            Color foregroundColor = Color.FromName(Settings.GetSetting(Resources.Foreground));
            if (netOn == 0)
            {
                backgroundColor = Color.FromName(Settings.GetSetting(Resources.DisabledBackground));
                foregroundColor = Color.FromName(Settings.GetSetting(Resources.DisabledForeground));
            }
            using (SolidBrush foregroundBrush = new SolidBrush(foregroundColor))
            using (SolidBrush backgroundBrush = new SolidBrush(backgroundColor))
            {
                float inset = (float)System.Math.Abs(_size * .03125);
                graphics?.FillRectangle(backgroundBrush, inset, inset, _size - inset, _size - inset);
                using (Pen pen = new Pen(foregroundColor, inset * 2))
                {
                    graphics?.DrawRectangle(pen, inset, inset, _size - inset * 2, _size - inset * 2);
                }
            }
        }

        private static void DrawNetOnOnGraphics(int netOn, Graphics graphics)
        {
            float fontSize = (float)System.Math.Abs(_size * .68125);
            float insetX = (float)-System.Math.Abs(fontSize * .08);
            float insetY = (float)System.Math.Abs(fontSize * .2);
            Color foregroundColor = Color.FromName(Settings.GetSetting(Resources.Foreground));
            if (netOn == 0)
            {
                fontSize = (float)System.Math.Abs(_size * .48125);
                insetX = (float)-System.Math.Abs(fontSize * .08);
                insetY = (float)System.Math.Abs(fontSize * .535);
                foregroundColor = Color.FromName(Settings.GetSetting(Resources.DisabledForeground));
            }
            using (Font font = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold,
                GraphicsUnit.Pixel, 0, false))
            using (Brush brush = new SolidBrush(foregroundColor))
            {
                if (netOn == 0)
                {
                    graphics?.DrawString(Resources.Off, font, brush, insetX, insetY);
                }
                else
                {
                    graphics?.DrawString(Resources.On, font, brush, insetX, insetY);
                }
            }
        }

        #endregion Private static helper methods
    }
}