using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcoIoTests;

namespace Sandbox.TcoIo.Wpf
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

            Entry.TcoIoTests.Connector.BuildAndStart().ReadWriteCycleDelay = 75;

            App.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
}
