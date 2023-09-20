using System;

using System.Windows;



using System.IO;
using System.Reflection;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoAimTtiPowerSupply.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { TcoAimTtiPowerSupplyTests.Entry.TcoAimTtiPowerSupplyTestsPlc.MAIN._wpfContext }))
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            




            TcoAimTtiPowerSupplyTests.Entry.TcoAimTtiPowerSupplyTestsPlc.Connector.BuildAndStart();
            TcoAimTtiPowerSupplyTests.Entry.TcoAimTtiPowerSupplyTestsPlc.MAIN._wpfContextCall.Synchron = true;
            TcoAimTtiPowerSupplyTests.Entry.TcoAimTtiPowerSupplyTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = true;
            TcoAimTtiPowerSupplyTests.Entry.TcoAimTtiPowerSupplyTestsPlc.MAIN._wpfContext._supply.Initialize();
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

    

    }
}
