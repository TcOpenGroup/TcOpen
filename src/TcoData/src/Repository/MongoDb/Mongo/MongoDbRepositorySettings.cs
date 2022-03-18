using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using TcOpen.Inxton.Data;


namespace TcOpen.Inxton.Data.MongoDb
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDbRepositorySettings<T> : RepositorySettings  where T : IBrowsableDataObject 
    {
        private string _databaseName;
        private string _collectionName;

        /// <summary>
        /// Creates new instance of <see cref="MongoDbRepositorySettings{T}"/> for a <see cref="MongoDbRepository{T}"/> with NON-SECURED access.
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="databaseName">Database name</param>
        /// <param name="collectionName">Collection name</param>
        /// <param name="credentials">Credentials</param>
        public MongoDbRepositorySettings(string connectionString, string databaseName, string collectionName)
        {            
            SetupSerialisationAndMapping();
            Client = GetClient(connectionString);
            Database = GetDatabase(databaseName);
            Collection = GetCollection(collectionName);
        }


        /// <summary>
        /// Creates new instance of <see cref="MongoDbRepositorySettings{T}"/> for a <see cref="MongoDbRepository{T}"/> with secured access.
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="databaseName">Database name</param>
        /// <param name="collectionName">Collection name</param>
        /// <param name="credentials">Credentials</param>
        public MongoDbRepositorySettings(string connectionString, string databaseName, string collectionName, MongoDbCredentials credentials)
        {
            SetupSerialisationAndMapping();
            Client = GetClient(connectionString, credentials);
            Database = GetDatabase(databaseName);
            Collection = GetCollection(collectionName);
        }

        private IMongoClient GetClient(string connectionString)
        {
            var existingClient = Clients.Where(p => p.Key == connectionString).Select(p => p.Value);
            if(existingClient.Count() >= 1)
            {
                return existingClient.ElementAt(0);
            }
            else
            {
                Clients[connectionString] = new MongoClient(connectionString);
            }

            return Clients[connectionString];
        }

        private IMongoClient GetClient(string connectionString, MongoDbCredentials dbCredentials)
        {
            var existingClient = Clients.Where(p => p.Key == connectionString).Select(p => p.Value);
            if (existingClient.Count() >= 1)
            {
                return existingClient.ElementAt(0);
            }
            else
            {
                var settings = MongoClientSettings.FromConnectionString(connectionString);
                settings.Credential = MongoCredential.CreateCredential(dbCredentials.UsersDatabase, dbCredentials.Username, dbCredentials.Password);
                Clients[connectionString] = new MongoClient(settings);
            }

            return Clients[connectionString];
        }
        private IMongoDatabase GetDatabase(string databaseName)
        {
            _databaseName = databaseName;
            var existingClient = Databases.Where(p => p.Key == databaseName).Select(p => p.Value);
            if (existingClient.Count() >= 1)
            {
                return existingClient.ElementAt(0);
            }
            else
            {
                Databases[databaseName] = this.Client.GetDatabase(databaseName);
            }

            return Databases[databaseName];
        }
        private IMongoCollection<T> GetCollection(string collectionName)
        {
            _collectionName = collectionName;
            var existingClient = Collections.Where(p => p.Key == collectionName).Select(p => p.Value);
            if (existingClient.Count() >= 1)
            {
                return existingClient.ElementAt(0);
            }
            else
            {
                Collections[collectionName] = this.Database.GetCollection<T>(collectionName);
            }

            return Collections[collectionName];
        }

        private static Dictionary<string, IMongoClient> Clients = new Dictionary<string, IMongoClient>();
        private static Dictionary<string, IMongoDatabase> Databases = new Dictionary<string, IMongoDatabase>();
        private static Dictionary<string, IMongoCollection<T>> Collections = new Dictionary<string, IMongoCollection<T>>();

        private static IEnumerable<Type> GetContainingTypes(Type RootType, List<Type> types = null)
        {
            if (types == null)
            {
                types = new List<Type>();
            }

            foreach (var tps in RootType.GetProperties())
            {
                if (tps.PropertyType.GetInterface("IPlain") != null)
                {
                    if (!types.Exists(p => p == tps.PropertyType))
                    {
                        types.Add(tps.PropertyType);
                        GetContainingTypes(tps.PropertyType, types);
                    }
                }
            }

            return types;
        }

        private static void SetupSerialisationAndMapping()
        {
            if (BsonClassMap.GetRegisteredClassMaps().FirstOrDefault(p => p.ClassType == typeof(T)) == null)
            {
                BsonClassMap.RegisterClassMap<T>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.SetIdMember(cm.GetMemberMap(c => c._recordId));
                });
            }

            var usedTypes = GetContainingTypes(typeof(T));

            foreach (var type in usedTypes)
            {
                var existing = BsonClassMap.GetRegisteredClassMaps().FirstOrDefault(p => p.ClassType == type);
                if (existing == null)
                {
                    var bsonMap = new BsonClassMap(type);
                    bsonMap.AutoMap();
                    bsonMap.SetIgnoreExtraElements(true);
                    BsonClassMap.RegisterClassMap(bsonMap);
                }
            }

            try
            {
                BsonSerializer.RegisterSerializer(typeof(UInt64), new UInt64Serializer(BsonType.Int64, new RepresentationConverter(true, false)));
                BsonSerializer.RegisterSerializer(typeof(UInt32), new UInt32Serializer(BsonType.Int64, new RepresentationConverter(true, false)));
                BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);
                BsonSerializer.RegisterSerializer(typeof(float), new FloatTruncationSerializer());
            }
            catch (BsonSerializationException)
            {

                // swallow;
            }

        }

        public IMongoClient Client { get; private set; }
        public IMongoDatabase Database { get; private set; }
        public IMongoCollection<T> Collection { get; private set; }

        public string GetConnectionInfo()
        {
            return $"{this.Client.Settings.Server.Host}:{this.Client.Settings.Server.Port} {this._databaseName}.{this._collectionName}";
        }

        public void WaitForMongoServerAvailability()
        {                     
            Console.WriteLine($"Checking that mognodb server at '{Client.Settings.Server.ToString()}' is running.");

            while (true)
            {
                if (IsPortOpen(Client.Settings.Server.Host, Client.Settings.Server.Port, new TimeSpan(10000)))
                {
                    break;
                }
            }

            Console.WriteLine($"Connection to server '{Client.Settings.Server.ToString()}' is available.");
        }

        private bool IsPortOpen(string host, int port, TimeSpan timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);
                    if (!success)
                    {
                        return false;
                    }

                    client.EndConnect(result);
                }

            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
