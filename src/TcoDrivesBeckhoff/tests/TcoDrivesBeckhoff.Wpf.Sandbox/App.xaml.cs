using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Raven.Embedded;
using TcoDrivesBeckhoff;
using TcoDrivesBeckhoffTests;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.RavenDb;
using TcOpen.Inxton.RepositoryDataSet;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoDrivesBeckhoff.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
            : base()
        {
            TcOpen
                .Inxton.TcoAppDomain.Current.Builder.SetPlcDialogs(
                    DialogProxyServiceWpf.Create(
                        new[]
                        {
                            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext
                        }
                    )
                )
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);

            StartRavenDBEmbeddedServer();

            //example in mongo
            //var handler = RepositoryDataSetHandler<PositioningParamItem>.CreateSet(new MongoDbRepository<EntitySet<PositioningParamItem>>
            //                 (new MongoDbRepositorySettings<EntitySet<PositioningParamItem>>(@"mongodb://localhost:27017", "Positions", TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._simpleAxis.Symbol)));


            // TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._simpleAxis.InitializeRemoteDataExchange(handler);

            // var handler2 = RepositoryDataSetHandler<PositioningParamItem>.CreateSet(new MongoDbRepository<EntitySet<PositioningParamItem>>
            //                 (new MongoDbRepositorySettings<EntitySet<PositioningParamItem>>(@"mongodb://localhost:27017", "Positions", TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._multiAxis.Symbol)));


            // TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._multiAxis.InitializeRemoteDataExchange(handler2);

            // var datasets = TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._multiAxis.RepositoryHandler.ListOfDataSets;


            var handler = RepositoryDataSetHandler<PositioningParamItem>.CreateSet(
                new RavenDbRepository<EntitySet<PositioningParamItem>>(
                    new RavenDbRepositorySettings<EntitySet<PositioningParamItem>>(
                        new string[] { @"http://localhost:8080" },
                        $"Positions_{TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._singleAxis.Symbol}",
                        "",
                        ""
                    )
                )
            );

            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._singleAxis.InitializeRemoteDataExchange(
                handler
            );

            var handler2 = RepositoryDataSetHandler<PositioningParamItem>.CreateSet(
                new RavenDbRepository<EntitySet<PositioningParamItem>>(
                    new RavenDbRepositorySettings<EntitySet<PositioningParamItem>>(
                        new string[] { @"http://localhost:8080" },
                        $"Positions_{TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._multiAxis.Symbol}",
                        "",
                        ""
                    )
                )
            );

            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._multiAxis.InitializeRemoteDataExchange(
                handler2
            );

            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.Connector.BuildAndStart();
            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContextCall.Synchron =
                true;
            TcoDrivesBeckhoffTests
                .Entry
                .TcoDrivesBeckhoffTestsPlc
                .MAIN
                ._wpfContext
                ._serviceModeActive
                .Synchron = true;
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        /// <summary>
        /// Starts embedded instance of RavenDB server.
        /// IMPORTANT!
        /// CHECK EULA BEFORE USING @ https://ravendb.org/terms
        /// GET APPROPRIATE LICENCE https://ravendb.org/buy FREE COMUNITY EDITION IS ASLO AVAILABLE, BUT YOU NEED TO REGISTER.
        /// STORAGE IS DIRECTED TO THE BIN FOLDER OF REDIRECT
        /// `DataDirectory` property in this method to persist tha data elsewhere.
        /// </summary>
        private static void StartRavenDBEmbeddedServer()
        {
            // Start embedded RavenDB server

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(
                "Starting embedded RavenDB server instance. "
                    + "\nYou should not use this instance in production. "
                    + "\nUsing embedded RavenDB server you agree to the respective EULA."
                    + "\nYou will need to register the licence."
                    + "\nThe data are strored in temporary 'bin' folder of your application, "
                    + "\nif you want to persist your data safely redirect the DataDirectory into different location."
            );
            Console.WriteLine("---------------------------------------------------");

            EmbeddedServer.Instance.StartServer(
                new ServerOptions
                {
                    DataDirectory = Path.Combine(
                        new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName,
                        "tmp",
                        "data"
                    ),
                    AcceptEula = true,
                    ServerUrl = "http://127.0.0.1:8080",
                }
            );

            // EmbeddedServer.Instance.OpenStudioInBrowser();
        }

        public static RepositoryDataSetHandler<PositioningParamItem> PositionHandler { get; set; }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                TcoDrivesBeckhoffTests
                    .Entry
                    .TcoDrivesBeckhoffTestsPlc
                    .MAIN
                    ._wpfContextCall
                    .Synchron = false;
                TcoDrivesBeckhoffTests
                    .Entry
                    .TcoDrivesBeckhoffTestsPlc
                    .MAIN
                    ._wpfContext
                    ._serviceModeActive
                    .Synchron = false;
            }
            catch { }

            base.OnExit(e);
        }
    }
}
