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

        public void LogMessage(PlainTcoMessage message)
        {
            var document = new BsonDocument
    {
        { "TimeStamp", BsonValue.Create(message.TimeStamp) },
        { "TimeStampAcknowledged", BsonValue.Create(message.TimeStampAcknowledged) },
        { "Identity", BsonValue.Create(message.Identity) },
        { "Text", BsonValue.Create(message.Text) },
        { "Category", BsonValue.Create(message.Category) },
        { "Cycle", BsonValue.Create(message.Cycle) },
        { "PerCycleCount", BsonValue.Create(message.PerCycleCount) },
        { "ExpectDequeing", BsonValue.Create(message.ExpectDequeing) },
        { "Pinned", BsonValue.Create(message.Pinned) },
        { "Location", BsonValue.Create(message.Location) },
        { "Source", BsonValue.Create(message.Source) },
        { "ParentsHumanReadable", BsonValue.Create(message.ParentsHumanReadable) },
        { "Raw", BsonValue.Create(message.Raw) },
        { "MessageDigest", BsonValue.Create(message.MessageDigest) },
        { "ParentsObjectSymbol", BsonValue.Create(message.ParentsObjectSymbol) }
    };

            _collection.InsertOne(document);
        }


        public void UpdateMessage(ulong identity, DateTime timeStamp, DateTime timeStampAcknowledged)
        {
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("Identity", BsonValue.Create(identity)),
                Builders<BsonDocument>.Filter.Eq("TimeStamp", BsonValue.Create(timeStamp))
            );

            var update = Builders<BsonDocument>.Update.Set("TimeStampAcknowledged", BsonValue.Create(timeStampAcknowledged));

            _collection.UpdateOne(filter, update);
        }


        public bool MessageExistsInDatabase(ulong identity, DateTime timeStamp)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Identity", identity) & Builders<BsonDocument>.Filter.Eq("TimeStamp", timeStamp);
            var result = _collection.Find(filter).FirstOrDefault();

            return result != null;
        }

        public List<PlainTcoMessage> ReadMessages()
        {
            var sort = Builders<BsonDocument>.Sort.Descending("TimeStamp");
            var messages = _collection.Find(new BsonDocument()).Sort(sort).Limit(100).ToList();
            return messages.Select(m => new PlainTcoMessage
            {
                TimeStamp = m["TimeStamp"].ToUniversalTime(),
                TimeStampAcknowledged = m["TimeStampAcknowledged"].ToUniversalTime(),
                Identity = (ulong)m["Identity"].AsInt64,
                Text = m["Text"].AsString,
                ////Category = (short)m["Category"].AsInt32,
                //Cycle = (ulong)m["Cycle"].AsInt64,
                //PerCycleCount = (byte)m["PerCycleCount"].AsInt32,
                //ExpectDequeing = m["ExpectDequeing"].AsBoolean,
                //Pinned = m["Pinned"].AsBoolean,
                //Location = m["Location"].AsString,
                //Source = m["Source"].AsString,
                //ParentsHumanReadable = m["ParentsHumanReadable"].AsString,
                //Raw = m["Raw"].AsString,
                //MessageDigest = (uint)m["MessageDigest"].AsInt32,
                //ParentsObjectSymbol = m["ParentsObjectSymbol"].AsString
            }).ToList();
        }
    }
}

    
