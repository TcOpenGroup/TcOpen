using Grafana.Backend.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PlcHammer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TcOpenHammer.Grafana.API.Model.Mongo
{
    /// <summary>
    /// Service which access the Database and its collection.
    /// This service then provides public collections for other servicese or controllers.
    /// </summary>
    public class MongoService
    {
        internal readonly IMongoCollection<PlainStation001_ProductionData> ProcessData;
        internal readonly IMongoCollection<enumModesObservedValue> StationModes;
        internal readonly IMongoCollection<ObservedValue<string>> RecipeHistory;
        private readonly ILogger<MongoService> _logger;

        public MongoService(DatabaseSettings settings, ILogger<MongoService> logger)
        {
            _logger = logger;
            SetupSerialisationAndMapping();
            SerializionObservedValue();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            ProcessData = database.GetCollection<PlainStation001_ProductionData>(settings.ProductionCollectionName);
            StationModes = database.GetCollection<enumModesObservedValue>(settings.StationModesCollection);
            RecipeHistory = database.GetCollection<ObservedValue<string>>(settings.Station001RecipeHistoryCollection);
        }

        private void SerializionObservedValue()
        {
            BsonClassMap.RegisterClassMap<ObservedValue<string>>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.SetIdMember(cm.GetMemberMap(c => c._recordId));
            });
        }

        /// <summary>
        /// This is code is copied from Vortex.Data library to deal with serialization.
        /// </summary>
        private void SetupSerialisationAndMapping()
        {
            if (BsonClassMap.GetRegisteredClassMaps().FirstOrDefault(p => p.ClassType == typeof(PlainStation001_ProductionData)) == null)
            {
                BsonClassMap.RegisterClassMap<PlainStation001_ProductionData>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.SetIdMember(cm.GetMemberMap(c => c._EntityId));
                });
            }


            var usedTypes = GetContainingTypes(typeof(PlainStation001_ProductionData));

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
                // Serializer added bcs of weird behavior in Mongo GUI tools when 0 was of type Float when in real it was double.
                BsonSerializer.RegisterSerializer(typeof(float), new FloatTruncationSerializer());
            }
            catch (BsonSerializationException e)
            {
                _logger.LogError(e,nameof(SetupSerialisationAndMapping));
            }

        }

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
    }

}
