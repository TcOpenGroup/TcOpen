using Serilog;
using System.Windows;
using TcoDataTests;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.MongoDb;

namespace Sandbox.TcoData.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //TcoCore.Threading.Dispatcher.SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            Entry.TcoDataTests.Connector.BuildAndStart();

            // App setup
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter(new LoggerConfiguration()
                                        .WriteTo.Console()        // This will write log into application console.  
                                                                  // .WriteTo.Notepad()        // This will write logs to first instance of notepad program.
                                        .MinimumLevel.Verbose())) // Sets the logger configuration (default reports only to console).
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get); // This is necessary for UI operation.                  
               

            // Initialize logger
            Entry.TcoDataTests.MAIN.sandbox._logger.StartLoggingMessages(TcoCore.eMessageCategory.All);

            var parameters = new MongoDbRepositorySettings<PlainSandboxData>("mongodb://localhost:27017", "TestDataBase", "TestCollection");
            var repository = Repository.Factory<PlainSandboxData>(parameters);

            repository.OnRecordUpdateValidation = (data) =>
            {
                return new DataItemValidation[]
                    {
                        new DataItemValidation($"'{nameof(data.sampleData.SampleInt)}' must be greater than 0", data.sampleData.SampleInt <= 0),
                        new DataItemValidation($"'{nameof(data.sampleData.SampleInt2)}' must be less than 0", data.sampleData.SampleInt2 > 0)
                    };
            };

            Entry.TcoDataTests.MAIN.sandbox.DataManager.InitializeRepository(repository);
            Entry.TcoDataTests.MAIN.sandbox.DataManager.InitializeRemoteDataExchange();
        }
    }
}
