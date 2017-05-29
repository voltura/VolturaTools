#region Using statements

using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

#endregion

namespace DiskSpace
{
    /// <summary>
    /// Application log
    /// </summary>
    internal static class Log
    {

        /// <summary>
        /// Init log
        /// </summary>
        internal static void Init()
        {
            FileStream fs = null;
            TextWriterTraceListener traceListener = null;
            try
            {
                string logFile = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".log";
                fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFile), FileMode.Append);
                Trace.Listeners.Clear();
                traceListener = new TextWriterTraceListener(fs);
                Trace.Listeners.Add(traceListener);
                Trace.AutoFlush = true;
                Trace.UseGlobalLock = true;
            }
            catch
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
                throw;
            }
        }

        internal static void Close()
        {
            Truncate();
            Trace.Close();
        }

        /// <summary>
        /// Truncate log
        /// </summary>
        internal static void Truncate()
        {
            Trace.Close();
            string logFile = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".log";
            FileInfo fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFile));
            if (fi.Exists)
            {
                int trimSize = Properties.Settings.Default.logFileSizeMB * 1024 * 1024;
                if (fi.Length > trimSize)
                {
                    using (MemoryStream ms = new MemoryStream(trimSize))
                    {
                        using (FileStream s = new FileStream(logFile, FileMode.Open, FileAccess.ReadWrite))
                        {
                            s.Seek(-trimSize, SeekOrigin.End);
                            byte[] bytes = new byte[trimSize];
                            s.Read(bytes, 0, trimSize);
                            ms.Write(bytes, 0, trimSize);
                            ms.Position = 0;
                            s.SetLength(trimSize);
                            s.Position = 0;
                            ms.CopyTo(s);
                        }
                    }
                }
            }
            Init();
        }

        #region Public static log properties

        /// <summary>
        /// Log info
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal static string Info
        {
            private get
            {
                return string.Empty;
            }
            set
            {
                Trace.TraceInformation("{0} {1}", 
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff", CultureInfo.InvariantCulture), 
                    value);
            }
        }

        /// <summary>
        /// Log error
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal static Exception Error
        {
            private get
            {
                return new ArgumentNullException(Properties.Resources.DiskSpace);
            }
            set
            {
                Trace.TraceError("{0} {1}", 
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff", CultureInfo.InvariantCulture), 
                    value.ToString());
            }
        }
                
        #endregion
    }
}
