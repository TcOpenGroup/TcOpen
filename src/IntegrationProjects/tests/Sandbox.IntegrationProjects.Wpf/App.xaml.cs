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

            Entry.IntegrationProjectsPlc.Connector.ReadWriteCycleDelay = 250;
            Entry.IntegrationProjectsPlc.Connector.BuildAndStart();

            Entry.IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._settings.InitializeRepository(
                TcoData.Repository.Json.Repository.Factory(new TcOpen.Inxton.Data.Json.JsonRepositorySettings<PlainST001_ProcessData>(@"C:\TcOpen\Data\Settings")));
            Entry.IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._repository.InitializeRepository(
                TcoData.Repository.Json.Repository.Factory(new TcOpen.Inxton.Data.Json.JsonRepositorySettings<PlainST001_ProcessData>(@"C:\TcOpen\Data\ProcessData")));

        }
    }
}
