using System;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.MongoDb;

namespace TcoDataUnitTests
{
    public class MongoDbSecuredRepositoryTest : RepositoryBaseTests
    {
        public override void Init()
        {
            if (this.repository == null)
            {
                var a = new DataTestObject();
                var credentials = new MongoDbCredentials(
                    "admin",
                    "TcOpenAdmin",
                    "changeMeToAStrongPassword"
                );
                var parameters = new MongoDbRepositorySettings<DataTestObject>(
                    @"mongodb://localhost:27018",
                    "TestDataBase",
                    "TestCollection",
                    credentials
                );
                var parametersAltered =
                    new MongoDbRepositorySettings<DataTestObjectAlteredStructure>(
                        @"mongodb://localhost:27018",
                        "TestDataBase",
                        "TestCollection",
                        credentials
                    );
                this.repository = Repository.Factory<DataTestObject>(parameters);

                this.repository_altered_structure =
                    Repository.Factory<DataTestObjectAlteredStructure>(parametersAltered);

                this.repository.OnCreate = (id, data) =>
                {
                    data._Created = DateTimeProviders.DateTimeProvider.Now;
                    data._Modified = DateTimeProviders.DateTimeProvider.Now;
                };
                this.repository.OnUpdate = (id, data) =>
                {
                    data._Modified = DateTimeProviders.DateTimeProvider.Now;
                };
            }

            foreach (var item in this.repository.GetRecords("*"))
            {
                repository.Delete(item._EntityId);
            }
        }
    }
}
