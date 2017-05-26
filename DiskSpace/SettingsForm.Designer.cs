using System;
using System.Globalization;

namespace DiskSpace
{
    partial class SettingsForm
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.cmbDrives = new System.Windows.Forms.ComboBox();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.lblGB = new System.Windows.Forms.Label();
            this.txtNotificationLimitGB = new System.Windows.Forms.TextBox();
            this.chkNotificationLimit = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.chkAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.chkDisplayNotifications = new System.Windows.Forms.CheckBox();
            this.chkStartMinimized = new System.Windows.Forms.CheckBox();
            this.lblDrive = new System.Windows.Forms.Label();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.minimizePanelFrame = new System.Windows.Forms.Panel();
            this.minimizePanel = new System.Windows.Forms.Panel();
            this.settingsPanel.SuspendLayout();
            this.minimizePanelFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDrives
            // 
            this.cmbDrives.BackColor = System.Drawing.Color.Black;
            this.cmbDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrives.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDrives.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDrives.ForeColor = System.Drawing.Color.White;
            this.cmbDrives.FormattingEnabled = true;
            this.cmbDrives.ItemHeight = 28;
            this.cmbDrives.Location = new System.Drawing.Point(229, 259);
            this.cmbDrives.Name = "cmbDrives";
            this.cmbDrives.Size = new System.Drawing.Size(148, 36);
            this.cmbDrives.TabIndex = 6;
            // 
            // settingsPanel
            // 
            this.settingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsPanel.BackColor = System.Drawing.Color.Black;
            this.settingsPanel.Controls.Add(this.lblGB);
            this.settingsPanel.Controls.Add(this.txtNotificationLimitGB);
            this.settingsPanel.Controls.Add(this.chkNotificationLimit);
            this.settingsPanel.Controls.Add(this.btnSave);
            this.settingsPanel.Controls.Add(this.chkStartWithWindows);
            this.settingsPanel.Controls.Add(this.chkAlwaysOnTop);
            this.settingsPanel.Controls.Add(this.chkDisplayNotifications);
            this.settingsPanel.Controls.Add(this.chkStartMinimized);
            this.settingsPanel.Controls.Add(this.lblDrive);
            this.settingsPanel.Controls.Add(this.cmbDrives);
            this.settingsPanel.Location = new System.Drawing.Point(1, 35);
            this.settingsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.settingsPanel.MaximumSize = new System.Drawing.Size(614, 311);
            this.settingsPanel.MinimumSize = new System.Drawing.Size(614, 311);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(614, 311);
            this.settingsPanel.TabIndex = 2;
            // 
            // lblGB
            // 
            this.lblGB.AutoSize = true;
            this.lblGB.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGB.Location = new System.Drawing.Point(323, 162);
            this.lblGB.Name = "lblGB";
            this.lblGB.Size = new System.Drawing.Size(39, 28);
            this.lblGB.TabIndex = 8;
            this.lblGB.Text = "GB";
            // 
            // txtNotificationLimitGB
            // 
            this.txtNotificationLimitGB.BackColor = System.Drawing.Color.Black;
            this.txtNotificationLimitGB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotificationLimitGB.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DiskSpace.Properties.Settings.Default, "NotificatonAmountLimit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNotificationLimitGB.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtNotificationLimitGB.ForeColor = System.Drawing.Color.White;
            this.txtNotificationLimitGB.Location = new System.Drawing.Point(229, 160);
            this.txtNotificationLimitGB.Margin = new System.Windows.Forms.Padding(0);
            this.txtNotificationLimitGB.Name = "txtNotificationLimitGB";
            this.txtNotificationLimitGB.Size = new System.Drawing.Size(91, 34);
            this.txtNotificationLimitGB.TabIndex = 5;
            this.txtNotificationLimitGB.Text = "700";
            this.txtNotificationLimitGB.TextChanged += new System.EventHandler(this.NotificationLimitGB_TextChanged);
            // 
            // chkNotificationLimit
            // 
            this.chkNotificationLimit.AutoSize = true;
            this.chkNotificationLimit.Checked = global::DiskSpace.Properties.Settings.Default.NotificationLimitActive;
            this.chkNotificationLimit.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "NotificationLimitActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkNotificationLimit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkNotificationLimit.Location = new System.Drawing.Point(13, 161);
            this.chkNotificationLimit.Name = "chkNotificationLimit";
            this.chkNotificationLimit.Size = new System.Drawing.Size(203, 32);
            this.chkNotificationLimit.TabIndex = 4;
            this.chkNotificationLimit.Text = global::DiskSpace.Properties.Resources.NotificationLimit;
            this.chkNotificationLimit.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(407, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(196, 43);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = global::DiskSpace.Properties.Resources.SaveButtonTitle;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // chkStartWithWindows
            // 
            this.chkStartWithWindows.AutoSize = true;
            this.chkStartWithWindows.Checked = global::DiskSpace.Properties.Settings.Default.startWithWindows;
            this.chkStartWithWindows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartWithWindows.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "startWithWindows", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkStartWithWindows.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStartWithWindows.Location = new System.Drawing.Point(13, 24);
            this.chkStartWithWindows.Name = "chkStartWithWindows";
            this.chkStartWithWindows.Size = new System.Drawing.Size(135, 32);
            this.chkStartWithWindows.TabIndex = 0;
            this.chkStartWithWindows.Text = global::DiskSpace.Properties.Resources.StartWithWindowsText;
            this.chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.Checked = global::DiskSpace.Properties.Settings.Default.alwaysOnTop;
            this.chkAlwaysOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlwaysOnTop.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "alwaysOnTop", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAlwaysOnTop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(229, 89);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(224, 32);
            this.chkAlwaysOnTop.TabIndex = 3;
            this.chkAlwaysOnTop.Text = global::DiskSpace.Properties.Resources.AlwaysOnTop;
            this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // chkDisplayNotifications
            // 
            this.chkDisplayNotifications.AutoSize = true;
            this.chkDisplayNotifications.Checked = global::DiskSpace.Properties.Settings.Default.notifyWhenSpaceChange;
            this.chkDisplayNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayNotifications.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "notifyWhenSpaceChange", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkDisplayNotifications.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisplayNotifications.Location = new System.Drawing.Point(229, 24);
            this.chkDisplayNotifications.Name = "chkDisplayNotifications";
            this.chkDisplayNotifications.Size = new System.Drawing.Size(263, 32);
            this.chkDisplayNotifications.TabIndex = 1;
            this.chkDisplayNotifications.Text = global::DiskSpace.Properties.Resources.ShowNotifications;
            this.chkDisplayNotifications.UseVisualStyleBackColor = true;
            // 
            // chkStartMinimized
            // 
            this.chkStartMinimized.AutoSize = true;
            this.chkStartMinimized.Checked = global::DiskSpace.Properties.Settings.Default.startMinimized;
            this.chkStartMinimized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartMinimized.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "startMinimized", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkStartMinimized.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStartMinimized.Location = new System.Drawing.Point(13, 89);
            this.chkStartMinimized.Name = "chkStartMinimized";
            this.chkStartMinimized.Size = new System.Drawing.Size(190, 32);
            this.chkStartMinimized.TabIndex = 2;
            this.chkStartMinimized.Text = global::DiskSpace.Properties.Resources.StartMinimized;
            this.chkStartMinimized.UseVisualStyleBackColor = true;
            // 
            // lblDrive
            // 
            this.lblDrive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrive.Location = new System.Drawing.Point(32, 262);
            this.lblDrive.Name = "lblDrive";
            this.lblDrive.Size = new System.Drawing.Size(181, 28);
            this.lblDrive.TabIndex = 5;
            this.lblDrive.Text = "Drive to monitor";
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(616, 32);
            this.lblSettingsTitle.TabIndex = 1;
            this.lblSettingsTitle.Text = "Settings";
            this.lblSettingsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingsTitle_MouseDown);
            this.lblSettingsTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SettingsTitle_MouseMove);
            // 
            // minimizePanelFrame
            // 
            this.minimizePanelFrame.Controls.Add(this.minimizePanel);
            this.minimizePanelFrame.Location = new System.Drawing.Point(549, 0);
            this.minimizePanelFrame.Name = "minimizePanelFrame";
            this.minimizePanelFrame.Size = new System.Drawing.Size(65, 32);
            this.minimizePanelFrame.TabIndex = 3;
            this.minimizePanelFrame.Click += new System.EventHandler(this.MinimizePanelFrame_Click);
            this.minimizePanelFrame.MouseEnter += new System.EventHandler(this.MinimizePanel_MouseEnter);
            this.minimizePanelFrame.MouseLeave += new System.EventHandler(this.MinimizePanel_MouseLeave);
            // 
            // minimizePanel
            // 
            this.minimizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizePanel.BackColor = System.Drawing.Color.White;
            this.minimizePanel.Location = new System.Drawing.Point(34, 9);
            this.minimizePanel.Margin = new System.Windows.Forms.Padding(0);
            this.minimizePanel.Name = "minimizePanel";
            this.minimizePanel.Size = new System.Drawing.Size(28, 10);
            this.minimizePanel.TabIndex = 3;
            this.minimizePanel.Click += new System.EventHandler(this.MinimizePanel_Click);
            this.minimizePanel.MouseEnter += new System.EventHandler(this.MinimizePanel_MouseEnter);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(616, 348);
            this.Controls.Add(this.minimizePanelFrame);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.lblSettingsTitle);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.minimizePanelFrame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox chkStartWithWindows;
        private System.Windows.Forms.CheckBox chkStartMinimized;
        private System.Windows.Forms.CheckBox chkDisplayNotifications;
        private System.Windows.Forms.CheckBox chkAlwaysOnTop;
        private System.Windows.Forms.ComboBox cmbDrives;
        private System.Windows.Forms.Label lblDrive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.CheckBox chkNotificationLimit;
        private System.Windows.Forms.TextBox txtNotificationLimitGB;
        private System.Windows.Forms.Label lblGB;
        private System.Windows.Forms.Panel minimizePanelFrame;
        private System.Windows.Forms.Panel minimizePanel;
    }
}