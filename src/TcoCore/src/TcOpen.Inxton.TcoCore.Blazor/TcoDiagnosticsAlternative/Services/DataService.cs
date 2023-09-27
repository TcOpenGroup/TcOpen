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

        private async Task<IEnumerable<MongoDbLogItem>> GetDataAsync()
        {
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task RefreshDataAsync()
        {
            CachedData = await GetDataAsync();
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


        public void OrderData()
        {
            CachedData = CachedData
                .OrderByDescending(item => item.TimeStampAcknowledged.HasValue ? 0 : 1)  // Nulls first
                .ThenByDescending(item => item.UtcTimeStamp)
                .ThenByDescending(item => item.TimeStampAcknowledged)
                .ToList();
        }


    }

}
