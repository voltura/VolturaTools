#region Using statements

using System;
using System.Drawing;
using System.Windows.Forms;
using DiskSpace.Properties;
using System.Linq;

#endregion

namespace DiskSpace.Forms
{
    /// <summary>
    ///     Settings form
    /// </summary>
    public partial class EmailSettingsForm : Form
    {
        #region Protected class properties

        /// <summary>
        ///     Mouse location offset used form form movement
        /// </summary>
        protected Point Offset { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Settings form constructor
        /// </summary>
        public EmailSettingsForm()
        {
            InitializeComponent();
            InitializeFormFromSettings();
        }

        #endregion

        #region Events handling

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e) => SaveSettings();

        private void Save_Click(object sender, EventArgs e) => Close();

        private void SettingsTitle_MouseDown(object sender, MouseEventArgs e) => UpdateOffset(e);

        private void SettingsTitle_MouseMove(object sender, MouseEventArgs e) => MoveForm(e);

        private void MinimizePanel_MouseEnter(object sender, EventArgs e) => FocusMinimizeIcon();

        private void MinimizePanel_MouseLeave(object sender, EventArgs e) => UnfocusMinimizeIcon();

        private void MinimizePanel_Click(object sender, EventArgs e) => Close();

        private void MinimizePanelFrame_Click(object sender, EventArgs e) => Close();

        private void SettingsForm_Load(object sender, EventArgs e) => InitializeFormFromSettings();

        private void SendTestEmail_Click(object sender, EventArgs e)
        {
            SaveSettings();
            SendTestEmail();
        }

        private void SmtpPort_TextChanged(object sender, EventArgs e) => AllowIntNumberOnly();

        private void SmtpPort_KeyDown(object sender, KeyEventArgs e) => OnlyAllowNumericInput(e);

        #endregion

        #region Private methods

        private void InitializeFormFromSettings() => SetControlTextsFromResources();

        private void SetControlTextsFromResources()
        {
            btnSave.Text = Resources.SaveButtonTitle;
            lblSettingsTitle.Text = Resources.EmailSettings;
            Text = Resources.EmailSettings;
            btnSendTestEmail.Text = Resources.SendTestEmail;
        }

        private static void SaveSettings() => Settings.Default.Save();

        private void FocusMinimizeIcon() => minimizePanel.BackColor = Color.LightGray;

        private void MoveForm(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Top = Cursor.Position.Y - Offset.Y;
            Left = Cursor.Position.X - Offset.X;
        }

        private void UpdateOffset(MouseEventArgs e) => Offset = new Point(e.X, e.Y);

        private void UnfocusMinimizeIcon() => minimizePanel.BackColor = Color.White;

        private void SendTestEmail()
        {
            if (Mail.Send("Test email from " + ProductName + Resources.Space + ProductVersion,
                "This is a test email from " + ProductName + Resources.Space + ProductVersion, Settings.Default)) return;
            MessageForm.LogAndDisplayMessage(Resources.FailedToSendEmail);
        }

        private void AllowIntNumberOnly()
        {
            if (string.IsNullOrEmpty(txtSmtpPort.Text)) return;
            var isInt = int.TryParse(txtSmtpPort.Text, out int i);
            if (i < 0)
            {
                txtSmtpPort.Text = txtSmtpPort.Text.TrimStart(new char[] { '-'});
            }
            while (!isInt && !string.IsNullOrEmpty(txtSmtpPort.Text))
            {
                if (txtSmtpPort.Text.Length > 1 && !isInt)
                {
                    txtSmtpPort.Text = txtSmtpPort.Text.Substring(0, txtSmtpPort.Text.Length - 1);
                }
                isInt = uint.TryParse(txtSmtpPort.Text, out uint _);
                if ((txtSmtpPort.Text.Length == 1) && !isInt)
                {
                    txtSmtpPort.Text = string.Empty;
                }
            }
        }

        private static void OnlyAllowNumericInput(KeyEventArgs e)
        {
            if (!((e.KeyValue > 47 && e.KeyValue < 58) ||
                e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Escape ||
                e.KeyCode == Keys.Back ||
                e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right))
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion
    }
}