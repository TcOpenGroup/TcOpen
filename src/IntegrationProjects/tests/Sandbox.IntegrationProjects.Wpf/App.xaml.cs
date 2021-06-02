using IntegrationProjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sandbox.IntegrationProjects.Wpf
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
            
            Entry.IntegrationProjectsPlc.Connector.BuildAndStart().ReadWriteCycleDelay = 75;

            Entry.IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._settings.InitializeRepository(
                TcoData.Repository.Json.Repository.Factory(new TcoData.Repository.Json.JsonRepositorySettings<PlainST001_ProcessData>(@"C:\TcOpen\Data\")));
                
        }
    }
}
