using HmiProjectTemplate.Wpf;
using MainPlc;
using MainPlcConnector;
using Serilog;
using System;
using System.Windows;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.TcoCore.Wpf;

namespace HmiTemplate.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainPlc.Connector.BuildAndStart().ReadWriteCycleDelay = 150;

            var authenticationService = SecurityManager.Create(new MongoDbRepository<UserData>(new MongoDbRepositorySettings<UserData>("mongodb://localhost:27017", "Hammer", "Users")));

            // App setup
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter(new LoggerConfiguration()
                                        .MinimumLevel.Verbose())) // Sets the logger configuration (default reports only to console).
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get) // This is necessary for UI operation.  
                .SetSecurity(authenticationService)
                .SetEditValueChangeLogging(Entry.PlcHammer.Connector)
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { MainPlc.MAIN }));


            SetUpRepositories();
        }

        private void SetUpRepositories()
        {
            var ProcessDataRepoSettings = new MongoDbRepositorySettings<PlainProcessData>(Constants.CONNECTION_STRING_DB, Constants.DB_NAME, "ProcessSettings");
            IntializeProcessDataRepositoryWithDataExchange(MainPlc.MAIN._technology._processSettings, new MongoDbRepository<PlainProcessData>(ProcessDataRepoSettings));
        }

        
        private static void IntializeProcessDataRepositoryWithDataExchange(ProcessDataManager processData, IRepository<PlainProcessData> repository)
        {
            repository.OnCreate = (id, data) => { data._Created = DateTime.Now; data._Modified = DateTime.Now; data.qlikId = id; };
            repository.OnUpdate = (id, data) => { data._Modified = DateTime.Now; };
            processData.InitializeRemoteDataExchange(repository);
        }

        public static MainPlc.MainPlcTwinController MainPlc 
        {  
            get
            {
                return designTime ? Entry.PlcHammerDesign : Entry.PlcHammer;                
            }
        }

        private static bool designTime = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
    }
}
