#region Using statements

using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DiskSpace.Forms.Controls;
using DiskSpace.Properties;
using Microsoft.Win32;

#endregion

namespace DiskSpace.Forms
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
                    HandleNotifications();
                    Log.Info = string.Format(CultureInfo.InvariantCulture, "{0} {1:0}{2}{3}",
                        Settings.Default.driveLetter,
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
        public DriveInfo ActiveDriveInfo
        {
            get => _di ?? (_di = new DriveInfo(Settings.Default.driveLetter));
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
                _settingsForm.Icon = Resources.ssdIconWhite;
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
                decimal diskCapacityInGb = Math.Round((decimal) ActiveDriveInfo.TotalSize / 1024 / 1024 / 1024);
                if (diskCapacityInGb / 1024 >= 1M)
                {
                    return string.Format(CultureInfo.InvariantCulture,
                               "{0:0.0}", diskCapacityInGb / 1024).Replace(".0", "") +
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
                Log.Error = ex;
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

            //TODO: Should not be needed to set icon here, remove
            SuspendLayout();
            //Icon = Resources.ssdIconWhite;
            this.ContextMenuStrip = null;
            diskSpaceNotifyIcon.ContextMenuStrip = contextMenuStrip;
            diskSpaceNotifyIcon.Icon = new Icon(Resources.ssdIconWhite, 32, 32);
            Icon = new Icon(Resources.ssdIconWhite, 32, 32);
            ResumeLayout();
            //Refresh();
            //InvokePaint(this, new PaintEventArgs(CreateGraphics(),new Rectangle(new Point(0,0), new Size(new Point(32,32)))));
            
            UpdateTitleText();
            Text = Resources.DiskSpace;
            contextMenuStrip.Text = Resources.DiskSpace;
            if (Settings.Default.startMinimized)
            {
                Log.Info = "Starting minimized";
                WindowState = FormWindowState.Minimized;
                Hide();
            }
            else
            {
                Log.Info = "Showing main form";
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
            HandleNotifications();
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
            showToolStripMenuItem.Text = Resources.ShowHide;
            Settings.Default.DriveChanged += DriveLetterSettingChanged;
            checkTimer.Enabled = true;
        }

        private void UpdateTitleText()
        {
            string titleText = Resources.DiskSpace + Resources.Space + ActiveDriveInfo.Name.Substring(0, 2)
                               + Resources.Space + DiskSize;
            if (lblTitle.Text != titleText)
            {
                lblTitle.Text = titleText;
                Log.Info = "Main form title changed to '" + titleText + "'";
            }
        }

        private static void CheckSettings()
        {
            try
            {
                // check driveLetter setting
                if (string.IsNullOrEmpty(Settings.Default.driveLetter) ||
                    (Settings.Default.driveLetter.Length != 1))
                {
                    Settings.Default.driveLetter = "C";
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
                    if (d.Name.Contains(Settings.Default.driveLetter))
                    {
                        configuredDriveExists = true;
                        break;
                    }
                }
                if (!configuredDriveExists)
                {
                    Settings.Default.driveLetter =
                        (string.IsNullOrEmpty(foundDrive) || foundDrive.Length < 1)
                            ? "C"
                            : foundDrive.Substring(0, 1);
                    Log.Info = "Monitored disk not found, reset to " + Settings.Default.driveLetter;
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
            settingsIcon.Image = Resources.gearsIconWhite;
        }

        private void FocusSettingsIcon()
        {
            settingsIcon.Image = Resources.gearsIconBlue;
        }

        private void FocusTitleIcon()
        {
            titleIcon.Image = Resources.ssdIconGreyPng;
        }

        private void UnfocusTitleIcon()
        {
            titleIcon.Image = Resources.ssdIconWhitePng;
        }

        private void MinimizeAndHideMainForm()
        {
            Settings.Default.mainFormLocation = Location;
            Visible = false;
            WindowState = FormWindowState.Minimized;
        }

        private void Cleanup()
        {
            Log.Info = "Running cleanup";
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
            string freeSpaceInfoText = Settings.Default.driveLetter +
                                       Resources.DriveSeparator + freeSpaceFormText + Resources.FreeSpace;
            if (diskSpaceNotifyIcon.BalloonTipText != freeSpaceInfoText)
            {
                diskSpaceNotifyIcon.BalloonTipText = freeSpaceInfoText;
                diskSpaceNotifyIcon.Text = freeSpaceInfoText;
                Log.Info = "Notification text updated to: " + freeSpaceInfoText;
            }
        }

        private void UpdateFormFreeSpaceText(string freeSpaceFormText)
        {
            if (lblFreeSpace.Text != freeSpaceFormText)
            {
                lblFreeSpace.Text = freeSpaceFormText;
            }
        }

        private void HandleNotifications()
        {
            uint uSpace = Convert.ToUInt32(CurrentFreeSpace);
            if (!Settings.Default.notifyWhenSpaceChange)
            {
                return;
            }
            bool limitReached = (Settings.Default.NotificationLimitActive &&
                                 Settings.Default.NotificationLimitGB >= uSpace);
            if ((!Settings.Default.NotificationLimitActive) ||
                limitReached)
            {
                if (LastNotifiedFreeSpace != CurrentFreeSpace)
                {
                    LastNotifiedFreeSpace = CurrentFreeSpace;
                    if (Settings.Default.sendEmail)
                    {
                        Mail.Send(ProductName +
                                  Resources.Space +
                                  ProductVersion +
                                  Resources.Space +
                                  Resources.Notification,
                            diskSpaceNotifyIcon.Text +
                            Resources.Space +
                            Resources.ProductURL
                        );
                    }
                    diskSpaceNotifyIcon.BalloonTipIcon = limitReached ? ToolTipIcon.Warning : ToolTipIcon.Info;
                    diskSpaceNotifyIcon.ShowBalloonTip(limitReached ? 10000 : 500);
                    diskSpaceNotifyIcon.Visible = true;
                    Log.Info = "Displayed balloon tip";
                }
            }
        }

        private void ToogleFormVisibility()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = true;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                Settings.Default.Save();
                Visible = false;
                WindowState = FormWindowState.Minimized;
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
            Log.Info = "Displayed settings form";
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
            diskSpaceNotifyIcon.ContextMenuStrip.Show(Cursor.Position);
            //titleIcon.ContextMenuStrip.Show(this, new Point(10, 10));
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
            string currentDriveLetter = Settings.Default.driveLetter;
            string nextDriveLetter = LocalDrives.GetNextDriveLetter(currentDriveLetter);
            if (nextDriveLetter != currentDriveLetter)
            {
                ActiveDriveInfo = new DriveInfo(nextDriveLetter);
                Settings.Default.driveLetter = nextDriveLetter;
                Settings.Default.Save();
                Log.Info = "Now montitoring " + Settings.Default.driveLetter;
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
                        Arguments = Settings.Default.driveLetter,
                        UseShellExecute = false
                    };
                    p.Start();
                    Log.Info = "Started Disk Clean-up";
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
                Log.Info = "Started Disk Management";
            }
        }

        private void ChangeFreespaceColor(Color color)
        {
            lblFreeSpace.ForeColor = color;
        }

        #endregion

        #region Event handling

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            CurrentFreeSpace = Math.Round((decimal) ActiveDriveInfo.AvailableFreeSpace / 1024 / 1024 / 1024,
                MidpointRounding.ToEven);
            Log.Truncate();
        }

        private void DriveLetterSettingChanged(object sender, EventArgs e)
        {
            ActiveDriveInfo = new DriveInfo(Settings.Default.driveLetter);
            Log.Info = "Drive letter setting updated to " + Settings.Default.driveLetter;
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
            if (e.Button == MouseButtons.Right)
            {
                if (!contextMenuStrip.Visible)
                    contextMenuStrip.Show(Cursor.Position);
            }
            else
            {
                ToogleFormVisibility();
            }
        }

        private void DiskSpaceNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ToogleFormVisibility();
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
            Log.Info = "Quit clicked";
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

        private void LogFileIcon_Click(object sender, EventArgs e)
        {
            Log.Info = "Display log file requested from main form";
            Log.Show();
        }

        private void LogFileIcon_MouseEnter(object sender, EventArgs e)
        {
            logFileIcon.Image = Resources.logIconBlue;
        }

        private void LogFileIcon_MouseLeave(object sender, EventArgs e)
        {
            logFileIcon.Image = Resources.logIconWhite;
        }

        private void LogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Info = "Display log file requested from context menu";
            Log.Show();
        }

        private void FreeSpace_MouseEnter(object sender, EventArgs e)
        {
            ChangeFreespaceColor(Color.DeepSkyBlue);
        }

        private void FreeSpace_MouseLeave(object sender, EventArgs e)
        {
            ChangeFreespaceColor(Color.White);
        }

        private void TitleIcon_MouseEnter(object sender, EventArgs e)
        {
            FocusTitleIcon();
        }

        private void TitleIcon_MouseLeave(object sender, EventArgs e)
        {
            UnfocusTitleIcon();
        }

        #endregion
    }
}