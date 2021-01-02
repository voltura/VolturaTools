﻿#region Using statements

using DiskSpace.Forms;
using DiskSpace.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

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
            Log.Error = (Exception)e.ExceptionObject;
            Log.Close("=== Application ended ===");
            Environment.Exit(1);
        }

        private static void RunApplication()
        {
            Log.Info = "=== Application started ===";
            Log.Info = Application.ProductName + " version " + Application.ProductVersion;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (MainForm mainForm = new MainForm())
            {
                Application.Run(mainForm);
            }
            Log.Close("=== Application ended ===");
        }

        private static bool StartedWithCommandLineArguments(IReadOnlyCollection<string> args)
        {
            return args.Count != 0;
        }

        private static void HandleCommandLineExecution(string[] args)
        {
            string allParams = string.Join(" ", args);
            bool startWithWindows = allParams.Contains("autorun=1");
            bool notifications = allParams.Contains("notifications=1");
            bool minimized = allParams.Contains("minimized=1");
            bool start = allParams.Contains("start=1");
            bool calledFromInstaller = allParams.Contains("autorun=") && allParams.Contains("notifications=") &&
                                      allParams.Contains("minimized=") && allParams.Contains("start=");
            if (calledFromInstaller)
            {
                Log.Info = "Installer configuration values received";
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
            string executable = Path.GetFileName(Application.ExecutablePath);
            Log.Info = string.Format(CultureInfo.InvariantCulture,
                Resources.CommandLineTip, executable);
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                Resources.CommandLineTip, executable));
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