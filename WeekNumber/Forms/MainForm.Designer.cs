namespace WeekNumber
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.currentWeekNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkWeekTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            this.currentWeekNotifyIcon.Visible = true;
            this.checkWeekTimer.Interval = 60000;
            this.checkWeekTimer.Tick += new System.EventHandler(this.CheckWeekTimer_Tick);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1, 1);
            this.ControlBox = false;
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Week Number";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.NotifyIcon currentWeekNotifyIcon;
        private System.Windows.Forms.Timer checkWeekTimer;
    }
}