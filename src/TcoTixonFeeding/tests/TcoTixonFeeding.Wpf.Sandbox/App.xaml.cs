using System;

using System.Windows;



using System.IO;
using System.Reflection;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoTixonFeeding.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { TcoTixonFeedingTests.Entry.TcoTixonFeedingTestsPlc.MAIN._wpfContext }))
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            




            TcoTixonFeedingTests.Entry.TcoTixonFeedingTestsPlc.Connector.BuildAndStart();
            TcoTixonFeedingTests.Entry.TcoTixonFeedingTestsPlc.MAIN._wpfContextCall.Synchron = true;
            TcoTixonFeedingTests.Entry.TcoTixonFeedingTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = true;
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

     
      
    }
}
