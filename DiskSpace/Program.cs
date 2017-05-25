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
            bool calledFromInstaller = false;
            if (args.Length > 0)
            {
                string allParams = string.Join("|", args);
                bool startWithWindows = allParams.Contains("autorun=1");
                bool notifications = allParams.Contains("notifications=1");
                bool minimized = allParams.Contains("minimized=1");
                bool start = allParams.Contains("start=1");
                calledFromInstaller = allParams.Contains("autorun=") == true &&
                                        allParams.Contains("notifications=") == true &&
                                        allParams.Contains("minimized=") == true &&
                                        allParams.Contains("start=") == true;
                if (calledFromInstaller)
                {
                    SettingsForm.UpdateSettings(startWithWindows, notifications, minimized);
                    if (start)
                    {
                        using (Process p = new Process())
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo(Application.ExecutablePath)
                            {
                                UseShellExecute = false
                            };
                            p.StartInfo = startInfo;
                            p.Start();
                        }
                    }
                    return;
                }
                else
                {
                    string executable = Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf(@"\", 
                        StringComparison.Ordinal) + 1);
                    IFormatProvider formatProvider = CultureInfo.InvariantCulture;
                    Console.WriteLine(string.Format(formatProvider, Properties.Resources.CommandLineTip, executable));
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}