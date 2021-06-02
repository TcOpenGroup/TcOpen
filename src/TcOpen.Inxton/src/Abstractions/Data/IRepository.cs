using System.Collections.Generic;
using System.Linq;

namespace TcOpen.Inxton.Abstractions.Data
{
    public interface IRepository
    {
        long Count { get; }
        void Create(string identifier, object data);
        void Delete(string identifier);
        long FilteredCount(string id);
        dynamic Read(string identifier);
        void Update(string identifier, object data);
    }

    public interface IRepository<T> where T : IBrowsableDataObject
    {
        long Count { get; }
        IQueryable<T> Queryable { get; }

        void Create(string identifier, T data);
        void Delete(string identifier);
        long FilteredCount(string id);
        IEnumerable<T> GetRecords(string identifier = "*", int limit = 100, int skip = 0);
        T Read(string identifier);
        void Update(string identifier, T data);
    }
}
