using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.MongoDb;

namespace TcoDataUnitTests
{
    public class FloatSerializerObject : DataTestObject
    {
        public List<float> Floats { get; set; }
    }

    [TestFixture()]
    // Due to some serialization error when running more tests at once, this has to run last.
    public class ZFloatSerializerMongoTests
    {
        private List<float> Floats = new List<float>
        {
            float.MinValue,
            float.MaxValue,
            float.PositiveInfinity,
            float.NegativeInfinity,
            0.0f,
            0.5f,
            14.5f,
            3.1415926535897932384626433832795028841971693993751058209749445923f,
            (float.MaxValue /2),
            (float.MaxValue /4),
            (float.MaxValue /8),
            (float.MaxValue /16),
            (float.MaxValue /32),
            float.Epsilon
        };

        protected IRepository<FloatSerializerObject> repository;

        [OneTimeSetUp]
        public void Init()
        {
            if (this.repository == null)
            {
                var a = new FloatSerializerObject();
                var parameters = new MongoDbRepositorySettings<FloatSerializerObject>("mongodb://localhost:27017", "TestDataBase", nameof(ZFloatSerializerMongoTests));
                this.repository = Repository.Factory<FloatSerializerObject>(parameters);
                foreach (var item in this.repository.GetRecords("*"))
                {
                    repository.Delete(item._EntityId);
                }
            }
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            var toCreate = Enumerable.Range(0, 10)
                .Select(x => new
                {
                    testObject = new FloatSerializerObject()
                    {
                        Name = "Pepo" + x, 
                        DateOfBirth = DateTime.Now,
                        Age = 15 + x,
                        Floats = Floats 
                    },
                    id = $"test_{Guid.NewGuid()}"
                }
                ).ToList();

            //-- Act
            foreach (var x in toCreate)
            {
                repository.Create(x.id, x.testObject);
            }

            //-- Assert
            Assert.True(repository.GetRecords().Count() == toCreate.Count);
        }

        [Test, Order(2)]
        public void ReadTest()
        {
            var x = repository.GetRecords().ToList();
            Assert.AreEqual(x.First().Floats, Floats);
            //try to alter the values in robo3t and see if it reads again.
        }

    }
}
