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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.cmbDrives = new System.Windows.Forms.ComboBox();
            this.localDrivesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.localDrivesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.chkAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.chkDisplayNotifications = new System.Windows.Forms.CheckBox();
            this.chkStartMinimized = new System.Windows.Forms.CheckBox();
            this.lblDrive = new System.Windows.Forms.Label();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.localDrivesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localDrivesBindingSource1)).BeginInit();
            this.settingsPanel.SuspendLayout();
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
            this.cmbDrives.Location = new System.Drawing.Point(219, 151);
            this.cmbDrives.Name = "cmbDrives";
            this.cmbDrives.Size = new System.Drawing.Size(263, 36);
            this.cmbDrives.TabIndex = 4;
            // 
            // settingsPanel
            // 
            this.settingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsPanel.BackColor = System.Drawing.Color.Black;
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
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(407, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(196, 43);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = global::DiskSpace.Properties.Settings.Default.SaveButtonTitle;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.chkStartWithWindows.Size = new System.Drawing.Size(116, 32);
            this.chkStartWithWindows.TabIndex = 0;
            this.chkStartWithWindows.Text = global::DiskSpace.Properties.Settings.Default.StartWithWindowsText;
            this.chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.Checked = global::DiskSpace.Properties.Settings.Default.alwaysOnTop;
            this.chkAlwaysOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlwaysOnTop.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "alwaysOnTop", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAlwaysOnTop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(219, 89);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(224, 32);
            this.chkAlwaysOnTop.TabIndex = 3;
            this.chkAlwaysOnTop.Text = global::DiskSpace.Properties.Settings.Default.AlwaysOnTopText;
            this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // chkDisplayNotifications
            // 
            this.chkDisplayNotifications.AutoSize = true;
            this.chkDisplayNotifications.Checked = global::DiskSpace.Properties.Settings.Default.notifyWhenSpaceChange;
            this.chkDisplayNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayNotifications.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "notifyWhenSpaceChange", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkDisplayNotifications.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisplayNotifications.Location = new System.Drawing.Point(219, 24);
            this.chkDisplayNotifications.Name = "chkDisplayNotifications";
            this.chkDisplayNotifications.Size = new System.Drawing.Size(263, 32);
            this.chkDisplayNotifications.TabIndex = 1;
            this.chkDisplayNotifications.Text = global::DiskSpace.Properties.Settings.Default.ShowNotificationsText;
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
            this.chkStartMinimized.Text = global::DiskSpace.Properties.Settings.Default.StartMinimizedText;
            this.chkStartMinimized.UseVisualStyleBackColor = true;
            // 
            // lblDrive
            // 
            this.lblDrive.AutoSize = true;
            this.lblDrive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrive.Location = new System.Drawing.Point(32, 154);
            this.lblDrive.Name = "lblDrive";
            this.lblDrive.Size = new System.Drawing.Size(171, 28);
            this.lblDrive.TabIndex = 5;
            this.lblDrive.Text = global::DiskSpace.Properties.Settings.Default.DriveToMonitorText;
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(616, 32);
            this.lblSettingsTitle.TabIndex = 1;
            this.lblSettingsTitle.Text = global::DiskSpace.Properties.Settings.Default.SettingsFormTitle;
            this.lblSettingsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblSettingsTitle_MouseDown);
            this.lblSettingsTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblSettingsTitle_MouseMove);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(616, 348);
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
            ((System.ComponentModel.ISupportInitialize)(this.localDrivesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localDrivesBindingSource1)).EndInit();
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox chkStartWithWindows;
        private System.Windows.Forms.CheckBox chkStartMinimized;
        private System.Windows.Forms.CheckBox chkDisplayNotifications;
        private System.Windows.Forms.CheckBox chkAlwaysOnTop;
        private System.Windows.Forms.ComboBox cmbDrives;
        private System.Windows.Forms.BindingSource localDrivesBindingSource;
        private System.Windows.Forms.BindingSource localDrivesBindingSource1;
        private System.Windows.Forms.Label lblDrive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Panel settingsPanel;
    }
}