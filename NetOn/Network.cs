﻿using System;
using System.Diagnostics;

namespace NetOn
{
    internal static class Network
    {
        internal static bool Enable()
        {
            // TODO: Get name(s) of network interfaces + make admin elevation
            ProcessStartInfo startInfo = new ProcessStartInfo("netsh", @"interface set interface ""Ethernet"" admin=enable")
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas"
            };
            using (Process process = Process.Start(startInfo))
            {
                do { } while (!process.WaitForExit(1000));
            }
            return true;
        }
        internal static bool Disable()
        {
            // TODO: Get name(s) of network interfaces + make admin elevation
            ProcessStartInfo startInfo = new ProcessStartInfo("netsh", @"interface set interface ""Ethernet"" admin=disable")
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas"
            };
            using (Process process = Process.Start(startInfo))
            {
                do { } while (!process.WaitForExit(1000));
            }
            return true;
        }

        internal static int Status()
        {
            var status = 0;
            // TODO: Get name(s) of network interfaces + make admin elevation
            ProcessStartInfo startInfo = new ProcessStartInfo("netsh", @"interface show interface ""Ethernet""")
            {
                CreateNoWindow = true,
//                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
            Process process = new Process
            {
                EnableRaisingEvents = true
            };
            process.StartInfo = startInfo;
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {
                if (e.Data != null && e.Data.Contains("Administrative state: Enabled")) status = 1;
            };
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => {
                if (e.Data != null && e.Data.Contains("Administrative state: Enabled")) status = 1;
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit(2000);
            return status;
        }
    }
}
