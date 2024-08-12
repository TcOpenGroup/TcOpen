using System;
using System.IO;
using System.Reflection;
using System.Windows;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoUrRobotics.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
            : base()
        {
            TcOpen
                .Inxton.TcoAppDomain.Current.Builder.SetPlcDialogs(
                    DialogProxyServiceWpf.Create(
                        new[] { TcoUrRoboticsTests.Entry.TcoUrRoboticsTestsPlc.MAIN._wpfContext }
                    )
                )
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);

            TcoUrRoboticsTests.Entry.TcoUrRoboticsTestsPlc.Connector.BuildAndStart();
            TcoUrRoboticsTests.Entry.TcoUrRoboticsTestsPlc.MAIN._wpfContextCall.Synchron = true;
            TcoUrRoboticsTests
                .Entry
                .TcoUrRoboticsTestsPlc
                .MAIN
                ._wpfContext
                ._serviceModeActive
                .Synchron = true;
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }
    }
}
