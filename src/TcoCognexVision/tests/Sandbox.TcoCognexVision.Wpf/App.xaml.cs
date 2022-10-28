using TcoCognexVisionTests;
using System.Windows;

namespace Sandbox.TcoCognexVision.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);

            Entry.TcoCognexVisionTestsPlc.Connector.BuildAndStart().ReadWriteCycleDelay = 75;
        }
    }
}
