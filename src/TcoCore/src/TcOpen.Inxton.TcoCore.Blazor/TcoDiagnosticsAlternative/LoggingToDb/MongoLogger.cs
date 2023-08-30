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
                { "Text", BsonValue.Create(message.Text) },
                // Add other fields
            };

            _collection.InsertOne(document);
        }
    }
}
