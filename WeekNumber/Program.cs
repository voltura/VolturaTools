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

        private readonly System.Windows.Forms.Timer _timer;
        private static readonly object _lockObject = new object();
        private static readonly Mutex _mutex = new Mutex(true, "550adc75-8afb-4813-ac91-8c8c6cb681ae");
        private readonly TaskbarGui _gui;
        private readonly Week _week;

        #endregion

        #region Application starting point

        [STAThread]
        private static void Main()
        {
            if (!_mutex.WaitOne(TimeSpan.Zero, true)) return;
            Application.EnableVisualStyles();
            new Program();
            Application.Run();
            _mutex.ReleaseMutex();
        }

        #endregion

        #region Constructor

        public Program()
        {
            try
            {
                _week = new Week();
                _gui = new TaskbarGui(Week.Current);
                InitiateTimer(ref _timer);
            }
            catch (Exception ex)
            {
                Message.ShowError(Text.UnhandledException, ex);
                Application.Exit();
            }
        }

        #endregion

        #region Private methods

        private void InitiateTimer(ref System.Windows.Forms.Timer timer)
        {
            timer = new System.Windows.Forms.Timer
            {
                Interval = 60000,
                Enabled = true
            };
            timer.Tick += delegate
            {
                Application.DoEvents();
                if (!_week.WasChanged()) return;
                lock (_lockObject)
                {
                    try
                    {
                        _gui.UpdateWeek(Week.Current);
                    }
                    catch (Exception ex)
                    {
                        Message.ShowError(Text.FailedToSetIcon, ex); 
                        Dispose();
                        Application.Exit();
                    }
                }
            };
        }

        #endregion

        #region IDisposable methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Stop();
                _timer?.Dispose();
                _gui.Dispose();
            }
        }

        #endregion
    }
}