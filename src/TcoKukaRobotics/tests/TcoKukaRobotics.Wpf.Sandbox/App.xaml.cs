using System;

using System.Windows;



using System.IO;
using System.Reflection;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoKukaRobotics.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { TcoKukaRoboticsTests.Entry.TcoKukaRoboticsTestsPlc.MAIN._wpfContext }))
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            




            TcoKukaRoboticsTests.Entry.TcoKukaRoboticsTestsPlc.Connector.BuildAndStart();
            TcoKukaRoboticsTests.Entry.TcoKukaRoboticsTestsPlc.MAIN._wpfContextCall.Synchron = true;
            TcoKukaRoboticsTests.Entry.TcoKukaRoboticsTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = true;
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

     
      
    }
}
