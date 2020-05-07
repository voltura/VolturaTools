﻿using System.ComponentModel;
using System.Windows.Forms;

namespace DiskSpace.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            _settingsForm?.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblFreeSpace = new System.Windows.Forms.Label();
            this.minimizePanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.minimizeContainerPanel = new System.Windows.Forms.Panel();
            this.diskSpaceNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.diskCleanupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runDiskCleanupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diskManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTimer = new System.Windows.Forms.Timer(this.components);
            this.titleIcon = new System.Windows.Forms.PictureBox();
            this.settingsIcon = new System.Windows.Forms.PictureBox();
            this.logFileIcon = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logFileIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFreeSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblFreeSpace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFreeSpace.Font = new System.Drawing.Font("Segoe UI Black", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreeSpace.ForeColor = System.Drawing.Color.White;
            this.lblFreeSpace.Location = new System.Drawing.Point(0, 74);
            this.lblFreeSpace.Margin = new System.Windows.Forms.Padding(0);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.Size = new System.Drawing.Size(810, 193);
            this.lblFreeSpace.TabIndex = 1;
            this.lblFreeSpace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFreeSpace.Click += new System.EventHandler(this.FreeSpace_Click);
            this.lblFreeSpace.MouseEnter += new System.EventHandler(this.FreeSpace_MouseEnter);
            this.lblFreeSpace.MouseLeave += new System.EventHandler(this.FreeSpace_MouseLeave);
            // 
            // minimizePanel
            // 
            this.minimizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizePanel.BackColor = System.Drawing.Color.White;
            this.minimizePanel.Location = new System.Drawing.Point(731, 17);
            this.minimizePanel.Margin = new System.Windows.Forms.Padding(0);
            this.minimizePanel.Name = "minimizePanel";
            this.minimizePanel.Size = new System.Drawing.Size(60, 19);
            this.minimizePanel.TabIndex = 2;
            this.minimizePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MinimizePanel_MouseClick);
            this.minimizePanel.MouseEnter += new System.EventHandler(this.MinimizePanel_MouseEnter);
            this.minimizePanel.MouseLeave += new System.EventHandler(this.MinimizePanel_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(67, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(797, 74);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            // 
            // minimizeContainerPanel
            // 
            this.minimizeContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeContainerPanel.Location = new System.Drawing.Point(724, 0);
            this.minimizeContainerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeContainerPanel.Name = "minimizeContainerPanel";
            this.minimizeContainerPanel.Size = new System.Drawing.Size(82, 74);
            this.minimizeContainerPanel.TabIndex = 3;
            this.minimizeContainerPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MinimizeContainerPanel_MouseClick);
            this.minimizeContainerPanel.MouseEnter += new System.EventHandler(this.MinimizeContainerPanel_MouseEnter);
            this.minimizeContainerPanel.MouseLeave += new System.EventHandler(this.MinimizeContainerPanel_MouseLeave);
            // 
            // diskSpaceNotifyIcon
            // 
            this.diskSpaceNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.diskSpaceNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("diskSpaceNotifyIcon.Icon")));
            this.diskSpaceNotifyIcon.Visible = true;
            this.diskSpaceNotifyIcon.BalloonTipClicked += new System.EventHandler(this.DiskSpaceNotifyIcon_BalloonTipClicked);
            this.diskSpaceNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DiskSpaceNotifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.contextMenuStrip.DropShadowEnabled = false;
            this.contextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.quitToolStripMenuItem,
            this.logToolStripMenuItem,
            this.showToolStripMenuItem,
            this.toolStripSeparator1,
            this.diskCleanupToolStripMenuItem,
            this.runDiskCleanupToolStripMenuItem,
            this.diskManagementToolStripMenuItem});
            this.contextMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(422, 370);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(421, 60);
            this.settingsToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Settings;
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(421, 60);
            this.quitToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Quit;
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(421, 60);
            this.logToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Log;
            this.logToolStripMenuItem.Click += new System.EventHandler(MainForm.LogToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(421, 60);
            this.showToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.ShowHide;
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(418, 6);
            // 
            // diskCleanupToolStripMenuItem
            // 
            this.diskCleanupToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diskCleanupToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.diskCleanupToolStripMenuItem.Name = "diskCleanupToolStripMenuItem";
            this.diskCleanupToolStripMenuItem.Size = new System.Drawing.Size(421, 60);
            this.diskCleanupToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Diskcleanup;
            this.diskCleanupToolStripMenuItem.Click += new System.EventHandler(MainForm.DiskCleanupToolStripMenuItem_Click);
            // 
            // runDiskCleanupToolStripMenuItem
            // 
            this.runDiskCleanupToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runDiskCleanupToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.runDiskCleanupToolStripMenuItem.Name = "diskCleanupToolStripMenuItem";
            this.runDiskCleanupToolStripMenuItem.Size = new System.Drawing.Size(421, 60);
            this.runDiskCleanupToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.RunDiskcleanup;
            this.runDiskCleanupToolStripMenuItem.Click += new System.EventHandler(MainForm.RunDiskCleanupToolStripMenuItem_Click);
            // 
            // diskManagementToolStripMenuItem
            // 
            this.diskManagementToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diskManagementToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.diskManagementToolStripMenuItem.Name = "diskManagementToolStripMenuItem";
            this.diskManagementToolStripMenuItem.Size = new System.Drawing.Size(421, 60);
            this.diskManagementToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.DiskManagement;
            this.diskManagementToolStripMenuItem.Click += new System.EventHandler(MainForm.DiskManagementToolStripMenuItem_Click);
            // 
            // checkTimer
            // 
            this.checkTimer.Interval = 1000;
            this.checkTimer.Tick += new System.EventHandler(this.CheckTimer_Tick);
            // 
            // titleIcon
            // 
            this.titleIcon.BackColor = System.Drawing.Color.Transparent;
            this.titleIcon.Image = global::DiskSpace.Properties.Resources.ssdIconWhitePng;
            this.titleIcon.InitialImage = global::DiskSpace.Properties.Resources.ssdIconWhitePng;
            this.titleIcon.Location = new System.Drawing.Point(10, 10);
            this.titleIcon.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.titleIcon.Name = "titleIcon";
            this.titleIcon.Size = new System.Drawing.Size(62, 60);
            this.titleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.titleIcon.TabIndex = 1;
            this.titleIcon.TabStop = false;
            this.titleIcon.Click += new System.EventHandler(this.TitleIcon_Click);
            this.titleIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleIcon_MouseDown);
            this.titleIcon.MouseEnter += new System.EventHandler(this.TitleIcon_MouseEnter);
            this.titleIcon.MouseLeave += new System.EventHandler(this.TitleIcon_MouseLeave);
            this.titleIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleIcon_MouseMove);
            // 
            // settingsIcon
            // 
            this.settingsIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.settingsIcon.Image = global::DiskSpace.Properties.Resources.gearsIconWhite;
            this.settingsIcon.Location = new System.Drawing.Point(700, 161);
            this.settingsIcon.Margin = new System.Windows.Forms.Padding(0);
            this.settingsIcon.Name = "settingsIcon";
            this.settingsIcon.Size = new System.Drawing.Size(101, 97);
            this.settingsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.settingsIcon.TabIndex = 2;
            this.settingsIcon.TabStop = false;
            this.settingsIcon.Click += new System.EventHandler(this.SettingsIcon_Click);
            this.settingsIcon.MouseEnter += new System.EventHandler(this.SettingsIcon_MouseEnter);
            this.settingsIcon.MouseLeave += new System.EventHandler(this.SettingsIcon_MouseLeave);
            // 
            // logFileIcon
            // 
            this.logFileIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.logFileIcon.Image = global::DiskSpace.Properties.Resources.logIconWhite;
            this.logFileIcon.Location = new System.Drawing.Point(10, 161);
            this.logFileIcon.Margin = new System.Windows.Forms.Padding(0);
            this.logFileIcon.Name = "logFileIcon";
            this.logFileIcon.Size = new System.Drawing.Size(101, 97);
            this.logFileIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logFileIcon.TabIndex = 4;
            this.logFileIcon.TabStop = false;
            this.logFileIcon.Click += new System.EventHandler(MainForm.LogFileIcon_Click);
            this.logFileIcon.MouseEnter += new System.EventHandler(this.LogFileIcon_MouseEnter);
            this.logFileIcon.MouseLeave += new System.EventHandler(this.LogFileIcon_MouseLeave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(288F, 288F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(810, 267);
            this.ControlBox = false;
            this.Controls.Add(this.logFileIcon);
            this.Controls.Add(this.titleIcon);
            this.Controls.Add(this.minimizePanel);
            this.Controls.Add(this.settingsIcon);
            this.Controls.Add(this.lblFreeSpace);
            this.Controls.Add(this.minimizeContainerPanel);
            this.Controls.Add(this.lblTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(40, 40);
            this.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logFileIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Label lblFreeSpace;
        private PictureBox settingsIcon;
        private PictureBox titleIcon;
        private Panel minimizePanel;
        private Label lblTitle;
        private Panel minimizeContainerPanel;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private Timer checkTimer;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem diskCleanupToolStripMenuItem;
        private ToolStripMenuItem runDiskCleanupToolStripMenuItem;
        private ToolStripMenuItem diskManagementToolStripMenuItem;
        private PictureBox logFileIcon;
        private ToolStripMenuItem logToolStripMenuItem;
        private NotifyIcon diskSpaceNotifyIcon;
    }
}

