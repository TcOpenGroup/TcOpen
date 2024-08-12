#if DEBUG

using NUnit.Framework;
using TcoDataTests;
using TcOpen.Inxton.Data.MongoDb;
using System.Linq;

namespace TcoDataExamples.Tests
{
    public class Tests
    {
        public Tests()
        {
            Entry.TcoDataTests.Connector.BuildAndStart();
        }

        [OneTimeSetUp]
        public void InitializeRepository()
        {
            #region RepositoryInitialization
            repository = new MongoDbRepository<PlainSandboxData>(
                new MongoDbRepositorySettings<PlainSandboxData>(
                    "mongodb://localhost:27017",
                    "MyExampleDatabase",
                    "MyExampleCollection"
                )
            );

            Entry.TcoDataTests.MAIN.sandbox.DataManager.InitializeRepository(repository);
            Entry.TcoDataTests.MAIN.sandbox.DataManager.InitializeRemoteDataExchange();
            #endregion
        }

        private MongoDbRepository<PlainSandboxData> repository;

        [Test]
        public void TestExamples()
        {
            var interStateDelay = 1000;
            var rec1 = Entry.TcoDataTests.MAIN.sandbox.recordId_1.Synchron;
            var rec2 = Entry.TcoDataTests.MAIN.sandbox.recordId_2.Synchron;
            var state = Entry.TcoDataTests.MAIN.sandbox._state;
            state.Synchron = (short)eExamplesStates.Initialize_1;
            System.Threading.Thread.Sleep(interStateDelay);

            state.Synchron = (short)eExamplesStates.Initialize_2;
            System.Threading.Thread.Sleep(interStateDelay);

            state.Synchron = (short)eExamplesStates.Create;
            System.Threading.Thread.Sleep(interStateDelay);
            Assert.IsTrue(repository.Queryable.Where(p => p._EntityId == rec1).Any());

            state.Synchron = (short)eExamplesStates.Update;
            System.Threading.Thread.Sleep(interStateDelay);
            var record = repository.Queryable.Where(p => p._EntityId == rec1).FirstOrDefault();
            Assert.AreEqual(33, record.sampleData.SampleInt);
            Assert.AreEqual("Max", record.sampleData.SampleString);
            Assert.AreEqual(1, record.sampleData.SampleNestedStructure.SampleLREAL);

            state.Synchron = (short)eExamplesStates.CreateOrUpdate;
            System.Threading.Thread.Sleep(interStateDelay);
            record = repository.Queryable.Where(p => p._EntityId == rec2).FirstOrDefault();
            Assert.AreEqual(44, record.sampleData.SampleInt);
            Assert.AreEqual("Lewis", record.sampleData.SampleString);
            Assert.AreEqual(7, record.sampleData.SampleNestedStructure.SampleLREAL);

            state.Synchron = (short)eExamplesStates.Read;
            System.Threading.Thread.Sleep(interStateDelay);
            record = repository.Queryable.Where(p => p._EntityId == rec1).FirstOrDefault();
            Assert.AreEqual(33, record.sampleData.SampleInt);
            Assert.AreEqual("Max", record.sampleData.SampleString);
            Assert.AreEqual(1, record.sampleData.SampleNestedStructure.SampleLREAL);

            state.Synchron = (short)eExamplesStates.Delete;
            System.Threading.Thread.Sleep(interStateDelay);
            record = repository.Queryable.Where(p => p._EntityId == rec1).FirstOrDefault();
            Assert.IsTrue(!repository.Queryable.Where(p => p._EntityId == rec1).Any());

            state.Synchron = (short)eExamplesStates.Exists;
            System.Threading.Thread.Sleep(interStateDelay * 3);
            var plain = Entry.TcoDataTests.MAIN.sandbox.DataManager._messenger._mime.PlainMessage;

            Assert.AreEqual("Record knight exists.", plain.Text);
        }
    }
}

#endif
