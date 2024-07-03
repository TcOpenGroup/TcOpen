using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Tc.Prober.Runners;
using TcoCore;
using TcoDataTests;
using TcOpen.Inxton.Data;

namespace TcoDataUnitTests
{
    [TestFixture, Timeout(5000)]
    public abstract class TcoDataExchangeBaseTest
    {
        TestDataContext sut;
        public IRepository<PlainstProcessData> Repository { get; set; }

        public string DataFolder { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            sut = Entry.TcoDataTests.MAIN.dataTests;
            Init();
            sut.DataTests.DataManager.InitializeRepository<PlainstProcessData>(
                Repository as IRepository
            );

            try
            {
                sut.DataTests.DataManager.InitializeRemoteDataExchange();
            }
            catch (Exception)
            {
                //throw;
            }

            Entry.TcoDataTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;

            sut.ExecuteProbeRun(10, (int)eDataTests.RestoreTasks);
        }

        public abstract void Init();

        [SetUp]
        public void RunTwoCycles() => sut.ExecuteProbeRun(2, 0);

        [Test, Order((int)eDataTests.CreateData)]
        public void Recored_with_id_will_be_created()
        {
            //-- Arrange
            var identifier = "some-id-123";
            sut._identifier.Synchron = identifier;
            sut._createDataDone.Synchron = false;
            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.CreateData,
                endCondition: () => sut._createDataDone.Synchron
            );
            var record = Repository.GetRecords(identifier).First();
            //-- Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(
                "Writing failure from PLC",
                record.Cu_1.A_Check_1.ProcessedData.FailureDescription
            );
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
                endCondition: () => sut._updateDataDone.Synchron
            );
            var record = Repository.GetRecords(identifier).First();
            //-- Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(
                "Updated failure from PLC",
                record.Cu_1.A_Check_1.ProcessedData.FailureDescription
            );
            Assert.AreEqual(20, record.Cu_1.A_Check_1.ProcessedData.Maximum);
            Assert.AreEqual(10, record.Cu_1.A_Check_1.ProcessedData.Minimum);
        }

        [Test, Order((int)eDataTests.ReadData)]
        public void Stored_data_will_be_written_to_Plc_when_Read_reqeusted()
        {
            //-- Arrange
            var data = new PlainstProcessData
            {
                _EntityId = "Artificial",
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
            Repository.Create(data._EntityId, data);

            //-- Act
            sut._identifier.Synchron = data._EntityId;
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.ReadData,
                endCondition: () => sut._readDataDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(data._EntityId, sut.DataTests.DataManager._data._EntityId.Synchron);
            Assert.AreEqual(
                data.Cu_1.A_Check_1.ProcessedData.FailureDescription,
                sut.DataTests
                    .DataManager
                    ._data
                    .Cu_1
                    .A_Check_1
                    .ProcessedData
                    .FailureDescription
                    .Synchron
            );
            Assert.AreEqual(
                data.Cu_1.A_Check_1.ProcessedData.Minimum,
                sut.DataTests.DataManager._data.Cu_1.A_Check_1.ProcessedData.Minimum.Synchron
            );
            Assert.AreEqual(
                data.Cu_1.A_Check_1.ProcessedData.Maximum,
                sut.DataTests.DataManager._data.Cu_1.A_Check_1.ProcessedData.Maximum.Synchron
            );
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
                endCondition: () => sut._deleteDataDone.Synchron
            );

            record = Repository.GetRecords(identifier).FirstOrDefault();

            //-- Assert
            Assert.IsNull(record);
        }

        [Test, Order((int)eDataTests.UpdateDataWithoutChange)]
        public void Update_data_without_change_works()
        {
            //-- Arrange
            var identifier = "some-id-123456";
            sut._identifier.Synchron = identifier;
            sut._updateDataWithoutChangeDone.Synchron = false;

            //-- Act
            sut.DataTests
                .DataManager
                ._data
                .Cu_1
                .A_Check_1
                .ProcessedData
                .FailureDescription
                .Synchron = nameof(Update_data_without_change_works);
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.UpdateDataWithoutChange,
                endCondition: () => sut._updateDataWithoutChangeDone.Synchron
            );

            var record = Repository.GetRecords(identifier).First();

            //-- Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(
                nameof(Update_data_without_change_works),
                record.Cu_1.A_Check_1.ProcessedData.FailureDescription
            );
        }

        [Test, Order((int)eDataTests.UpdateDataWithInvalidId)]
        public void Update_data_with_invalid_id_will_report_the_issue()
        {
            //-- Arrange
            sut._updateDataWithInvalidIdDone.Synchron = false;

            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.UpdateDataWithInvalidId,
                endCondition: () => sut._updateDataWithInvalidIdDone.Synchron
            );

            //-- Assert
            Assert.True(sut.DataTests.DataManager._updateTask._hasException.Synchron);
        }

        [Test, Order((int)eDataTests.ReadDataWithInvlaidId)]
        public void Read_data_with_invalid_id_will_report_the_issue()
        {
            //-- Arrange
            sut._readDataWithInvlaidIdDone.Synchron = false;
            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.ReadDataWithInvlaidId,
                endCondition: () => sut._readDataWithInvlaidIdDone.Synchron
            );

            //-- Assert
            Assert.True(sut.DataTests.DataManager._readTask._hasException.Synchron);
        }

        [Test, Order((int)eDataTests.DeleteDataWithInvalidId)]
        public void Delete_data_with_invalid_id_will_report_the_issue()
        {
            //-- Arrange
            sut._deleteDataWithInvalidIdDone.Synchron = false;
            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.DeleteDataWithInvalidId,
                endCondition: () => sut._deleteDataWithInvalidIdDone.Synchron
            );

            //-- Assert
            Assert.False(sut.DataTests.DataManager._deleteTask._hasException.Synchron);
            Assert.True(
                sut.DataTests.DataManager._deleteTask._taskState.Synchron == (int)eTaskState.Done
            );
        }

        [Test, Order((int)eDataTests.DataExist)]
        public void Data_exists_with_valid_id()
        {
            //-- Arrange
            sut._dataExistsDone.Synchron = false;
            sut._identifier.Synchron = "Idoexist";

            sut.DataTests.DataManager.GetRepository<PlainstProcessData>()
                .Create(sut._identifier.Synchron, new PlainstProcessData());

            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.DataExist,
                endCondition: () => sut._dataExistsDone.Synchron
            );

            //-- Assert
            Assert.True(sut.DataTests.DataManager._idExistsTask._exists.Synchron);
        }

        [Test, Order((int)eDataTests.DataExist)]
        public void Data_exists_with_invalid_id()
        {
            //-- Arrange
            sut._dataExistsDone.Synchron = false;
            sut._identifier.Synchron = "Idonotexist";

            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.DataExist,
                endCondition: () => sut._dataExistsDone.Synchron
            );

            //-- Assert
            Assert.IsFalse(sut.DataTests.DataManager._idExistsTask._exists.Synchron);
        }

        [Test, Order((int)eDataTests.CreateOrUpdate)]
        public void CreateOrUpdate_non_existing_id()
        {
            //-- Arrange
            sut._createOrUpdateDone.Synchron = false;
            sut._identifier.Synchron = "CreateUpdateNonExisting";

            Assert.IsFalse(
                sut.DataTests.DataManager.GetRepository<PlainstProcessData>()
                    .Exists(sut._identifier.Synchron)
            );

            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.CreateOrUpdate,
                endCondition: () => sut._createOrUpdateDone.Synchron
            );

            //-- Assert
            Assert.IsTrue(
                sut.DataTests.DataManager.GetRepository<PlainstProcessData>()
                    .Exists(sut._identifier.Synchron)
            );
        }

        [Test, Order((int)eDataTests.CreateOrUpdate)]
        public void CreateOrUpdate_existing_id()
        {
            //-- Arrange
            sut._createOrUpdateDone.Synchron = false;
            sut._identifier.Synchron = "CreateUpdateExisting";

            sut.DataTests.DataManager._data.EntityHeader.Synchron = false;
            sut.DataTests.DataManager._data.Cu_1.A_Check_1.ProcessedData.Maximum.Synchron = 0;
            var plain = sut.DataTests.DataManager._data.CreatePlainerType();
            sut.DataTests.DataManager._data.FlushOnlineToPlain(plain);
            sut.DataTests.DataManager.GetRepository<PlainstProcessData>()
                .Create(sut._identifier.Synchron, plain);

            Assert.IsTrue(
                sut.DataTests.DataManager.GetRepository<PlainstProcessData>()
                    .Exists(sut._identifier.Synchron)
            );

            sut.DataTests.DataManager._data.EntityHeader.Synchron = true;
            sut.DataTests.DataManager._data.Cu_1.A_Check_1.ProcessedData.Maximum.Synchron = 784.0f;

            //-- Act
            sut.ExecuteProbeRun(
                testId: (int)eDataTests.CreateOrUpdate,
                endCondition: () => sut._createOrUpdateDone.Synchron
            );

            //-- Assert
            Assert.IsTrue(
                sut.DataTests.DataManager.GetRepository<PlainstProcessData>()
                    .Exists(sut._identifier.Synchron)
            );
            Assert.IsTrue(
                sut.DataTests.DataManager.GetRepository<PlainstProcessData>()
                    .Read(sut._identifier.Synchron)
                    .EntityHeader
            );
            Assert.AreEqual(
                784.0f,
                sut.DataTests.DataManager.GetRepository<PlainstProcessData>()
                    .Read(sut._identifier.Synchron)
                    .Cu_1.A_Check_1.ProcessedData.Maximum
            );
        }
    }
}
