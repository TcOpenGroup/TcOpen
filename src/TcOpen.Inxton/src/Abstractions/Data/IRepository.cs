using System.Collections.Generic;
using System.Linq;

namespace TcOpen.Inxton.Data
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

    public delegate void OnCreateDelegate<T>(string id, T data);
    public delegate void OnUpdateDelegate<T>(string id, T data);

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
        OnCreateDelegate<T> OnCreate { get; set; }
        OnUpdateDelegate<T> OnUpdate { get; set; }
    }
}
