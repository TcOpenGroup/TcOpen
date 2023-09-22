using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcoCore.TcoDiagnosticsAlternative.LoggingToDb
{
    public class MongoLogger: IMongoLogger
    {
        private readonly IMongoClient _client;
        private readonly IMongoCollection<BsonDocument> _collection;
        public int MaxEntries { get; set; }

        public MongoLogger()
        {
            MaxEntries = TcoDiagnosticsAlternativeView.MaxDatabaseEntries;
            _client = new MongoClient("mongodb://admin:1234@localhost:27017");
            var database = _client.GetDatabase("mydatabase");
            _collection = database.GetCollection<BsonDocument>("mycollection");

            EnsureIndexes();
        }

        private void EnsureIndexes()
        {
            var indexExists = _collection.Indexes.List().ToList().Any(index => index["name"] == "Text_Source_Category_TimeStampAcknowledged");
            if (!indexExists)
            {
                var keys = Builders<BsonDocument>.IndexKeys
                    .Ascending("Text")
                    .Ascending("Source")
                    .Ascending("Category")
                    .Ascending("TimeStampAcknowledged");

                _collection.Indexes.CreateOne(new CreateIndexModel<BsonDocument>(keys));
            }
        }

    private FilterDefinition<BsonDocument> GetMessageFilter(PlainTcoMessage message)
        {
            return Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("TimeStamp", BsonValue.Create(message.TimeStamp)),
                Builders<BsonDocument>.Filter.Eq("Raw", BsonValue.Create(message.Raw)),
                Builders<BsonDocument>.Filter.Eq("Source", BsonValue.Create(message.Source)),
                Builders<BsonDocument>.Filter.Eq("TimeStampAcknowledged", BsonNull.Value),
                Builders<BsonDocument>.Filter.Eq("Identity", BsonValue.Create(message.Identity)),
                Builders<BsonDocument>.Filter.Eq("Cycle", BsonValue.Create(message.Cycle)),
                Builders<BsonDocument>.Filter.Eq("Category", BsonValue.Create(message.Category))
            );
        }

        public bool MessageExistsInDatabase(PlainTcoMessage message)
        {
            var filter = GetMessageFilter(message);
            var result = _collection.Find(filter).FirstOrDefault();
            return result != null;
        }


        private void InsertMessage(PlainTcoMessage message)
        {
            // If the message doesn't exist, then proceed with saving it
            var update = Builders<BsonDocument>.Update
                .Set("Text", BsonValue.Create(message.Text))
                .Set("TimeStamp", BsonValue.Create(message.TimeStamp))
                .Set("TimeStampAcknowledged", BsonNull.Value)
                .Set("Identity", BsonValue.Create(message.Identity))
                .Set("Text", BsonValue.Create(message.Text))
                .Set("Category", BsonValue.Create(message.Category))
                .Set("Cycle", BsonValue.Create(message.Cycle))
                .Set("PerCycleCount", BsonValue.Create(message.PerCycleCount))
                .Set("ExpectDequeing", BsonValue.Create(message.ExpectDequeing))
                .Set("Pinned", BsonValue.Create(message.Pinned))
                .Set("Location", BsonValue.Create(message.Location))
                .Set("Source", BsonValue.Create(message.Source))
                .Set("ParentsHumanReadable", BsonValue.Create(message.ParentsHumanReadable))
                .Set("Raw", BsonValue.Create(message.Raw))
                .Set("MessageDigest", BsonValue.Create((ulong)(message.MessageDigest)))
                .Set("ParentsObjectSymbol", BsonValue.Create(message.ParentsObjectSymbol));

            UpdateOptions options = new UpdateOptions { IsUpsert = true };
            var filter = GetMessageFilter(message);
            _collection.UpdateOne(filter, update, options);
        }


        public void LogMessage(PlainTcoMessage message)
        {

            if (string.IsNullOrEmpty(message.Text) || string.IsNullOrEmpty(message.Source))
            {
                // Don't log the message
                return;
            }

            var existingMessage = GetSimilarMessage(message);

            if (existingMessage == null)
            {
                InsertMessage(message);
                EnsureMaxEntries();
            }
        }


        public void UpdateMessages(ulong identity, DateTime timeStamp, DateTime timeStampAcknowledged, bool pinned)
        {
            // Define a filter to find messages with the given identity and where TimeStampAcknowledged is older than 1980
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("TimeStamp", BsonValue.Create(timeStamp)),
                Builders<BsonDocument>.Filter.Eq("TimeStampAcknowledged", BsonNull.Value)
            );

            // Define the update operation
            var update = Builders<BsonDocument>.Update
                .Set("TimeStampAcknowledged", BsonValue.Create(timeStampAcknowledged.AddHours(1)))
                .Set("Pinned", BsonValue.Create(pinned));

            // Update all matching documents
            _collection.UpdateMany(filter, update);
        }

        private PlainTcoMessageExtended MapBsonToPlainTcoMessage(BsonDocument bsonMessage)
        {
            return new PlainTcoMessageExtended
            {
                TimeStamp = bsonMessage["TimeStamp"].IsBsonNull ? DateTime.MinValue : bsonMessage["TimeStamp"].ToUniversalTime(),
                TimeStampAcknowledged = bsonMessage["TimeStampAcknowledged"].IsBsonNull ? (DateTime?)null : bsonMessage["TimeStampAcknowledged"].ToUniversalTime(),
                Identity = (ulong)bsonMessage["Identity"].AsInt64,
                Text = bsonMessage["Text"].AsString,
                Category = (short)bsonMessage["Category"].AsInt32,
                Cycle = (ulong)bsonMessage["Cycle"].AsInt64,
                PerCycleCount = (byte)bsonMessage["PerCycleCount"].AsInt32,
                ExpectDequeing = bsonMessage["ExpectDequeing"].AsBoolean,
                Pinned = bsonMessage["Pinned"].AsBoolean,
                Location = bsonMessage["Location"].AsString,
                Source = bsonMessage["Source"].AsString,
                ParentsHumanReadable = bsonMessage["ParentsHumanReadable"].AsString,
                Raw = bsonMessage["Raw"].AsString,
                MessageDigest = (uint)bsonMessage["MessageDigest"].AsInt64,
                ParentsObjectSymbol = bsonMessage["ParentsObjectSymbol"].AsString
            };
        }


        public PlainTcoMessage GetSimilarMessage(PlainTcoMessage message)
        {
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("Text", BsonValue.Create(message.Text)),
                Builders<BsonDocument>.Filter.Eq("Source", BsonValue.Create(message.Source)),
                Builders<BsonDocument>.Filter.Eq("Category", BsonValue.Create(message.Category)),
                Builders<BsonDocument>.Filter.Eq("TimeStampAcknowledged", BsonNull.Value)
            );

            var existingMessageBson = _collection.Find(filter).FirstOrDefault();
            return existingMessageBson != null ? MapBsonToPlainTcoMessage(existingMessageBson) : null;
        }


        public List<PlainTcoMessageExtended> ReadMessages()
        {
            var sort = Builders<BsonDocument>.Sort.Descending("TimeStamp");
            var messages = _collection.Find(new BsonDocument()).Sort(sort).Limit(1000).ToList();
            return messages.Select(MapBsonToPlainTcoMessage).ToList();
        }

        private void EnsureMaxEntries()
        {
            var currentCount = _collection.CountDocuments(new BsonDocument());

            if (currentCount > MaxEntries)
            {
                var excessCount = currentCount - MaxEntries;

                // Find the oldest entries based on TimeStamp or any other relevant field
                var oldestEntries = _collection.Find(new BsonDocument())
                                              .Sort(Builders<BsonDocument>.Sort.Ascending("TimeStamp")) // Assuming TimeStamp is the field you want to sort by
                                              .Limit((int)excessCount)
                                              .ToList();

                foreach (var entry in oldestEntries)
                {
                    _collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", entry["_id"]));
                }
            }
        }

    }
}
