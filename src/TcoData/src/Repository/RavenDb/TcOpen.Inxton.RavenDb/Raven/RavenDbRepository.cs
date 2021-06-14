using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.RavenDb
{
    public static class SharedData
    {
        public static readonly List<IDocumentStore> Stores = new List<IDocumentStore>();
    }

    public class RavenDbRepository<T> : RepositoryBase<T>
        where T : IBrowsableDataObject
    {
        private readonly IDocumentStore _store;

        public RavenDbRepository(RavenDbRepositorySettingsBase<T> parameters)
        {
            var existing = SharedData.Stores.SingleOrDefault(x => x.Database == parameters.Store.Database);

            if (existing != null)
            {
                _store = existing;
            }
            else
            {
                SharedData.Stores.Add(parameters.Store);
                _store = parameters.Store;
            }
        }

        protected override void CreateNvi(string identifier, T data)
        {
            using (var session = _store.OpenSession())
            {
                T entity = session.Query<T>().SingleOrDefault(x => x._EntityId == identifier);
                if (entity != null)
                    throw new DuplicateIdException($"Record with ID {identifier} already exists in this collection.", null);

                session.Store(data, identifier);
                session.Advanced.WaitForIndexesAfterSaveChanges();
                session.SaveChanges();
            }
        }

        protected override T ReadNvi(string identifier)
        {
            using (var session = _store.OpenSession())
            {
                T entity = session.Load<T>(identifier);

                if (entity == null)
                    throw new UnableToLocateRecordId($"Unable to locate record with ID: {identifier} in {_store}.", null);

                return entity;
            }
        }

        protected override void UpdateNvi(string identifier, T data)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    T entity = session.Load<T>(identifier);

                    if (entity == null)
                        throw new UnableToUpdateRecord($"Unable to locate record with ID: {identifier} in {_store}.", null);

                    session.Advanced.Evict(entity);
                    session.Store(data, identifier);
                    session.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new UnableToUpdateRecord($"Unable to update record ID:{identifier} in {_store}.", ex);
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

        protected override IEnumerable<T> GetRecordsNvi(string identifier, int limit, int skip)
        {
            using (var session = _store.OpenSession())
            {
                if (identifier == "*")
                {
                    return session.Query<T>()
                        .Skip(skip)
                        .Take(limit)
                        .ToArray();
                }
                else
                {
                    return session.Query<T>()
                    .Search(x => x._EntityId, "*" + identifier + "*")
                    .Skip(skip)
                    .Take(limit)
                    .ToArray();                    
                }
            }
        }

        protected override long FilteredCountNvi(string identifierContent)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<T> Queryable { get; }
    }
}
