using System.ServiceProcess;

namespace PersistentClipboard
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
#if (!DEBUG)
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PersistentClipboardService()
            };
            ServiceBase.Run(ServicesToRun);
#else
            PersistentClipboardService service = new PersistentClipboardService();
            service.GetTextFromClipboardFromSTAAppartmentState();
            service.SetTextToClipboardFromSTAAppartmentState();
#endif
        }
    }
}
