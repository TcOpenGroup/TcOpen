using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.Data.MongoDb
{
    /// <summary>
    /// Provides access to basic operations for MongoDB.
    /// To use this code, mongo database must run somewhere. To start MongoDB locally you can use following code
    /// 
    /// Start MongoDB without authentication 
    ///     <code>
    ///         "C:\Program Files\MongoDB\Server\4.4\bin\mongod.exe"  --dbpath C:\DATA\DB446\ 
    ///     </code>
    ///     
    /// Start MongoDB with authentication. You don't have to use the "--port" attribute or use a different "--dbpath". The only 
    /// reason why would you want to run authenticated database on a different dbpath and port simultaneously is if they're running
    /// on the same machine.
    /// 
    /// More info about the use credentials <see cref="MongoDbCredentials"/>
    /// 
    ///     <code>
    ///         "C:\Program Files\MongoDB\Server\4.4\bin\mongod.exe"  --dbpath C:\DATA\DB446_AUTH\ --auth --port 27018
    ///     </code>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDbRepository<T> : RepositoryBase<T>
        where T : IBrowsableDataObject
    {
        private IMongoCollection<T> collection;
        private readonly string location;

        /// <summary>
        /// Creates new instance of <see cref="MongoDbRepository{T}"/>.
        /// </summary>
        /// <param name="parameters">Repository settings</param>
        public MongoDbRepository(MongoDbRepositorySettings<T> parameters)
        {
            location = parameters.GetConnectionInfo();
            this.collection = parameters.Collection;
        }

        private bool RecordExists(string identifier)
        { return collection.Find(p => p._EntityId == identifier).Count() >= 1; }

        protected override void CreateNvi(string identifier, T data)
        {
            try
            {
                var x = System.Threading.Thread.CurrentThread.GetApartmentState();
              
                if (RecordExists(identifier))
                {
                    throw new DuplicateIdException($"Record with ID {identifier} already exists in this collection.",
                                                   null);
                }

                data._recordId = ObjectId.GenerateNewId();

                collection.InsertOne(data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void DeleteNvi(string identifier) { collection.DeleteOne(p => p._EntityId == identifier); }

        protected override long FilteredCountNvi(string id)
        {
            var filter = Builders<T>.Filter.Regex(p => p._EntityId, new BsonRegularExpression($"^{id}", ""));
            if (id == "*" || string.IsNullOrWhiteSpace(id))
            {
#pragma warning disable CS0618 // CountDocuments is very slow compared to Count() even though Count is obsolete. 
                return collection
                    .Find(new BsonDocument())
                    .Count();
            }
            else
            {
                return collection
                    .Find(filter)
                    .Count();
            }
        }
#pragma warning restore CS0618 


        protected override IEnumerable<T> GetRecordsNvi(string identifier, int limit, int skip, eSearchMode searchMode)
        {
            var filetered = new List<T>();
            FilterDefinition<T> filter;

            switch (searchMode)
            {             
                case eSearchMode.StartsWith:
                    filter = Builders<T>.Filter.Regex(p => p._EntityId, new BsonRegularExpression($"^{identifier}", ""));
                    break;
                case eSearchMode.Contains:
                    filter = Builders<T>.Filter.Regex(p => p._EntityId, new BsonRegularExpression($".*{identifier}", ""));
                    break;
                case eSearchMode.Exact:                    
                default:
                    filter = Builders<T>.Filter.Eq(p => p._EntityId, identifier);
                    break;
            }

            
            if (identifier == "*" || string.IsNullOrWhiteSpace(identifier))
            {
                return collection
                    .Find(new BsonDocument())
                    .Sort(new SortDefinitionBuilder<T>().Descending("$natural"))
                    .Limit(limit)
                    .Skip(skip)
                    .ToList();
            }
            else
            {
                return collection
                    .Find(filter)
                    .Limit(limit)
                    .Skip(skip)
                    .ToList();
            }
        }

        protected override T ReadNvi(string identifier)
        {
            try
            {
                var record = collection.Find(p => p._EntityId == identifier).FirstOrDefault();
                if (record == null)
                {
                    throw new UnableToLocateRecordId($"Unable to locate record with ID: {identifier} in {location}.",
                                                     null);
                }

                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void UpdateNvi(string identifier, T data)
        {
            try
            {
                if (!RecordExists(identifier))
                {
                    throw new UnableToLocateRecordId($"Unable to locate record with ID: {identifier} in {location}.",
                                                     null);
                }

                collection.ReplaceOne(p => p._EntityId == identifier, data);
            }
            catch (Exception ex)
            {
                throw new UnableToUpdateRecord($"Unable to update record ID:{identifier} in {location}.", ex);
            }
        }

        protected override bool ExistsNvi(string identifier)
        {
            return RecordExists(identifier);
        }

        protected override long CountNvi => collection.Count(new BsonDocument());

        /// <summary>
        /// Gets the <see cref="IMongoCollection{T}"/> of this repository.
        /// </summary>
        public IMongoCollection<T> Collection => this.collection;

        public override IQueryable<T> Queryable => this.collection.AsQueryable();
    }
}
