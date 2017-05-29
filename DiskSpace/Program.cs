#region To-do

// Display disk properties using below techniques
// https://msdn.microsoft.com/en-us/library/aa394173(v=vs.85).aspx
// https://docs.microsoft.com/en-us/windows-hardware/drivers/install/invoking-a-device-properties-dialog-box-from-a-command-line-prompt

#endregion

#region Using statements

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

#endregion

[assembly: CLSCompliant(true)]
namespace DiskSpace
{
    static class Program
    {
        #region Application entrypoint

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            if (StartedWithCommandLineArguments(args))
            {
                HandleCommandLineExecution(args);
                return;
            }
            RunApplication();
        }

        #endregion

        #region Private static methods and functions

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error = (Exception) e.ExceptionObject;
            Log.Info = "=== Application ended ===";
            Log.Close();
            Environment.Exit(1);
        }

        private static void RunApplication()
        {
            Log.Init();
            Log.Info = "=== Application started ===";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            Log.Info = "=== Application ended ===";
            Log.Close();
        }

        private static bool StartedWithCommandLineArguments(string[] args)
        {
            return args.Length != 0;
        }

        private static void HandleCommandLineExecution(string[] args)
        {
            string allParams = string.Join("|", args);
            bool startWithWindows = allParams.Contains("autorun=1");
            bool notifications = allParams.Contains("notifications=1");
            bool minimized = allParams.Contains("minimized=1");
            bool start = allParams.Contains("start=1");
            bool calledFromInstaller = allParams.Contains("autorun=") == true &&
                                        allParams.Contains("notifications=") == true &&
                                        allParams.Contains("minimized=") == true &&
                                        allParams.Contains("start=") == true;
            if (calledFromInstaller)
            {
                UpdateSettings(startWithWindows, notifications, minimized);
                if (start)
                {
                    StartApplicationAsSeparateProcess();
                }
                return;
            }
            ShowCommandLineUsage();
        }

        private static void ShowCommandLineUsage()
        {
            string executable = System.IO.Path.GetFileName(Application.ExecutablePath);
            Log.Info = string.Format(CultureInfo.InvariantCulture,
                Properties.Resources.CommandLineTip, executable);
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                Properties.Resources.CommandLineTip, executable));
        }

        private static void StartApplicationAsSeparateProcess()
        {
            using (Process p = new Process())
            {
                p.StartInfo = new ProcessStartInfo(Application.ExecutablePath)
                {
                    UseShellExecute = false
                };
                p.Start();
            }
        }

        /// <summary>
        /// Update settings
        /// </summary>
        /// <param name="startWithWindows">Start application when Windows starts</param>
        /// <param name="notifications">Display balloon tip notifications</param>
        /// <param name="minimized">Start application minimized</param>
        private static void UpdateSettings(bool startWithWindows, bool notifications, bool minimized)
        {
            Properties.Settings.Default.startMinimized = minimized;
            Properties.Settings.Default.startWithWindows = startWithWindows;
            Properties.Settings.Default.notifyWhenSpaceChange = notifications;
            Properties.Settings.Default.Save();
        }

        #endregion
    }
}