using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

[assembly: CLSCompliant(true)]
namespace DiskSpace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
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
                }
                else
                {
                    string executable = Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf(@"\", 
                        StringComparison.Ordinal) + 1);
                    Console.WriteLine(string.Format(CultureInfo.InvariantCulture, 
                        Properties.Resources.CommandLineTip, executable));
                }
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
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
    }
}