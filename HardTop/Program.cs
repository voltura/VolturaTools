#region Using statements

using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#endregion Using statements

namespace HardTop
{
    internal class Program
    {
        #region Private variable to allow only one instance of application

        private static readonly Mutex Mutex = new Mutex(true, "43A57E91-1D03-448D-857C-75B3A81F69A7");

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
            using (HardTopApplicationContext context = new HardTopApplicationContext())
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