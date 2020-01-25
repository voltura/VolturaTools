#region Using statements

using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#endregion Using statements

namespace NetOn
{
    internal class Program
    {
        #region Private variable to allow only one instance of application

        private static readonly Mutex Mutex = new Mutex(true, "1842EDCA-3D8A-434F-90A2-27DE516EC73A");

        #endregion Private variable to allow only one instance of application

        #region Application starting point

        [STAThread]
        private static void Main()
        {
            if (!Mutex.WaitOne(TimeSpan.Zero, true))
            {
                return;
            }
            Application.EnableVisualStyles();
            Application.VisualStyleState = VisualStyleState.ClientAndNonClientAreasEnabled;
            using (NetOnApplicationContext context = new NetOnApplicationContext())
            {
                if (context?.Gui != null)
                {
                    Application.Run(context);
                }
            }

            Mutex.ReleaseMutex();
        }

        #endregion Application starting point
    }
}