namespace DiskSpace
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTimer = new System.Windows.Forms.Timer(this.components);
            this.titleIcon = new System.Windows.Forms.PictureBox();
            this.settingsIcon = new System.Windows.Forms.PictureBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.diskCleanupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblFreeSpace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFreeSpace.Font = new System.Drawing.Font("Segoe UI Black", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreeSpace.ForeColor = System.Drawing.Color.White;
            this.lblFreeSpace.Location = new System.Drawing.Point(0, 32);
            this.lblFreeSpace.Margin = new System.Windows.Forms.Padding(0);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.Size = new System.Drawing.Size(336, 83);
            this.lblFreeSpace.TabIndex = 1;
            this.lblFreeSpace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFreeSpace.Click += new System.EventHandler(this.FreeSpace_Click);
            // 
            // minimizePanel
            // 
            this.minimizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizePanel.BackColor = System.Drawing.Color.White;
            this.minimizePanel.Location = new System.Drawing.Point(303, 7);
            this.minimizePanel.Margin = new System.Windows.Forms.Padding(0);
            this.minimizePanel.Name = "minimizePanel";
            this.minimizePanel.Size = new System.Drawing.Size(25, 8);
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
            this.lblTitle.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(332, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            // 
            // minimizeContainerPanel
            // 
            this.minimizeContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeContainerPanel.Location = new System.Drawing.Point(300, 0);
            this.minimizeContainerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeContainerPanel.Name = "minimizeContainerPanel";
            this.minimizeContainerPanel.Size = new System.Drawing.Size(34, 32);
            this.minimizeContainerPanel.TabIndex = 3;
            this.minimizeContainerPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MinimizeContainerPanel_MouseClick);
            this.minimizeContainerPanel.MouseEnter += new System.EventHandler(this.MinimizeContainerPanel_MouseEnter);
            this.minimizeContainerPanel.MouseLeave += new System.EventHandler(this.MinimizeContainerPanel_MouseLeave);
            // 
            // diskSpaceNotifyIcon
            // 
            this.diskSpaceNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.diskSpaceNotifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.diskSpaceNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("diskSpaceNotifyIcon.Icon")));
            this.diskSpaceNotifyIcon.Visible = true;
            this.diskSpaceNotifyIcon.BalloonTipClicked += new System.EventHandler(this.DiskSpaceNotifyIcon_BalloonTipClicked);
            this.diskSpaceNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DiskSpaceNotifyIcon_MouseClick);
            this.diskSpaceNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DiskSpaceNotifyIcon_MouseDoubleClick);
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
            this.showToolStripMenuItem,
            this.toolStripSeparator1,
            this.diskCleanupToolStripMenuItem});
            this.contextMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(165, 150);
            this.contextMenuStrip.MouseLeave += new System.EventHandler(this.ContextMenuStrip_MouseLeave);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.settingsToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Settings;
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.quitToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Quit;
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.showToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Show;
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // checkTimer
            // 
            this.checkTimer.Interval = 1000;
            this.checkTimer.Tick += new System.EventHandler(this.CheckTimer_Tick);
            // 
            // titleIcon
            // 
            this.titleIcon.BackColor = System.Drawing.Color.Transparent;
            this.titleIcon.ContextMenuStrip = this.contextMenuStrip;
            this.titleIcon.Image = global::DiskSpace.Properties.Resources.ssd;
            this.titleIcon.Location = new System.Drawing.Point(4, 0);
            this.titleIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.titleIcon.Name = "titleIcon";
            this.titleIcon.Size = new System.Drawing.Size(32, 32);
            this.titleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.titleIcon.TabIndex = 1;
            this.titleIcon.TabStop = false;
            this.titleIcon.Click += new System.EventHandler(this.TitleIcon_Click);
            this.titleIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleIcon_MouseDown);
            this.titleIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleIcon_MouseMove);
            // 
            // settingsIcon
            // 
            this.settingsIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.settingsIcon.Image = global::DiskSpace.Properties.Resources.simple_gears;
            this.settingsIcon.Location = new System.Drawing.Point(284, 69);
            this.settingsIcon.Margin = new System.Windows.Forms.Padding(0);
            this.settingsIcon.Name = "settingsIcon";
            this.settingsIcon.Size = new System.Drawing.Size(46, 42);
            this.settingsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.settingsIcon.TabIndex = 2;
            this.settingsIcon.TabStop = false;
            this.settingsIcon.Click += new System.EventHandler(this.SettingsIcon_Click);
            this.settingsIcon.MouseEnter += new System.EventHandler(this.SettingsIcon_MouseEnter);
            this.settingsIcon.MouseLeave += new System.EventHandler(this.SettingsIcon_MouseLeave);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // diskCleanupToolStripMenuItem
            // 
            this.diskCleanupToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.diskCleanupToolStripMenuItem.Name = "diskCleanupToolStripMenuItem";
            this.diskCleanupToolStripMenuItem.Size = new System.Drawing.Size(164, 28);
            this.diskCleanupToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Diskcleanup;
            this.diskCleanupToolStripMenuItem.Click += new System.EventHandler(this.DiskCleanupToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(336, 115);
            this.ContextMenuStrip = this.contextMenuStrip;
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MainForm_VisibleChanged);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblFreeSpace;
        private System.Windows.Forms.PictureBox settingsIcon;
        private System.Windows.Forms.PictureBox titleIcon;
        private System.Windows.Forms.Panel minimizePanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel minimizeContainerPanel;
        private System.Windows.Forms.NotifyIcon diskSpaceNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Timer checkTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem diskCleanupToolStripMenuItem;
    }
}

