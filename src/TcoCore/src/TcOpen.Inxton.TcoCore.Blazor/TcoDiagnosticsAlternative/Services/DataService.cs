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

        //public IEnumerable<MongoDbLogItem> CachedData { get; set; }
        //public IEnumerable<MongoDbLogItem> CachedDataEntriesToUpdate { get; set; }

        //set this by the Plc via the View.cs
        public IEnumerable<PlainTcoMessage> ActiveMessage { get; set; }
               
        public async Task<(List<MongoDbLogItem> messages, long count)> GetDataGetDataAsyncForActive(
            int itemsPerPage,
            int currentPage,
            eMessageCategory category = eMessageCategory.All,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? keyword = null)
        {
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            var dateFilter = BuildDateFilter(startDate, endDate);
            var keywordFilter = BuildKeywordFilter(keyword);
            var categoryFilter = BuildCategoryFilter(category);

            var combinedFilter = Builders<MongoDbLogItem>.Filter.And(dateFilter, keywordFilter, categoryFilter);

            var messages = await collection.Find(combinedFilter)
                .Skip((currentPage - 1) * itemsPerPage)
                .Limit(itemsPerPage)
                .ToListAsync();
            var count = await collection.CountDocumentsAsync(combinedFilter);

            return (messages, count);
        }


        public Task<IEnumerable<MongoDbLogItem>> GetDataGetDataAsyncForArchive(int itemsPerPage, eMessageCategory category, int currentPage) => throw new NotImplementedException();
        
        
        
        public Task AcknowledgeAllMessages() => throw new NotImplementedException();
        public Task AcknowledgeMessage(ulong identity, int messageDigest) => throw new NotImplementedException();




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



        //public async Task UpdateAllMessagesInDb(List<PlainTcoMessage> msg)
        //{
        //    DateTime timeStampAcknowledged = DateTime.UtcNow;

        //    var _collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

        //    var _update = Builders<MongoDbLogItem>.Update.Set(item => item.TimeStampAcknowledged, timeStampAcknowledged);

        //    var _bulkops = new List<WriteModel<MongoDbLogItem>>();

        //    foreach (var _item in msg)
        //    {
        //        var _filter = Builders<MongoDbLogItem>.Filter.And(
        //         Builders<MongoDbLogItem>.Filter.Eq("Properties.ExtractedIdentity", _item.Identity),
        //         Builders<MongoDbLogItem>.Filter.Eq("Properties.sender.Payload.MessageDigest", _item.MessageDigest)
        //     );

        //        var _updateOne = new UpdateOneModel<MongoDbLogItem>(_filter, _update);
        //        _bulkops.Add(_updateOne);
        //    }

        //    if (_bulkops.Any())
        //    {
        //        await _collection.BulkWriteAsync(_bulkops);
        //    }
        //}

        //public async Task AcknowledgeAllMessages(IEnumerable<PlainTcoMessage> messageDisplay)
        //{
        //    if (CategorizedMessages == null)
        //    {
        //        CategorizeMessages(messageDisplay.ToList());
        //    }

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
        //}

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

        //public async Task UpdateMessageInDb(ulong? identity, int messageDigest)
        //{
        //    await FilterDataEntriesToUpdate();

        //    var entriesToUpdate = CachedDataEntriesToUpdate
        //        .Where(item => item.Properties.ExtractedIdentity == identity
        //                    && item.Properties.sender.Payload.MessageDigest == messageDigest)
        //        .ToList();

        //    //Problems with Dotnet5 and Logging
        //    DateTime timeStampAcknowledged = DateTime.UtcNow.AddHours(1);
        //    // Update the TimeStampAcknowledged field for each matching entry
        //    foreach (var entry in entriesToUpdate)
        //    {
        //        entry.TimeStampAcknowledged = timeStampAcknowledged;
        //    }

        //    // Get the collection
        //    var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

        //    // Build the update definition
        //    var update = Builders<MongoDbLogItem>.Update
        //        .Set(item => item.TimeStampAcknowledged, timeStampAcknowledged);

        //    // Apply the update to each matching document in the database
        //    foreach (var entry in entriesToUpdate)
        //    {
        //        var filter = Builders<MongoDbLogItem>.Filter.Eq(item => item.Id, entry.Id);
        //        await collection.UpdateOneAsync(filter, update);
        //    }
        //}

        //public async Task AutoAckNonPinnedMessages(IEnumerable<PlainTcoMessage> messages)
        //{
        //    foreach (var message in CategorizedMessages.NonActiveMessages.Where(m => m != null))
        //    {
        //        ulong? identity = message.Properties?.ExtractedIdentity;
        //        int? messageDigest = message.Properties?.sender?.Payload?.MessageDigest;
        //        if (identity.HasValue && messageDigest.HasValue)
        //        {
        //            await UpdateMessageInDb(identity, messageDigest.Value);
        //        }
        //    }
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

        //public async Task FilterDataEntriesToUpdate()
        //{
        //    CachedDataEntriesToUpdate = CachedData
        //                   .Where(item => item.TimeStampAcknowledged == null)
        //                   .ToList();
        //    await PurgeNewerSimilarMessages();
        //}

        //public void ExtractIdentity()
        //{
        //    var regex = new Regex(@"Identity: (\d+)", RegexOptions.Compiled);
        //    foreach (var item in CachedData)
        //    {
        //        var match = regex.Match(item.RenderedMessage);
        //        if (match.Success)
        //        {
        //            var identityString = match.Groups[1].Value;
        //            if (ulong.TryParse(identityString, out ulong identity))
        //            {
        //                item.Properties.ExtractedIdentity = identity;
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Failed to parse identity: {identityString}");  // Debug log
        //            }
        //        }
        //    }
        //}

        //public class MessageResult
        //{
        //    public IEnumerable<MongoDbLogItem> NonActiveMessages { get; set; }
        //    public IEnumerable<MongoDbLogItem> ActiveMessages { get; set; }
        //    public IEnumerable<MongoDbLogItem> ActiveMessagesPinned { get; set; }
        //}

        //public MessageResult CategorizedMessages { get; set; }

        ////Categorizes Messages depending on 
        //// if active in DB, and not in Plc => it shall Acknowledge them
        //// if active in DB and in PLC => Ignore
        //// if Pinned or not
        //public void CategorizeMessages(List<PlainTcoMessage> msg)
        //{
        //    var nonActiveInMessages = new List<MongoDbLogItem>();
        //    var activeInMessages = new List<MongoDbLogItem>();
        //    var activeInMessagesPinned = new List<MongoDbLogItem>();

        //    foreach (var item in CachedDataEntriesToUpdate)
        //    {
        //        var matchingMessage = msg.FirstOrDefault(m =>
        //            m.Identity == item.Properties?.ExtractedIdentity &&
        //            m.MessageDigest == item.Properties?.sender?.Payload?.MessageDigest);

        //        if (matchingMessage == null)
        //        {
        //            nonActiveInMessages.Add(item);
        //        }
        //        else if (matchingMessage.OnlinerMessage.Pinned.Cyclic == true)
        //        {
        //            activeInMessagesPinned.Add(item);
        //        }
        //        else
        //        {
        //            activeInMessages.Add(item);
        //        }
        //    }

        //    CategorizedMessages = new MessageResult
        //    {
        //        NonActiveMessages = nonActiveInMessages,
        //        ActiveMessages = activeInMessages,
        //        ActiveMessagesPinned = activeInMessagesPinned
        //    };
        //}

        //public async Task<int> GetTotalPagesAsync(int itemsPerPage, eMessageCategory? category)
        //{
        //    var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

        //    var filterBuilder = Builders<MongoDbLogItem>.Filter;
        //    var filter = filterBuilder.Empty;

        //    if (category.HasValue)
        //    {
        //        int categoryValue = (int)category.Value;
        //        var correspondingLevel = MessageCategoryMapper.MapMessageCategoryToLevel((eMessageCategory)categoryValue);
        //        var correspondingCategory = MessageCategoryMapper.MapLevelToMessageCategory(correspondingLevel);
        //        var levelsToInclude = MessageCategoryMapper.GetAllLevelsGreaterThanOrEqualTo(correspondingCategory);
        //        filter &= filterBuilder.In(item => item.Level, levelsToInclude);
        //    }

        //    var totalCount = await collection.CountDocumentsAsync(filter);
        //    var totalPages = (int)Math.Ceiling((double)totalCount / itemsPerPage);

        //    return totalPages;
    }

}
