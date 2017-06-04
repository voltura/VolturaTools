using System.ComponentModel;
using System.Windows.Forms;

namespace DiskSpace
{
    partial class EmailSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailSettingsForm));
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.chkUseSsl = new System.Windows.Forms.CheckBox();
            this.txtEmailPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtEmailUserName = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.lblSmtpServer = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtToEmail = new System.Windows.Forms.TextBox();
            this.lblFromEmail = new System.Windows.Forms.Label();
            this.txtFromEmail = new System.Windows.Forms.TextBox();
            this.btnSendTestEmail = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.minimizePanelFrame = new System.Windows.Forms.Panel();
            this.minimizePanel = new System.Windows.Forms.Panel();
            this.titleIcon = new System.Windows.Forms.PictureBox();
            this.settingsPanel.SuspendLayout();
            this.minimizePanelFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // settingsPanel
            // 
            this.settingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.settingsPanel.Controls.Add(this.chkUseSsl);
            this.settingsPanel.Controls.Add(this.txtEmailPassword);
            this.settingsPanel.Controls.Add(this.lblPassword);
            this.settingsPanel.Controls.Add(this.lblUser);
            this.settingsPanel.Controls.Add(this.txtEmailUserName);
            this.settingsPanel.Controls.Add(this.lblPort);
            this.settingsPanel.Controls.Add(this.txtSmtpPort);
            this.settingsPanel.Controls.Add(this.lblSmtpServer);
            this.settingsPanel.Controls.Add(this.txtSmtpServer);
            this.settingsPanel.Controls.Add(this.lblTo);
            this.settingsPanel.Controls.Add(this.txtToEmail);
            this.settingsPanel.Controls.Add(this.lblFromEmail);
            this.settingsPanel.Controls.Add(this.txtFromEmail);
            this.settingsPanel.Controls.Add(this.btnSendTestEmail);
            this.settingsPanel.Controls.Add(this.btnSave);
            this.settingsPanel.Location = new System.Drawing.Point(0, 32);
            this.settingsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(548, 237);
            this.settingsPanel.TabIndex = 2;
            // 
            // chkUseSsl
            // 
            this.chkUseSsl.AutoSize = true;
            this.chkUseSsl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.chkUseSsl.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseSsl.Checked = global::DiskSpace.Properties.Settings.Default.useSSL;
            this.chkUseSsl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSsl.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DiskSpace.Properties.Settings.Default, "useSSL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkUseSsl.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.chkUseSsl.FlatAppearance.BorderSize = 2;
            this.chkUseSsl.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkUseSsl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.chkUseSsl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.chkUseSsl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkUseSsl.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseSsl.Location = new System.Drawing.Point(12, 193);
            this.chkUseSsl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkUseSsl.Name = "chkUseSsl";
            this.chkUseSsl.Size = new System.Drawing.Size(87, 27);
            this.chkUseSsl.TabIndex = 21;
            this.chkUseSsl.Text = "Use SSL";
            this.chkUseSsl.UseVisualStyleBackColor = false;
            // 
            // txtEmailPassword
            // 
            this.txtEmailPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.txtEmailPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DiskSpace.Properties.Settings.Default, "emailPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtEmailPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.txtEmailPassword.ForeColor = System.Drawing.Color.White;
            this.txtEmailPassword.Location = new System.Drawing.Point(435, 151);
            this.txtEmailPassword.Name = "txtEmailPassword";
            this.txtEmailPassword.PasswordChar = '•';
            this.txtEmailPassword.Size = new System.Drawing.Size(101, 30);
            this.txtEmailPassword.TabIndex = 20;
            this.txtEmailPassword.Text = global::DiskSpace.Properties.Settings.Default.emailPassword;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblPassword.Location = new System.Drawing.Point(343, 153);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(82, 23);
            this.lblPassword.TabIndex = 19;
            this.lblPassword.Text = "Password";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblUser.Location = new System.Drawing.Point(12, 153);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(44, 23);
            this.lblUser.TabIndex = 18;
            this.lblUser.Text = "User";
            // 
            // txtEmailUserName
            // 
            this.txtEmailUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.txtEmailUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailUserName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DiskSpace.Properties.Settings.Default, "emailUserName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtEmailUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.txtEmailUserName.ForeColor = System.Drawing.Color.White;
            this.txtEmailUserName.Location = new System.Drawing.Point(73, 151);
            this.txtEmailUserName.Name = "txtEmailUserName";
            this.txtEmailUserName.Size = new System.Drawing.Size(264, 30);
            this.txtEmailUserName.TabIndex = 17;
            this.txtEmailUserName.Text = global::DiskSpace.Properties.Settings.Default.emailUserName;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblPort.Location = new System.Drawing.Point(431, 107);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(41, 23);
            this.lblPort.TabIndex = 16;
            this.lblPort.Text = "Port";
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.txtSmtpPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSmtpPort.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.txtSmtpPort.ForeColor = System.Drawing.Color.White;
            this.txtSmtpPort.Location = new System.Drawing.Point(478, 105);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(58, 30);
            this.txtSmtpPort.TabIndex = 15;
            this.txtSmtpPort.Text = "587";
            // 
            // lblSmtpServer
            // 
            this.lblSmtpServer.AutoSize = true;
            this.lblSmtpServer.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblSmtpServer.Location = new System.Drawing.Point(12, 107);
            this.lblSmtpServer.Name = "lblSmtpServer";
            this.lblSmtpServer.Size = new System.Drawing.Size(59, 23);
            this.lblSmtpServer.TabIndex = 14;
            this.lblSmtpServer.Text = "Server";
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.txtSmtpServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSmtpServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DiskSpace.Properties.Settings.Default, "smtpServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSmtpServer.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.txtSmtpServer.ForeColor = System.Drawing.Color.White;
            this.txtSmtpServer.Location = new System.Drawing.Point(73, 105);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(352, 30);
            this.txtSmtpServer.TabIndex = 13;
            this.txtSmtpServer.Text = global::DiskSpace.Properties.Settings.Default.smtpServer;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblTo.Location = new System.Drawing.Point(12, 58);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 23);
            this.lblTo.TabIndex = 12;
            this.lblTo.Text = "To";
            // 
            // txtToEmail
            // 
            this.txtToEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.txtToEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DiskSpace.Properties.Settings.Default, "ToEmailAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtToEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.txtToEmail.ForeColor = System.Drawing.Color.White;
            this.txtToEmail.Location = new System.Drawing.Point(73, 56);
            this.txtToEmail.Name = "txtToEmail";
            this.txtToEmail.Size = new System.Drawing.Size(463, 30);
            this.txtToEmail.TabIndex = 11;
            this.txtToEmail.Text = global::DiskSpace.Properties.Settings.Default.ToEmailAddress;
            // 
            // lblFromEmail
            // 
            this.lblFromEmail.AutoSize = true;
            this.lblFromEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblFromEmail.Location = new System.Drawing.Point(12, 16);
            this.lblFromEmail.Name = "lblFromEmail";
            this.lblFromEmail.Size = new System.Drawing.Size(50, 23);
            this.lblFromEmail.TabIndex = 10;
            this.lblFromEmail.Text = "From";
            // 
            // txtFromEmail
            // 
            this.txtFromEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.txtFromEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DiskSpace.Properties.Settings.Default, "FromEmailAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFromEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.txtFromEmail.ForeColor = System.Drawing.Color.White;
            this.txtFromEmail.Location = new System.Drawing.Point(73, 14);
            this.txtFromEmail.Name = "txtFromEmail";
            this.txtFromEmail.Size = new System.Drawing.Size(463, 30);
            this.txtFromEmail.TabIndex = 9;
            this.txtFromEmail.Text = global::DiskSpace.Properties.Settings.Default.FromEmailAddress;
            // 
            // btnSendTestEmail
            // 
            this.btnSendTestEmail.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSendTestEmail.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSendTestEmail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSendTestEmail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnSendTestEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendTestEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendTestEmail.Location = new System.Drawing.Point(219, 186);
            this.btnSendTestEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSendTestEmail.Name = "btnSendTestEmail";
            this.btnSendTestEmail.Size = new System.Drawing.Size(179, 40);
            this.btnSendTestEmail.TabIndex = 8;
            this.btnSendTestEmail.Text = "Send test email";
            this.btnSendTestEmail.UseVisualStyleBackColor = false;
            this.btnSendTestEmail.Click += new System.EventHandler(this.SendTestEmail_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(404, 186);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 40);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = global::DiskSpace.Properties.Resources.SaveButtonTitle;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
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
            this.lblSettingsTitle.Text = "Email settings - SMTP";
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
            // EmailSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(548, 269);
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
            this.Name = "EmailSettingsForm";
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
        private Button btnSave;
        private Label lblSettingsTitle;
        private Panel settingsPanel;
        private Panel minimizePanelFrame;
        private Panel minimizePanel;
        private PictureBox titleIcon;
        private Button btnSendTestEmail;
        private Label lblFromEmail;
        private TextBox txtFromEmail;
        private Label lblUser;
        private TextBox txtEmailUserName;
        private Label lblPort;
        private TextBox txtSmtpPort;
        private Label lblSmtpServer;
        private TextBox txtSmtpServer;
        private Label lblTo;
        private TextBox txtToEmail;
        private TextBox txtEmailPassword;
        private Label lblPassword;
        private CheckBox chkUseSsl;
    }
}