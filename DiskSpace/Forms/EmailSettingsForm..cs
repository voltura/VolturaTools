#region Using statements

using System;
using System.Drawing;
using System.Windows.Forms;
using DiskSpace.Properties;

#endregion

namespace DiskSpace.Forms
{
    /// <summary>
    /// Settings form
    /// </summary>
    public partial class EmailSettingsForm : Form
    {
        #region Private member variables

        #endregion

        #region Protected class properties

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        protected Point Offset { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Settings form constructor
        /// </summary>
        public EmailSettingsForm()
        {
            InitializeComponent();
            InitializeFormFromSettings();
        }

        #endregion

        #region Events handling

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsTitle_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateOffset(e);
        }

        private void SettingsTitle_MouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e);
        }

        private void MinimizePanel_MouseEnter(object sender, EventArgs e)
        {
            FocusMinimizeIcon();
        }

        private void MinimizePanel_MouseLeave(object sender, EventArgs e)
        {
            UnfocusMinimizeIcon();
        }

        private void MinimizePanel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizePanelFrame_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            InitializeFormFromSettings();
        }

        private void SendTestEmail_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            SendTestEmail();
        }

        #endregion

        #region Private methods

        private void InitializeFormFromSettings()
        {
            SetControlTextsFromResources();
        }

        private void SetControlTextsFromResources()
        {
            btnSave.Text = Resources.SaveButtonTitle;
            lblSettingsTitle.Text = Resources.EmailSettings;
            Text = Resources.EmailSettings;
            btnSendTestEmail.Text = Resources.SendTestEmail;
        }

        private void SaveSettings()
        {
            UnfocusMinimizeIcon();
            Settings.Default.Save();
        }

        private void FocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.LightGray;
        }

        private void MoveForm(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Top = Cursor.Position.Y - Offset.Y;
                Left = Cursor.Position.X - Offset.X;
            }
        }

        private void UpdateOffset(MouseEventArgs e)
        {
            Offset = new Point(e.X, e.Y);
        }

        private void UnfocusMinimizeIcon()
        {
            minimizePanel.BackColor = Color.White;
        }

        private void SendTestEmail()
        {
            if (Mail.Send("Test email from " + ProductName + ProductVersion,
                "Test email from " + ProductName + ProductVersion))
            {
                return;
            }
            using (MessageForm message = new MessageForm())
            {
                message.SetMessage(Resources.FailedToSendEmail);
                message.ShowDialog();
            }
        }

        #endregion
    }
}