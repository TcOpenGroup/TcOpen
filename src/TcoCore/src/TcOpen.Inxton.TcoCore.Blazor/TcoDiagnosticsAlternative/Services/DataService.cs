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

        public async Task UpdateAllMessagesInDb(List<PlainTcoMessage> msg)
        {
            DateTime timeStampAcknowledged = DateTime.UtcNow;

            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            var update = Builders<MongoDbLogItem>.Update.Set(item => item.TimeStampAcknowledged, timeStampAcknowledged);

            var bulkOps = new List<WriteModel<MongoDbLogItem>>();

            foreach (var item in CategorizedMessages.NonActiveMessages)
            {
                var filter = Builders<MongoDbLogItem>.Filter.Eq(i => i.Id, item.Id);
                var updateOne = new UpdateOneModel<MongoDbLogItem>(filter, update);
                bulkOps.Add(updateOne);
            }

            if (bulkOps.Any())  
            {
                await collection.BulkWriteAsync(bulkOps);
            }
        }
        public async Task TryAcknowledgeAllMessages(IEnumerable<PlainTcoMessage> messageDisplay)
        {
            if (CategorizedMessages == null)
            {
                CategorizeMessages(messageDisplay.ToList());
            }

            List<PlainTcoMessage> messagesToUpdate = new List<PlainTcoMessage>();

            foreach (var messageToAcknowledge in CategorizedMessages.ActiveMessagesPinned)
            {
                // Check the messageDisplay for the same identity
                var activeMessage = messageDisplay.FirstOrDefault(m => m.Identity == messageToAcknowledge.Properties?.ExtractedIdentity);
                bool isActive = false;

                if (activeMessage != null)
                {
                    activeMessage.OnlinerMessage.Pinned.Cyclic = false;
                    await PurgeNewerSimilarMessages();
                    await Task.Delay(1);  // Replacing Thread.Sleep with Task.Delay for async methods
                    isActive = activeMessage.OnlinerMessage.IsActive;
                }

                if (!isActive)
                {
                    messagesToUpdate.Add(activeMessage);
                }
                else
                {
                    Console.WriteLine($"Message Still Active {activeMessage}");
                }
            }

            if (messagesToUpdate.Any())
            {
                await UpdateAllMessagesInDb(messagesToUpdate);
            }
        }

        public async Task UpdateMessageInDb(ulong? identity, int messageDigest)
        {
            FilterDataEntriesToUpdate();

            var entriesToUpdate = CachedDataEntriesToUpdate
                .Where(item => item.Properties.ExtractedIdentity == identity
                            && item.Properties.sender.Payload.MessageDigest == messageDigest)
                .ToList();

            //Problems with Dotnet5 and Logging
            DateTime timeStampAcknowledged = DateTime.UtcNow.AddHours(1);
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

        public async Task AutoAckNonPinnedMessages(IEnumerable<PlainTcoMessage> messages)
        {
            foreach (var message in CategorizedMessages.NonActiveMessages.Where(m => m != null))
            {
                ulong? identity = message.Properties?.ExtractedIdentity;
                int? messageDigest = message.Properties?.sender?.Payload?.MessageDigest;
                if (identity.HasValue && messageDigest.HasValue)
                {
                    await UpdateMessageInDb(identity, messageDigest.Value);
                }
            }
        }

        public async Task PurgeNewerSimilarMessages()
        {
            var collection = _database.GetCollection<MongoDbLogItem>(_collectionName);

            foreach (var unacknowledgedMessage in CachedDataEntriesToUpdate)
            {
                // 2. For each of these messages, find newer similar messages based on identity and digest in the cache
                var newerIdenticalMessages = CachedDataEntriesToUpdate.Where(m =>
                    m.Properties.ExtractedIdentity == unacknowledgedMessage.Properties.ExtractedIdentity &&
                    m.Properties.sender.Payload.MessageDigest == unacknowledgedMessage.Properties.sender.Payload.MessageDigest &&
                    m.UtcTimeStamp > unacknowledgedMessage.UtcTimeStamp).ToList();

                if (newerIdenticalMessages.Any())
                {
                    // 3. Delete these newer similar messages from the database
                    var filter = Builders<MongoDbLogItem>.Filter.In(m => m.Id, newerIdenticalMessages.Select(x => x.Id));
                    await collection.DeleteManyAsync(filter);
                    Console.WriteLine($"Purged {newerIdenticalMessages.Count} duplicates for {unacknowledgedMessage.MessageTemplate.Text}");
                }
            }
        }

        public async void FilterDataEntriesToUpdate()
        {
            CachedDataEntriesToUpdate = CachedData
                           .Where(item => item.TimeStampAcknowledged == null)
                           .ToList();
            await PurgeNewerSimilarMessages();
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

        public class MessageResult
        {
            public IEnumerable<MongoDbLogItem> NonActiveMessages { get; set; }
            //public IEnumerable<MongDbLogItem> NonActiveMessagesPinned { get; set; }
            public IEnumerable<MongoDbLogItem> ActiveMessages { get; set; }
            public IEnumerable<MongoDbLogItem> ActiveMessagesPinned { get; set; }
        }

        public MessageResult CategorizedMessages { get; set; }

        //Categorizes Messages depending on 
        // if active in DB, and not in Plc => it shall Acknowledge them
        // if active in DB and in PLC => Ignore
        // if Pinned or not
        public void CategorizeMessages(List<PlainTcoMessage> msg)
        {
            var nonActiveInMessages = new List<MongoDbLogItem>();
            var activeInMessages = new List<MongoDbLogItem>();
            var activeInMessagesPinned = new List<MongoDbLogItem>();

            foreach (var item in CachedDataEntriesToUpdate)
            {
                var matchingMessage = msg.FirstOrDefault(m =>
                    m.Identity == item.Properties?.ExtractedIdentity &&
                    m.MessageDigest == item.Properties?.sender?.Payload?.MessageDigest);

                if (matchingMessage == null)
                {
                    nonActiveInMessages.Add(item);
                }
                else if (matchingMessage.OnlinerMessage.Pinned.Cyclic == true)
                {
                    activeInMessagesPinned.Add(item);
                }
                else
                {
                    activeInMessages.Add(item);
                }
            }

            CategorizedMessages = new MessageResult
            {
                NonActiveMessages = nonActiveInMessages,
                ActiveMessages = activeInMessages,
                ActiveMessagesPinned = activeInMessagesPinned
            };
        }


        public int GetCachedDataCount()
        {
            return CachedData?.Count() ?? 0;
        }

    }

}
