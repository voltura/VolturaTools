#region Using statements

using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using DiskSpace.Properties;
using Microsoft.Win32;

#endregion

namespace DiskSpace
{
    /// <summary>
    /// The application main form
    /// </summary>
    public partial class MainForm : Form
    {
        #region Member variables

        DriveInfo _di;
        SettingsForm _settingsForm;
        private decimal _currentFreeSpace;

        #endregion

        #region Properties

        /// <summary>
        /// Current free space
        /// </summary>
        private decimal CurrentFreeSpace
        {
            get => _currentFreeSpace;
            set
            {
                if (_currentFreeSpace != value)
                {
                    _currentFreeSpace = value;
                    UpdateFreespaceTexts();
                    ShowBalloonTipNotification();
                    Log.Info = string.Format(CultureInfo.InvariantCulture, "{0} {1:0}{2}{3}",
                        Settings.Default.DriveLetter,
                        value,
                        Resources.GB,
                        Resources.FreeSpace);
                }
            }
        }

        /// <summary>
        /// Last recorded free space
        /// </summary>
        public decimal LastNotifiedFreeSpace { get; set; }

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        public Point Offset { get; set; }

        /// <summary>
        /// DriveInfo object used to check free disk space
        /// </summary>
        public DriveInfo Di
        {
            get => _di ?? (_di = new DriveInfo(Settings.Default.DriveLetter));
            set => _di = value;
        }

        /// <summary>
        /// Settings form
        /// </summary>
        public SettingsForm ApplicationSettingsForm
        {
            get
            {
                if (_settingsForm == null)
                {
                    _settingsForm = new SettingsForm();
                }
                _settingsForm.Icon = Resources.samsung_m2_ssd;
                return _settingsForm;
            }
            set => _settingsForm = value;
        }

        private static string CleanMgrFullPath
        {
            get
            {
                var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
                string cleanMgrFullPath = Path.Combine(systemPath, "cleanmgr.exe");
                return cleanMgrFullPath;
            }
        }

        private string DiskSize
        {
            get
            {
                decimal diskCapacityInGb = Math.Round((decimal)Di.TotalSize / 1024 / 1024 / 1024);
                if (diskCapacityInGb / 1024 >= 1M)
                {
                    return string.Format(CultureInfo.InvariantCulture,
                                        "{0:0.0}", diskCapacityInGb / 1024).Replace(".0","") +
                                        Resources.TB;
                }
                return string.Format(CultureInfo.InvariantCulture,
                                    "{0:0}", diskCapacityInGb) + 
                                    Resources.GB;
            }
        }        
        #endregion

        #region Constructor

        /// <summary>
        /// Main form constructor
        /// </summary>
        public MainForm()
        {
            try
            {
                InitializeComponent();
                InitApplication();
            }
            catch (Exception ex)
            {
                Cleanup();
                MessageBox.Show(this,
                    ex.Message, 
                    ProductName + ProductVersion, 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error, 
                    MessageBoxDefaultButton.Button1, 
                    MessageBoxOptions.DefaultDesktopOnly);
                throw;
            }
        }

        #endregion

        #region Private functions

        private void InitApplication()
        {
            Log.Info = "Init Application";
            contextMenuStrip.Renderer = new CustomColorsRenderer();
            CheckSettings();
            Icon = Resources.samsung_m2_ssd;
            diskSpaceNotifyIcon.Icon = Resources.samsung_m2_ssd;
            UpdateTitleText();
            Text = Resources.DiskSpace;
            contextMenuStrip.Text = Resources.DiskSpace;
            if (Settings.Default.startMinimized)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
            }
            else
            {
                WindowState = FormWindowState.Normal;
                Show();
            }
            if (Settings.Default.startWithWindows)
            {
                string regKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                string appPath = string.Format(CultureInfo.InvariantCulture, "\"{0}\"",
                    Application.ExecutablePath);
                Registry.SetValue(regKeyPath, "DiskSpace", appPath);
            }
            else
            {
                Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\DiskSpace",
                    false);
            }
            UpdateFreespaceTexts();
            ShowBalloonTipNotification();
            TopMost = Settings.Default.alwaysOnTop;
            Visible = !Settings.Default.startMinimized;
            quitToolStripMenuItem.Text = Resources.Quit;
            settingsToolStripMenuItem.Text = Resources.Settings;
            diskCleanupToolStripMenuItem.Text = Resources.Diskcleanup;
            diskManagementToolStripMenuItem.Text = Resources.DiskManagement;
            if (!File.Exists(CleanMgrFullPath))
            {
                diskCleanupToolStripMenuItem.Enabled = false;
            }
            UpdateContextMenuItemText();
            Settings.Default.DriveChanged += DriveLetterSettingChanged;
            checkTimer.Enabled = true;
        }

        private void UpdateTitleText()
        {
            string titleText = Resources.DiskSpace + Resources.Space + Di.Name.Substring(0,2)
                + Resources.Space + DiskSize;
            if (lblTitle.Text != titleText)
            {
                lblTitle.Text = titleText;
            }
        }

        private static void CheckSettings()
        {
            try
            {
                // check driveLetter setting
                if (string.IsNullOrEmpty(Settings.Default.DriveLetter) ||
                    (Settings.Default.DriveLetter.Length != 1))
                {
                    Settings.Default.DriveLetter = "C";
                }
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                bool configuredDriveExists = false;
                string foundDrive = string.Empty;
                foreach (DriveInfo d in allDrives)
                {
                    if (string.IsNullOrEmpty(foundDrive))
                    {
                        foundDrive = d.Name;
                    }
                    if (d.Name.Contains(Settings.Default.DriveLetter))
                    {
                        configuredDriveExists = true;
                        break;
                    }
                }
                if (!configuredDriveExists)
                {
                    Settings.Default.DriveLetter =
                        (string.IsNullOrEmpty(foundDrive) || foundDrive.Length < 1)
                        ? "C" : foundDrive.Substring(0, 1);
                }
            }
            finally
            {
                Settings.Default.Save();
            }
        }

        private void SaveOffsetAndLocation(MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Settings.Default.mainFormLocation = Location;
            }
            Offset = new Point(e.X, e.Y);
        }

        private void MoveMainFormAndSaveLocation(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Top = Cursor.Position.Y - Offset.Y;
                Left = Cursor.Position.X - Offset.X;
                if (WindowState == FormWindowState.Normal)
                {
                    Settings.Default.mainFormLocation = Location;
                }
            }
        }

        private void FocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.LightGray;
        }

        private void UnfocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.White;
        }

        private void UnfocusSettingsIcon()
        {
            settingsIcon.Image = Resources.simple_gears;
        }

        private void FocusSettingsIcon()
        {
            settingsIcon.Image = Resources.simple_gears_grey;
        }

        private void MinimizeAndHideMainForm()
        {
            Settings.Default.mainFormLocation = Location;
            Visible = false;
            WindowState = FormWindowState.Minimized;
        }

        private void Cleanup()
        {
            ApplicationSettingsForm?.Dispose();
            diskSpaceNotifyIcon?.Dispose();
            contextMenuStrip?.Dispose();
        }

        private void SaveMainFormLocation()
        {
            if (WindowState == FormWindowState.Normal && Visible)
            {
                Settings.Default.mainFormLocation = Location;
                if (Settings.Default.mainFormLocation.Y + Height > Screen.GetWorkingArea(this).Height)
                {
                    Settings.Default.mainFormLocation = new Point(1, 1);
                }
                Settings.Default.Save();
            }
        }

        private void UpdateFreespaceTexts()
        {
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            string freeSpaceFormText = string.Format(formatProvider, "{0:0.00}", CurrentFreeSpace).Replace(".00", "") +
                Resources.GB;
            UpdateFormFreeSpaceText(freeSpaceFormText);
            UpdateTitleText();
            UpdateNotificationFreeSpaceText(freeSpaceFormText);
        }

        private void UpdateNotificationFreeSpaceText(string freeSpaceFormText)
        {
            string freeSpaceInfoText = Settings.Default.DriveLetter +
                Resources.DriveSeparator + freeSpaceFormText + Resources.FreeSpace;
            if (diskSpaceNotifyIcon.BalloonTipText != freeSpaceInfoText)
            {
                diskSpaceNotifyIcon.BalloonTipText = freeSpaceInfoText;
                diskSpaceNotifyIcon.Text = freeSpaceInfoText;
            }
        }

        private void UpdateFormFreeSpaceText(string freeSpaceFormText)
        {
            if (lblFreeSpace.Text != freeSpaceFormText)
            {
                lblFreeSpace.Text = freeSpaceFormText;
            }
        }

        private void ShowBalloonTipNotification()
        {
            uint uSpace = Convert.ToUInt32(CurrentFreeSpace);

            if (Settings.Default.notifyWhenSpaceChange)
            {
                bool limitReached = (Settings.Default.NotificationLimitActive &&
                    Settings.Default.NotificationLimitGB >= uSpace);
                if ((!Settings.Default.NotificationLimitActive) ||
                    limitReached)
                {
                    if (LastNotifiedFreeSpace != CurrentFreeSpace)
                    {
                        LastNotifiedFreeSpace = CurrentFreeSpace;
                        diskSpaceNotifyIcon.BalloonTipIcon = limitReached ?
                            ToolTipIcon.Warning : ToolTipIcon.Info;
                        diskSpaceNotifyIcon.ShowBalloonTip(limitReached ? 10000 : 500);
                        diskSpaceNotifyIcon.Visible = true;
                    }
                }
            }
        }

        private void ToogleFormVisibility()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = true;
                WindowState = FormWindowState.Normal;
                showToolStripMenuItem.Text = Resources.Hide;
            }
            else
            {
                Settings.Default.Save();
                Visible = false;
                WindowState = FormWindowState.Minimized;
                showToolStripMenuItem.Text = Resources.Show;
            }
        }

        private void CloseSettingsForm()
        {
            if (ApplicationSettingsForm.WindowState == FormWindowState.Normal &&
                ApplicationSettingsForm.Visible)
            {
                ApplicationSettingsForm.Visible = false;
                ApplicationSettingsForm.Close();
            }
        }

        private void ShowSettingsForm()
        {
            if (ApplicationSettingsForm.Visible)
            {
                ApplicationSettingsForm.BringToFront();
                ApplicationSettingsForm.Focus();
            }
            else
            {
                ApplicationSettingsForm.ShowDialog(this);
            }
        }

        private void ShowContextMenuAtTitleIcon()
        {
            titleIcon.ContextMenuStrip.Show(this, new Point(10, 10));
        }

        private void SetMainFormLocationFromSettings()
        {
            if (Settings.Default.mainFormLocation.Y + Height > Screen.GetWorkingArea(this).Height)
            {
                Settings.Default.mainFormLocation = new Point(1, 1);
            }
            Location = Settings.Default.mainFormLocation;
        }

        private void ChangeMonitoredDrive()
        {
            string currentDriveLetter = Settings.Default.DriveLetter;
            string nextDriveLetter = LocalDrives.GetNextDriveLetter(currentDriveLetter);
            if (nextDriveLetter != currentDriveLetter)
            {
                Di = new DriveInfo(nextDriveLetter);
                Settings.Default.DriveLetter = nextDriveLetter;
                Settings.Default.Save();
            }
        }

        private static void LaunchCleanManager()
        {
            if (File.Exists(CleanMgrFullPath))
            {
                using (Process p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo(CleanMgrFullPath)
                    {
                        Arguments = Settings.Default.DriveLetter,
                        UseShellExecute = false
                    };
                    p.Start();
                }
            }
        }

        private static void LaunchDiskManagement()
        {
            using (Process p = new Process())
            {
                p.StartInfo = new ProcessStartInfo("diskmgmt.msc")
                {
                    UseShellExecute = true
                };
                p.Start();
            }
        }

        private void UpdateContextMenuItemText()
        {
            showToolStripMenuItem.Text = Visible ?
                Resources.Hide : Resources.Show;
        }

        #endregion

        #region Event handling

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            CurrentFreeSpace = Di.AvailableFreeSpace / 1024 / 1024 / 1024;
            UpdateContextMenuItemText();
            Log.Truncate();
        }

        private void DriveLetterSettingChanged(object sender, EventArgs e)
        {
            Di = new DriveInfo(Settings.Default.DriveLetter);
            UpdateFreespaceTexts();
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            SaveOffsetAndLocation(e);
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            MoveMainFormAndSaveLocation(e);
        }

        private void MinimizePanel_MouseEnter(object sender, EventArgs e)
        {
            FocusMinimizeIcon();
        }

        private void MinimizePanel_MouseLeave(object sender, EventArgs e)
        {
            UnfocusMinimizeIcon();
        }

        private void SettingsIcon_MouseLeave(object sender, EventArgs e)
        {
            UnfocusSettingsIcon();
        }

        private void SettingsIcon_MouseEnter(object sender, EventArgs e)
        {
            FocusSettingsIcon();
        }

        private void MinimizeContainerPanel_MouseEnter(object sender, EventArgs e)
        {
            FocusMinimizeIcon();
        }

        private void MinimizeContainerPanel_MouseLeave(object sender, EventArgs e)
        {
            UnfocusMinimizeIcon();
        }

        private void MinimizeContainerPanel_MouseClick(object sender, MouseEventArgs e)
        {
            MinimizeAndHideMainForm();
        }

        private void MinimizePanel_MouseClick(object sender, MouseEventArgs e)
        {
            MinimizeAndHideMainForm();
        }

        private void TitleIcon_MouseDown(object sender, MouseEventArgs e)
        {
            SaveOffsetAndLocation(e);
        }

        private void TitleIcon_MouseMove(object sender, MouseEventArgs e)
        {
            MoveMainFormAndSaveLocation(e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMainFormLocation();
            Cleanup();
        }

        private void DiskSpaceNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            ToogleDiskSpaceNotifyIconContextMenuVisibility();
        }

        private void ToogleDiskSpaceNotifyIconContextMenuVisibility()
        {
            if (diskSpaceNotifyIcon.ContextMenuStrip.Visible)
            {
                diskSpaceNotifyIcon.ContextMenuStrip.Hide();
            }
            else
            {
                diskSpaceNotifyIcon.ContextMenuStrip.Show(Cursor.Position);
            }
        }

        private void DiskSpaceNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToogleFormVisibility();
        }

        private void DiskSpaceNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ToogleFormVisibility();
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            UpdateContextMenuItemText();
        }

        private void DiskManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchDiskManagement();
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveMainFormLocation();
            if (Visible)
            {
                CloseSettingsForm();
            }
            ToogleFormVisibility();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal && Visible)
            {
                Settings.Default.mainFormLocation = Location;
                Settings.Default.Save();
            }
            Close();
        }

        private void SettingsIcon_Click(object sender, EventArgs e)
        {
            SettingsToolStripMenuItem_Click(sender, e);
        }

        private void ContextMenuStrip_MouseLeave(object sender, EventArgs e)
        {
            contextMenuStrip.Hide();
        }

        private void TitleIcon_Click(object sender, EventArgs e)
        {
            ShowContextMenuAtTitleIcon();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetMainFormLocationFromSettings();
        }

        private void FreeSpace_Click(object sender, EventArgs e)
        {
            ChangeMonitoredDrive();
        }

        private void DiskCleanupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchCleanManager();
        }

        #endregion
    }
}