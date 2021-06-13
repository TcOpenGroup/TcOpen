using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.RavenDb;

namespace TcoDataUnitTests
{
    public class RavenDbTestRepositorySettings<T> : RavenDbRepositorySettingsBase<T> where T : IBrowsableDataObject
    {
        public readonly Fixture Fixture;
        
        public RavenDbTestRepositorySettings()
        {
            Fixture = new Fixture();
            Store = Fixture.Store;
        }
    }
}
