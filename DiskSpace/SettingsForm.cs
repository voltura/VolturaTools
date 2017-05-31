#region Using statements

using System;
using System.Drawing;
using System.Windows.Forms;
using DiskSpace.Properties;

#endregion

namespace DiskSpace
{
    /// <summary>
    /// Settings form
    /// </summary>
    public partial class SettingsForm : Form
    {
        #region Private member variables

        #endregion

        #region Protected class properties

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        protected Point Offset { get; set; }

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
            Settings.Default.DriveLetter = cmbDrives.SelectedValue.ToString();
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
            AcceptOnlyNumericNotificationGbInput();
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

        private void NotificationLimitGB_MouseEnter(object sender, EventArgs e)
        {
            SetActiveBackColorOnNotifictationLimitGb();
        }

        private void NotificationLimitGB_MouseLeave(object sender, EventArgs e)
        {
            if (!txtNotificationLimitGB.Focused)
            {
                ResetBackColorOnNotificationLimitGb();
            }
        }

        private void NotificationLimitGB_Enter(object sender, EventArgs e)
        {
            SetActiveBackColorOnNotifictationLimitGb();
        }

        private void NotificationLimitGB_Leave(object sender, EventArgs e)
        {
            ResetBackColorOnNotificationLimitGb();
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
            cmbDrives.SelectedValue = Settings.Default.DriveLetter;
            chkNotificationLimit.Checked = Settings.Default.NotificationLimitActive;
            chkStartWithWindows.Checked = Settings.Default.startWithWindows;
            chkAlwaysOnTop.Checked = Settings.Default.alwaysOnTop;
            chkDisplayNotifications.Checked = Settings.Default.notifyWhenSpaceChange;
            chkStartMinimized.Checked = Settings.Default.startMinimized;
            chkStartMinimized.CheckState = CheckState.Checked;
        }

        private void SetControlTextsFromResources()
        {
            chkNotificationLimit.Text = Resources.NotificationLimit;
            btnSave.Text = Resources.SaveButtonTitle;
            chkStartWithWindows.Text = Resources.StartWithWindowsText;
            chkAlwaysOnTop.Text = Resources.AlwaysOnTop;
            chkDisplayNotifications.Text = Resources.ShowNotifications;
            chkStartMinimized.Text = Resources.StartMinimized;
            lblDrive.Text = Resources.DriveToMonitor;
            lblSettingsTitle.Text = Resources.Settings;
            lblGB.Text = Resources.GB;
            Text = Resources.Settings;
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
                chkNotificationLimit.DataBindings.Add(new Binding("Checked", Settings.Default, "NotificationLimitActive", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (txtNotificationLimitGB.DataBindings.Count == 0)
            {
                txtNotificationLimitGB.DataBindings.Add(new Binding("Text", Settings.Default, "NotificatonAmountLimit", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (chkStartWithWindows.DataBindings.Count == 0)
            {
                chkStartWithWindows.DataBindings.Add(new Binding("Checked", Settings.Default, "startWithWindows", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (chkAlwaysOnTop.DataBindings.Count == 0)
            {
                chkAlwaysOnTop.DataBindings.Add(new Binding("Checked", Settings.Default, "alwaysOnTop", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (chkDisplayNotifications.DataBindings.Count == 0)
            {
                chkDisplayNotifications.DataBindings.Add(new Binding("Checked", Settings.Default, "notifyWhenSpaceChange", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            if (chkStartMinimized.DataBindings.Count == 0)
            {
                chkStartMinimized.DataBindings.Add(new Binding("Checked", Settings.Default, "startMinimized", true, DataSourceUpdateMode.OnPropertyChanged));
            }
        }

        private void SaveSettings()
        {
            UnfocusMinimizeIcon();
            UpdateNotificationLimitSetting();
            UpdateDriveLetterSetting();
            Settings.Default.Save();
        }

        private void FocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.LightGray;
        }

        private void AcceptOnlyNumericNotificationGbInput()
        {
            if (!uint.TryParse(txtNotificationLimitGB.Text, out uint _))
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
            Settings.Default.NotificationLimitGB = uint.TryParse(txtNotificationLimitGB.Text, out uint notificationLimit) ? notificationLimit : 10;
        }

        private void UnfocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.White;
        }

        private void SetActiveBackColorOnNotifictationLimitGb()
        {
            txtNotificationLimitGB.BackColor = Color.DeepSkyBlue;
        }

        private void ResetBackColorOnNotificationLimitGb()
        {
            txtNotificationLimitGB.BackColor = settingsPanel.BackColor;
        }

        #endregion
    }
}