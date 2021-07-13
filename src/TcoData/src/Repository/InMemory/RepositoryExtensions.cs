using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.Data.InMemory
{
    public static class Repository
    {
        public static IRepository<T> Factory<T>(this InMemoryRepositorySettings<T> parameters) where T : IBrowsableDataObject
        {
            return new InMemoryRepository<T>(parameters);
        }
    }
}
