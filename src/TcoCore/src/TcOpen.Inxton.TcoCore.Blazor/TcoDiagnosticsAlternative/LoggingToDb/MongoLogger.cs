using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

namespace TcoCore.TcoDiagnosticsAlternative.LoggingToDb
{
    public class MongoLogger : IMongoLogger
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly MongoClient _client;

        public MongoLogger()
        {
            _client = new MongoClient("mongodb://admin:1234@localhost:27017");
            var database = _client.GetDatabase("mydatabase");
            _collection = database.GetCollection<BsonDocument>("mycollection");
        }


        private FilterDefinition<BsonDocument> GetMessageFilter(PlainTcoMessage message)
        {
            return Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("TimeStamp", BsonValue.Create(message.TimeStamp)),
                Builders<BsonDocument>.Filter.Eq("Raw", BsonValue.Create(message.Raw)),
                Builders<BsonDocument>.Filter.Eq("Source", BsonValue.Create(message.Source)),
                Builders<BsonDocument>.Filter.Eq("MessageDigest", BsonValue.Create(message.MessageDigest)),
                Builders<BsonDocument>.Filter.Eq("Identity", BsonValue.Create(message.Identity)),
                Builders<BsonDocument>.Filter.Eq("Cycle", BsonValue.Create(message.Cycle))
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
                .Set("TimeStampAcknowledged", BsonValue.Create(message.TimeStampAcknowledged))
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

            //Console.WriteLine("Before MongoDB operation.");
            _collection.UpdateOne(filter, update, options);
        }


        public void LogMessage(PlainTcoMessage message)
        {
            var existingMessage = GetSimilarMessage(message);

            //if (MessageExistsInDatabase(message)) {
            //    return;
            //}

            if (existingMessage == null ||
                (message.Category >= existingMessage.Category && message.Text != existingMessage.Text))
            {
                InsertMessage(message);
            }
        }


        public void UpdateMessages(ulong identity, DateTime timeStamp, DateTime timeStampAcknowledged, bool pinned)
        {
            // Define a filter to find messages with the given identity and where TimeStampAcknowledged is older than 1980
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("TimeStamp", BsonValue.Create(timeStamp)),
                Builders<BsonDocument>.Filter.Lt("TimeStampAcknowledged", BsonValue.Create(new DateTime(1980, 1, 1, 0, 0, 0)))
            );

            // Define the update operation
            var update = Builders<BsonDocument>.Update
                .Set("TimeStampAcknowledged", BsonValue.Create(timeStampAcknowledged.AddHours(1)))
                .Set("Pinned", BsonValue.Create(pinned));

            // Update all matching documents
            _collection.UpdateMany(filter, update);
        }

        private PlainTcoMessage MapBsonToPlainTcoMessage(BsonDocument bsonMessage)
        {
            return new PlainTcoMessage
            {
                TimeStamp = bsonMessage["TimeStamp"].ToUniversalTime(),
                TimeStampAcknowledged = bsonMessage["TimeStampAcknowledged"].ToUniversalTime(),
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
                Builders<BsonDocument>.Filter.Eq("Identity", BsonValue.Create(message.Identity)),
                Builders<BsonDocument>.Filter.Lte("Cycle", BsonValue.Create(message.Cycle + 20)),
                Builders<BsonDocument>.Filter.Gte("Cycle", BsonValue.Create(message.Cycle - 20)),
                Builders<BsonDocument>.Filter.Eq("TimeStamp", BsonValue.Create(message.TimeStamp))
            );

            var existingMessageBson = _collection.Find(filter).FirstOrDefault();
            return existingMessageBson != null ? MapBsonToPlainTcoMessage(existingMessageBson) : null;
        }


        public List<PlainTcoMessage> ReadMessages()
        {
            var sort = Builders<BsonDocument>.Sort.Descending("TimeStamp");
            var messages = _collection.Find(new BsonDocument()).Sort(sort).Limit(1000).ToList();
            return messages.Select(MapBsonToPlainTcoMessage).ToList();
        }

    }
}
