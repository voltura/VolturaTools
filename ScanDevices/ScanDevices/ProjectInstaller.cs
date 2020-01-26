using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

namespace ScanDevices
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void ServiceInstaller_Committed(object sender, InstallEventArgs e)
        {
            SetServiceToRestartAfterFailure();
            StartService();
        }

        private void SetServiceToRestartAfterFailure()
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "SC.EXE";
                p.StartInfo.Arguments = $"failure {serviceInstaller.ServiceName} reset= 60 actions= restart/6000/restart/6000/restart/6000";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
            }
        }

        private void StartService()
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "SC.EXE";
                p.StartInfo.Arguments = $"start {serviceInstaller.ServiceName}";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
            }
        }
    }
}
