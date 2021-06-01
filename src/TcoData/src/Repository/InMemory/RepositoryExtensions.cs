using TcOpen.Inxton.Abstractions.Data;

namespace TcoData.Repository.InMemory
{
    public static class Repository
    {
        public static IRepository<T> Factory<T>(this InMemoryRepositorySettings<T> parameters) where T : IBrowsableDataObject
        {
            return new InMemoryRepository<T>(parameters);
        }
    }
}
