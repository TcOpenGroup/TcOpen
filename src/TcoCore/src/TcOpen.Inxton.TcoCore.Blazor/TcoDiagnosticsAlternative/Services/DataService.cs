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

        //set viwa the view.cs, gets it from ViewModel.cs via Plc
        public IEnumerable<PlainTcoMessage> MessageDisplay { get; set; }
        private List<PlainTcoMessage> StillActiveMessagesAfterPinnedTest { get; set; }

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

            await CheckForDuplication(collection);

            var combinedFilter = FilterHelper.BuildDateFilter(startDate, endDate)
                                        & FilterHelper.BuildKeywordFilter(keyword)
                                        & FilterHelper.BuildCategoryFilter(category)
                                        & FilterHelper.BuildTimeStampAcknowledgedFilter();

            var messages = await collection.Find(combinedFilter)
                .Skip((currentPage - 1) * itemsPerPage)
                .Limit(itemsPerPage)
                .SortByDescending(item => item.UtcTimeStamp)
                .ToListAsync();
            var count = await collection.CountDocumentsAsync(combinedFilter);

            
            return (messages, count);
        }

        private async Task CheckForDuplication(IMongoCollection<MongoDbLogItem> collection)
        {
            Console.WriteLine("Starting CheckForDuplication...");

            var filter = FilterHelper.BuildTimeStampAcknowledgedFilter();
            var messages = await collection.Find(filter).ToListAsync();

            Console.WriteLine($"Total messages fetched: {messages.Count}");

            var distinctCombinations = messages
                .Where(m => ExtractIdentityFromRenderedMessage(m.RenderedMessage).HasValue && m.Properties.sender.Payload.MessageDigest != 0)
                .Select(m => new
                {
                    Identity = ExtractIdentityFromRenderedMessage(m.RenderedMessage),
                    Digest = m.Properties.sender.Payload.MessageDigest
                })
                .Distinct();

            Console.WriteLine($"Distinct combinations found: {distinctCombinations.Count()}");

            foreach (var combo in distinctCombinations)
            {
                var similarMessages = messages.Where(m =>
                    ExtractIdentityFromRenderedMessage(m.RenderedMessage) == combo.Identity &&
                    m.Properties.sender.Payload.MessageDigest == combo.Digest )
                    .ToList();

                Console.WriteLine($"For combo (Identity: {combo.Identity}, Digest: {combo.Digest}), found {similarMessages.Count} similar messages.");

                if (similarMessages.Count > 1)
                {
                    Console.WriteLine($"Purging {similarMessages.Count - 1} duplicate messages for combo (Identity: {combo.Identity}, Digest: {combo.Digest})");
                    await PurgeMultipleEntriesAsync(collection, similarMessages);
                }
            }
        }

        private async Task PurgeMultipleEntriesAsync(IMongoCollection<MongoDbLogItem> collection, List<MongoDbLogItem> messagesToPurge)
        {
            var idsToDelete = messagesToPurge.Select(m => m.Id).ToList();
            var deleteFilter = Builders<MongoDbLogItem>.Filter.In("_id", idsToDelete);
            await collection.DeleteManyAsync(deleteFilter);
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

            var combinedFilter = FilterHelper.BuildDateFilter(startDate, endDate)
                                & FilterHelper.BuildKeywordFilter(keyword)
                                & FilterHelper.BuildCategoryFilter(category)
                                & FilterHelper.BuildTimeStampAcknowledgedForArchiveFilter();


            var messages = await collection.Find(combinedFilter)
                .Skip((currentPage - 1) * itemsPerPage)
                .Limit(itemsPerPage)
                .SortByDescending(item => item.UtcTimeStamp)
                .ToListAsync();

            var count = await collection.CountDocumentsAsync(combinedFilter);
            return (messages, count);
        }

        public Task AcknowledgeMessage(ulong identity, int messageDigest) => throw new NotImplementedException();



        //public async Task AutoAcknowledgeMessages(IEnumerable<MongoDbLogItem> msg)
        //{
        //    var messagesInDb = await QueryDb();
        //    var messagesFromPlc = MessageDisplay.ToList();

        //    var messageToAck = new List<MongoDbLogItem>();

        //    if (messagesInDb != null)
        //    {
        //        messageToAck = messagesInDb;
        //        foreach (var message in messagesInDb)
        //        {
        //            var identity = ExtractIdentityFromRenderedMessage(message.RenderedMessage);
        //            if (!identity.HasValue) continue;

        //            bool isActive = messagesFromPlc.Any(x =>
        //                x.Identity == (ulong)identity.Value &&
        //                x.MessageDigest == message.Properties.sender.Payload.MessageDigest
        //            );

        //            if (!isActive && message != null)
        //            {
        //                messageToAck.Remove(message);
        //            }
        //        }

        //    await BulkUpdateTimestampAcknowledgedAsync(messageToAck);
        //    }
        //}

        public async Task AutoAcknowledgeMessages(IEnumerable<MongoDbLogItem> msg)
        {
            var messagesInDbNoTimeStampAck = await QueryDb();
            var messagesFromPlc = MessageDisplay.ToList();
            var messageToAck = new List<MongoDbLogItem>();

            if (messagesInDbNoTimeStampAck != null)
            {
                foreach (var message in messagesInDbNoTimeStampAck)
                {
                    var identity = ExtractIdentityFromRenderedMessage(message.RenderedMessage);

                    bool isInActive;

                    if (identity.HasValue)
                    {
                        isInActive = !messagesFromPlc.Any(x =>
                            x.Identity == (ulong)identity.Value &&
                            x.MessageDigest == message.Properties.sender.Payload.MessageDigest
                        );
                    }
                    else
                    {
                        isInActive = true;
                    }

                    if (isInActive && message != null)
                    {
                        messageToAck.Add(message);
                    }
                }
                await BulkUpdateTimestampAcknowledgedAsync(messageToAck); // Update the timestamps for messages to acknowledge.
            }
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

        public async Task AcknowledgeAllMessages()
        {
            //List<PlainTcoMessage> messagesToUpdate = new List<PlainTcoMessage>();

            //foreach (var message in MessageDisplay.Where(m => m.Pinned))
            //{
            //    if (message != null)
            //    {
            //        await GetListofStillActiveMessages(message);
            //    }
            //}

            //var filteredActiveMessages = StillActiveMessagesAfterPinnedTest;

            //foreach (var message in MessagesInDbFiltered)
            //{
            //    var identity = ExtractIdentityFromRenderedMessage(message.RenderedMessage);
            //    if (!identity.HasValue) continue;

            //    bool isMessageAcknowledged = filteredActiveMessages.Any(x =>
            //        x.Identity == (ulong)identity.Value &&
            //        x.MessageDigest == message.Properties.sender.Payload.MessageDigest
            //    );

            //    if (isMessageAcknowledged && MessagesReadyToAck != null)
            //    {
            //        MessagesReadyToAck.Remove(message);
            //    }
            //}
            //await BulkUpdateTimestampAcknowledgedAsync();
        }

        public async Task<List<MongoDbLogItem>> QueryDb()
        {
            var _collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            // Step 1: Query for entries with TimestampAcknowledged == null
            var filter = FilterHelper.BuildTimeStampAcknowledgedFilter();
            var projection = Builders<MongoDbLogItem>.Projection
                .Include(item => item.TimeStampAcknowledged)
                .Include(item => item.Properties.sender.Payload.MessageDigest)
                .Include(item => item.RenderedMessage);

            var unAcknowledgedMessages = await _collection.Find(filter).Project<MongoDbLogItem>(projection).ToListAsync();

            if (!unAcknowledgedMessages.Any())
            {
                return new List<MongoDbLogItem>();
            }
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

        private async Task GetListofStillActiveMessages(PlainTcoMessage message)
        {
            if (message == null)
            {
                return;
            }

                message.OnlinerMessage.Pinned.Cyclic = false;

                //await PurgeNewerSimilarMessages();

                await Task.Delay(2);
                if (message.OnlinerMessage.IsActive)
                {
                    StillActiveMessagesAfterPinnedTest.Add(message);  
                }
        }

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


        public int CalculateDepth(MongoDbLogItem msg)
        {
            if (string.IsNullOrEmpty(msg.Properties.sender.Payload.ParentSymbol))
            {
                return 1;
            }

            int depth = msg.Properties.sender.Payload.ParentSymbol.Split('.').Length;
            return depth;
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
    }

}
