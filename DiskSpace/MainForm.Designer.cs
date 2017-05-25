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
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTimer = new System.Windows.Forms.Timer(this.components);
            this.titleIcon = new System.Windows.Forms.PictureBox();
            this.settingsIcon = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.BackColor = System.Drawing.Color.Black;
            this.lblFreeSpace.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreeSpace.ForeColor = System.Drawing.Color.White;
            this.lblFreeSpace.Location = new System.Drawing.Point(1, 34);
            this.lblFreeSpace.Margin = new System.Windows.Forms.Padding(0);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.Size = new System.Drawing.Size(374, 108);
            this.lblFreeSpace.TabIndex = 1;
            this.lblFreeSpace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minimizePanel
            // 
            this.minimizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizePanel.BackColor = System.Drawing.Color.White;
            this.minimizePanel.Location = new System.Drawing.Point(344, 9);
            this.minimizePanel.Margin = new System.Windows.Forms.Padding(0);
            this.minimizePanel.Name = "minimizePanel";
            this.minimizePanel.Size = new System.Drawing.Size(28, 10);
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
            this.lblTitle.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(374, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            // 
            // minimizeContainerPanel
            // 
            this.minimizeContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeContainerPanel.Location = new System.Drawing.Point(342, 0);
            this.minimizeContainerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeContainerPanel.Name = "minimizeContainerPanel";
            this.minimizeContainerPanel.Size = new System.Drawing.Size(32, 32);
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
            this.contextMenuStrip.BackColor = System.Drawing.Color.Black;
            this.contextMenuStrip.DropShadowEnabled = false;
            this.contextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.contextMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(137, 106);
            this.contextMenuStrip.MouseLeave += new System.EventHandler(this.ContextMenuStrip_MouseLeave);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(136, 32);
            this.showToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Show;
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(136, 32);
            this.settingsToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Settings;
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(136, 32);
            this.quitToolStripMenuItem.Text = global::DiskSpace.Properties.Resources.Quit;
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // checkTimer
            // 
            this.checkTimer.Interval = 1000;
            this.checkTimer.Tick += new System.EventHandler(this.CheckTimer_Tick);
            // 
            // titleIcon
            // 
            this.titleIcon.BackColor = System.Drawing.Color.Transparent;
            this.titleIcon.Image = global::DiskSpace.Properties.Resources.ssd;
            this.titleIcon.Location = new System.Drawing.Point(5, 1);
            this.titleIcon.Name = "titleIcon";
            this.titleIcon.Size = new System.Drawing.Size(32, 32);
            this.titleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.titleIcon.TabIndex = 1;
            this.titleIcon.TabStop = false;
            this.titleIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleIcon_MouseDown);
            this.titleIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleIcon_MouseMove);
            // 
            // settingsIcon
            // 
            this.settingsIcon.BackColor = System.Drawing.Color.Black;
            this.settingsIcon.Image = global::DiskSpace.Properties.Resources.simple_gears;
            this.settingsIcon.Location = new System.Drawing.Point(320, 86);
            this.settingsIcon.Margin = new System.Windows.Forms.Padding(0);
            this.settingsIcon.Name = "settingsIcon";
            this.settingsIcon.Size = new System.Drawing.Size(52, 52);
            this.settingsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.settingsIcon.TabIndex = 2;
            this.settingsIcon.TabStop = false;
            this.settingsIcon.Click += new System.EventHandler(this.SettingsIcon_Click);
            this.settingsIcon.MouseEnter += new System.EventHandler(this.SettingsIcon_MouseEnter);
            this.settingsIcon.MouseLeave += new System.EventHandler(this.SettingsIcon_MouseLeave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(378, 144);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.titleIcon);
            this.Controls.Add(this.minimizePanel);
            this.Controls.Add(this.settingsIcon);
            this.Controls.Add(this.lblFreeSpace);
            this.Controls.Add(this.minimizeContainerPanel);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
    }
}

