#region Using statements

using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace DiskSpace.Forms.Controls
{
    /// <summary>
    /// Combobox with custom colors
    /// </summary>
    public class CustomComboBox : ComboBox
    {
        #region Public properties

        /// <summary>
        /// DrawMode
        /// </summary>
        public new DrawMode DrawMode { get; set; }

        /// <summary>
        /// Highlight color
        /// </summary>
        public Color HighlightColor { get; set; }

        #endregion

        #region Public constructor

        /// <summary>
        /// Constructor, sets highlight color
        /// </summary>
        public CustomComboBox()
        {
            base.DrawMode = DrawMode.OwnerDrawFixed;
            HighlightColor = Color.DeepSkyBlue;
            DrawItem += CustomComboBox_DrawItem;
        }

        #endregion

        #region Private methods

        private void CustomComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            ComboBox combo = sender as ComboBox;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (SolidBrush brush = new SolidBrush(HighlightColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            else
            {
                if (combo != null)
                    using (SolidBrush brush = new SolidBrush(combo.BackColor))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
            }
            if (combo != null)
                using (SolidBrush brush = new SolidBrush(combo.ForeColor))
                {
                    Collection<Drive> drives = (Collection<Drive>) combo.DataSource;
                    e.Graphics.DrawString(drives[e.Index].Description, e.Font,
                        brush,
                        new Point(e.Bounds.X, e.Bounds.Y));
                }
            e.DrawFocusRectangle();
        }

        #endregion
    }
}
