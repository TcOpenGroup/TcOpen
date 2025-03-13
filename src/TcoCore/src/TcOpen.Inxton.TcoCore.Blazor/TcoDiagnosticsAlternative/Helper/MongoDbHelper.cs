using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;

using TcoCore;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Helper
{
    class MongoDbHelper
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public MongoDbHelper(IOptions<Configure.MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoUri);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            _collectionName = settings.Value.CollectionName;
        }

        public async Task<List<MongoDbLogItem>> QueryDb()
        {
            var _collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            // Step 1: Query for entries with TimestampAcknowledged == null
            var filter = FilterHelper.BuildTimeStampAcknowledgedFilter();
            var projection = FilterHelper.BuildMinimalProjection();

            var unAcknowledgedMessages = await _collection.Find(filter).Project<MongoDbLogItem>(projection).ToListAsync();

            if (!unAcknowledgedMessages.Any())
            {
                return new List<MongoDbLogItem>();
            }
            return unAcknowledgedMessages;
        }

        //Checking for Duplication in Db due to the way, the Plc Logger and Messenger works
        public async Task CheckForDuplication()
        {
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);
            var filter = FilterHelper.BuildTimeStampAcknowledgedFilter();
            var messages = await collection.Find(filter).ToListAsync();

            var distinctCombinations = messages
                .Where(m => IdentityExtractor.ExtractIdentityFromRenderedMessage(m.RenderedMessage).HasValue && m.Properties.sender.Payload.MessageDigest != 0)
                .Select(m => new
                {
                    Identity = IdentityExtractor.ExtractIdentityFromRenderedMessage(m.RenderedMessage),
                    Digest = m.Properties.sender.Payload.MessageDigest
                })
                .Distinct();

            foreach (var combo in distinctCombinations)
            {
                var similarMessages = messages.Where(m =>
                    IdentityExtractor.ExtractIdentityFromRenderedMessage(m.RenderedMessage) == combo.Identity &&
                    m.Properties.sender.Payload.MessageDigest == combo.Digest)
                    .OrderBy(m => m.UtcTimeStamp)
                    .ToList();

                if (similarMessages.Count > 1)
                {
                    await PurgeMultipleEntriesAsync(collection, similarMessages.Skip(1).ToList());
                }
            }
        }

        public async Task PurgeMultipleEntriesAsync(IMongoCollection<MongoDbLogItem> collection, List<MongoDbLogItem> messagesToPurge)
        {
            var idsToDelete = messagesToPurge.Select(m => m.Id).ToList();

            var deleteFilter = Builders<MongoDbLogItem>.Filter.In("_id", idsToDelete);
            await collection.DeleteManyAsync(deleteFilter);
        }

        public async Task BulkUpdateTimestampAcknowledgedAsync(IEnumerable<MongoDbLogItem> messageToAck)
        {
            var _collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            var timeStampAcknowledged = DateTime.UtcNow;
            var bulkOps = new List<WriteModel<MongoDbLogItem>>();

            if (messageToAck == null || !messageToAck.Any())
            {
                return;
            }

            foreach (var message in messageToAck)
            {
                var filter = 
                    
                    Builders<MongoDbLogItem>.Filter.Eq(item => item.Id, message.Id);
                var update = Builders<MongoDbLogItem>.Update.Set(item => item.TimeStampAcknowledged, timeStampAcknowledged);

                var upsertOne = new UpdateOneModel<MongoDbLogItem>(filter, update);
                bulkOps.Add(upsertOne);
            }

            if (bulkOps.Any())
            {
                await _collection.BulkWriteAsync(bulkOps);
            }
        }

        public async Task<UpdateResult> UpdateTimeStampAcknowledgeAsync(ObjectId? id, UpdateDefinition<MongoDbLogItem> update)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "The provided ObjectId is null.");
            }

            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);
            return await collection.UpdateOneAsync(m => m.Id == id, update);
        }

        public async Task<(List<MongoDbLogItem> messages, long count)> GetDataAsync(
            int itemsPerPage,
            int currentPage,
            FilterDefinition<MongoDbLogItem> filter)
        {
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            var messages = await collection.Find(filter)
                .Skip((currentPage - 1) * itemsPerPage)
                .Limit(itemsPerPage)
                .SortByDescending(item => item.UtcTimeStamp)
                .ToListAsync();

            var count = await collection.CountDocumentsAsync(filter);

            foreach (var message in messages) {
                message.AdjustForDaylightSavingTime();
            }

            return (messages, count);
        }

        public async Task<PlainTcoMessage> GetPlainTcoMessageFromId(ObjectId id, IEnumerable<PlainTcoMessage > messageDisplay)
        {
            // Fetch the MongoDbLogItem from the database using the provided ObjectId
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);
            var message = await collection.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (message == null)
            {
                return null; 
            }

            var identity = IdentityExtractor.ExtractIdentityFromRenderedMessage(message.RenderedMessage);

            if (!identity.HasValue)
            {
                return null; 
            }

            var plainTcoMessage = messageDisplay.FirstOrDefault(x =>
                x.Identity == (ulong)identity.Value &&
                x.MessageDigest == message.Properties.sender.Payload.MessageDigest
            );

            return plainTcoMessage;
        }

        public async Task<ObjectId?> GetMongoDbLogItemIdFromPlainTcoMessage(PlainTcoMessage plainTcoMessage)
        {
            if (plainTcoMessage == null)
            {
                return null;
            }

            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            var filter = FilterHelper.BuildTimeStampAcknowledgeNullFilter();
            var projection = FilterHelper.BuildMinimalProjection();

            var results = await collection.Find(filter).Project(projection).ToListAsync();

            foreach (var item in results)
            {
                var renderedMessage = item["RenderedMessage"].AsString;
                var extractedIdentity = IdentityExtractor.ExtractIdentityFromRenderedMessage(renderedMessage);

                if (extractedIdentity.HasValue)
                {

                    var specificFilter = FilterHelper.BuildPlainTcoMessageFilter(plainTcoMessage, extractedIdentity.Value);
                    var message = await collection.Find(specificFilter).FirstOrDefaultAsync();

                    if (message != null)
                    {
                        return message.Id;
                    }
                }
            }

            return null;
        }
    }
}
