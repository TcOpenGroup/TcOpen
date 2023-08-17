using System;

using System.Windows;



using System.IO;
using System.Reflection;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoAbbRobotics.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { TcoAbbRoboticsTests.Entry.TcoAbbRoboticsTestsPlc.MAIN._wpfContext }))
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            




            TcoAbbRoboticsTests.Entry.TcoAbbRoboticsTestsPlc.Connector.BuildAndStart();
            TcoAbbRoboticsTests.Entry.TcoAbbRoboticsTestsPlc.MAIN._wpfContextCall.Synchron = true;
            TcoAbbRoboticsTests.Entry.TcoAbbRoboticsTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = true;
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

     
      
    }
}
