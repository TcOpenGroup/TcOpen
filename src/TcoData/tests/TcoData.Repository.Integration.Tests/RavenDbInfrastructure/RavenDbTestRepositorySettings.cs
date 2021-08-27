using TcOpen.Inxton.Data;
using TcOpen.Inxton.RavenDb;

namespace TcoDataUnitTests
{
    public static class SharedData
    {
        public static Fixture Fixture = null;
    }

    public class RavenDbTestRepositorySettings<T> : RavenDbRepositorySettingsBase<T> where T : IBrowsableDataObject
    {
        public RavenDbTestRepositorySettings()
        {
            if (SharedData.Fixture == null)
                SharedData.Fixture = new Fixture();
            
            Store = SharedData.Fixture.Store;
        }
    }
}
