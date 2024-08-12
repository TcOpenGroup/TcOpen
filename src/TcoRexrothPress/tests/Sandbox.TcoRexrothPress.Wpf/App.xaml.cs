using System.Windows;
using TcoRexrothPressTests;

namespace Sandbox.TcoRexrothPress.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            TcOpen
                .Inxton.TcoAppDomain.Current.Builder.SetUpLogger(
                    new TcOpen.Inxton.Logging.SerilogAdapter()
                )
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);

            Entry.TcoRexrothPressTestsPlc.Connector.BuildAndStart().ReadWriteCycleDelay = 75;

            Entry.TcoRexrothPressTestsPlc.MAIN.myVeryFirstTcoContextInstance.sfk.InitializeTask();
        }
    }
}
