using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using Serilog;
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
            TcOpen
                .Inxton.TcoAppDomain.Current.Builder.SetUpLogger(
                    new TcOpen.Inxton.Logging.SerilogAdapter(
                        new LoggerConfiguration()
                            .WriteTo.Console() // This will write log into application console.
                            // .WriteTo.Notepad()        // This will write logs to first instance of notepad program.
                            .MinimumLevel.Verbose()
                    )
                ) // Sets the logger configuration (default reports only to console).
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get); // This is necessary for UI operation.

            // Initialize logger
            Entry.TcoDataTests.MAIN.sandbox._logger.StartLoggingMessages(
                TcoCore.eMessageCategory.All
            );

            var parameters = new MongoDbRepositorySettings<PlainSandboxData>(
                "mongodb://localhost:27017",
                "TestDataBase",
                "TestCollection"
            );
            var repository = Repository.Factory<PlainSandboxData>(parameters);

            repository.OnRecordUpdateValidation = (data) =>
            {
                return new DataItemValidation[]
                {
                    new DataItemValidation(
                        $"'{nameof(data.sampleData.SampleInt)}' must be greater than 0",
                        data.sampleData.SampleInt <= 0
                    ),
                    new DataItemValidation(
                        $"'{nameof(data.sampleData.SampleInt2)}' must be less than 0",
                        data.sampleData.SampleInt2 > 0
                    )
                };
            };

            Entry.TcoDataTests.MAIN.sandbox.DataManager.InitializeRepository(repository);
            Entry.TcoDataTests.MAIN.sandbox.DataManager.InitializeRemoteDataExchange();

            //var parametersFragmented = new MongoDbRepositorySettings<PlainSandboxData>("mongodb://localhost:27017", "TestDataBase", "TestCollectionFragmented");
            //var updateFragments = new List<(Expression<Func<PlainSandboxData, PlainSampleDataStructure>>, PlainSampleDataStructure)>
            //{
            //    { (doc=>doc.sampleData , new PlainSampleDataStructure(){ SampleString = "SI KRAL"} ) },

            //};
            //;
            //var repositoryFragmented = new MongoDbFragmentedRepository<PlainSandboxData, PlainSandboxData>(parametersFragmented, updateFragments);



            var parametersFragmented = new MongoDbRepositorySettings<PlainSandboxData>(
                "mongodb://localhost:27017",
                "TestDataBase",
                "TestCollectionFragmented"
            );
            List<Expression<Func<PlainSandboxData, PlainSandboxData>>> fragmentExpression =
                new List<Expression<Func<PlainSandboxData, PlainSandboxData>>>();
            fragmentExpression.Add(data => new PlainSandboxData
            {
                someString = data.someString,
                someInteger = data.someInteger,
                sampleData = data.sampleData
            });
            ;
            var repositoryFragmented = new MongoDbFragmentedRepository<
                PlainSandboxData,
                PlainSandboxData
            >(parametersFragmented, fragmentExpression);

            Entry.TcoDataTests.MAIN.sandbox.DataManagerFragmented.InitializeRepository(
                repositoryFragmented
            );

            var parametersFragmentedPlc1 = new MongoDbRepositorySettings<PlainstProcessData_Plc1>(
                "mongodb://localhost:27017",
                "TestDataBase",
                "TestProcessData"
            );

            List<
                Expression<Func<PlainstProcessData_Plc1, PlainstProcessData_Plc1>>
            > fragmentExpressionPlc1 =
                new List<Expression<Func<PlainstProcessData_Plc1, PlainstProcessData_Plc1>>>();
            //fragmentExpressionPlc1.Add(data => new PlainstProcessData_Plc1 { EntityHeader = data.EntityHeader,/*_Modified = data._Modified */});
            fragmentExpressionPlc1.Add(data => new PlainstProcessData_Plc1 { Cu_1 = data.Cu_1 });

            var repositoryFragmentedPlc1 = new MongoDbFragmentedRepository<
                PlainstProcessData_Plc1,
                PlainstProcessData_Plc1
            >(parametersFragmentedPlc1, fragmentExpressionPlc1);
            Entry.TcoDataTests.MAIN.sandbox.DataManagerPlc1.InitializeRepository(
                repositoryFragmentedPlc1
            );

            var parametersFragmentedPlc2 = new MongoDbRepositorySettings<PlainstProcessData_Plc2>(
                "mongodb://localhost:27017",
                "TestDataBase",
                "TestProcessData"
            );

            List<
                Expression<Func<PlainstProcessData_Plc2, PlainstProcessData_Plc2>>
            > fragmentExpressionPlc2 =
                new List<Expression<Func<PlainstProcessData_Plc2, PlainstProcessData_Plc2>>>();
            fragmentExpressionPlc2.Add(data => new PlainstProcessData_Plc2
            {
                EntityHeader = data.EntityHeader,
                Cu_2 = data.Cu_2 /*someInteger = data.someInteger*/
            });
            ;
            var repositoryFragmentedPlc2 = new MongoDbFragmentedRepository<
                PlainstProcessData_Plc2,
                PlainstProcessData_Plc2
            >(parametersFragmentedPlc2, fragmentExpressionPlc2);
            Entry.TcoDataTests.MAIN.sandbox.DataManagerPlc2.InitializeRepository(
                repositoryFragmentedPlc2
            );
        }
    }
}
