using System;
using System.Windows.Forms;
using System.Drawing;

namespace DiskSpace
{
    /// <summary>
    /// Settings form
    /// </summary>
    public partial class SettingsForm : Form
    {
        Point offset;

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        public Point Offset { get => offset; set => offset = value; }

        /// <summary>
        /// Settings form constructor
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
            cmbDrives.DataSource = LocalDrives.Drives();
            cmbDrives.DisplayMember = "Description";
            cmbDrives.ValueMember = "DriveName";
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.driveLetter = cmbDrives.SelectedValue.ToString();
            Properties.Settings.Default.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblSettingsTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Offset = new Point(e.X, e.Y);
        }

        private void lblSettingsTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Top = Cursor.Position.Y - Offset.Y;
                Left = Cursor.Position.X - Offset.X;
            }
        }
    }
}