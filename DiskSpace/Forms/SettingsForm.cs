#region Using statements

using System;
using System.Drawing;
using System.Windows.Forms;
using DiskSpace.Properties;

#endregion

namespace DiskSpace.Forms
{
    /// <summary>
    ///     Settings form
    /// </summary>
    public partial class SettingsForm : Form
    {
        #region Private member variables

        private readonly EmailSettingsForm _emailSettings;

        #endregion

        #region Protected class properties

        /// <summary>
        ///     Mouse location offset used form form movement
        /// </summary>
        protected Point Offset { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Settings form constructor
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
            InitializeFormFromSettings();
            _emailSettings = new EmailSettingsForm();
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
            SetDriveSelectionFromUserSettings();
            TickCheckboxesBasedOnUserSettings();
        }

        private void SetDriveSelectionFromUserSettings() => cmbDrives.SelectedValue = Settings.Default.driveLetter;

        private void TickCheckboxesBasedOnUserSettings()
        {
            chkNotificationLimit.Checked = Settings.Default.NotificationLimitActive;
            chkStartWithWindows.Checked = Settings.Default.startWithWindows;
            chkAlwaysOnTop.Checked = Settings.Default.alwaysOnTop;
            chkDisplayNotifications.Checked = Settings.Default.notifyWhenSpaceChange;
            chkStartMinimized.Checked = Settings.Default.startMinimized;
        }

        private void SetControlTextsFromResources()
        {
            SetCheckboxTextsFromResources();
            SetLabelTextsFromResources();
            SetButtonTextsFromResources();
            Text = Resources.Settings;
        }

        private void SetButtonTextsFromResources() => btnSave.Text = Resources.SaveButtonTitle;

        private void SetLabelTextsFromResources()
        {
            lblDrive.Text = Resources.DriveToMonitor;
            lblSettingsTitle.Text = Resources.Settings;
            lblGB.Text = Resources.GB;
        }

        private void SetCheckboxTextsFromResources()
        {
            chkNotificationLimit.Text = Resources.NotificationLimit;
            chkStartWithWindows.Text = Resources.StartWithWindowsText;
            chkAlwaysOnTop.Text = Resources.AlwaysOnTop;
            chkDisplayNotifications.Text = Resources.ShowNotifications;
            chkStartMinimized.Text = Resources.StartMinimized;
        }

        private void PopulateDrivesInDropdown()
        {
            cmbDrives.DataSource = LocalDrives.Drives();
            cmbDrives.DisplayMember = "Description";
            cmbDrives.ValueMember = "DriveName";
        }

        private void AddFieldDataBindings()
        {
            CheckNotificationLimitDataBinding();
            CheckNotificationLimitGbDataBindings();
            CheckStartWithWindowsDataBindings();
            CheckAlwaysOnTopDataBindings();
            CheckDisplayNotificationsDataBindings();
            CheckStartMinimizedDataBindings();
        }

        private void CheckStartMinimizedDataBindings()
        {
            if (chkStartMinimized.DataBindings.Count != 0) return;
            chkStartMinimized.DataBindings.Add(new Binding("Checked", Settings.Default, "startMinimized", true,
                DataSourceUpdateMode.OnPropertyChanged));
        }

        private void CheckDisplayNotificationsDataBindings()
        {
            if (chkDisplayNotifications.DataBindings.Count != 0) return;
            chkDisplayNotifications.DataBindings.Add(new Binding("Checked", Settings.Default,
                "notifyWhenSpaceChange", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void CheckAlwaysOnTopDataBindings()
        {
            if (chkAlwaysOnTop.DataBindings.Count != 0) return;
            chkAlwaysOnTop.DataBindings.Add(new Binding("Checked", Settings.Default, "alwaysOnTop", true,
                DataSourceUpdateMode.OnPropertyChanged));
        }

        private void CheckStartWithWindowsDataBindings()
        {
            if (chkStartWithWindows.DataBindings.Count != 0) return;
            chkStartWithWindows.DataBindings.Add(new Binding("Checked", Settings.Default, "startWithWindows", true,
                DataSourceUpdateMode.OnPropertyChanged));
        }

        private void CheckNotificationLimitGbDataBindings()
        {
            if (txtNotificationLimitGB.DataBindings.Count != 0) return;
            txtNotificationLimitGB.DataBindings.Add(new Binding("Text", Settings.Default, "NotificatonAmountLimit",
                true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void CheckNotificationLimitDataBinding()
        {
            if (chkNotificationLimit.DataBindings.Count != 0) return;
            chkNotificationLimit.DataBindings.Add(new Binding("Checked", Settings.Default,
                "NotificationLimitActive", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void SaveSettings()
        {
            UnfocusMinimizeIcon();
            UpdateNotificationLimitSetting();
            UpdateDriveLetterSetting();
            Settings.Default.Save();
        }

        private void FocusMinimizeIcon() => minimizePanel.BackColor = Color.LightGray;

        private void AcceptOnlyNumericNotificationGbInput()
        {
            if (uint.TryParse(txtNotificationLimitGB.Text, out uint _)) return;
            txtNotificationLimitGB.Text = string.Empty;
        }

        private void MoveForm(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Top = Cursor.Position.Y - Offset.Y;
            Left = Cursor.Position.X - Offset.X;
        }

        private void UpdateOffset(MouseEventArgs e) => Offset = new Point(e.X, e.Y);

        private void UpdateNotificationLimitSetting() => Settings.Default.NotificationLimitGB =
            uint.TryParse(txtNotificationLimitGB.Text, out uint notificationLimit) ? notificationLimit : 10;

        private void UnfocusMinimizeIcon() => minimizePanel.BackColor = Color.White;

        private void SetActiveBackColorOnNotifictationLimitGb() => txtNotificationLimitGB.BackColor = Color.DeepSkyBlue;

        private void ResetBackColorOnNotificationLimitGb() => txtNotificationLimitGB.BackColor = settingsPanel.BackColor;

        private void ShowEmailSettingsForm() => _emailSettings.ShowDialog(this);

        #endregion

        #region Events handling

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e) => SaveSettings();

        private void UpdateDriveLetterSetting() => Settings.Default.driveLetter = cmbDrives.SelectedValue.ToString();

        private void Save_Click(object sender, EventArgs e) => Close();

        private void SettingsTitle_MouseDown(object sender, MouseEventArgs e) => UpdateOffset(e);

        private void SettingsTitle_MouseMove(object sender, MouseEventArgs e) => MoveForm(e);

        private void NotificationLimitGB_TextChanged(object sender, EventArgs e) => AcceptOnlyNumericNotificationGbInput();

        private void MinimizePanel_MouseEnter(object sender, EventArgs e) => FocusMinimizeIcon();

        private void MinimizePanel_MouseLeave(object sender, EventArgs e) => UnfocusMinimizeIcon();

        private void MinimizePanel_Click(object sender, EventArgs e) => Close();

        private void MinimizePanelFrame_Click(object sender, EventArgs e) => Close();

        private void SettingsForm_Load(object sender, EventArgs e) => InitializeFormFromSettings();

        private void NotificationLimitGB_MouseEnter(object sender, EventArgs e) => SetActiveBackColorOnNotifictationLimitGb();

        private void NotificationLimitGB_MouseLeave(object sender, EventArgs e)
        {
            if (txtNotificationLimitGB.Focused) return;
            ResetBackColorOnNotificationLimitGb();
        }

        private void NotificationLimitGB_Enter(object sender, EventArgs e) => SetActiveBackColorOnNotifictationLimitGb();

        private void NotificationLimitGB_Leave(object sender, EventArgs e) => ResetBackColorOnNotificationLimitGb();

        private void ConfigureEmail_Click(object sender, EventArgs e) => ShowEmailSettingsForm();

        #endregion
    }
}