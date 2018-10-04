#region Using statements

using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#endregion

namespace WeekNumber
{
    internal class Program
    {
        #region Private variable

        private static readonly Mutex _mutex = new Mutex(true, "550adc75-8afb-4813-ac91-8c8c6cb681ae");

        #endregion

        #region Application starting point

        [STAThread]
        private static void Main()
        {
            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.VisualStyleState =
                                    VisualStyleState.ClientAndNonClientAreasEnabled;
                WeekApplicationContext context = new WeekApplicationContext();
                if (context?._gui != null)
                {
                    Application.Run(context);
                }
                _mutex.ReleaseMutex();
            }
        }

        #endregion
    }
}