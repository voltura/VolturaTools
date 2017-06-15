#region Using statements

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using DiskSpace.Forms;
using DiskSpace.Properties;

#endregion

[assembly: CLSCompliant(true)]

namespace DiskSpace
{
    internal static class Program
    {
        #region Application entrypoint

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            if (StartedWithCommandLineArguments(args))
            {
                HandleCommandLineExecution(args);
                return;
            }
            RunApplication();
        }

        #endregion

        #region Private static methods and functions

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error = (Exception) e.ExceptionObject;
            Log.Close("=== Application ended ===");
            Environment.Exit(1);
        }

        private static void RunApplication()
        {
            Log.Init();
            Log.Info = "=== Application started ===";
            Log.Info = Application.ProductName + " version " + Application.ProductVersion;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            Log.Close("=== Application ended ===");
        }

        private static bool StartedWithCommandLineArguments(IReadOnlyCollection<string> args)
        {
            return args.Count != 0;
        }

        private static void HandleCommandLineExecution(string[] args)
        {
            var allParams = string.Join(" ", args);
            var startWithWindows = allParams.Contains("autorun=1");
            var notifications = allParams.Contains("notifications=1");
            var minimized = allParams.Contains("minimized=1");
            var start = allParams.Contains("start=1");
            var calledFromInstaller = allParams.Contains("autorun=") && allParams.Contains("notifications=") &&
                                      allParams.Contains("minimized=") && allParams.Contains("start=");
            if (calledFromInstaller)
            {
                Log.Info = "Installer configuration values received";
                UpdateSettings(startWithWindows, notifications, minimized);
                if (start) StartApplicationAsSeparateProcess();
                return;
            }
            ShowCommandLineUsage();
        }

        private static void ShowCommandLineUsage()
        {
            var executable = Path.GetFileName(Application.ExecutablePath);
            Log.Info = string.Format(CultureInfo.InvariantCulture,
                Resources.CommandLineTip, executable);
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                Resources.CommandLineTip, executable));
        }

        private static void StartApplicationAsSeparateProcess()
        {
            using (var p = new Process())
            {
                p.StartInfo = new ProcessStartInfo(Application.ExecutablePath)
                {
                    UseShellExecute = false
                };
                p.Start();
            }
        }

        /// <summary>
        ///     Update settings
        /// </summary>
        /// <param name="startWithWindows">Start application when Windows starts</param>
        /// <param name="notifications">Display balloon tip notifications</param>
        /// <param name="minimized">Start application minimized</param>
        private static void UpdateSettings(bool startWithWindows, bool notifications, bool minimized)
        {
            Settings.Default.startMinimized = minimized;
            Settings.Default.startWithWindows = startWithWindows;
            Settings.Default.notifyWhenSpaceChange = notifications;
            Settings.Default.Save();
        }

        #endregion
    }
}