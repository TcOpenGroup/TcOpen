using System;
using System.Collections.Generic;
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

        public MongoLogger()
        {
            var client = new MongoClient("mongodb://admin:1234@localhost:27017");
            var database = client.GetDatabase("mydatabase");
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

        public void LogMessage(PlainTcoMessage message)
        {
            // If the timestamp is earlier than 1980, do not save the message
            if (message.TimeStamp < new DateTime(1980, 1, 1, 0, 0, 0))
            {
                return;
            }

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
                .Set("MessageDigest", BsonValue.Create(message.MessageDigest))
                .Set("ParentsObjectSymbol", BsonValue.Create(message.ParentsObjectSymbol));

            UpdateOptions options = new UpdateOptions { IsUpsert = true };

            var filter = GetMessageFilter(message);

            _collection.UpdateOne(filter, update, options);
        }


        public void UpdateMessages(ulong identity, DateTime timeStamp, DateTime timeStampAcknowledged, bool pinned)
        {
            // Check if the message already exists and if its TimeStampAcknowledged is older than 1980
            var existingMessage = _collection.Find(Builders<BsonDocument>.Filter.Eq("Identity", identity)).FirstOrDefault();
            if (existingMessage != null && existingMessage["TimeStampAcknowledged"].ToUniversalTime() < new DateTime(1980, 1, 1, 0, 0, 0))
            {
                var filter = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("Identity", BsonValue.Create(identity)),
                    Builders<BsonDocument>.Filter.Eq("TimeStamp", BsonValue.Create(timeStamp))
                );

                var update = Builders<BsonDocument>.Update.
                    Set("TimeStampAcknowledged", BsonValue.Create(timeStampAcknowledged.AddHours(2))).
                    Set("Pinned", BsonValue.Create(pinned));

                _collection.UpdateOne(filter, update);
            }
        }

        public List<PlainTcoMessage> ReadMessages()
        {
            var sort = Builders<BsonDocument>.Sort.Descending("TimeStamp");
            var messages = _collection.Find(new BsonDocument()).Sort(sort).Limit(40).ToList();
            return messages.Select(m => new PlainTcoMessage
            {
                TimeStamp = m["TimeStamp"].ToUniversalTime(),
                TimeStampAcknowledged = m["TimeStampAcknowledged"].ToUniversalTime(),
                Identity = (ulong)m["Identity"].AsInt64,
                Text = m["Text"].AsString,
                Category= (short)m["Category"].AsInt32,
                //Cycle = (ulong)m["Cycle"].AsInt64,
                PerCycleCount = (byte)m["PerCycleCount"].AsInt32,
                ExpectDequeing = m["ExpectDequeing"].AsBoolean,
                Pinned = m["Pinned"].AsBoolean,
                Location = m["Location"].AsString,
               Source = m["Source"].AsString,
                ParentsHumanReadable = m["ParentsHumanReadable"].AsString,
                Raw = m["Raw"].AsString,
                //MessageDigest = (uint)m["MessageDigest"].AsInt32,
                ParentsObjectSymbol = m["ParentsObjectSymbol"].AsString
            }).ToList();
        }
    }
}




      


