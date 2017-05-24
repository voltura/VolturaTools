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

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Offset = new Point(e.X, e.Y);
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Top = Cursor.Position.Y - Offset.Y;
                Left = Cursor.Position.X - Offset.X;
            }
        }

        private void minimizePanel_MouseEnter(object sender, EventArgs e)
        {
            minimizePanel.BackColor = Color.LightGray;
        }

        private void minimizePanel_MouseLeave(object sender, EventArgs e)
        {
            minimizePanel.BackColor = Color.White;
        }

        private void settingsIcon_MouseLeave(object sender, EventArgs e)
        {
            settingsIcon.Image = Properties.Resources.simple_gears;
        }

        private void settingsIcon_MouseEnter(object sender, EventArgs e)
        {
            settingsIcon.Image = Properties.Resources.simple_gears_grey;
        }

        private void minimizeContainerPanel_MouseEnter(object sender, EventArgs e)
        {
            minimizePanel_MouseEnter(sender, e);
        }

        private void minimizeContainerPanel_MouseLeave(object sender, EventArgs e)
        {
            minimizePanel_MouseLeave(sender, e);
        }

        private void minimizeContainerPanel_MouseClick(object sender, MouseEventArgs e)
        {
            minimizePanel_MouseClick(sender, e);
        }

        private void minimizePanel_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void titleIcon_MouseDown(object sender, MouseEventArgs e)
        {
            lblTitle_MouseDown(sender, e);
        }

        private void titleIcon_MouseMove(object sender, MouseEventArgs e)
        {
            lblTitle_MouseMove(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            diskSpaceNotifyIcon.Dispose();
        }

        private void diskSpaceNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (diskSpaceNotifyIcon.ContextMenuStrip.Visible)
                diskSpaceNotifyIcon.ContextMenuStrip.Hide();
            else
                diskSpaceNotifyIcon.ContextMenuStrip.Show(Cursor.Position);
        }

        private void checkTimer_Tick(object sender, EventArgs e)
        {
            UpdateFreespace();
        }

        private void UpdateFreespace()
        {
            decimal space = DI.AvailableFreeSpace / 1024 / 1024 / 1024;
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
                    diskSpaceNotifyIcon.Visible = true;
                    diskSpaceNotifyIcon.ShowBalloonTip(500);
                }
            }
        }

        private void ToogleFormVisibility()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                Show();
                showToolStripMenuItem.Text = Properties.Resources.Hide;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                showToolStripMenuItem.Text = Properties.Resources.Show;
            }
        }

        private void diskSpaceNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToogleFormVisibility();
        }

        private void diskSpaceNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ToogleFormVisibility();
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            showToolStripMenuItem.Text = (WindowState == FormWindowState.Normal) ? 
                Properties.Resources.Hide : Properties.Resources.Show;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToogleFormVisibility();
        }

        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog(this);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsIcon_Click(object sender, EventArgs e)
        {
            settingsToolStripMenuItem_Click(sender, e);
        }
    }
}