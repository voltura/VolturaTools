using System.ServiceProcess;

namespace ScanDevices
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ScanDevicesService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}