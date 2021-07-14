using TcoDataTests;
using NUnit.Framework;
using TcOpen.Inxton.Data.MongoDb;

namespace TcoDataUnitTests
{
    [TestFixture, Timeout(5000)]
    public class TcoDataExchangeMongoDbTest : TcoDataExchangeBaseTest
    {
        public override void Init()
        {
            var parameters = new MongoDbRepositorySettings<PlainstProcessData>("mongodb://localhost:27017", "TestDataBase", "TcoDataExchangeMongoDbTest");
            Repository = new MongoDbRepository<PlainstProcessData>(parameters);
            foreach(var record in Repository.GetRecords())
            {
                Repository.Delete(record._EntityId);
            }
        }
    }
}
