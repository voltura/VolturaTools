#region Using statements

using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Win32;
using System.Diagnostics;

#endregion

namespace DiskSpace
{
    /// <summary>
    /// The application main form
    /// </summary>
    public partial class MainForm : Form
    {
        #region Member variables

        DriveInfo di = null;
        Point offset;
        SettingsForm settingsForm = null;
        private decimal lastFreeSpace = 0M;
        private decimal currentFreeSpace = 0M;

        #endregion

        #region Properties

        /// <summary>
        /// Current free space
        /// </summary>
        private decimal CurrentFreeSpace
        {
            get => currentFreeSpace;
            set
            {
                if (currentFreeSpace != value)
                {
                    currentFreeSpace = value;
                    UpdateFreespaceTexts();
                    ShowBalloonTipNotification();
                    Log.Info = string.Format(CultureInfo.InvariantCulture, "{0} {1:0}{2}{3}",
                        Properties.Settings.Default.DriveLetter,
                        value,
                        Properties.Resources.GB,
                        Properties.Resources.FreeSpace);
                }
            }
        }

        /// <summary>
        /// Last recorded free space
        /// </summary>
        public decimal LastNotifiedFreeSpace { get => lastFreeSpace; set => lastFreeSpace = value; }

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        public Point Offset { get => offset; set => offset = value; }

        /// <summary>
        /// DriveInfo object used to check free disk space
        /// </summary>
        public DriveInfo DI
        {
            get
            {
                if (di == null)
                {
                    di = new DriveInfo(Properties.Settings.Default.DriveLetter);
                }
                return di;
            }
            set => di = value;
        }

        /// <summary>
        /// Settings form
        /// </summary>
        public SettingsForm ApplicationSettingsForm
        {
            get
            {
                if (settingsForm == null)
                {
                    settingsForm = new SettingsForm();
                }
                settingsForm.Icon = Properties.Resources.samsung_m2_ssd;
                return settingsForm;
            }
            set => settingsForm = value;
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
                decimal diskCapacityInGB = Math.Round((decimal)DI.TotalSize / 1024 / 1024 / 1024);
                if (diskCapacityInGB / 1024 >= 1M)
                {
                    return string.Format(CultureInfo.InvariantCulture,
                                        "{0:0.0}", diskCapacityInGB / 1024).Replace(".0","") +
                                        Properties.Resources.TB;
                }
                return string.Format(CultureInfo.InvariantCulture,
                                    "{0:0}", diskCapacityInGB) + 
                                    Properties.Resources.GB;
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
            Icon = Properties.Resources.samsung_m2_ssd;
            diskSpaceNotifyIcon.Icon = Properties.Resources.samsung_m2_ssd;
            UpdateTitleText();
            Text = Properties.Resources.DiskSpace;
            contextMenuStrip.Text = Properties.Resources.DiskSpace;
            if (Properties.Settings.Default.startMinimized)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
            }
            else
            {
                WindowState = FormWindowState.Normal;
                Show();
            }
            if (Properties.Settings.Default.startWithWindows)
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
            TopMost = Properties.Settings.Default.alwaysOnTop;
            Visible = !Properties.Settings.Default.startMinimized;
            quitToolStripMenuItem.Text = Properties.Resources.Quit;
            settingsToolStripMenuItem.Text = Properties.Resources.Settings;
            diskCleanupToolStripMenuItem.Text = Properties.Resources.Diskcleanup;
            diskManagementToolStripMenuItem.Text = Properties.Resources.DiskManagement;
            if (!File.Exists(CleanMgrFullPath))
            {
                diskCleanupToolStripMenuItem.Enabled = false;
            }
            UpdateContextMenuItemText();
            Properties.Settings.Default.DriveChanged += DriveLetterSettingChanged;
            checkTimer.Enabled = true;
        }

        private void UpdateTitleText()
        {
            string titleText = Properties.Resources.DiskSpace + Properties.Resources.Space + DI.Name.Substring(0,2)
                + Properties.Resources.Space + DiskSize;
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
                if (string.IsNullOrEmpty(Properties.Settings.Default.DriveLetter) ||
                    (Properties.Settings.Default.DriveLetter.Length != 1))
                {
                    Properties.Settings.Default.DriveLetter = "C";
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
                    if (d.Name.Contains(Properties.Settings.Default.DriveLetter))
                    {
                        configuredDriveExists = true;
                        break;
                    }
                }
                if (!configuredDriveExists)
                {
                    Properties.Settings.Default.DriveLetter =
                        (string.IsNullOrEmpty(foundDrive) || foundDrive.Length < 1)
                        ? "C" : foundDrive.Substring(0, 1);
                }
            }
            finally
            {
                Properties.Settings.Default.Save();
            }
        }

        private void SaveOffsetAndLocation(MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.mainFormLocation = Location;
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
                    Properties.Settings.Default.mainFormLocation = Location;
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
            settingsIcon.Image = Properties.Resources.simple_gears;
        }

        private void FocusSettingsIcon()
        {
            settingsIcon.Image = Properties.Resources.simple_gears_grey;
        }

        private void MinimizeAndHideMainForm()
        {
            Properties.Settings.Default.mainFormLocation = Location;
            Visible = false;
            WindowState = FormWindowState.Minimized;
        }

        private void Cleanup()
        {
            if (ApplicationSettingsForm != null)
            {
                ApplicationSettingsForm.Dispose();
            }
            if (diskSpaceNotifyIcon != null)
            {
                diskSpaceNotifyIcon.Dispose();
            }
            if (contextMenuStrip != null)
            {
                contextMenuStrip.Dispose();
            }
        }

        private void SaveMainFormLocation()
        {
            if (WindowState == FormWindowState.Normal && Visible == true)
            {
                Properties.Settings.Default.mainFormLocation = Location;
                if (Properties.Settings.Default.mainFormLocation.Y + Height > Screen.GetWorkingArea(this).Height)
                {
                    Properties.Settings.Default.mainFormLocation = new Point(1, 1);
                }
                Properties.Settings.Default.Save();
            }
        }

        private void UpdateFreespaceTexts()
        {
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            string freeSpaceFormText = string.Format(formatProvider, "{0:0.00}", CurrentFreeSpace).Replace(".00", "") +
                Properties.Resources.GB;
            UpdateFormFreeSpaceText(freeSpaceFormText);
            UpdateTitleText();
            UpdateNotificationFreeSpaceText(freeSpaceFormText);
        }

        private void UpdateNotificationFreeSpaceText(string freeSpaceFormText)
        {
            string freeSpaceInfoText = Properties.Settings.Default.DriveLetter +
                Properties.Resources.DriveSeparator + freeSpaceFormText + Properties.Resources.FreeSpace;
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

            if (Properties.Settings.Default.notifyWhenSpaceChange)
            {
                bool limitReached = (Properties.Settings.Default.NotificationLimitActive == true &&
                    Properties.Settings.Default.NotificationLimitGB >= uSpace);
                if ((!Properties.Settings.Default.NotificationLimitActive) ||
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
                showToolStripMenuItem.Text = Properties.Resources.Hide;
            }
            else
            {
                Properties.Settings.Default.Save();
                Visible = false;
                WindowState = FormWindowState.Minimized;
                showToolStripMenuItem.Text = Properties.Resources.Show;
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
            if (Properties.Settings.Default.mainFormLocation != null)
            {
                if (Properties.Settings.Default.mainFormLocation.Y + Height > Screen.GetWorkingArea(this).Height)
                {
                    Properties.Settings.Default.mainFormLocation = new Point(1, 1);
                }
                Location = Properties.Settings.Default.mainFormLocation;
            }
        }

        private void ChangeMonitoredDrive()
        {
            string currentDriveLetter = Properties.Settings.Default.DriveLetter;
            string nextDriveLetter = LocalDrives.GetNextDriveLetter(currentDriveLetter);
            if (nextDriveLetter != currentDriveLetter)
            {
                DI = new DriveInfo(nextDriveLetter);
                Properties.Settings.Default.DriveLetter = nextDriveLetter;
                Properties.Settings.Default.Save();
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
                        Arguments = Properties.Settings.Default.DriveLetter,
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
                    UseShellExecute = true,
                };
                p.Start();
            }
        }

        private void UpdateContextMenuItemText()
        {
            showToolStripMenuItem.Text = Visible ?
                Properties.Resources.Hide : Properties.Resources.Show;
        }

        #endregion

        #region Event handling

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            CurrentFreeSpace = DI.AvailableFreeSpace / 1024 / 1024 / 1024;
            UpdateContextMenuItemText();
            Log.Truncate();
        }

        private void DriveLetterSettingChanged(object sender, EventArgs e)
        {
            DI = new DriveInfo(Properties.Settings.Default.DriveLetter);
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
                Properties.Settings.Default.mainFormLocation = Location;
                Properties.Settings.Default.Save();
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