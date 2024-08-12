using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
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

        protected void EnsureDatabaseExists(
            IDocumentStore store,
            string database = null,
            bool createDatabaseIfNotExists = true
        )
        {
            database = database ?? store.Database;

            if (string.IsNullOrWhiteSpace(database))
                throw new ArgumentException(
                    "Value cannot be null or whitespace.",
                    nameof(database)
                );

            try
            {
                store.Maintenance.ForDatabase(database).Send(new GetStatisticsOperation());
            }
            catch (DatabaseDoesNotExistException)
            {
                if (createDatabaseIfNotExists == false)
                    throw;

                try
                {
                    store.Maintenance.Server.Send(
                        new CreateDatabaseOperation(new DatabaseRecord(database))
                    );
                }
                catch (ConcurrencyException)
                {
                    // The database was already created before calling CreateDatabaseOperation
                }
            }
        }

        public RavenDbRepository(RavenDbRepositorySettingsBase<T> parameters)
        {
            var existing = SharedData.Stores.SingleOrDefault(x =>
                x.Database == parameters.Store.Database
            );

            if (existing != null)
            {
                _store = existing;
            }
            else
            {
                SharedData.Stores.Add(parameters.Store);
                _store = parameters.Store;
            }

            EnsureDatabaseExists(_store, parameters.Store.Database);
        }

        protected override void CreateNvi(string identifier, T data)
        {
            using (var session = _store.OpenSession())
            {
                T entity = session.Query<T>().SingleOrDefault(x => x._EntityId == identifier);
                if (entity != null)
                    throw new DuplicateIdException(
                        $"Record with ID {identifier} already exists in this collection.",
                        null
                    );

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
                    throw new UnableToLocateRecordId(
                        $"Unable to locate record with ID: {identifier} in {_store}.",
                        null
                    );

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
                        throw new UnableToUpdateRecord(
                            $"Unable to locate record with ID: {identifier} in {_store}.",
                            null
                        );

                    session.Advanced.Evict(entity);
                    session.Store(data, identifier);
                    session.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new UnableToUpdateRecord(
                    $"Unable to update record ID:{identifier} in {_store}.",
                    ex
                );
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

        protected override long CountNvi =>
            _store.Maintenance.Send(new GetStatisticsOperation()).CountOfDocuments;

        protected override IEnumerable<T> GetRecordsNvi(
            string identifier,
            int limit,
            int skip,
            eSearchMode searchMode
        )
        {
            using (var session = _store.OpenSession())
            {
                if (
                    string.IsNullOrEmpty(identifier)
                    || string.IsNullOrWhiteSpace(identifier)
                    || identifier == "*"
                )
                {
                    return session.Query<T>().Skip(skip).Take(limit).ToArray();
                }
                else
                {
                    switch (searchMode)
                    {
                        case eSearchMode.StartsWith:
                            return session
                                .Query<T>()
                                .Where(x => x._EntityId.StartsWith(identifier))
                                .Skip(skip)
                                .Take(limit)
                                .ToArray();
                        case eSearchMode.Contains:
                            return session
                                .Query<T>()
                                .Search(x => x._EntityId, $"*{identifier}*")
                                .Skip(skip)
                                .Take(limit)
                                .ToArray();
                        case eSearchMode.Exact:
                        default:
                            return session
                                .Query<T>()
                                .Where(x => x._EntityId == identifier)
                                .Skip(skip)
                                .Take(limit)
                                .ToArray();
                    }
                }
            }
        }

        protected override long FilteredCountNvi(string identifier, eSearchMode searchMode)
        {
            if (identifier == "*")
            {
                return _store
                    .Maintenance.Send(new GetCollectionStatisticsOperation())
                    .CountOfDocuments;
            }
            else
            {
                using (var session = _store.OpenSession())
                {
                    switch (searchMode)
                    {
                        case eSearchMode.StartsWith:
                            return session
                                .Query<T>()
                                .Where(x => x._EntityId.StartsWith(identifier))
                                .Count();
                        case eSearchMode.Contains:
                            return session
                                .Query<T>()
                                .Search(x => x._EntityId, $"*{identifier}*")
                                .Count();
                        case eSearchMode.Exact:
                        default:
                            return session.Query<T>().Where(x => x._EntityId == identifier).Count();
                    }
                }
            }
        }

        protected override bool ExistsNvi(string identifier)
        {
            using (var session = _store.OpenSession())
            {
                T entity = session.Load<T>(identifier);
                return entity != null;
            }
        }

        public override IQueryable<T> Queryable
        {
            get
            {
                using (var session = _store.OpenSession())
                {
                    return session.Query<T>().ToList().AsQueryable();
                }
            }
        }
    }
}
