using System.ComponentModel;
using System.Windows.Forms;
using DiskSpace.Forms.Controls;

namespace DiskSpace.Forms
{
    partial class SettingsForm
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
            this.cmbDrives = new DiskSpace.Forms.Controls.CustomComboBox();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.btnConfigureEmail = new System.Windows.Forms.Button();
            this.chkSendEmail = new System.Windows.Forms.CheckBox();
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
            this.titleIcon = new System.Windows.Forms.PictureBox();
            this.settingsPanel.SuspendLayout();
            this.minimizePanelFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDrives
            // 
            this.cmbDrives.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cmbDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrives.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDrives.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDrives.ForeColor = System.Drawing.Color.White;
            this.cmbDrives.FormattingEnabled = true;
            this.cmbDrives.HighlightColor = System.Drawing.Color.DeepSkyBlue;
            this.cmbDrives.ItemHeight = 23;
            this.cmbDrives.Location = new System.Drawing.Point(204, 108);
            this.cmbDrives.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbDrives.Name = "cmbDrives";
            this.cmbDrives.Size = new System.Drawing.Size(332, 29);
            this.cmbDrives.TabIndex = 6;
            // 
            // settingsPanel
            // 
            this.settingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.settingsPanel.Controls.Add(this.btnConfigureEmail);
            this.settingsPanel.Controls.Add(this.chkSendEmail);
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
            this.settingsPanel.Location = new System.Drawing.Point(0, 32);
            this.settingsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(548, 237);
            this.settingsPanel.TabIndex = 2;
            // 
            // btnConfigureEmail
            // 
            this.btnConfigureEmail.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnConfigureEmail.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnConfigureEmail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnConfigureEmail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnConfigureEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigureEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfigureEmail.Location = new System.Drawing.Point(204, 184);
            this.btnConfigureEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfigureEmail.Name = "btnConfigureEmail";
            this.btnConfigureEmail.Size = new System.Drawing.Size(194, 42);
            this.btnConfigureEmail.TabIndex = 10;
            this.btnConfigureEmail.Text = "Email settings";
            this.btnConfigureEmail.UseVisualStyleBackColor = false;
            this.btnConfigureEmail.Click += new System.EventHandler(this.ConfigureEmail_Click);
            // 
            // chkSendEmail
            // 
            this.chkSendEmail.AutoSize = true;
            this.chkSendEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.chkSendEmail.Checked = global::DiskSpace.Properties.Settings.Default.sendEmail;
            this.chkSendEmail.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "sendEmail", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkSendEmail.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.chkSendEmail.FlatAppearance.BorderSize = 2;
            this.chkSendEmail.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkSendEmail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkSendEmail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.chkSendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSendEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSendEmail.Location = new System.Drawing.Point(204, 76);
            this.chkSendEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSendEmail.Name = "chkSendEmail";
            this.chkSendEmail.Size = new System.Drawing.Size(168, 27);
            this.chkSendEmail.TabIndex = 9;
            this.chkSendEmail.Text = "Email notifications";
            this.chkSendEmail.UseVisualStyleBackColor = false;
            // 
            // lblGB
            // 
            this.lblGB.AutoSize = true;
            this.lblGB.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGB.Location = new System.Drawing.Point(445, 45);
            this.lblGB.Name = "lblGB";
            this.lblGB.Size = new System.Drawing.Size(32, 23);
            this.lblGB.TabIndex = 8;
            this.lblGB.Text = "GB";
            // 
            // txtNotificationLimitGB
            // 
            this.txtNotificationLimitGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.txtNotificationLimitGB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotificationLimitGB.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DiskSpace.Properties.Settings.Default, "NotificatonAmountLimit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNotificationLimitGB.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotificationLimitGB.ForeColor = System.Drawing.Color.White;
            this.txtNotificationLimitGB.Location = new System.Drawing.Point(395, 43);
            this.txtNotificationLimitGB.Margin = new System.Windows.Forms.Padding(0);
            this.txtNotificationLimitGB.Name = "txtNotificationLimitGB";
            this.txtNotificationLimitGB.Size = new System.Drawing.Size(47, 30);
            this.txtNotificationLimitGB.TabIndex = 5;
            this.txtNotificationLimitGB.Text = "700";
            this.txtNotificationLimitGB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNotificationLimitGB.TextChanged += new System.EventHandler(this.NotificationLimitGB_TextChanged);
            this.txtNotificationLimitGB.Enter += new System.EventHandler(this.NotificationLimitGB_Enter);
            this.txtNotificationLimitGB.Leave += new System.EventHandler(this.NotificationLimitGB_Leave);
            this.txtNotificationLimitGB.MouseEnter += new System.EventHandler(this.NotificationLimitGB_MouseEnter);
            this.txtNotificationLimitGB.MouseLeave += new System.EventHandler(this.NotificationLimitGB_MouseLeave);
            // 
            // chkNotificationLimit
            // 
            this.chkNotificationLimit.AutoSize = true;
            this.chkNotificationLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.chkNotificationLimit.Checked = global::DiskSpace.Properties.Settings.Default.NotificationLimitActive;
            this.chkNotificationLimit.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "NotificationLimitActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkNotificationLimit.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.chkNotificationLimit.FlatAppearance.BorderSize = 2;
            this.chkNotificationLimit.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkNotificationLimit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkNotificationLimit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.chkNotificationLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkNotificationLimit.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNotificationLimit.Location = new System.Drawing.Point(204, 43);
            this.chkNotificationLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkNotificationLimit.Name = "chkNotificationLimit";
            this.chkNotificationLimit.Size = new System.Drawing.Size(156, 27);
            this.chkNotificationLimit.TabIndex = 4;
            this.chkNotificationLimit.Text = global::DiskSpace.Properties.Resources.NotificationLimit;
            this.chkNotificationLimit.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(404, 184);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 42);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = global::DiskSpace.Properties.Resources.SaveButtonTitle;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // chkStartWithWindows
            // 
            this.chkStartWithWindows.AutoSize = true;
            this.chkStartWithWindows.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.chkStartWithWindows.Checked = global::DiskSpace.Properties.Settings.Default.startWithWindows;
            this.chkStartWithWindows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartWithWindows.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "startWithWindows", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkStartWithWindows.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.chkStartWithWindows.FlatAppearance.BorderSize = 2;
            this.chkStartWithWindows.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkStartWithWindows.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkStartWithWindows.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.chkStartWithWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkStartWithWindows.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStartWithWindows.Location = new System.Drawing.Point(13, 10);
            this.chkStartWithWindows.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkStartWithWindows.Name = "chkStartWithWindows";
            this.chkStartWithWindows.Size = new System.Drawing.Size(104, 27);
            this.chkStartWithWindows.TabIndex = 0;
            this.chkStartWithWindows.Text = global::DiskSpace.Properties.Resources.StartWithWindowsText;
            this.chkStartWithWindows.UseVisualStyleBackColor = false;
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.chkAlwaysOnTop.Checked = global::DiskSpace.Properties.Settings.Default.alwaysOnTop;
            this.chkAlwaysOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlwaysOnTop.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "alwaysOnTop", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAlwaysOnTop.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.chkAlwaysOnTop.FlatAppearance.BorderSize = 2;
            this.chkAlwaysOnTop.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkAlwaysOnTop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkAlwaysOnTop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.chkAlwaysOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAlwaysOnTop.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(13, 76);
            this.chkAlwaysOnTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(137, 27);
            this.chkAlwaysOnTop.TabIndex = 3;
            this.chkAlwaysOnTop.Text = "Always on top";
            this.chkAlwaysOnTop.UseVisualStyleBackColor = false;
            // 
            // chkDisplayNotifications
            // 
            this.chkDisplayNotifications.AutoSize = true;
            this.chkDisplayNotifications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.chkDisplayNotifications.Checked = global::DiskSpace.Properties.Settings.Default.notifyWhenSpaceChange;
            this.chkDisplayNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayNotifications.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "notifyWhenSpaceChange", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkDisplayNotifications.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.chkDisplayNotifications.FlatAppearance.BorderSize = 2;
            this.chkDisplayNotifications.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkDisplayNotifications.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkDisplayNotifications.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.chkDisplayNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDisplayNotifications.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisplayNotifications.Location = new System.Drawing.Point(204, 10);
            this.chkDisplayNotifications.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkDisplayNotifications.Name = "chkDisplayNotifications";
            this.chkDisplayNotifications.Size = new System.Drawing.Size(169, 27);
            this.chkDisplayNotifications.TabIndex = 1;
            this.chkDisplayNotifications.Text = "Show notifications";
            this.chkDisplayNotifications.UseVisualStyleBackColor = false;
            // 
            // chkStartMinimized
            // 
            this.chkStartMinimized.AutoSize = true;
            this.chkStartMinimized.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.chkStartMinimized.Checked = global::DiskSpace.Properties.Settings.Default.startMinimized;
            this.chkStartMinimized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartMinimized.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "startMinimized", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkStartMinimized.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.chkStartMinimized.FlatAppearance.BorderSize = 2;
            this.chkStartMinimized.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkStartMinimized.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkStartMinimized.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.chkStartMinimized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkStartMinimized.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStartMinimized.Location = new System.Drawing.Point(13, 43);
            this.chkStartMinimized.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkStartMinimized.Name = "chkStartMinimized";
            this.chkStartMinimized.Size = new System.Drawing.Size(145, 27);
            this.chkStartMinimized.TabIndex = 2;
            this.chkStartMinimized.Text = "Start in Taskbar";
            this.chkStartMinimized.UseVisualStyleBackColor = false;
            // 
            // lblDrive
            // 
            this.lblDrive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblDrive.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrive.Location = new System.Drawing.Point(30, 109);
            this.lblDrive.Name = "lblDrive";
            this.lblDrive.Size = new System.Drawing.Size(151, 27);
            this.lblDrive.TabIndex = 5;
            this.lblDrive.Text = "Monitor drive";
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSettingsTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.lblSettingsTitle.Size = new System.Drawing.Size(548, 32);
            this.lblSettingsTitle.TabIndex = 1;
            this.lblSettingsTitle.Text = "Settings";
            this.lblSettingsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingsTitle_MouseDown);
            this.lblSettingsTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SettingsTitle_MouseMove);
            // 
            // minimizePanelFrame
            // 
            this.minimizePanelFrame.Controls.Add(this.minimizePanel);
            this.minimizePanelFrame.Location = new System.Drawing.Point(510, 0);
            this.minimizePanelFrame.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.minimizePanelFrame.Name = "minimizePanelFrame";
            this.minimizePanelFrame.Size = new System.Drawing.Size(36, 32);
            this.minimizePanelFrame.TabIndex = 3;
            this.minimizePanelFrame.Click += new System.EventHandler(this.MinimizePanelFrame_Click);
            this.minimizePanelFrame.MouseEnter += new System.EventHandler(this.MinimizePanel_MouseEnter);
            this.minimizePanelFrame.MouseLeave += new System.EventHandler(this.MinimizePanel_MouseLeave);
            // 
            // minimizePanel
            // 
            this.minimizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizePanel.BackColor = System.Drawing.Color.White;
            this.minimizePanel.Location = new System.Drawing.Point(7, 7);
            this.minimizePanel.Margin = new System.Windows.Forms.Padding(0);
            this.minimizePanel.Name = "minimizePanel";
            this.minimizePanel.Size = new System.Drawing.Size(25, 8);
            this.minimizePanel.TabIndex = 3;
            this.minimizePanel.Click += new System.EventHandler(this.MinimizePanel_Click);
            this.minimizePanel.MouseEnter += new System.EventHandler(this.MinimizePanel_MouseEnter);
            // 
            // titleIcon
            // 
            this.titleIcon.BackColor = System.Drawing.Color.Transparent;
            this.titleIcon.Image = global::DiskSpace.Properties.Resources.simple_gears;
            this.titleIcon.InitialImage = global::DiskSpace.Properties.Resources.simple_gears;
            this.titleIcon.Location = new System.Drawing.Point(4, 0);
            this.titleIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.titleIcon.Name = "titleIcon";
            this.titleIcon.Size = new System.Drawing.Size(32, 32);
            this.titleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.titleIcon.TabIndex = 4;
            this.titleIcon.TabStop = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(548, 269);
            this.ControlBox = false;
            this.Controls.Add(this.titleIcon);
            this.Controls.Add(this.minimizePanelFrame);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.lblSettingsTitle);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.minimizePanelFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private CheckBox chkStartWithWindows;
        private CheckBox chkStartMinimized;
        private CheckBox chkDisplayNotifications;
        private CheckBox chkAlwaysOnTop;
        private CustomComboBox cmbDrives;
        private Label lblDrive;
        private Button btnSave;
        private Label lblSettingsTitle;
        private Panel settingsPanel;
        private CheckBox chkNotificationLimit;
        private TextBox txtNotificationLimitGB;
        private Label lblGB;
        private Panel minimizePanelFrame;
        private Panel minimizePanel;
        private PictureBox titleIcon;
        private CheckBox chkSendEmail;
        private Button btnConfigureEmail;
    }
}