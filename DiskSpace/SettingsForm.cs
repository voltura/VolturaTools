#region Using statements

using System;
using System.Windows.Forms;
using System.Drawing;

#endregion
namespace DiskSpace
{
    /// <summary>
    /// Settings form
    /// </summary>
    public partial class SettingsForm : Form
    {
        #region Private member variables

        Point offset;

        #endregion

        #region Protected class properties

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        protected Point Offset { get => offset; set => offset = value; }

        #endregion

        #region Constructor

        /// <summary>
        /// Settings form constructor
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
            InitializeFormFromSettings();
        }

        #endregion

        #region Events handling

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void UpdateDriveLetterSetting()
        {
            Properties.Settings.Default.driveLetter = cmbDrives.SelectedValue.ToString();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsTitle_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateOffset(e);
        }

        private void SettingsTitle_MouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e);
        }

        private void NotificationLimitGB_TextChanged(object sender, EventArgs e)
        {
            AcceptOnlyNumericNotificationGBInput();
        }

        private void MinimizePanel_MouseEnter(object sender, EventArgs e)
        {
            FocusMinimizeIcon();
        }

        private void MinimizePanel_MouseLeave(object sender, EventArgs e)
        {
            UnfocusMinimizeIcon();
        }

        private void MinimizePanel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizePanelFrame_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            InitializeFormFromSettings();
        }

        #endregion

        #region Private methods

        private void InitializeFormFromSettings()
        {
            PopulateDrivesInDropdown();
            SetControlTextsFromResources();
            SetValuesFromSettings();
            AddFieldDataBindings();
        }

        private void SetValuesFromSettings()
        {
            cmbDrives.SelectedValue = Properties.Settings.Default.driveLetter;
            chkNotificationLimit.Checked = Properties.Settings.Default.NotificationLimitActive;
            chkStartWithWindows.Checked = Properties.Settings.Default.startWithWindows;
            chkAlwaysOnTop.Checked = Properties.Settings.Default.alwaysOnTop;
            chkDisplayNotifications.Checked = Properties.Settings.Default.notifyWhenSpaceChange;
            chkStartMinimized.Checked = Properties.Settings.Default.startMinimized;
            chkStartMinimized.CheckState = CheckState.Checked;
        }

        private void SetControlTextsFromResources()
        {
            chkNotificationLimit.Text = Properties.Resources.NotificationLimit;
            btnSave.Text = Properties.Resources.SaveButtonTitle;
            chkStartWithWindows.Text = Properties.Resources.StartWithWindowsText;
            chkAlwaysOnTop.Text = Properties.Resources.AlwaysOnTop;
            chkDisplayNotifications.Text = Properties.Resources.ShowNotifications;
            chkStartMinimized.Text = Properties.Resources.StartMinimized;
            lblDrive.Text = Properties.Resources.DriveToMonitor;
            lblSettingsTitle.Text = Properties.Resources.Settings;
            lblGB.Text = Properties.Resources.GB;
            Text = Properties.Resources.Settings;
        }

        private void PopulateDrivesInDropdown()
        {
            cmbDrives.DataSource = LocalDrives.Drives();
            cmbDrives.DisplayMember = "Description";
            cmbDrives.ValueMember = "DriveName";
        }

        private void AddFieldDataBindings()
        {
            if (chkNotificationLimit.DataBindings.Count == 0)
            {
                chkNotificationLimit.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "NotificationLimitActive", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (txtNotificationLimitGB.DataBindings.Count == 0)
            {
                txtNotificationLimitGB.DataBindings.Add(new Binding("Text", Properties.Settings.Default, "NotificatonAmountLimit", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (chkStartWithWindows.DataBindings.Count == 0)
            {
                chkStartWithWindows.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "startWithWindows", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (chkAlwaysOnTop.DataBindings.Count == 0)
            {
                chkAlwaysOnTop.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "alwaysOnTop", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (chkDisplayNotifications.DataBindings.Count == 0)
            {
                chkDisplayNotifications.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "notifyWhenSpaceChange", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (chkStartMinimized.DataBindings.Count == 0)
            {
                chkStartMinimized.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "startMinimized", true, DataSourceUpdateMode.OnPropertyChanged));
            }
        }

        private void SaveSettings()
        {
            UnfocusMinimizeIcon();
            UpdateNotificationLimitSetting();
            UpdateDriveLetterSetting();
            Properties.Settings.Default.Save();
        }

        private void FocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.LightGray;
        }

        private void AcceptOnlyNumericNotificationGBInput()
        {
            if (!uint.TryParse(txtNotificationLimitGB.Text, out uint parsedValue))
            {
                txtNotificationLimitGB.Text = string.Empty;
            }
        }

        private void MoveForm(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Top = Cursor.Position.Y - Offset.Y;
                Left = Cursor.Position.X - Offset.X;
            }
        }

        private void UpdateOffset(MouseEventArgs e)
        {
            Offset = new Point(e.X, e.Y);
        }

        private void UpdateNotificationLimitSetting()
        {
            if (uint.TryParse(txtNotificationLimitGB.Text, out uint notificationLimit))
            {
                Properties.Settings.Default.NotificationLimitGB = notificationLimit;
            }
            else
            {
                Properties.Settings.Default.NotificationLimitGB = 10;
            }
        }

        private void UnfocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.White;
        }

        #endregion
    }
}