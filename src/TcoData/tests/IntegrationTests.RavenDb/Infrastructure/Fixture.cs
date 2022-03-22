using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.TestDriver;
using TcOpen.Inxton.RavenDb;

namespace IntegrationTests.RavenDb.Infrastructure
{
    public class Fixture : RavenTestDriver
    {
        public readonly IDocumentStore Store;

        static Fixture()
        {
            var testServerOptions = new TestServerOptions
            {
                FrameworkVersion = null // user latest, optional
            };

            ConfigureServer(testServerOptions);
        }

        public Fixture()
        {
            Store = this.GetDocumentStore();
            IndexCreation.CreateIndexes(typeof(RavenDbRepository<>).Assembly, Store);
        }
    }
}
