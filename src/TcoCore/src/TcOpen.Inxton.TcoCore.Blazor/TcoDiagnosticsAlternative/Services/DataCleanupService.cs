using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;


namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services
{
    public class DataCleanupService : IDisposable
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly Timer _timer;

        private int cycleToDeleteInHour { get; set; } = 1;
        private int maxNumber { get; set; } = 10_000;

        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public DataCleanupService(IOptions<Configure.MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoUri);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            _collectionName = settings.Value.CollectionName;
            _collection = _database.GetCollection<BsonDocument>(_collectionName);

            _timer = new Timer(Cleanup, null, TimeSpan.Zero, TimeSpan.FromHours(cycleToDeleteInHour));  // Adjust the interval as needed
            Console.WriteLine($"Cleanup busy");  // Debug log
        }

        private async void Cleanup(object state)
        {
            try
            {
                Console.WriteLine($"Cleanup started at {DateTime.UtcNow}");
                var count = await _collection.CountDocumentsAsync(new BsonDocument());
                
                if (count > maxNumber)  // Adjust the threshold as needed
                {
                    var oldestLogs = await _collection.Find(new BsonDocument())
                        .Sort(Builders<BsonDocument>.Sort.Ascending("timestamp"))  // Assuming your documents have a "timestamp" field
                        .Limit((int)(count - maxNumber)) 
                        .ToListAsync();

                    var deleteFilter = Builders<BsonDocument>.Filter.In("_id", oldestLogs.Select(log => log["_id"]));
                    await _collection.DeleteManyAsync(deleteFilter);
                    Console.WriteLine($"Cleanup Done");  // Debug log
                }
            }
            catch(System.Exception ex)
            {
                Console.WriteLine($"Cleanup failed: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
