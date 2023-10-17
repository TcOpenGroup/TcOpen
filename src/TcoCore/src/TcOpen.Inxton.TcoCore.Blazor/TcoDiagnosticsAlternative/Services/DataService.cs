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

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services
{
    public class DataService : IDataService
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public DataService(IOptions<Configure.MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoUri);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            _collectionName = settings.Value.CollectionName;
        }

        //set this by the Plc via the View.cs
        public IEnumerable<PlainTcoMessage> ActiveMessage { get; set; }
        //Query from Db, used for AutoAcknow
        private List<MongoDbLogItem> MessagesReadyToAck { get; set; }
        private List<MongoDbLogItem> MessagesUnAckedInDb { get; set; }

        public async Task<(List<MongoDbLogItem> messages, long count)> GetDataAsycForActive(
                                    int itemsPerPage,
                                    int currentPage,
                                    eMessageCategory category,
                                    int? depthValue,
                                    DateTime? startDate = null,
                                    DateTime? endDate = null,
                                    string? keyword = null)
        {
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            var combinedFilter = BuildDateFilter(startDate, endDate)
                                    & BuildKeywordFilter(keyword)
                                    & BuildCategoryFilter(category)
                                    & BuildTimeStampAcknowledgedFilter();

            var messages = await collection.Find(combinedFilter)
                .Skip((currentPage - 1) * itemsPerPage)
                .Limit(itemsPerPage)
                .SortByDescending(item => item.UtcTimeStamp)
                .ToListAsync();
            var count = await collection.CountDocumentsAsync(combinedFilter);
            MessagesUnAckedInDb = messages;

            return (messages, count);
        }

        public async Task<(List<MongoDbLogItem> messages, long count)> GetDataAsyncForArchive(
                                     int itemsPerPage,   
                                     int currentPage, 
                                     eMessageCategory category,  
                                     int? depthValue,
                                     DateTime? startDate = null, 
                                     DateTime? endDate = null, 
                                     string? keyword = null)
        {
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            var combinedFilter = BuildDateFilter(startDate, endDate)
                                    & BuildKeywordFilter(keyword)
                                    & BuildCategoryFilter(category)
                                    & BuildTimeStampAcknowledgedForArchiveFilter();

            var messages = await collection.Find(combinedFilter)
                .Skip((currentPage - 1) * itemsPerPage)
                .Limit(itemsPerPage)
                .SortByDescending(item => item.UtcTimeStamp)
                .ToListAsync();

            var count = await collection.CountDocumentsAsync(combinedFilter);
            return (messages, count);
        }

        public Task AcknowledgeAllMessages() => throw new NotImplementedException();
        public Task AcknowledgeMessage(ulong identity, int messageDigest) => throw new NotImplementedException();

       
        //private FilterDefinition<MongoDbLogItem> BuildDepthValueFilter(int? depthValue, int? depthValueThreshold)
        //{
        //    var filterBuilder = Builders<MongoDbLogItem>.Filter;

        //    if (depthValue.HasValue)
        //    {
        //        return filterBuilder.Gte(depthValueThreshold.Value, depthValue.Value);
        //    }

        //    return filterBuilder.Empty;
        //}
        //}
        public async Task TryAutoAcknowledgeMessages(IEnumerable<MongoDbLogItem> msg)
        {
            var unAcknowledgedMessages = await QueryDb();
                // Step 2: Filter these entries based on the ActiveMessage list
                var filteredActiveMessages = ActiveMessage.ToList();
            //.Where(x => !x.Pinned);

            // Step 3: Compare each unacknowledged message with the PlainTcoMessage list
            var messagesToAck = new List<MongoDbLogItem>(unAcknowledgedMessages);  // Start with all unacknowledged messages

            foreach (var message in unAcknowledgedMessages)
            {
                var identity = ExtractIdentityFromRenderedMessage(message.RenderedMessage);
                if (!identity.HasValue) continue;  // If identity is null, skip to the next iteration

                bool isMessageAcknowledged = filteredActiveMessages.Any(x =>
                    x.Identity == (ulong)identity.Value &&
                    x.MessageDigest == message.Properties.sender.Payload.MessageDigest
                );

                if (isMessageAcknowledged)
                {
                    messagesToAck.Remove(message);
                }
            }
            await BulkUpdateTimestampAcknowledgedAsync(messagesToAck);
        }


        public async Task BulkUpdateTimestampAcknowledgedAsync(List<MongoDbLogItem> messagesToUpdate)
        {
            var _collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            var timeStampAcknowledged = DateTime.UtcNow;
            var bulkOps = new List<WriteModel<MongoDbLogItem>>();

            foreach (var message in messagesToUpdate)
            {
                var filter = Builders<MongoDbLogItem>.Filter.Eq(item => item.Id, message.Id);  
                var update = Builders<MongoDbLogItem>.Update.Set(item => item.TimeStampAcknowledged, timeStampAcknowledged);

                var upsertOne = new UpdateOneModel<MongoDbLogItem>(filter, update);
                bulkOps.Add(upsertOne);
            }

            if (bulkOps.Any())
            {
                await _collection.BulkWriteAsync(bulkOps);
            }
        }

        //private FilterDefinition<MongoDbLogItem> BuildDepthValueFilter(int? depthValue, int? depthValueThreshold)
        //{
        //    var filterBuilder = Builders<MongoDbLogItem>.Filter;

        //    if (depthValue.HasValue)
        //    {
        //        return filterBuilder.Gte(depthValueThreshold.Value, depthValue.Value);
        //    }

        //    return filterBuilder.Empty;

        public async Task AcknowledgeAllMessages(IEnumerable<MongoDbLogItem> msg)
        {

            //    List<PlainTcoMessage> messagesToUpdate = new List<PlainTcoMessage>();

            //    foreach (var _messageToAcknowledge in CategorizedMessages.ActiveMessagesPinned)
            //    {
            //        var _resultMessage = await TryAcknowledgeMessageInternal(_messageToAcknowledge, messageDisplay);
            //        if (_resultMessage != null)
            //        {
            //            messagesToUpdate.Add(_resultMessage);
            //        }
            //    }

            //    if (messagesToUpdate.Any())
            //    {
            //        await UpdateAllMessagesInDb(messagesToUpdate);
            //    }
            }

            public async Task<List<MongoDbLogItem>> QueryDb()
        {
            var _collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            // Step 1: Query for entries with TimestampAcknowledged == null
            var filter = BuildTimeStampAcknowledgedFilter();
            var projection = Builders<MongoDbLogItem>.Projection
                .Include(item => item.TimeStampAcknowledged)
                .Include(item => item.Properties.sender.Payload.MessageDigest)
                .Include(item => item.RenderedMessage);

            var unAcknowledgedMessages = await _collection.Find(filter).Project<MongoDbLogItem>(projection).ToListAsync();

            return unAcknowledgedMessages;
        }


        //public async Task AcknowledgeSingleMessage(ulong? identity, int messageDigest, IEnumerable<PlainTcoMessage> messageDisplay)
        //{
        //    if (CategorizedMessages == null)
        //    {
        //        CategorizeMessages(messageDisplay.ToList());
        //    }

        //    List<PlainTcoMessage> messagesToUpdate = new List<PlainTcoMessage>();

        //    foreach (var messageToAcknowledge in CategorizedMessages.ActiveMessagesPinned.Where(m =>
        //                                 m.Properties?.ExtractedIdentity == identity &&
        //                                 m.Properties?.sender?.Payload?.MessageDigest == messageDigest))
        //    {
        //        var _resultMessage = await TryAcknowledgeMessageInternal(messageToAcknowledge, messageDisplay);
        //        if (_resultMessage != null)
        //        {
        //            messagesToUpdate.Add(_resultMessage);
        //        }

        //        if (messagesToUpdate.Any())
        //        {
        //            await UpdateAllMessagesInDb(messagesToUpdate);
        //        }
        //    }
        //}

        //private async Task<PlainTcoMessage> TryAcknowledgeMessageInternal(MongoDbLogItem messageToAcknowledge, IEnumerable<PlainTcoMessage> messageDisplay)
        //{
        //    var _activeMessage = messageDisplay.FirstOrDefault(m => m.Identity == messageToAcknowledge.Properties?.ExtractedIdentity);
        //    if (_activeMessage != null)
        //    {
        //        _activeMessage.OnlinerMessage.Pinned.Cyclic = false;
        //        await PurgeNewerSimilarMessages();
        //        await Task.Delay(2); 
        //        if (!_activeMessage.OnlinerMessage.IsActive)
        //        {
        //            return _activeMessage;
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Message Still Active {_activeMessage}");
        //        }
        //    }
        //    return null;
        //}




        //public async Task PurgeNewerSimilarMessages()
        //{
        //    if (_database == null || string.IsNullOrEmpty(_collectionName))
        //    {
        //        Console.WriteLine("Database or collection name is not initialized.");
        //        return;
        //    }

        //    if (CachedDataEntriesToUpdate == null)
        //    {
        //        Console.WriteLine("CachedDataEntriesToUpdate is not initialized.");
        //        return;
        //    }

        //    var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

        //    foreach (var unacknowledgedMessage in CachedDataEntriesToUpdate)
        //    {
        //        // 2. For each of these messages, find newer similar messages based on identity and digest in the cache
        //        var newerIdenticalMessages = CachedDataEntriesToUpdate.Where(m =>
        //            m?.Properties?.ExtractedIdentity == unacknowledgedMessage?.Properties?.ExtractedIdentity &&
        //            m?.Properties?.sender?.Payload?.MessageDigest == unacknowledgedMessage?.Properties?.sender?.Payload?.MessageDigest &&
        //            m?.UtcTimeStamp > unacknowledgedMessage?.UtcTimeStamp).ToList();

        //        if (newerIdenticalMessages.Any())
        //        {
        //            // 3. Delete these newer similar messages from the database
        //            var filter = Builders<MongoDbLogItem>.Filter.In(m => m.Id, newerIdenticalMessages.Select(x => x.Id));
        //            await collection.DeleteManyAsync(filter);
        //            //Console.WriteLine($"Purged {newerIdenticalMessages.Count} duplicates for {unacknowledgedMessage?.MessageTemplate?.Text}");
        //        }
        //    }
        //}



        private FilterDefinition<MongoDbLogItem> BuildDateFilter(DateTime? startDate, DateTime? endDate)
        {
            var filterBuilder = Builders<MongoDbLogItem>.Filter;
            var filter = filterBuilder.Empty;

            if (startDate.HasValue)
            {
                filter &= filterBuilder.Gte(item => item.UtcTimeStamp, startDate.Value);
            }
            if (endDate.HasValue)
            {
                filter &= filterBuilder.Lte(item => item.UtcTimeStamp, endDate.Value);
            }

            return filter;
        }

        private FilterDefinition<MongoDbLogItem> BuildKeywordFilter(string keyword)
        {
            var filterBuilder = Builders<MongoDbLogItem>.Filter;

            return !string.IsNullOrEmpty(keyword)
                ? filterBuilder.Regex(item => item.RenderedMessage, new BsonRegularExpression(keyword, "i"))
                : filterBuilder.Empty;
        }

        private FilterDefinition<MongoDbLogItem> BuildCategoryFilter(eMessageCategory? category)
        {
            var filterBuilder = Builders<MongoDbLogItem>.Filter;

            if (category.HasValue)
            {
                var levelsToInclude = MessageCategoryMapper.GetAllLevelsGreaterThanOrEqualTo(category.Value);
                return filterBuilder.In(item => item.Level, levelsToInclude);
            }

            return filterBuilder.Empty;
        }

        public ulong? ExtractIdentityFromRenderedMessage(string renderedMessage)
        {
            var regex = new Regex(@"Identity: (\d+)", RegexOptions.Compiled);
            var match = regex.Match(renderedMessage);
            if (match.Success)
            {
                var identityString = match.Groups[1].Value;
                if (ulong.TryParse(identityString, out ulong identity))
                {
                    return identity;
                }
                else
                {
                    Console.WriteLine($"Failed to parse identity: {identityString}");  // Debug log
                    return null;
                }
            }
            return null;
        }

        //private FilterDefinition<MongoDbLogItem> BuildIdentityFilter(List<long> identities)
        //{
        //    return Builders<MongoDbLogItem>.Filter.In(item => ExtractIdentityFromRenderedMessage(item.RenderedMessage).Value, identities); 
        //}

        private FilterDefinition<MongoDbLogItem> BuildMessageDigestFilter(List<int> messageDigests)
        {
            return Builders<MongoDbLogItem>.Filter.In(item => item.Properties.sender.Payload.MessageDigest, messageDigests);
        }

        private FilterDefinition<MongoDbLogItem> BuildTimeStampAcknowledgedFilter()
        {
            return Builders<MongoDbLogItem>.Filter.Eq(item => item.TimeStampAcknowledged, null);
        }

        private FilterDefinition<MongoDbLogItem> BuildTimeStampAcknowledgedForArchiveFilter()
        {
            var filterBuilder = Builders<MongoDbLogItem>.Filter;
            return filterBuilder.Ne(item => item.TimeStampAcknowledged, null);
        }

        public int CalculateDepth(MongoDbLogItem msg)
        {
            if (string.IsNullOrEmpty(msg.Properties.sender.Payload.ParentSymbol))
            {
                return 1;
            }

            int depth = msg.Properties.sender.Payload.ParentSymbol.Split('.').Length;
            return depth;
        }

    }

}
