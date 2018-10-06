#region Using statements

using System;
using System.Windows.Forms;

#endregion

namespace WeekNumber
{
    internal class WeekApplicationContext : ApplicationContext
    {
        #region Private variables

        private Week _week = null;
        public TaskbarGui _gui = null;
        private readonly Timer _timer = null;

        #endregion

        #region Constructor

        internal WeekApplicationContext()
        {
            try
            {
                Application.ApplicationExit += new EventHandler(OnApplicationExit);
                _week = new Week();
                _gui = new TaskbarGui(Week.Current);
                _timer = GetTimer;
            }
            catch (Exception ex)
            {
                _timer?.Stop();
                Message.Show(Text.UnhandledException, ex);
                Application.Exit();
            }
        }

        #endregion

        #region Private Timer property

        private Timer GetTimer
        {
            get
            {
                var timer = new Timer
                {
                    Interval = 60000,
                    Enabled = true
                };
                timer.Tick += new EventHandler(OnTimerTick);
                return timer;
            }
        }

        #endregion

        #region Private event handlers

        private void OnApplicationExit(object sender, EventArgs e) => Cleanup(forceExit: false);

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (!_week.WasChanged())
            {
                return;
            }
            var timer = (Timer)sender;
            timer?.Stop();
            Application.DoEvents();
            try
            {
                _gui?.UpdateIcon(Week.Current);
            }
            catch (Exception ex)
            {
                Message.Show(Text.FailedToSetIcon, ex);
                Cleanup();
                return;
            }
            timer?.Start();
        }

        #endregion

        #region Private Cleanup method

        private void Cleanup(bool forceExit = true)
        {
            _timer?.Stop();
            _timer?.Dispose();
            _gui?.Dispose();
            _gui = null;
            if (forceExit)
            {
                Application.Exit();
            }
        }

        #endregion
    }
}