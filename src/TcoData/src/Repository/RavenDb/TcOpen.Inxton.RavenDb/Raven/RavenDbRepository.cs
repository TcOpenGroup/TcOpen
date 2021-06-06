using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.RavenDb.Raven
{
    public class RavenDbRepository<T> : RepositoryBase<T>
        where T : IBrowsableDataObject
    {
        protected override void CreateNvi(string identifier, T data)
        {
            throw new NotImplementedException();
        }

        protected override T ReadNvi(string identifier)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateNvi(string identifier, T data)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteNvi(string identifier)
        {
            throw new NotImplementedException();
        }

        protected override long CountNvi { get; }

        protected override IEnumerable<T> GetRecordsNvi(string identifierContent, int limit, int skip)
        {
            throw new NotImplementedException();
        }

        protected override long FilteredCountNvi(string identifierContent)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<T> Queryable { get; }
    }
}
