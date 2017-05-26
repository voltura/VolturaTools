using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Win32;

namespace DiskSpace
{
    /// <summary>
    /// The application main form
    /// </summary>
    public partial class MainForm : Form
    {
        DriveInfo di = null;
        Point offset;
        SettingsForm settingsForm = null;

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        public Point Offset { get => offset; set => offset = value; }

        /// <summary>
        /// DriveInfo object used to check free disk space
        /// </summary>
        public DriveInfo DI { get => di; set => di = value; }

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

        /// <summary>
        /// Main form constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            try
            {
                InitApplication();
            }
            catch (Exception ex)
            {
                if (ApplicationSettingsForm != null)
                {
                    ApplicationSettingsForm.Dispose();
                }
                MessageBox.Show(this, 
                    ex.ToString(), 
                    ProductName + ProductVersion, 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error, 
                    MessageBoxDefaultButton.Button1, 
                    MessageBoxOptions.DefaultDesktopOnly);
                throw;
            }
            
        }

        private void InitApplication()
        {
            CheckSettings();
            Icon = Properties.Resources.samsung_m2_ssd;
            diskSpaceNotifyIcon.Icon = Properties.Resources.samsung_m2_ssd;
            di = new DriveInfo(Properties.Settings.Default.driveLetter);
            lblTitle.Text = Properties.Resources.DiskSpace;
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
            UpdateFreespaceAndShowBalloonTip();
            TopMost = Properties.Settings.Default.alwaysOnTop;
            checkTimer.Enabled = true;
            Visible = !Properties.Settings.Default.startMinimized;
            quitToolStripMenuItem.Text = Properties.Resources.Quit;
            settingsToolStripMenuItem.Text = Properties.Resources.Settings;
        }

        private static void CheckSettings()
        {
            try
            {
                // check driveLetter setting
                if (string.IsNullOrEmpty(Properties.Settings.Default.driveLetter) ||
                    (Properties.Settings.Default.driveLetter.Length != 1))
                {
                    Properties.Settings.Default.driveLetter = "C";
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
                    if (d.Name.Contains(Properties.Settings.Default.driveLetter))
                    {
                        configuredDriveExists = true;
                        break;
                    }
                }
                if (!configuredDriveExists)
                {
                    Properties.Settings.Default.driveLetter =
                        (string.IsNullOrEmpty(foundDrive) || foundDrive.Length < 1) 
                        ? "C" : foundDrive.Substring(0, 1);
                }
            }
            finally
            {
                Properties.Settings.Default.Save();
            }
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            SaveOffsetAndLocation(e);
        }

        private void SaveOffsetAndLocation(MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.mainFormLocation = Location;
            }
            Offset = new Point(e.X, e.Y);
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            MoveMainFormAndSaveLocation(e);
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

        private void MinimizePanel_MouseEnter(object sender, EventArgs e)
        {
            FocusMinimizeIcon();
        }

        private void FocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.LightGray;
        }

        private void MinimizePanel_MouseLeave(object sender, EventArgs e)
        {
            UnfocusMinimizeIcon();
        }

        private void UnfocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.White;
        }

        private void SettingsIcon_MouseLeave(object sender, EventArgs e)
        {
            UnfocusSettingsIcon();
        }

        private void UnfocusSettingsIcon()
        {
            settingsIcon.Image = Properties.Resources.simple_gears;
        }

        private void SettingsIcon_MouseEnter(object sender, EventArgs e)
        {
            FocusSettingsIcon();
        }

        private void FocusSettingsIcon()
        {
            settingsIcon.Image = Properties.Resources.simple_gears_grey;
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

        private void MinimizeAndHideMainForm()
        {
            Properties.Settings.Default.mainFormLocation = Location;
            Visible = false;
            WindowState = FormWindowState.Minimized;
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
            CleanupAndSaveMainFormLocation();
        }

        private void CleanupAndSaveMainFormLocation()
        {
            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.mainFormLocation = Location;
                if (Properties.Settings.Default.mainFormLocation.Y + Height > Screen.GetWorkingArea(this).Height)
                {
                    Properties.Settings.Default.mainFormLocation = new Point(1, 1);
                }
                Properties.Settings.Default.Save();
            }
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

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            UpdateFreespaceAndShowBalloonTip();
        }

        private void UpdateFreespaceAndShowBalloonTip()
        {
            decimal space = DI.AvailableFreeSpace / 1024 / 1024 / 1024;
            uint uSpace = Convert.ToUInt32(space);
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            string freeSpace = string.Format(formatProvider, "{0:0.00}", space).Replace(".00", "") + 
                Properties.Resources.GB;
            if (lblFreeSpace.Text != freeSpace)
            {
                lblFreeSpace.Text = freeSpace;
                diskSpaceNotifyIcon.BalloonTipText = Properties.Settings.Default.driveLetter +
                    Properties.Resources.DriveSeparator + freeSpace + Properties.Resources.FreeSpace;
                diskSpaceNotifyIcon.Text = diskSpaceNotifyIcon.BalloonTipText;
                if (Properties.Settings.Default.notifyWhenSpaceChange)
                {
                    bool limitReached = (Properties.Settings.Default.NotificationLimitActive == true &&
                        Properties.Settings.Default.NotificationLimitGB >= uSpace);
                    if ((!Properties.Settings.Default.NotificationLimitActive) ||
                        limitReached)
                    {
                        diskSpaceNotifyIcon.BalloonTipIcon = limitReached ? ToolTipIcon.Warning : ToolTipIcon.Info;
                        diskSpaceNotifyIcon.ShowBalloonTip(limitReached ? 10000:500);
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

        private void UpdateContextMenuItemText()
        {
            showToolStripMenuItem.Text = Visible ?
                Properties.Resources.Hide : Properties.Resources.Show;
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

        private void CloseSettingsForm()
        {
            if (ApplicationSettingsForm.WindowState == FormWindowState.Normal &&
                ApplicationSettingsForm.Visible)
            {
                ApplicationSettingsForm.Visible = false;
                ApplicationSettingsForm.Close();
            }
        }

        private void SaveMainFormLocation()
        {
            if (WindowState == FormWindowState.Normal && Visible)
            {
                Properties.Settings.Default.mainFormLocation = Location;
                Properties.Settings.Default.Save();
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
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

        private void ShowContextMenuAtTitleIcon()
        {
            titleIcon.ContextMenuStrip.Show(this, new Point(10, 10));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetMainFormLocationFromSettings();
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
    }
}