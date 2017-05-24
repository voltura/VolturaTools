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
        DriveInfo di;
        Point offset;
        SettingsForm settingsForm;

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        public Point Offset { get => offset; set => offset = value; }

        /// <summary>
        /// DriveInfo object used to check free disk space
        /// </summary>
        public DriveInfo DI { get => di; set => di = value; }

        /// <summary>
        /// Main form constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitApplication();
        }

        private void InitApplication()
        {
            CheckSettings();
            di = new DriveInfo(Properties.Settings.Default.driveLetter);
            this.lblTitle.Text = Properties.Resources.DiskSpace;
            this.Text = Properties.Resources.DiskSpace;
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
            UpdateFreespace();
            settingsForm = new SettingsForm();
            TopMost = Properties.Settings.Default.alwaysOnTop;
            checkTimer.Enabled = true;
            showToolStripMenuItem.Text = Properties.Settings.Default.startMinimized 
                ? Properties.Resources.Show : Properties.Resources.Hide;
            quitToolStripMenuItem.Text = Properties.Resources.Quit;
            settingsToolStripMenuItem.Text = Properties.Resources.Settings;
            lblTitle.Text = Properties.Resources.DiskSpace;
            Text = Properties.Resources.DiskSpace;
            contextMenuStrip.Text = Properties.Resources.DiskSpace;
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
                    if (Properties.Settings.Default.driveLetter.Equals(d.Name))
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
            Offset = new Point(e.X, e.Y);
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Top = Cursor.Position.Y - Offset.Y;
                Left = Cursor.Position.X - Offset.X;
            }
        }

        private void MinimizePanel_MouseEnter(object sender, EventArgs e)
        {
            minimizePanel.BackColor = Color.LightGray;
        }

        private void MinimizePanel_MouseLeave(object sender, EventArgs e)
        {
            minimizePanel.BackColor = Color.White;
        }

        private void SettingsIcon_MouseLeave(object sender, EventArgs e)
        {
            settingsIcon.Image = Properties.Resources.simple_gears;
        }

        private void SettingsIcon_MouseEnter(object sender, EventArgs e)
        {
            settingsIcon.Image = Properties.Resources.simple_gears_grey;
        }

        private void MinimizeContainerPanel_MouseEnter(object sender, EventArgs e)
        {
            MinimizePanel_MouseEnter(sender, e);
        }

        private void MinimizeContainerPanel_MouseLeave(object sender, EventArgs e)
        {
            MinimizePanel_MouseLeave(sender, e);
        }

        private void MinimizeContainerPanel_MouseClick(object sender, MouseEventArgs e)
        {
            MinimizePanel_MouseClick(sender, e);
        }

        private void MinimizePanel_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void TitleIcon_MouseDown(object sender, MouseEventArgs e)
        {
            Title_MouseDown(sender, e);
        }

        private void TitleIcon_MouseMove(object sender, MouseEventArgs e)
        {
            Title_MouseMove(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            diskSpaceNotifyIcon.Dispose();
        }

        private void DiskSpaceNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            showToolStripMenuItem.Text = (WindowState == FormWindowState.Minimized) ? Properties.Resources.Show : Properties.Resources.Hide;
            if (diskSpaceNotifyIcon.ContextMenuStrip.Visible)
                diskSpaceNotifyIcon.ContextMenuStrip.Hide();
            else
                diskSpaceNotifyIcon.ContextMenuStrip.Show(Cursor.Position);
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            UpdateFreespace();
        }

        private void UpdateFreespace()
        {
            decimal space = DI.AvailableFreeSpace / 1024 / 1024 / 1024;
            uint uSpace = Convert.ToUInt32(space);
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            string freeSpace = string.Format(formatProvider, "{0:0.00}", space).Replace(".00", "") + Properties.Resources.GB;
            if (lblFreeSpace.Text != freeSpace)
            {
                lblFreeSpace.Text = freeSpace;
                diskSpaceNotifyIcon.BalloonTipText = Properties.Settings.Default.driveLetter +
                    Properties.Resources.DriveSeparator + freeSpace + Properties.Resources.FreeSpace;
                diskSpaceNotifyIcon.Text = diskSpaceNotifyIcon.BalloonTipText;
                if (Properties.Settings.Default.notifyWhenSpaceChange)
                {
                    if ((!Properties.Settings.Default.NotificationLimitActive) || 
                        (Properties.Settings.Default.NotificationLimitActive == true &&
                        Properties.Settings.Default.NotificationLimitGB >= uSpace))
                    {
                        diskSpaceNotifyIcon.Visible = true;
                        diskSpaceNotifyIcon.ShowBalloonTip(500);
                    }
                }
            }
        }

        private void ToogleFormVisibility()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
                showToolStripMenuItem.Text = Properties.Resources.Hide;
            }
            else
            {
                Hide();
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
            showToolStripMenuItem.Text = (WindowState == FormWindowState.Normal) ? 
                Properties.Resources.Hide : Properties.Resources.Show;
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToogleFormVisibility();
        }
        
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog(this);
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
    }
}