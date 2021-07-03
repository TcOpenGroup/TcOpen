using PlcHammer;
using PlcHammerConnector;
using System;
using System.Linq;
using System.Windows;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.Data.Json;
using System.Reflection;
using System.IO;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.Abstractions.Security;

namespace HMI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Creates new instance of this application.
        /// </summary>
        public App()
        {
            // App setup
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter()) // Sets the logger configuration (default reports only to console).
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get)
                .SetSecurity(SecurityManager.Create(SetUpUserRepository()));    // This is necessary for UI operation.            

            // Execute PLC connector operations.
            Entry.PlcHammer.Connector.ReadWriteCycleDelay = 50; // Cyclical access period.
            
            Entry.PlcHammer.Connector.BuildAndStart(); // Create connection, loads symbols, and fires cyclic operations.



            // SetUpMongoDatabase();
            SetUpJsonRepositories();

            
        }

        private static void SetUpRepositories(IRepository<PlainStation001_ProductionData> processRecipiesRepository,
                                              IRepository<PlainStation001_ProductionData> processTraceabiltyRepository,
                                              IRepository<PlainStation001_TechnologicalSettings> technologyDataRepository)
        {
            Entry.PlcHammer.TECH_MAIN._app._station001._processRecipies.InitializeRepository(processRecipiesRepository);
            Entry.PlcHammer.TECH_MAIN._app._station001._processRecipies.InitializeRemoteDataExchange();

            Entry.PlcHammer.TECH_MAIN._app._station001._processTraceabilty.InitializeRepository(processTraceabiltyRepository);
            Entry.PlcHammer.TECH_MAIN._app._station001._processTraceabilty.InitializeRemoteDataExchange();

            Entry.PlcHammer.TECH_MAIN._app._station001._technologicalDataManager.InitializeRepository(technologyDataRepository);
            Entry.PlcHammer.TECH_MAIN._app._station001._technologicalDataManager.InitializeRemoteDataExchange();
        }

        private static IRepository<UserData> SetUpUserRepository()
        {
            var executingAssemblyFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var repositoryDirectory = Path.GetFullPath($"{executingAssemblyFile.Directory}..\\..\\..\\..\\..\\JSONREPOS\\");

            if (!Directory.Exists(repositoryDirectory))
            {
                Directory.CreateDirectory(repositoryDirectory);
            }
        
            return new JsonRepository<UserData>(new JsonRepositorySettings<UserData>(Path.Combine(repositoryDirectory, "Users")));            
        }

        private static void SetUpJsonRepositories()
        {
            var executingAssemblyFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var repositoryDirectory = Path.GetFullPath($"{executingAssemblyFile.Directory}..\\..\\..\\..\\..\\JSONREPOS\\");

            if(!Directory.Exists(repositoryDirectory))
            {
                Directory.CreateDirectory(repositoryDirectory);
            }
            
            var processRecipiesRepository = new JsonRepository<PlainStation001_ProductionData>(new JsonRepositorySettings<PlainStation001_ProductionData>(Path.Combine(repositoryDirectory, "ProcessSettings")));                        
            var processTraceabiltyRepository = new JsonRepository<PlainStation001_ProductionData>(new JsonRepositorySettings<PlainStation001_ProductionData>(Path.Combine(repositoryDirectory, "Traceability")));
            var technologyDataRepository = new JsonRepository<PlainStation001_TechnologicalSettings>(new JsonRepositorySettings<PlainStation001_TechnologicalSettings>(Path.Combine(repositoryDirectory, "TechnologicalSettings")));


            SetUpRepositories(processRecipiesRepository, processTraceabiltyRepository, technologyDataRepository);
        }

        private static void SetUpMongoDatabase()
        {
            var mongoUri = "mongodb://localhost:27017";
            var databaseName = "Hammer";
          
            var processRecipiesRepository = new MongoDbRepository<PlainStation001_ProductionData>(new MongoDbRepositorySettings<PlainStation001_ProductionData>(mongoUri, databaseName, "ProcessSettings"));
            var processTraceabiltyRepository = new MongoDbRepository<PlainStation001_ProductionData>(new MongoDbRepositorySettings<PlainStation001_ProductionData>(mongoUri, databaseName, "Traceability"));
            var technologyDataRepository = new MongoDbRepository<PlainStation001_TechnologicalSettings>(new MongoDbRepositorySettings<PlainStation001_TechnologicalSettings>(mongoUri, databaseName, "TechnologicalSettings"));

            SetUpRepositories(processRecipiesRepository, processTraceabiltyRepository, technologyDataRepository);
        }   
    }
}
