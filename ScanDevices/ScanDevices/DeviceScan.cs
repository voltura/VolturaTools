using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ScanDevices
{
    internal class DeviceScan : IDisposable
    {
        private bool scanning = false;
        private readonly Timer timer;
        private int pdnDevInst = 0;
        private int numberOfLoggedErrors = 0;
        private bool stop = false;
        public bool Running { get; private set; }

        public DeviceScan()
        {
            Running = true;
            timer = new Timer(TimerEventProcessor, null, 0, 1000);
        }

        public void Stop()
        {
            stop = true;
        }

        private void TimerEventProcessor(object o)
        {
            if (scanning == false && stop == false)
            {
                ExecuteScan();
            }
            else
            {
                if (stop)
                {
                    timer.Dispose();
                    Running = false;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private void ExecuteScan()
        {
            try
            {
                scanning = true;
                if (NativeMethods.CM_Locate_DevNodeA(ref pdnDevInst, null, NativeMethods.CM_LOCATE_DEVNODE_NORMAL) != NativeMethods.CR_SUCCESS)
                {
                    LogErrorToEventLog("CM_Locate_DevNodeA failed");
                }
                if (NativeMethods.CM_Reenumerate_DevNode(pdnDevInst, NativeMethods.CM_REENUMERATE_NORMAL) != NativeMethods.CR_SUCCESS)
                {
                    LogErrorToEventLog("CM_Reenumerate_DevNode failed");
                }
            }
            catch (Exception ex)
            {
                LogErrorToEventLog(ex.ToString());
            }
            finally
            {
                scanning = false;
            }
        }

        private void LogErrorToEventLog(string errorString)
        {
            if (numberOfLoggedErrors < 1000)
            {
                numberOfLoggedErrors += 1;
                EventLog.WriteEntry(Path.GetFileName(AppDomain.CurrentDomain.ApplicationIdentity.FullName), errorString, EventLogEntryType.Error);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (timer != null)
                {
                    timer.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DeviceScan()
        {
            Dispose(false);
        }
    }
}