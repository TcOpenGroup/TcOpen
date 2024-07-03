using System;
using System.Linq;
using NUnit.Framework;
using TcOpen.Inxton.Data;

namespace TcoDataUnitTests
{
    [TestFixture()]
    public abstract class RepositoryBaseTests
    {
        protected IRepository<DataTestObject> repository;
        protected IRepository<DataTestObjectAlteredStructure> repository_altered_structure;

        public abstract void Init();

        [OneTimeSetUp]
        public void SetUp()
        {
            Init();
        }

        [TearDown]
        public virtual void TearDown()
        {
            repository.OnCreate = null;
            repository.OnRead = null;
            repository.OnUpdate = null;
            repository.OnDelete = null;
            repository.OnCreateDone = null;
            repository.OnReadDone = null;
            repository.OnUpdateDone = null;
            repository.OnDeleteDone = null;
            repository.OnCreateFailed = null;
            repository.OnReadFailed = null;
            repository.OnUpdateFailed = null;
            repository.OnDeleteFailed = null;
            repository.OnCreate = OnTcoCreate;
            repository.OnUpdate = OnTcoUpdate;
        }

        private void OnTcoCreate(string id, DataTestObject data)
        {
            data._Created = DateTimeProviders.DateTimeProvider.Now;
            data._Modified = DateTimeProviders.DateTimeProvider.Now;
        }

        private void OnTcoUpdate(string id, DataTestObject data)
        {
            data._Modified = DateTimeProviders.DateTimeProvider.Now;
        }

        [Test()]
        public void CreateTest()
        {
            //-- Arrange
            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15
            };
            var id = $"test_{Guid.NewGuid()}";
            //-- Act
            repository.Create(id, testObject);

            //-- Assert
            Assert.AreEqual(1, repository.GetRecords(id).Count());
        }

        [Test()]
        public void CreateWithDelegatesTest()
        {
            //-- Arrange
            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15
            };
            var id = $"test_{Guid.NewGuid()}";
            var onCreateCalled = false;
            var onCreateDoneCalled = false;
            var onCreateFailedCalled = false;

            //-- Act
            repository.OnCreate = null;
            repository.OnCreate = (identifier, data) => onCreateCalled = id == identifier;
            repository.OnCreateDone = (identifier, data) => onCreateDoneCalled = id == identifier;
            repository.OnCreateFailed = (identifier, data, exception) =>
                onCreateFailedCalled = exception != null;
            repository.Create(id, testObject);

            //-- Assert
            Assert.AreEqual(1, repository.GetRecords(id).Count());
            Assert.True(onCreateCalled);
            Assert.True(onCreateDoneCalled);
            Assert.False(onCreateFailedCalled);
        }

        [Test()]
        public void ReadTest()
        {
            //-- Arrange
            DateTimeProviders.DateTimeProvider = new StandardDateTimeProvider();
            var recordName = $"test_read_{Guid.NewGuid()}";
            var testObject = new DataTestObject()
            {
                Name = recordName,
                DateOfBirth = DateTime.Now,
                Age = 25,
                _Created = new DateTime()
            };
            repository.Create(recordName, testObject);

            //-- Act
            var rawTestObj = repository.Read(recordName);

            var testObj = rawTestObj as DataTestObject;

            //-- Assert
            Assert.That(testObject._Created, Is.EqualTo(testObj._Created).Within(1).Seconds);
            Assert.AreEqual(recordName, testObj.Name);
            Assert.AreEqual(25, testObj.Age);
        }

        [Test()]
        public void ReadWithDelegatesTest()
        {
            //-- Arrange
            DateTimeProviders.DateTimeProvider = new StandardDateTimeProvider();
            var recordName = $"test_read_{Guid.NewGuid()}";

            var testObject = new DataTestObject()
            {
                Name = recordName,
                DateOfBirth = DateTime.Now,
                Age = 25,
                _Created = DateTimeProviders.DateTimeProvider.Now
            };
            repository.Create(recordName, testObject);
            var onReadCalled = false;
            var onReadDoneCalled = false;
            var onReadFailedCalled = false;
            //-- Act
            repository.OnRead = (id) => onReadCalled = id == recordName;
            repository.OnReadDone = (id, data) => onReadDoneCalled = id == recordName;
            repository.OnReadFailed = (id, ex) => onReadFailedCalled = ex != null;
            var rawTestObj = repository.Read(recordName);

            var testObj = rawTestObj as DataTestObject;

            //-- Assert
            Assert.AreEqual(recordName, testObj.Name);
            Assert.AreEqual(25, testObj.Age);
            Assert.That(testObject._Created, Is.EqualTo(testObj._Created).Within(1).Seconds);
            Assert.True(onReadCalled);
            Assert.True(onReadDoneCalled);
            Assert.False(onReadFailedCalled);
        }

        [Test()]
        public void UpdateTest()
        {
            //-- Arrange
            var recordName = "test_update_" + Guid.NewGuid();
            var testObject = new DataTestObject()
            {
                Name = "Pepo prior update",
                DateOfBirth = DateTime.Now,
                Age = 88
            };
            repository.Create(recordName, testObject);

            System.Threading.Thread.Sleep(200);

            //-- Act
            testObject.Name = "Pepo post update";
            testObject.Age = 44;
            repository.Update(recordName, testObject);

            var testObj = repository.Read(recordName) as DataTestObject;

            //-- Assert
            Assert.AreEqual("Pepo post update", testObj.Name);
            Assert.AreEqual(44, testObj.Age);
            Assert.AreNotEqual(testObj._Created, testObj._Modified);
            Assert.IsTrue(DateTime.Now.Subtract(new TimeSpan(0, 0, 3)) < testObj._Modified);
        }

        [Test()]
        public void UpdateWithDelegatesTest()
        {
            //-- Arrange
            var recordName = "test_update_" + Guid.NewGuid();
            var testObject = new DataTestObject()
            {
                Name = "Pepo prior update",
                DateOfBirth = DateTime.Now,
                Age = 88
            };
            repository.Create(recordName, testObject);

            var onUpdateCalled = false;
            var onUpdateDoneCalled = false;
            var onUpdateFailedCalled = false;
            //-- Act
            testObject.Name = "Pepo post update";
            testObject.Age = 44;
            repository.OnUpdate = null;
            repository.OnUpdate = (id, data) =>
            {
                OnTcoUpdate(id, data);
                onUpdateCalled = id == recordName;
            };
            repository.OnUpdateDone = (id, data) => onUpdateDoneCalled = id == recordName;
            repository.OnUpdateFailed = (id, data, ex) => onUpdateFailedCalled = ex != null;
            System.Threading.Thread.Sleep(200);
            repository.Update(recordName, testObject);

            var testObj = repository.Read(recordName) as DataTestObject;

            //-- Assert
            Assert.AreEqual("Pepo post update", testObj.Name);
            Assert.AreEqual(44, testObj.Age);
            Assert.AreNotEqual(testObj._Created, testObj._Modified);
            Assert.IsTrue(DateTime.Now.Subtract(new TimeSpan(0, 0, 3)) < testObj._Modified);
            Assert.True(onUpdateCalled);
            Assert.True(onUpdateDoneCalled);
            Assert.False(onUpdateFailedCalled);
        }

        [Test()]
        public void CreateDuplicateExceptionTest()
        {
            //-- Arrange
            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15
            };
            var id = $"test_{Guid.NewGuid()}";
            //-- Act
            repository.Create(id, testObject);

            //-- Assert
            Assert.Throws(
                typeof(DuplicateIdException),
                () => repository.Create(id, new DataTestObject())
            );
        }

        [Test()]
        public void CreateDuplicateExceptionWithDelegatesTest()
        {
            //-- Arrange
            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15
            };
            var onCreateCalled = false;
            var onCreateDoneCalled = false;
            var onCreateFailedCalled = false;
            var entityId = $"test_{Guid.NewGuid()}";

            //-- Act
            repository.Create(entityId, testObject);
            repository.OnCreate = null;
            repository.OnCreate = (id, data) =>
            {
                OnTcoCreate(id, data);
                onCreateCalled = true;
            };
            repository.OnCreateDone = (id, data) => onCreateDoneCalled = true;
            repository.OnCreateFailed = (id, data, ex) =>
                onCreateFailedCalled = ex.GetType() == typeof(DuplicateIdException);
            //-- Assert
            Assert.Throws(
                typeof(DuplicateIdException),
                () => repository.Create(entityId, new DataTestObject())
            );
            Assert.True(onCreateCalled);
            Assert.False(onCreateDoneCalled);
            Assert.True(onCreateFailedCalled);
        }

        [Test()]
        public void UnableLocateRecordReadExceptionTest()
        {
            //-- Assert
            Assert.Throws(
                typeof(UnableToLocateRecordId),
                () => repository.Read("nonexisting_record")
            );
        }

        [Test()]
        public void UnableLocateRecordReadExceptionWithDelegatesTest()
        {
            //-- Arrange
            var onReadCalled = false;
            var onReadDoneCalled = false;
            var onReadFailedCalled = false;
            //-- Act
            repository.OnRead = (id) => onReadCalled = true;
            repository.OnReadDone = (id, data) => onReadDoneCalled = true;
            repository.OnReadFailed = (id, ex) => onReadFailedCalled = ex is UnableToLocateRecordId;

            //-- Assert
            Assert.Throws(
                typeof(UnableToLocateRecordId),
                () => repository.Read("nonexisting_record")
            );
            Assert.True(onReadCalled);
            Assert.False(onReadDoneCalled);
            Assert.True(onReadFailedCalled);
        }

        [Test()]
        public void UnableLocateRecordUpdateExceptionTest()
        {
            //-- Arrange
            var entityId = $"nonexisting_record_{Guid.NewGuid()}";
            //-- Assert
            Assert.Throws(
                typeof(UnableToUpdateRecord),
                () => repository.Update(entityId, new DataTestObject() { _EntityId = entityId })
            );
        }

        [Test()]
        public void UnableLocateRecordUpdateExceptionWithDelegatesTest()
        {
            //-- Arrange
            var entityId = $"nonexisting_record_{Guid.NewGuid()}";
            var onUpdateCalled = false;
            var onUpdateDoneCalled = false;
            var onUpdateFailedCalled = false;
            //-- Act
            repository.OnUpdate = null;
            repository.OnUpdate = (id, data) => onUpdateCalled = true;
            repository.OnUpdateDone = (id, data) => onUpdateDoneCalled = true;
            repository.OnUpdateFailed = (id, data, ex) =>
                onUpdateFailedCalled = ex is UnableToUpdateRecord;
            //-- Assert
            Assert.Throws(
                typeof(UnableToUpdateRecord),
                () => repository.Update(entityId, new DataTestObject() { _EntityId = entityId })
            );
            Assert.True(onUpdateCalled);
            Assert.False(onUpdateDoneCalled);
            Assert.True(onUpdateFailedCalled);
        }

        [Test()]
        public void IdentifierValueMismatchedExceptionTest()
        {
            //-- Arrange
            var entityId = $"test_{Guid.NewGuid()}";
            //-- Assert
            Assert.Throws(
                typeof(IdentifierValueMismatchedException),
                () => repository.Update(entityId, new DataTestObject())
            );
        }

        [Test()]
        public void IdentifierValueMismatchedExceptionWithDelegatesTest()
        {
            //-- Arrange
            var entityId = $"test_{Guid.NewGuid()}";
            var onUpdateCalled = false;
            var onUpdateDoneCalled = false;
            var onUpdateFailedCalled = false;
            //-- Act
            repository.OnUpdate = null;
            repository.OnUpdate = (id, data) =>
            {
                OnTcoUpdate(id, data);
                onUpdateCalled = true;
            };
            repository.OnUpdateDone = (id, data) => onUpdateDoneCalled = true;
            repository.OnUpdateFailed = (id, data, ex) =>
                onUpdateFailedCalled = ex is IdentifierValueMismatchedException;
            //-- Assert
            Assert.Throws(
                typeof(IdentifierValueMismatchedException),
                () => repository.Update(entityId, new DataTestObject())
            );
            Assert.True(onUpdateCalled);
            Assert.False(onUpdateDoneCalled);
            Assert.True(onUpdateFailedCalled);
        }

        [Test()]
        public void UnableUpateRecordUpdateExceptionTest()
        {
            //-- Arrange
            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15
            };

            //-- Act
            repository.Create("unupdatable", testObject);

            //-- Assert
            Assert.Throws(
                typeof(UnableToUpdateRecord),
                () => repository.Update("unupdatable", null)
            );
        }

        [Test()]
        public void ExistsTest_inexisting()
        {
            //-- Arrange
            var id = $"Idonotexist{Guid.NewGuid()}";

            //-- Act
            var actual = repository.Exists(id);

            //-- Assert
            Assert.IsFalse(actual);
        }

        [Test()]
        public void ExistsTest_existing()
        {
            //-- Arrange
            var id = $"Idoexist{Guid.NewGuid()}";
            repository.Create(id, new DataTestObject());
            //-- Act
            var actual = repository.Exists(id);

            //-- Assert
            Assert.IsTrue(actual);
        }

        [Test()]
        public void GetAllRecordsTest()
        {
            //-- Arrange

            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15,
                _Created = new DateTime()
            };
            var originalCount = repository.GetRecords("*").Count();

            //-- Act
            repository.Create($"test1_{Guid.NewGuid()}", testObject);
            repository.Create($"test2_{Guid.NewGuid()}", new DataTestObject());
            repository.Create($"test3_{Guid.NewGuid()}", new DataTestObject());

            //-- Act
            var actual = repository.GetRecords("*");

            //-- Assert
            Assert.AreEqual(3 + originalCount, actual.Count());
        }

        [Test()]
        public void GetFilteredRecordsTest()
        {
            //-- Arrange

            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15,
                _Created = new DateTime()
            };

            var records = repository.Queryable.Where(p => true).Select(p => p._EntityId).ToList();
            foreach (var id in records)
            {
                repository.Delete(id);
            }

            //-- Act
            repository.Create("ToFilter1", testObject);
            repository.Create("ToFilter2", new DataTestObject());
            repository.Create("ToFilter3", new DataTestObject());

            //-- Act
            var actual = repository.GetRecords("ToFilter2");

            //-- Assert
            Assert.AreEqual(1, actual.Count(), this.GetType().ToString());
        }

        [Test()]
        public void AlteredStructureTest()
        {
#if TcoData_Repository_Unit_Tests
            if (this is InMemoryRepositoryTests)
                return;
#endif
            //-- Arrange
            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15,
                _Created = new DateTime()
            };
            var testObjectAltered = new DataTestObjectAlteredStructure()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15,
                _Created = new DateTime()
            };

            //-- Act
            repository.Create("test1", testObject);
            repository_altered_structure.Create("test2", testObjectAltered);
            repository.Create("test3", testObject);

            //-- Act

            repository.Read("test1");
            repository.Read("test2");
            repository.Read("test3");
        }

        [Test()]
        public void EqualityTest()
        {
            //-- Arrange

            var testObject = new DataTestObject()
            {
                Name = "Pepo",
                DateOfBirth = DateTime.Now,
                Age = 15,
                _Created = new DateTime()
            };

            //-- Act
            repository.Create("equality", testObject);

            //-- Act

            var actual = repository.Read("equality");

            //-- Assert.

            testObject.AllTypes.AssertEquality(actual.AllTypes);
        }

        [Test()]
        public void GetRecordsNviTest()
        {
            repository.Create("f123", new DataTestObject());
            repository.Create("f1234", new DataTestObject());
            repository.Create("abc", new DataTestObject());
            repository.Create("abcd", new DataTestObject());
            repository.Create("abcdf123", new DataTestObject());

            Assert.AreEqual(0, repository.GetRecords("f12", searchMode: eSearchMode.Exact).Count());
            Assert.AreEqual(
                1,
                repository.GetRecords("f123", searchMode: eSearchMode.Exact).Count()
            );
            Assert.AreEqual(1, repository.GetRecords("abc", searchMode: eSearchMode.Exact).Count());

            Assert.AreEqual(
                1,
                repository.GetRecords("f1234", searchMode: eSearchMode.StartsWith).Count()
            );
            Assert.AreEqual(
                2,
                repository.GetRecords("f12", searchMode: eSearchMode.StartsWith).Count()
            );

            Assert.AreEqual(
                0,
                repository.GetRecords("z", searchMode: eSearchMode.Contains).Count()
            );
            Assert.AreEqual(
                3,
                repository.GetRecords("f123", searchMode: eSearchMode.Contains).Count()
            );
        }

        [Test(), Order(1000)]
        public void DateTimeProviderTest()
        {
            //-- Arrange
            var dateTimeProvider = new DummyDateTimeProvider();
            dateTimeProvider.SetDateTime = new DateTime(1976, 9, 1);
            DateTimeProviders.DateTimeProvider = dateTimeProvider;
            var testObject = new DataTestObject()
            {
                Name = "SimulatedTimePepo",
                DateOfBirth = DateTime.Now,
                Age = 15,
                _Created = new DateTime()
            };

            repository.Create(testObject.Name, testObject);

            //-- Act

            dateTimeProvider.SetDateTime = new DateTime(1979, 12, 4);

            testObject.Age = 100;
            repository.Update(testObject.Name, testObject);

            //-- Assert
            Assert.AreEqual("SimulatedTimePepo", repository.Read(testObject.Name).Name);
            Assert.AreEqual(100, repository.Read(testObject.Name).Age);
            Assert.AreEqual(new DateTime(1976, 9, 1), repository.Read(testObject.Name)._Created);
            Assert.AreEqual(new DateTime(1979, 12, 4), repository.Read(testObject.Name)._Modified);
        }

        class DummyDateTimeProvider : DateTimeProviderBase
        {
            public DateTime SetDateTime { private get; set; }
            public override DateTime Now
            {
                get { return SetDateTime; }
            }
        }
    }
}
