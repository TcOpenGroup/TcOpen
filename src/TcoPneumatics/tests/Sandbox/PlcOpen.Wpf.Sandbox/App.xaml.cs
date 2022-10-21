using System.Windows;
using TcoPneumaticsTests;

namespace TcoPneumatics.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App():base()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);

            Entry.TcoPneumaticsTestsPlc.Connector.BuildAndStart();           
        }
    }
}
