#region Using statements

using System;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace WeekNumber
{
    internal class Program : IDisposable
    {
        #region Private variables

        private Week _week;
        private TaskbarGui _gui;
        private System.Windows.Forms.Timer _timer;
        private static readonly object _lockObject = new object();
        private static readonly Mutex _mutex = new Mutex(true, "550adc75-8afb-4813-ac91-8c8c6cb681ae");

        #endregion

        #region Application starting point

        [STAThread]
        private static void Main()
        {
            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Program program = new Program();
                program.Run();
                Application.Run();
                _mutex.ReleaseMutex();
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
                    Interval = 60000,
                    Enabled = true
                };
                timer.Tick += new EventHandler(OnTimerTick);
                return timer;
            }
        }

        #endregion

        #region Event handler

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (!_week.WasChanged())
            {
                return;
            }
            Application.DoEvents();
            lock (_lockObject)
            {
                try
                {
                    _gui.UpdateIcon(Week.Current);
                }
                catch (Exception ex)
                {
                    Message.Show(Text.FailedToSetIcon, ex); 
                    Dispose();
                }
            }
        }

        #endregion

        #region Private method

        private void Run()
        {
            try
            {
                _week = new Week();
                _gui = new TaskbarGui(Week.Current);
                _gui.UserClose += new EventHandler(delegate { Dispose(); });
                _timer = GetTimer;
            }
            catch (Exception ex)
            {
                Message.Show(Text.UnhandledException, ex);
                Dispose();
            }
        }

        #endregion

        #region IDisposable methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            Application.Exit();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Stop();
                _timer?.Dispose();
                if (_gui != null)
                {
                    _gui.Dispose();
                }
            }
        }

        #endregion
    }
}