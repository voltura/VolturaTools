using System.Drawing;

namespace WeekNumber
{
    static class TextIcon
    {
        public static Icon Get(string text)
        {
            Bitmap bitmap = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage(bitmap);
            g.FillEllipse(Brushes.Black, 0, 0, 32, 32);
            g.DrawString(text,
                new Font(
                    FontFamily.GenericSerif,
                    20.0f,
                    FontStyle.Bold) { },
                Brushes.White,
                new PointF(1.0f, 1.0f));
            return Icon.FromHandle(bitmap.GetHicon());
        }
    }
}