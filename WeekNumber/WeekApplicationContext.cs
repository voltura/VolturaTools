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
        private readonly System.Windows.Forms.Timer _timer = null;

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
                Message.Show(Text.UnhandledException, ex);
            }
        }

        #endregion

        #region Private properties

        private System.Windows.Forms.Timer GetTimer
        {
            get
            {
                var timer = new System.Windows.Forms.Timer
                {
                    Interval = 6000,
                    Enabled = true
                };
                timer.Tick += new EventHandler(OnTimerTick);
                return timer;
            }
        }

        #endregion

        #region Event handlers

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Cleanup(forceExit: false);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (!_week.WasChanged())
            {
                return;
            }
            var timer = (System.Windows.Forms.Timer)sender;
            timer.Enabled = false;
            Application.DoEvents();
            try
            {
                _gui.UpdateIcon(Week.Current);
            }
            catch (Exception ex)
            {
                Message.Show(Text.FailedToSetIcon, ex);
                Cleanup();
            }
            finally
            {
                if (timer != null)
                {
                    timer.Enabled = true;
                }
            }
        }

        #endregion

        #region Private method

        private void Cleanup(bool forceExit = true)
        {
            _timer?.Stop();
            _timer?.Dispose();
            _gui?.Dispose();
            if (forceExit)
            {
                Application.ExitThread();
            }
        }

        #endregion
    }
}