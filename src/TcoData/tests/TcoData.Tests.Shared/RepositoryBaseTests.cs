using NUnit.Framework;
using System;
using System.Linq;
using TcOpen.Inxton.Abstractions.Data;
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

        }

        [Test()]
        public void CreateTest()
        {
            //-- Arrange
            var testObject = new DataTestObject() { Name = "Pepo", DateOfBirth = DateTime.Now, Age = 15 };
            var id = $"test_{Guid.NewGuid()}";
            //-- Act
            repository.Create(id, testObject);

            //-- Assert
            Assert.AreEqual(1, repository.GetRecords(id).Count());

            
        }

        [Test()]
        public void ReadTest()
        {

            //-- Arrange
            DateTimeProviders.DateTimeProvider = new StandardDateTimeProvider();
            var recordName = "test_read";
            var testObject = new DataTestObject() { Name = "Pepo read", DateOfBirth = DateTime.Now, Age = 25, _Created = new DateTime() };
            repository.Create(recordName, testObject);

            //-- Act
            var rawTestObj = repository.Read(recordName);

            var testObj = rawTestObj as DataTestObject;

            //-- Assert
            Assert.AreEqual("Pepo read", testObj.Name);
            Assert.AreEqual(25, testObj.Age);
            Assert.IsTrue(DateTime.Now.Subtract(new TimeSpan(0, 0, 3)) < testObj._Created);
        }

        [Test()]
        public void UpdateTest()
        {
            //-- Arrange
            var recordName = "test_update";
            var testObject = new DataTestObject() { Name = "Pepo prior update", DateOfBirth = DateTime.Now, Age = 88 };
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
        public void CreateDuplicateExceptionTest()
        {
            //-- Arrange
            var testObject = new DataTestObject() { Name = "Pepo", DateOfBirth = DateTime.Now, Age = 15 };

            //-- Act
            repository.Create("test", testObject);

            //-- Assert
            Assert.Throws(typeof(DuplicateIdException), () => repository.Create("test", new DataTestObject()));
        }

        [Test()]
        public void UnableLocateRecordReadExceptionTest()
        {                      
            //-- Assert
            Assert.Throws(typeof(UnableToLocateRecordId), () => repository.Read("nonexisting_record"));
        }

        [Test()]
        public void UnableLocateRecordUpdateExceptionTest()
        {
            //-- Assert
            Assert.Throws(typeof(UnableToUpdateRecord), () => repository.Update("nonexisting_record", new DataTestObject() { _EntityId = "nonexisting_record" }));
        }


        [Test()]
        public void IdentifierValueMismatchedExceptionTest()
        {
            //-- Assert
            Assert.Throws(typeof(IdentifierValueMismatchedException), () => repository.Update("Pepo", new DataTestObject()));
        }

        [Test()]
        public void UnableUpateRecordUpdateExceptionTest()
        {
            //-- Arrange
            var testObject = new DataTestObject() { Name = "Pepo", DateOfBirth = DateTime.Now, Age = 15 };

            //-- Act
            repository.Create("unupdatable", testObject);

            //-- Assert
            Assert.Throws(typeof(UnableToUpdateRecord), () => repository.Update("unupdatable", null));
        }

        [Test()]
        public void DeleteTest()
        {
            //-- Arrange
            var testObject = new DataTestObject() { Name = "Pepo to delete", DateOfBirth = DateTime.Now, Age = 15 };
            var id = $"toDelete{Guid.NewGuid()}";

            repository.Create(id, testObject);
            Assert.AreEqual(1, repository.GetRecords(id).Count());

            System.Threading.Thread.Sleep(100);

            //-- Act
            repository.Delete(id);

            //-- Assert
            Assert.AreEqual(0, repository.GetRecords(id).Count());
        }

        [Test()]
        public void GetAllRecordsTest()
        {
            //-- Arrange

            var testObject = new DataTestObject() { Name = "Pepo", DateOfBirth = DateTime.Now, Age = 15, _Created = new DateTime() };
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

            var testObject = new DataTestObject() { Name = "Pepo", DateOfBirth = DateTime.Now, Age = 15, _Created = new DateTime() };

            var records = repository.Queryable.Where(p => true).Select(p => p._EntityId).ToList();
            foreach (var id in records)
            {
                repository.Delete(id);
            }

            //-- Act
            repository.Create("testToFilter1", testObject);
            repository.Create("testToFilter2", new DataTestObject());
            repository.Create("testToFilter3", new DataTestObject());


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
            var testObject = new DataTestObject() { Name = "Pepo", DateOfBirth = DateTime.Now, Age = 15, _Created = new DateTime() };
            var testObjectAltered = new DataTestObjectAlteredStructure() { Name = "Pepo", DateOfBirth = DateTime.Now, Age = 15, _Created = new DateTime() };

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

            var testObject = new DataTestObject() { Name = "Pepo", DateOfBirth = DateTime.Now, Age = 15, _Created = new DateTime() };            

            //-- Act
            repository.Create("equality", testObject);
           


            //-- Act

            var actual = repository.Read("equality");

            //-- Assert.

            testObject.AllTypes.AssertEquality(actual.AllTypes);
        }

        [Test(), Order(1000)]
        public void DateTimeProviderTest()
        {
            //-- Arrange         
            var dateTimeProvider = new DummyDateTimeProvider();
            dateTimeProvider.SetDateTime = new DateTime(1976, 9, 1);
            DateTimeProviders.DateTimeProvider = dateTimeProvider;
            var testObject = new DataTestObject() { Name = "SimulatedTimePepo", DateOfBirth = DateTime.Now, Age = 15, _Created = new DateTime() };

            
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
            public DateTime SetDateTime { private get;  set; }
            public override DateTime Now { get { return SetDateTime; } }
        }
    }
}