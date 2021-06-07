using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.RavenDb
{
    public class RavenDbRepository<T> : RepositoryBase<T>
        where T : IBrowsableDataObject
    {
        private readonly IDocumentStore _store;

        public RavenDbRepository(RavenDbRepositorySettings<T> parameters)
        {
            _store = parameters.Store;
        }

        protected override void CreateNvi(string identifier, T data)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(data, identifier);
                session.SaveChanges();
            }
        }

        protected override T ReadNvi(string identifier)
        {
            using (var session = _store.OpenSession())
            {
                return session.Load<T>(identifier);
            }
        }

        protected override void UpdateNvi(string identifier, T data)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(data, identifier);
                session.SaveChanges();
            }
        }

        protected override void DeleteNvi(string identifier)
        {
            using (var session = _store.OpenSession())
            {
                session.Delete(identifier);
                session.SaveChanges();
            }
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
