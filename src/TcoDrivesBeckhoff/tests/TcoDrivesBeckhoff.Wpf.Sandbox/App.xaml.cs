using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcoDrivesBeckhoff;
using TcoDrivesBeckhoffTests;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.RepositoryDataSet;

namespace TcoDrivesBeckhoff.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);

           var handler = RepositoryDataSetHandler<PositioningParamItem>.CreateSet(new MongoDbRepository<EntitySet<PositioningParamItem>>(new MongoDbRepositorySettings<EntitySet<PositioningParamItem>>(@"mongodb://localhost:27017", "TestPositions", "Positions")));

            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._simpleAxis.InitializeRemoteDataExchange(handler);
            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._simpleAxis.Load();
            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.Connector.BuildAndStart();
            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContextCall.Synchron = true;
            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = true;
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        public static RepositoryDataSetHandler<PositioningParamItem> PositionHandler { get;  set; }

        protected override void OnExit(ExitEventArgs e)
        {
            
            try
            {
                TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContextCall.Synchron = false;
                TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = false;
            }
            catch
            {
            }

            base.OnExit(e);
        }       
    }
}
