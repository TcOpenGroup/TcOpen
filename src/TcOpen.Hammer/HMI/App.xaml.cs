using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using PlcHammer;
using PlcHammerConnector;
using Serilog;
using Serilog.Sinks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.Json;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Local.Security.Readers;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.TcoCore.Wpf;

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
            // Execute PLC connector operations.
            Entry.PlcHammer.Connector.ReadWriteCycleDelay = 50; // Cyclical access period.
            Entry.PlcHammer.Connector.BuildAndStart(); // Create connection, loads symbols, and fires cyclic operations.

            // Ask about data repository type
            System.Console.Write(
                "What data repository shall we use? ([M]ongoDB, [J]son). Press enter for Json [J]: "
            );
            var answer = System.Console.ReadLine().FirstOrDefault();
            System.Console.WriteLine("Starting application with selected repository type.");

            // Set-up users
            IAuthenticationService authenticationService;
            switch (answer)
            {
                case 'M':
                    //authenticationService = SecurityManager.Create(new MongoDbRepository<UserData>(new MongoDbRepositorySettings<UserData>("mongodb://localhost:27017", "Hammer", "Users")));

                    var users = new MongoDbRepository<UserData>(
                        new MongoDbRepositorySettings<UserData>(
                            "mongodb://localhost:27017",
                            "Hammer",
                            "Users"
                        )
                    );
                    var groups = new MongoDbRepository<GroupData>(
                        new MongoDbRepositorySettings<GroupData>(
                            "mongodb://localhost:27017",
                            "Hammer",
                            "Groups"
                        )
                    );
                    var roleGroupManager = new RoleGroupManager(groups);
                    authenticationService = SecurityManager.Create(users, roleGroupManager);
                    break;
                case 'J':
                    authenticationService = SecurityManager.Create(SetUpJsonRepository());
                    break;
                default:
                    authenticationService = SecurityManager.Create(SetUpJsonRepository());
                    break;
            }

            ISecurityManager securityManager = SecurityManager.Manager;
            securityManager.GetOrCreateRole(new Role("Service", "Maintenance"));

            // App setup
            TcOpen
                .Inxton.TcoAppDomain.Current.Builder.SetUpLogger(
                    new TcOpen.Inxton.Logging.SerilogAdapter(
                        new LoggerConfiguration()
                            .WriteTo.RichTextBox(LogTextBox)
                            .WriteTo.Console() // This will write log into application console.
                            .WriteTo.Notepad() // This will write logs to first instance of notepad program.
                            // uncomment this to send logs over MQTT, to receive the data run MQTTTestClient from this solution.
                            // .WriteTo.MQTT(new MQTTnet.Client.Options.MqttClientOptionsBuilder().WithTcpServer("broker.emqx.io").Build(), "fun_with_TcOpen_Hammer")
                            .MinimumLevel.Verbose()
                    )
                ) // Sets the logger configuration (default reports only to console).
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get) // This is necessary for UI operation.
                .SetSecurity(authenticationService)
                .SetEditValueChangeLogging(Entry.PlcHammer.Connector)
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { Entry.PlcHammer.TECH_MAIN }));

            // Initialize logger
            Entry.PlcHammer.TECH_MAIN._app._logger.StartLoggingMessages(
                TcoCore.eMessageCategory.Info
            );

            // Initialize external authentication
            authenticationService.ExternalAuthorization =
                ExternalTokenAuthorization.CreatePlcTokenReader(
                    Entry.PlcHammer.TECH_MAIN._app._station001._externalToken,
                    Entry.PlcHammer.TECH_MAIN._app._station001._externalTokenPresence
                );

            // Set up data exchange
            switch (answer)
            {
                case 'M':
                    SetUpMongoDatabase();
                    break;
                case 'J':
                    SetUpJsonRepositories();
                    break;
                default:
                    SetUpJsonRepositories();
                    break;
            }
            ObserveChanges();
        }

        private static IRepository<enumModesObservedValue> StationModesRepository { get; set; }
        private static IRepository<
            ObservedValue<string>
        > ProductionRecipeHistoryRepository { get; set; }

        private static void ObserveChanges()
        {
            Entry.PlcHammer.TECH_MAIN._app._station001._currentMode.PublishChanges(
                (IRepository)StationModesRepository,
                x => new enumModesObservedValue(x)
            );
            Entry.PlcHammer.TECH_MAIN._app._station001._processRecipies._data._EntityId.PublishChanges(
                (IRepository)ProductionRecipeHistoryRepository,
                recipeName => new ObservedValue<string>(recipeName)
            );
        }

        private static void SetUpRepositories(
            IRepository<PlainStation001_ProductionData> processRecipiesRepository,
            IRepository<PlainStation001_ProductionData> processTraceabiltyRepository,
            IRepository<PlainStation001_TechnologicalSettings> technologyDataRepository
        )
        {
            Entry.PlcHammer.TECH_MAIN._app._station001._processRecipies.InitializeRepository(
                processRecipiesRepository
            );
            Entry.PlcHammer.TECH_MAIN._app._station001._processRecipies.InitializeRemoteDataExchange();

            Entry.PlcHammer.TECH_MAIN._app._station001._processTraceabilty.InitializeRepository(
                processTraceabiltyRepository
            );
            Entry.PlcHammer.TECH_MAIN._app._station001._processTraceabilty.InitializeRemoteDataExchange();

            Entry.PlcHammer.TECH_MAIN._app._station001._technologicalDataManager.InitializeRepository(
                technologyDataRepository
            );
            Entry.PlcHammer.TECH_MAIN._app._station001._technologicalDataManager.InitializeRemoteDataExchange();
        }

        public static System.Windows.Controls.RichTextBox LogTextBox { get; } =
            new System.Windows.Controls.RichTextBox()
            {
                Background = Brushes.Black,
                Foreground = Brushes.LightGray,
                FontFamily = new FontFamily("Cascadia Mono, Consolas, Courier New, monospace"),
                VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto
            };

        private static IRepository<UserData> SetUpJsonRepository()
        {
            var executingAssemblyFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var repositoryDirectory = Path.GetFullPath(
                $"{executingAssemblyFile.Directory}..\\..\\..\\..\\..\\JSONREPOS\\"
            );

            if (!Directory.Exists(repositoryDirectory))
            {
                Directory.CreateDirectory(repositoryDirectory);
            }

            return new JsonRepository<UserData>(
                new JsonRepositorySettings<UserData>(Path.Combine(repositoryDirectory, "Users"))
            );
        }

        private static void SetUpJsonRepositories()
        {
            var executingAssemblyFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var repositoryDirectory = Path.GetFullPath(
                $"{executingAssemblyFile.Directory}..\\..\\..\\..\\..\\JSONREPOS\\"
            );

            if (!Directory.Exists(repositoryDirectory))
            {
                Directory.CreateDirectory(repositoryDirectory);
            }

            var processRecipiesRepository = new JsonRepository<PlainStation001_ProductionData>(
                new JsonRepositorySettings<PlainStation001_ProductionData>(
                    Path.Combine(repositoryDirectory, "ProcessSettings")
                )
            );
            var processTraceabiltyRepository = new JsonRepository<PlainStation001_ProductionData>(
                new JsonRepositorySettings<PlainStation001_ProductionData>(
                    Path.Combine(repositoryDirectory, "Traceability")
                )
            );
            var technologyDataRepository =
                new JsonRepository<PlainStation001_TechnologicalSettings>(
                    new JsonRepositorySettings<PlainStation001_TechnologicalSettings>(
                        Path.Combine(repositoryDirectory, "TechnologicalSettings")
                    )
                );

            SetUpRepositories(
                processRecipiesRepository,
                processTraceabiltyRepository,
                technologyDataRepository
            );
        }

        private static void SetUpMongoDatabase()
        {
            var mongoUri = "mongodb://localhost:27017";
            var databaseName = "Hammer";

            var processRecipiesRepository = new MongoDbRepository<PlainStation001_ProductionData>(
                new MongoDbRepositorySettings<PlainStation001_ProductionData>(
                    mongoUri,
                    databaseName,
                    "ProcessSettings"
                )
            );
            var processTraceabiltyRepository =
                new MongoDbRepository<PlainStation001_ProductionData>(
                    new MongoDbRepositorySettings<PlainStation001_ProductionData>(
                        mongoUri,
                        databaseName,
                        "Traceability"
                    )
                );
            var technologyDataRepository =
                new MongoDbRepository<PlainStation001_TechnologicalSettings>(
                    new MongoDbRepositorySettings<PlainStation001_TechnologicalSettings>(
                        mongoUri,
                        databaseName,
                        "TechnologicalSettings"
                    )
                );
            StationModesRepository = new MongoDbRepository<enumModesObservedValue>(
                new MongoDbRepositorySettings<enumModesObservedValue>(
                    mongoUri,
                    databaseName,
                    "Station001_Modes"
                )
            );
            ProductionRecipeHistoryRepository = new MongoDbRepository<ObservedValue<string>>(
                new MongoDbRepositorySettings<ObservedValue<string>>(
                    mongoUri,
                    databaseName,
                    "Station001_RecipeHistory"
                )
            );
            SetUpRepositories(
                processRecipiesRepository,
                processTraceabiltyRepository,
                technologyDataRepository
            );
        }
    }
}
