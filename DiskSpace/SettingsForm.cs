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
            if (txtNotificationLimitGB.DataBindings.Count == 0)
                txtNotificationLimitGB.DataBindings.Add(new Binding("Text", Properties.Settings.Default, "NotificatonAmountLimit", true, DataSourceUpdateMode.OnPropertyChanged));
            chkNotificationLimit.Checked = Properties.Settings.Default.NotificationLimitActive;
            if (chkNotificationLimit.DataBindings.Count == 0)
                chkNotificationLimit.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "NotificationLimitActive", true, DataSourceUpdateMode.OnPropertyChanged));
            chkNotificationLimit.Text = Properties.Resources.NotificationLimit;
            btnSave.Text = Properties.Resources.SaveButtonTitle;
            chkStartWithWindows.Checked = Properties.Settings.Default.startWithWindows;
            if (chkStartWithWindows.DataBindings.Count == 0)
                chkStartWithWindows.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "startWithWindows", true, DataSourceUpdateMode.OnPropertyChanged));
            chkStartWithWindows.Text = Properties.Resources.StartWithWindowsText;
            chkAlwaysOnTop.Checked = Properties.Settings.Default.alwaysOnTop;
            if (chkAlwaysOnTop.DataBindings.Count == 0)
                chkAlwaysOnTop.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "alwaysOnTop", true, DataSourceUpdateMode.OnPropertyChanged));
            chkAlwaysOnTop.Text = Properties.Resources.AlwaysOnTop;
            chkDisplayNotifications.Checked = Properties.Settings.Default.notifyWhenSpaceChange;
            if (chkDisplayNotifications.DataBindings.Count == 0)
                chkDisplayNotifications.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "notifyWhenSpaceChange", true, DataSourceUpdateMode.OnPropertyChanged));
            chkDisplayNotifications.Text = Properties.Resources.ShowNotifications;
            chkStartMinimized.Checked = Properties.Settings.Default.startMinimized;
            chkStartMinimized.CheckState = CheckState.Checked;
            if (chkStartMinimized.DataBindings.Count == 0)
                chkStartMinimized.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "startMinimized", true, DataSourceUpdateMode.OnPropertyChanged));
            chkStartMinimized.Text = Properties.Resources.StartMinimized;
            lblDrive.Text = Properties.Resources.DriveToMonitor;
            lblSettingsTitle.Text = Properties.Resources.Settings;
            lblGB.Text = Properties.Resources.GB;
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.driveLetter = cmbDrives.SelectedValue.ToString();
            Properties.Settings.Default.Save();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Offset = new Point(e.X, e.Y);
        }

        private void SettingsTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Top = Cursor.Position.Y - Offset.Y;
                Left = Cursor.Position.X - Offset.X;
            }
        }
    }
}