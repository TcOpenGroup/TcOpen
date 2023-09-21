using System;

using System.Windows;



using System.IO;
using System.Reflection;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoMitsubishiRobotics.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { TcoMitsubishiRoboticsTests.Entry.TcoMitsubishiRoboticsTestsPlc.MAIN._wpfContext }))
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            




            TcoMitsubishiRoboticsTests.Entry.TcoMitsubishiRoboticsTestsPlc.Connector.BuildAndStart();
            TcoMitsubishiRoboticsTests.Entry.TcoMitsubishiRoboticsTestsPlc.MAIN._wpfContextCall.Synchron = true;
            TcoMitsubishiRoboticsTests.Entry.TcoMitsubishiRoboticsTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = true;
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

     
      
    }
}
