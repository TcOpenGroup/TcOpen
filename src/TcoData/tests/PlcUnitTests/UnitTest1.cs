using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoDataTests;
using Tc.Prober.Runners;
using TcOpen.Abstractions.Data;
using TcoData.Repository.Json;
using System.Linq;
using System;
using System.IO;

namespace TcoDataUnitTests
{
    [TestFixture, Timeout(5000)]
    public class fbUnitTestExampleTests
    {
        TestDataContext sut;
        public IRepository<PlainstProcessData> Repository { get; set; }

        public string DataFolder { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            sut = Entry.TcoDataTests.MAIN.dataTests;
            var executingPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            DataFolder = Path.Combine(executingPath, "_data");
            Repository = new JsonRepository<PlainstProcessData>(new JsonRepositorySettings<PlainstProcessData>(DataFolder));
            ClearFolder(DataFolder);
            sut.DataTests.DataManager.InitializeRepository<PlainstProcessData>(Repository as IRepository);
            Entry.TcoDataTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
        }

        private void ClearFolder(string dataDir)
        {
            foreach (var file in Directory.GetFiles(dataDir))
            {
                File.Delete(file);
            }
        }
         

        [Test, Order((int)eDataTests.RestoreTasks)]
        public void Restore()
        {
            //-- Act
            sut.ExecuteProbeRun(
                counts : 10,
                testId: (int)eDataTests.RestoreTasks
          );
        }

            [Test, Order((int)eDataTests.CreateData)]
        public void Recored_with_id_will_be_created()
        {
            //-- Arrange
            var identifier = "some-id-123";
            sut._identifier.Synchron = identifier;
            sut._createDataDone.Synchron = false;
            ClearFolder(DataFolder);
            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.CreateData,
                endCondition: () => sut._createDataDone.Cyclic);
            var record = Repository.GetRecords(identifier).First();
            //-- Assert
            Assert.IsNotNull(record);
            Assert.AreEqual("Writing failure from PLC", record.Cu_1.A_Check_1.ProcessedData.FailureDescription);
            Assert.AreEqual(10, record.Cu_1.A_Check_1.ProcessedData.Maximum);
            Assert.AreEqual(5, record.Cu_1.A_Check_1.ProcessedData.Minimum);
        }


        [Test, Order((int)eDataTests.UpdateData)]
        public void Recored_with_id_will_be_updated()
        {
            //-- Arrange
            var identifier = "some-id-123";
            sut._identifier.Synchron = identifier;
            sut._updateDataDone.Synchron = false;
            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.UpdateData,
                endCondition: () => sut._updateDataDone.Cyclic);
            var record = Repository.GetRecords(identifier).First();
            //-- Assert
            Assert.IsNotNull(record);
            Assert.AreEqual("Updated failure from PLC", record.Cu_1.A_Check_1.ProcessedData.FailureDescription);
            Assert.AreEqual(20, record.Cu_1.A_Check_1.ProcessedData.Maximum);
            Assert.AreEqual(10, record.Cu_1.A_Check_1.ProcessedData.Minimum);
        }

        [Test, Order((int)eDataTests.ReadData)]
        public void Stored_data_will_be_written_to_Plc_when_Read_reqeusted()
        {
            //-- Arrange
            var data = new PlainstProcessData
            {
                _Id = "Artificial",
                _Created = DateTime.Now,
                _Modified = DateTime.Now,
                _recordId = Guid.NewGuid(),
                Cu_1 = new PlainstCu_ProcessData
                {
                    A_Check_1 = new PlainfbAnalogueValueData
                    {
                        ProcessedData = new PlainstAnalogueProcessData
                        {
                            FailureDescription = "Artificial Description",
                            Minimum = -10,
                            Maximum = -5
                        }
                    }
                }
            };
            sut._readDataDone.Synchron = false;
            Repository.Create(data._Id, data);

            //-- Act
            sut._identifier.Synchron = data._Id;
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.ReadData,
                endCondition: () => sut._readDataDone.Cyclic);
            //-- Assert
            Assert.AreEqual(data._Id, sut.DataTests.DataManager._data._Id.Synchron);
            Assert.AreEqual(data.Cu_1.A_Check_1.ProcessedData.FailureDescription, sut.DataTests.DataManager._data.Cu_1.A_Check_1.ProcessedData.FailureDescription.Synchron);
            Assert.AreEqual(data.Cu_1.A_Check_1.ProcessedData.Minimum, sut.DataTests.DataManager._data.Cu_1.A_Check_1.ProcessedData.Minimum.Synchron);
            Assert.AreEqual(data.Cu_1.A_Check_1.ProcessedData.Maximum, sut.DataTests.DataManager._data.Cu_1.A_Check_1.ProcessedData.Maximum.Synchron);
        }

        [Test, Order((int)eDataTests.DeleteData)]
        public void Record_with_id_will_be_deleted()
        {
            //-- Arrange
            var identifier = "some-id-123";
            sut._identifier.Synchron = identifier;
            sut._deleteDataDone.Synchron = false;
            //-- Act
            var record = Repository.GetRecords(identifier).First();

            sut.ExecuteProbeRun(
                testId: (int)eDataTests.DeleteData,
                endCondition: () => sut._deleteDataDone.Cyclic);

            record = Repository.GetRecords(identifier).FirstOrDefault();
            //-- Assert
            Assert.IsNull(record);
        }

    }
}
