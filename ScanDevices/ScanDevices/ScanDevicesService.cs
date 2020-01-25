using System.Diagnostics;
using System.ServiceProcess;

namespace ScanDevices
{
    public partial class ScanDevicesService : ServiceBase
    {
        private DeviceScan deviceScan;

        public ScanDevicesService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            deviceScan = new DeviceScan();
        }

        protected override void OnStop()
        {
            deviceScan.Stop();
            Stopwatch watch = Stopwatch.StartNew();
            while (deviceScan.Running && watch.ElapsedMilliseconds < 3000)
            {
                System.Threading.Thread.SpinWait(100);
            }
            watch.Stop();
            deviceScan.Dispose();
        }
    }
}