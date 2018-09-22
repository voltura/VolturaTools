using System;
using System.Windows.Forms;

namespace WeekNumber
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateCurrentWeekNotifyIcon();
        }

        private void UpdateCurrentWeekNotifyIcon()
        {
            currentWeekNotifyIcon.Icon = TextIcon.Get(Week.Current);
            currentWeekNotifyIcon.Text = Week.Current;
        }

        private void CheckWeekTimer_Tick(object sender, EventArgs e)
        {
            if (currentWeekNotifyIcon.Text == Week.Current) return;
            UpdateCurrentWeekNotifyIcon();
        }
    }
}