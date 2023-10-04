using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services
{
    public class DataService 
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public DataService(IOptions<Configure.MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoUri);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            _collectionName = settings.Value.CollectionName;
        }

        public IEnumerable<MongoDbLogItem> CachedData { get; set; }
        public IEnumerable<MongoDbLogItem> CachedDataEntriesToUpdate { get; set; }

        private async Task<IEnumerable<MongoDbLogItem>> GetDataAsync()
        {
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task RefreshDataAsync()
        {
            CachedData = await GetDataAsync();
        }

        private void FilterDataEntriesToUpdate()
        {
            CachedDataEntriesToUpdate = CachedData
                           .Where(item => item.TimeStampAcknowledged == null)
                           .ToList();
        }

        public void ExtractIdentity()
        {
            var regex = new Regex(@"Identity: (\d+)", RegexOptions.Compiled);
            foreach (var item in CachedData)
            {
                var match = regex.Match(item.RenderedMessage);
                if (match.Success)
                {
                    var identityString = match.Groups[1].Value;
                    if (ulong.TryParse(identityString, out ulong identity))
                    {
                        item.Properties.ExtractedIdentity = (ulong)identity;  // Cast to long if necessary
                    }
                    else
                    {
                        Console.WriteLine($"Failed to parse identity: {identityString}");  // Debug log
                    }
                }
            }
        }

        public async Task UpdateAllMessagesInDb()
        {
            // Filter the CachedData for entries where TimeStampAcknowledged is null
            FilterDataEntriesToUpdate();

             DateTime timeStampAcknowledged = DateTime.UtcNow;

            // Get the collection
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            // Build the update definition
            var update = Builders<MongoDbLogItem>.Update
                .Set(item => item.TimeStampAcknowledged, timeStampAcknowledged);

            // Apply the update to all matching documents in the database
            var filter = Builders<MongoDbLogItem>.Filter.Eq(item => item.TimeStampAcknowledged, null);
            await collection.UpdateManyAsync(filter, update);
        }



        public async Task UpdateMessageInDb(ulong? identity, int messageDigest)
        {
            FilterDataEntriesToUpdate();

            var entriesToUpdate = CachedDataEntriesToUpdate
                .Where(item => item.Properties.ExtractedIdentity == identity
                            && item.Properties.sender.Payload.MessageDigest == messageDigest)
                .ToList();

            DateTime timeStampAcknowledged = DateTime.UtcNow;
            // Update the TimeStampAcknowledged field for each matching entry
            foreach (var entry in entriesToUpdate)
            {
                entry.TimeStampAcknowledged = timeStampAcknowledged;
            }

            // Get the collection
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            // Build the update definition
            var update = Builders<MongoDbLogItem>.Update
                .Set(item => item.TimeStampAcknowledged, timeStampAcknowledged);

            // Apply the update to each matching document in the database
            foreach (var entry in entriesToUpdate)
            {
                var filter = Builders<MongoDbLogItem>.Filter.Eq(item => item.Id, entry.Id);
                await collection.UpdateOneAsync(filter, update);
            }
        }

        public int GetCachedDataCount()
        {
            return CachedData?.Count() ?? 0;
        }

    }

}
