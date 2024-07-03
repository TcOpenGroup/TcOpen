using System.Security.Cryptography.X509Certificates;
using IntegrationTests.RavenDb.Infrastructure;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.RavenDb;

namespace IntegrationTests.RavenDb
{
    public class RavenDbTestRepositorySettings<T> : RavenDbRepositorySettingsBase<T>
        where T : IBrowsableDataObject
    {
        public readonly Fixture Fixture;

        public RavenDbTestRepositorySettings()
        {
            Fixture = new Fixture();
            Store = Fixture.Store;
        }
    }
}
