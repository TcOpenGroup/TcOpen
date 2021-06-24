using PlcHammer;
using PlcHammerConnector;
using System;
using System.Linq;
using System.Windows;
using TcOpen.Inxton.MongoDb;

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
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);    // This is necessary for UI operation.

            // Execute PLC connector operations.
            Entry.PlcHammer.Connector.ReadWriteCycleDelay = 50; // Cyclical access period.
            
            Entry.PlcHammer.Connector.BuildAndStart(); // Create connection, loads symbols, and fires cyclic operations.

            SetUpDatabase();
        }

        private static void SetUpDatabase()
        {
            var mongoUri = "mongodb://localhost:27017";
            var databaseName = "Hammer";
            var collectionName = "HammerCollection";

            var processDataRepositorySettings = new MongoDbRepositorySettings<PlainStation001_ProductionData>(mongoUri, databaseName, collectionName);
            var processDataRepository = new MongoDbRepository<PlainStation001_ProductionData>(processDataRepositorySettings);
            
            Entry.PlcHammer.MAIN._app._station001._processDataManager.InitializeRepository(processDataRepository);

            Entry.PlcHammer.MAIN._app._station001._technologicalDataManager
                .InitializeRepository(new MongoDbRepository<PlainStation001_TechnologicalSettings>
                                     (new MongoDbRepositorySettings<PlainStation001_TechnologicalSettings>(mongoUri, databaseName, "TechnologicalSettings")));
        }
    }
}
