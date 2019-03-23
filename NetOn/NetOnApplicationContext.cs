#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace NetOn
{
    internal class NetOnApplicationContext : ApplicationContext
    {
        #region Internal Taskbar GUI

        internal IGui Gui;

        #endregion Internal Taskbar GUI

        #region Private variables

        private readonly Status _status;
        private readonly Timer _timer;

        #endregion Private variables

        #region Constructor

        internal NetOnApplicationContext()
        {
            try
            {
                Application.ApplicationExit += OnApplicationExit;
                _status = new Status();
                Gui = new TaskbarGui(Status.Current());
                _timer = GetTimer;
            }
            catch (Exception ex)
            {
                _timer?.Stop();
                Message.Show(Resources.UnhandledException, ex);
                Application.Exit();
            }
        }

        #endregion Constructor

        #region Private Timer property

        private Timer GetTimer
        {
            get
            {
                var timer = new Timer
                {
                    Interval = 1000,
                    Enabled = true
                };
                timer.Tick += OnTimerTick;
                return timer;
            }
        }

        #endregion Private Timer property

        #region Private event handlers

        private void OnApplicationExit(object sender, EventArgs e) => Cleanup(false);

        private void OnTimerTick(object sender, EventArgs e)
        {
            var timer = (Timer)sender;
            timer?.Stop();
            Application.DoEvents();
            try
            {
                Gui?.UpdateIcon(Status.Current());
            }
            catch (Exception ex)
            {
                Message.Show(Resources.FailedToSetIcon, ex);
                Cleanup();
                return;
            }
            timer?.Start();
        }

        #endregion Private event handlers

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

        #endregion Private Cleanup method
    }
}