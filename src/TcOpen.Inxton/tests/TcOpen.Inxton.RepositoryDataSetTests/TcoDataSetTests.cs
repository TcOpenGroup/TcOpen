using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Raven.Embedded;
using TcOpen.Inxton.RavenDb;
using TcOpen.Inxton.RepositoryDataSet;

namespace TcOpen.Inxton.RepositoryDataSetTests
{
    public class TcoDataSetTests
    {
        public RepositoryDataSetHandler<ProductionItem> _productionPlanHandler { get; private set; }

        [SetUp]
        public void Setup()
        {
            _productionPlanHandler = RepositoryDataSetHandler<ProductionItem>.CreateSet(
                new RavenDbRepository<EntitySet<ProductionItem>>(
                    new RavenDbRepositorySettings<EntitySet<ProductionItem>>(
                        new string[] { "http://127.0.0.1:8080" },
                        "ProductionPlanTest",
                        "",
                        ""
                    )
                )
            );
            _productionPlanHandler.Repository.OnCreate = (id, data) =>
            {
                data._Created = DateTime.Now;
                data._Modified = DateTime.Now;
            };
            _productionPlanHandler.Repository.OnUpdate = (id, data) =>
            {
                data._Modified = DateTime.Now;
            };
            //remove all records
            var records = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._EntityId)
                .ToList();
            records.ForEach(p => _productionPlanHandler.Repository.Delete(p));
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
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
        }

        [Test]
        public void create_single_set_data()
        {
            var setId = "testId";

            EntitySet<ProductionItem> entitySet = new EntitySet<ProductionItem>();

            _productionPlanHandler.Create(setId, entitySet);

            var setIdVerif = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._EntityId)
                .First();
            Assert.AreEqual(1, _productionPlanHandler.Repository.Queryable.Count());
            Assert.AreEqual(setId, setIdVerif);
        }

        [Test]
        [TestCase(3)]
        [TestCase(5)]
        public void create_single_set_data(int noOfTests)
        {
            foreach (var item in Enumerable.Range(1, noOfTests))
            {
                var setId = "testId" + item.ToString();

                EntitySet<ProductionItem> entitySet = new EntitySet<ProductionItem>();

                _productionPlanHandler.Create(setId, entitySet);

                var setIdVerif = _productionPlanHandler
                    .Repository.Queryable.Where(p => p._EntityId == setId)
                    .Select(p => p._EntityId)
                    .First();

                Assert.AreEqual(setId, setIdVerif);
            }
            Assert.AreEqual(noOfTests, _productionPlanHandler.Repository.Queryable.Count());
        }

        [Test]
        [Repeat(5)]
        public void create_single_set_data_many_times()
        {
            var setId = "testId";

            EntitySet<ProductionItem> entitySet = new EntitySet<ProductionItem>();

            _productionPlanHandler.Create(setId, entitySet);

            var setIdVerif = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._EntityId)
                .First();
            Assert.AreEqual(1, _productionPlanHandler.Repository.Queryable.Count());
            Assert.AreEqual(setId, setIdVerif);
        }

        [Test]
        public void update_single_set_data()
        {
            var setId = "testId";

            EntitySet<ProductionItem> entitySet = new EntitySet<ProductionItem>();

            _productionPlanHandler.Create(setId, entitySet);

            var created = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._Created)
                .First();
            var modified = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._Modified)
                .First();

            var collection = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p.Items)
                .First();

            Assert.AreEqual(0, collection.Count);

            entitySet.Items.Add(new ProductionItem());

            _productionPlanHandler.Update(setId, entitySet);

            var createdUpdated = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._Created)
                .First();
            var modifiedUpdated = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._Modified)
                .First();
            var collectionUpdated = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p.Items)
                .First();

            Assert.AreEqual(created, created);
            Assert.AreNotEqual(modified, modifiedUpdated);
            Assert.AreEqual(1, collectionUpdated.Count);
        }

        [Test]
        [Repeat(5)]
        public void update_single_set_data_many_times()
        {
            var setId = "testId";

            EntitySet<ProductionItem> entitySet = new EntitySet<ProductionItem>();

            _productionPlanHandler.Create(setId, entitySet);

            var created = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._Created)
                .First();
            var modified = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._Modified)
                .First();

            var collection = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p.Items)
                .First();

            Assert.AreEqual(0, collection.Count);

            entitySet.Items.Add(new ProductionItem());

            _productionPlanHandler.Update(setId, entitySet);

            var createdUpdated = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._Created)
                .First();
            var modifiedUpdated = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p._Modified)
                .First();
            var collectionUpdated = _productionPlanHandler
                .Repository.Queryable.Where(p => true)
                .Select(p => p.Items)
                .First();

            Assert.AreEqual(created, created);
            Assert.AreNotEqual(modified, modifiedUpdated);
            Assert.AreEqual(1, collectionUpdated.Count);
        }
    }

    public class ProductionItem : IDataSetItems
    {
        private string key;

        /// <summary>
        /// Gets or sets the key of this instruction item (list of process set).
        /// </summary>
        public string Key
        {
            get { return key; }
            set
            {
                if (key == value)
                {
                    return;
                }

                key = value;
            }
        }

        private int reqCount;

        /// <summary>
        /// Gets or sets required Ccounter value
        /// </summary>
        public int RequiredCount
        {
            get => reqCount;
            set
            {
                if (reqCount == value)
                {
                    return;
                }

                reqCount = value;
            }
        }

        private int actualCount;

        /// <summary>
        /// Gets or sets actual counter value.
        /// </summary>
        public int ActualCount
        {
            get => actualCount;
            set
            {
                if (actualCount == value)
                {
                    return;
                }

                actualCount = value;
            }
        }

        private string description;

        /// <summary>
        /// gets or sets additional information.
        /// </summary>
        public string Description
        {
            get => description;
            set
            {
                if (description == value)
                {
                    return;
                }

                description = value;
            }
        }
    }
}
