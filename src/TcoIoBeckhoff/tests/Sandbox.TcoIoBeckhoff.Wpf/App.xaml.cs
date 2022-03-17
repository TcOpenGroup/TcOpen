
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcoIoBeckhoffTests;

namespace Sandbox.TcoIoBeckhoff.Wpf
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
            
            Entry.TcoIoBeckhoffTests.Connector.BuildAndStart().ReadWriteCycleDelay = 75;
        }
    }
}