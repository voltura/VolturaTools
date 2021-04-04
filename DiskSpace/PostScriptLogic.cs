using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskSpace
{

    internal class PostScriptLogic
    {
        const string OPTIMIZE_ALL_VOLUMES_PS = @"
$Drive_Letters = Get-PSDrive | Select-Object -ExpandProperty 'Name' | Select-String -Pattern '^[a-z]$'
$All_Drive_Letters=""""
ForEach ($Drive in $Drive_Letters) {
	if (-Not ($Drive.Line -eq """" -or $Drive.Line -eq $null)) {
		$All_Drive_Letters = $All_Drive_Letters + $Drive.Line
    }
}

Write-Host "".PS1 output: START optimizing volumes"" $All_Drive_Letters

Optimize-Volume $All_Drive_Letters

Write-Host "".PS1 output: DONE optimizing volumes"" $All_Drive_Letters
";
        internal static async Task OptimizeAllVolumes()
        {
            string scriptFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                             Application.ProductName + ".ps1");
            Log.Info = $"Executing postscript file '{scriptFile}'";
            try
            {
                File.WriteAllText(scriptFile, OPTIMIZE_ALL_VOLUMES_PS);
                await RunPostScriptAsync(scriptFile);
                Log.Info = $"Done executing postscript file '{scriptFile}'";
            }
            catch (Exception ex)
            {
                Log.Info = $"Error executing postscript file '{scriptFile}'";
                Log.Error = ex;
            }
            finally
            {
                if (File.Exists(scriptFile)) File.Delete(scriptFile);
            }
        }

        internal static async Task RunPostScriptAsync(string scriptFile)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("powershell", $"-ExecutionPolicy Bypass -File {scriptFile}")
            {
                CreateNoWindow = true,
                Verb = "runas",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            using (Process process = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            })
            {
                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    if (e.Data != null)
                    {
                        Log.Info = e.Data;
                    }
                };
                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    if (e.Data != null)
                    {
                        Log.Info = e.Data;
                    }
                };
                await process.RunAsync();
                Log.Info = $"Process ({process.StartInfo.FileName} {process.StartInfo.Arguments}) finished running at {process.ExitTime} with exit code {process.ExitCode}";
            }
        }

        internal static List<string> RunPostScript(string scriptFile, int maxExecutionMs = 2000)
        {
            List<string> output = new List<string>();
            ProcessStartInfo startInfo = new ProcessStartInfo("powershell", $"-ExecutionPolicy Bypass -File {scriptFile}")
            {
                CreateNoWindow = true,
                Verb = "runas",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
            using (Process process = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            })
            {
                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    if (e.Data != null)
                    {
                        output.Add(e.Data);
                    }
                };
                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    if (e.Data != null)
                    {
                        output.Add(e.Data);
                    }
                };
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit(maxExecutionMs);
            }
            return output;
        }
    }

    /// <summary>
    /// Extensions for Task
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Task extention
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static Task RunAsync(this Process process)
        {
            var tcs = new TaskCompletionSource<object>();
            process.EnableRaisingEvents = true;
            process.Exited += (s, e) => tcs.TrySetResult(null);
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                if (e.Data != null)
                {
                    Log.Info = e.Data;
                }
            };
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                if (e.Data != null)
                {
                    Log.ErrorString = e.Data;
                }
            };
            // not sure on best way to handle false being returned
            if (!process.Start())
            {
                string issue = $"Failed to start process ({process.StartInfo.FileName} {process.StartInfo.Arguments}).";
                Log.ErrorString = issue;
                tcs.SetException(new Exception(issue));
            }
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            return tcs.Task;
        }

    }
}