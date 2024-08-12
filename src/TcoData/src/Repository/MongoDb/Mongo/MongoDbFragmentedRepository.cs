using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
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
    public class MongoDbFragmentedRepository<T, TFragment> : RepositoryBase<T>
        where T : IBrowsableDataObject
    {
        private IMongoCollection<T> collection;
        private List<Expression<Func<T, TFragment>>> _fragmentsExpresion;
        List<(
            Expression<Func<T, TFragment>> FragmentExpression,
            TFragment FragmentData
        )> _updateFragments;
        private readonly string location;

        /// <summary>
        /// Creates new instance of <see cref="MongoDbRepository{T}"/>.
        /// </summary>
        /// <param name="parameters">Repository settings</param>
        /// <param name="updateFragments">Fragments and data</param>
        public MongoDbFragmentedRepository(
            MongoDbRepositorySettings<T> parameters,
            List<(
                Expression<Func<T, TFragment>> FragmentExpression,
                TFragment FragmentData
            )> updateFragments
        )
        {
            location = parameters.GetConnectionInfo();
            this.collection = parameters.Collection;
            _updateFragments = updateFragments;
        }

        /// <summary>
        /// Creates new instance of <see cref="MongoDbRepository{T}"/>.
        /// </summary>
        /// <param name="parameters">Repository settings</param>
        /// <param name="fragmentsExpression">Expressions fragments witch will be added into data exchange </param>
        public MongoDbFragmentedRepository(
            MongoDbRepositorySettings<T> parameters,
            List<Expression<Func<T, TFragment>>> fragmentsExpression
        )
        {
            location = parameters.GetConnectionInfo();
            this.collection = parameters.Collection;
            _fragmentsExpresion = fragmentsExpression;
        }

        private bool RecordExists(string identifier)
        {
            return collection.Find(p => p._EntityId == identifier).Count() >= 1;
        }

        protected override void CreateNvi(string identifier, T data)
        {
            try
            {
                var x = System.Threading.Thread.CurrentThread.GetApartmentState();

                if (RecordExists(identifier))
                {
                    throw new DuplicateIdException(
                        $"Record with ID {identifier} already exists in this collection.",
                        null
                    );
                }

                data._recordId = ObjectId.GenerateNewId();

                collection.InsertOne(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void DeleteNvi(string identifier)
        {
            collection.DeleteOne(p => p._EntityId == identifier);
        }

        protected override long FilteredCountNvi(string id, eSearchMode searchMode)
        {
            var filetered = new List<T>();
            FilterDefinition<T> filter;

            var filterExpresion = ParseIdentifierForRegularExpression(id);

            switch (searchMode)
            {
                case eSearchMode.StartsWith:
                    filter = Builders<T>.Filter.Regex(
                        p => p._EntityId,
                        new BsonRegularExpression($"^{filterExpresion}", "")
                    );
                    break;
                case eSearchMode.Contains:
                    filter = Builders<T>.Filter.Regex(
                        p => p._EntityId,
                        new BsonRegularExpression($".*{filterExpresion}", "")
                    );
                    break;
                case eSearchMode.Exact:
                default:
                    filter = Builders<T>.Filter.Eq(p => p._EntityId, id);
                    break;
            }

            if (id == "*" || string.IsNullOrWhiteSpace(id))
            {
#pragma warning disable CS0618 // CountDocuments is very slow compared to Count() even though Count is obsolete.
                return collection.Find(new BsonDocument()).Count();
            }
            else
            {
                return collection.Find(filter).Count();
            }
        }
#pragma warning restore CS0618


        protected override IEnumerable<T> GetRecordsNvi(
            string identifier,
            int limit,
            int skip,
            eSearchMode searchMode
        )
        {
            var filetered = new List<T>();
            FilterDefinition<T> filter;

            var filterExpresion = ParseIdentifierForRegularExpression(identifier);

            switch (searchMode)
            {
                case eSearchMode.StartsWith:
                    filter = Builders<T>.Filter.Regex(
                        p => p._EntityId,
                        new BsonRegularExpression($"^{filterExpresion}", "")
                    );
                    break;
                case eSearchMode.Contains:
                    filter = Builders<T>.Filter.Regex(
                        p => p._EntityId,
                        new BsonRegularExpression($".*{filterExpresion}", "")
                    );
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
                return collection.Find(filter).Limit(limit).Skip(skip).ToList();
            }
        }

        /// <summary>
        /// Parses input string, so it is evaluated as verbatim string and not as regular expression. All special ascii characters are prefixed with "\".
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        private string ParseIdentifierForRegularExpression(string identifier)
        {
            var result = string.Empty;

            if (string.IsNullOrWhiteSpace(identifier))
            {
                return result;
            }

            foreach (var character in identifier)
            {
                if (
                    character <= 47
                    || (character >= 58 && character <= 64)
                    || (character >= 91 && character <= 96)
                    || (character >= 123 && character <= 126)
                )
                {
                    result += @"\";
                }
                result += character.ToString();
            }

            return result;
        }

        protected override T ReadNvi(string identifier)
        {
            try
            {
                var record = collection.Find(p => p._EntityId == identifier).FirstOrDefault();
                if (record == null)
                {
                    throw new UnableToLocateRecordId(
                        $"Unable to locate record with ID: {identifier} in {location}.",
                        null
                    );
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
            if (_fragmentsExpresion != null)
                UpdateNvi(identifier, _fragmentsExpresion, data);
            else if (_updateFragments != null)
                UpdateNvi(identifier, _updateFragments);
            else
                Replace(identifier, data);
        }

        protected void UpdateNvi(
            string identifier,
            List<Expression<Func<T, TFragment>>> fragmentsExpression,
            T data
        )
        {
            try
            {
                if (!RecordExists(identifier))
                {
                    throw new UnableToLocateRecordId(
                        $"Unable to locate record with ID: {identifier} in {location}.",
                        null
                    );
                }

                var filter = Builders<T>.Filter.Eq("_EntityId", identifier);

                collection.UpdateOne(
                    filter,
                    ConvertExpressionsToUpdateDefinition(fragmentsExpression, data)
                );
            }
            catch (Exception ex)
            {
                throw new UnableToUpdateRecord(
                    $"Unable to update record ID:{identifier} in {location}.",
                    ex
                );
            }
        }

        static UpdateDefinition<T> ConvertExpressionsToUpdateDefinition(
            List<Expression<Func<T, TFragment>>> expressions,
            T data
        )
        {
            var updateBuilder = Builders<T>.Update;
            var updateDefinitions = new List<UpdateDefinition<T>>();

            foreach (var expression in expressions)
            {
                var body = expression.Body as MemberInitExpression;

                if (body == null)
                {
                    throw new ArgumentException(
                        "Invalid expression format. Expected MemberInitExpression."
                    );
                }

                foreach (var binding in body.Bindings)
                {
                    var memberAssignment = binding as MemberAssignment;

                    if (memberAssignment != null)
                    {
                        var propertyName = GetPropertyName(memberAssignment.Member);
                        Type objectType = typeof(T);
                        PropertyInfo propertyInfo = objectType.GetProperty(propertyName);
                        var propertyValue = propertyInfo.GetValue(data);
                        var updateDefinition = updateBuilder.Set(propertyName, propertyValue);
                        updateDefinitions.Add(updateDefinition);
                    }
                }
            }

            return updateBuilder.Combine(updateDefinitions);
        }

        // Helper method to get the name of a property from a member expression
        static string GetPropertyName(MemberInfo member)
        {
            var propertyInfo = member as PropertyInfo;

            if (propertyInfo == null)
            {
                throw new ArgumentException("Member is not a PropertyInfo.");
            }

            return propertyInfo.Name;
        }

        // Helper method to evaluate expression and retrieve property value
        static object GetValueFromExpression(Expression expression)
        {
            var lambda = Expression.Lambda(expression).Compile();
            return lambda.DynamicInvoke();
        }

        protected void UpdateNvi(
            string identifier,
            List<(
                Expression<Func<T, TFragment>> FragmentExpression,
                TFragment FragmentData
            )> fragments
        )
        {
            try
            {
                if (!RecordExists(identifier))
                {
                    throw new UnableToLocateRecordId(
                        $"Unable to locate record with ID: {identifier} in {location}.",
                        null
                    );
                }

                var filter = Builders<T>.Filter.Eq("_EntityId", identifier);
                var updateDefinitions = new List<UpdateDefinition<T>>();

                foreach (var item in fragments)
                {
                    updateDefinitions.Add(
                        Builders<T>.Update.Set(item.FragmentExpression, item.FragmentData)
                    );
                }

                collection.UpdateOne(filter, Builders<T>.Update.Combine(updateDefinitions));
            }
            catch (Exception ex)
            {
                throw new UnableToUpdateRecord(
                    $"Unable to update record ID:{identifier} in {location}.",
                    ex
                );
            }
        }

        protected void Replace(string identifier, T data)
        {
            try
            {
                if (!RecordExists(identifier))
                {
                    throw new UnableToLocateRecordId(
                        $"Unable to locate record with ID: {identifier} in {location}.",
                        null
                    );
                }

                collection.ReplaceOne(p => p._EntityId == identifier, data);
            }
            catch (Exception ex)
            {
                throw new UnableToUpdateRecord(
                    $"Unable to update record ID:{identifier} in {location}.",
                    ex
                );
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
