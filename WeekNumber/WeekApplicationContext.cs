#region Using statements

using System;
using System.Windows.Forms;

#endregion

namespace WeekNumber
{
    internal class WeekApplicationContext : ApplicationContext
    {
        #region Internal Taskbar GUI

        internal TaskbarGui Gui;

        #endregion

        #region Private variables

        private readonly Week _week;
        private readonly Timer _timer;

        #endregion

        #region Constructor

        internal WeekApplicationContext()
        {
            try
            {
                Application.ApplicationExit += OnApplicationExit;
                _week = new Week();
                Gui = new TaskbarGui(Week.Current);
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
                timer.Tick += OnTimerTick;
                return timer;
            }
        }

        #endregion

        #region Private event handlers

        private void OnApplicationExit(object sender, EventArgs e) => Cleanup(false);

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
                Gui?.UpdateIcon(Week.Current);
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
            Gui?.Dispose();
            Gui = null;
            if (forceExit)
            {
                Application.Exit();
            }
        }

        #endregion
    }
}