#region Using statements

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DiskSpace.Properties;

#endregion

namespace DiskSpace.Forms
{
    /// <summary>
    /// Settings form
    /// </summary>
    public partial class MessageForm : Form
    {
        #region Public methods

        /// <summary>
        ///     Set message to display in form
        /// </summary>
        /// <param name="messageText">Message text</param>
        public void SetMessage(string messageText) => lblMessage.Text = messageText;
        
        /// <summary>
        ///     Set link to product URL
        /// </summary>
        public void SetLink() => Link.Text = Resources.ProductURL;

        #endregion

        #region Protected class properties

        /// <summary>
        /// Mouse location offset used form form movement
        /// </summary>
        protected Point Offset { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Settings form constructor
        /// </summary>
        /// <param name="messageText">Message text</param>
        public MessageForm(string messageText)
        {
            InitializeComponent();
            SetControlTexts();
            SetMessage(messageText);
        }

        /// <summary>
        ///     Settings form constructor
        /// </summary>
        public MessageForm()
        {
            InitializeComponent();
            SetControlTexts();
        }

        #endregion

        #region Public static methods

        /// <summary>
        ///     Display a message
        /// </summary>
        /// <param name="messageText"></param>
        public static void DisplayMessage(string messageText)
        {
            using (var message = new MessageForm(messageText))
            {
                message.ShowDialog();
            }
        }

        /// <summary>
        ///     Logs and displays message
        /// </summary>
        /// <param name="messageText"></param>
        public static void LogAndDisplayMessage(string messageText)
        {
            Log.Info = messageText;
            using (var message = new MessageForm(messageText))
            {
                message.ShowDialog();
            }
        }

        /// <summary>
        ///     Logs and displays message with Product URL link
        /// </summary>
        /// <param name="messageText"></param>
        public static void LogAndDisplayLinkMessage(string messageText)
        {
            Log.Info = messageText;
            using (var message = new MessageForm(messageText))
            {
                message.SetLink();
                message.ShowDialog();
            }
        }

        #endregion

        #region Events handling

        private void OK_Click(object sender, EventArgs e) => Close();

        private void SettingsTitle_MouseDown(object sender, MouseEventArgs e) => UpdateOffset(e);

        private void SettingsTitle_MouseMove(object sender, MouseEventArgs e) => MoveForm(e);

        private void MinimizePanel_MouseEnter(object sender, EventArgs e) => FocusMinimizeIcon();

        private void MinimizePanel_MouseLeave(object sender, EventArgs e) => UnfocusMinimizeIcon();

        private void MinimizePanel_Click(object sender, EventArgs e) => Close();

        private void MinimizePanelFrame_Click(object sender, EventArgs e) => Close();

        private void Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl();

        #endregion

        #region Private methods

        private void SetControlTexts()
        {
            btnOK.Text = Resources.OK;
            lblMessageFormTitle.Text = Resources.MessageTitle;
            Text = Resources.MessageTitle;
        }

        private void FocusMinimizeIcon() => minimizePanel.BackColor = Color.LightGray;

        private void MoveForm(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Top = Cursor.Position.Y - Offset.Y;
            Left = Cursor.Position.X - Offset.X;
        }

        private void UpdateOffset(MouseEventArgs e) => Offset = new Point(e.X, e.Y);

        private void UnfocusMinimizeIcon() => minimizePanel.BackColor = Color.White;

        private void OpenUrl()
        {
            using (var p = new Process())
            {
                p.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = Link.Text
                };
                p.Start();
            }
        }

        #endregion
    }
}