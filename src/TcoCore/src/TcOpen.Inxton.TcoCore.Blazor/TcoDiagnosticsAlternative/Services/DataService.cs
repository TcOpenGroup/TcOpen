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

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Helper;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services
{
    public class DataService : IDataService
    {
        private readonly MongoDbHelper _mongoDbHelper;
        private readonly MessageProcessingHelper _messageProcessingHelper;


        public DataService(IOptions<Configure.MongoDbSettings> settings)
        {
            _mongoDbHelper = new MongoDbHelper(settings);
            _messageProcessingHelper = new MessageProcessingHelper();
        }

        //set viwa the view.cs, gets it from ViewModel.cs via Plc
        public IEnumerable<PlainTcoMessage> MessageDisplay { get; set; }


        public async Task<(List<MongoDbLogItem> messages, long count)> GetDataAsycForActive(
                                        int itemsPerPage,
                                        int currentPage,
                                        eMessageCategory category,
                                        int? depthValue,
                                        DateTime? startDate = null,
                                        DateTime? endDate = null,
                                        string? keyword = null)
        {
            await _mongoDbHelper.CheckForDuplication();

            var combinedFilter = FilterHelper.BuildDateFilter(startDate, endDate)
                                        & FilterHelper.BuildKeywordFilter(keyword)
                                        & FilterHelper.BuildCategoryFilter(category)
                                        & FilterHelper.BuildTimeStampAcknowledgedFilter();

            return await _mongoDbHelper.GetDataAsync(itemsPerPage, currentPage, combinedFilter);
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
            var combinedFilter = FilterHelper.BuildDateFilter(startDate, endDate)
                                & FilterHelper.BuildKeywordFilter(keyword)
                                & FilterHelper.BuildCategoryFilter(category)
                                & FilterHelper.BuildTimeStampAcknowledgeNullFilter();

            return await _mongoDbHelper.GetDataAsync(itemsPerPage, currentPage, combinedFilter);
        }

        public async Task AcknowledgeAllMessages()
        {
            var messageDisplay = MessageDisplay.ToList();
            // Get the list of messages that are still pinned after setting them to false.
            var stillPinnedMessages = await _messageProcessingHelper.GetStillPinnedMsgsAfterSettingItFalse(MessageDisplay.ToList());

            // Determine which messages from MessageDisplay are not in the stillPinnedMessages list.
            var messagesToAcknowledge = messageDisplay
                                            .Where(x => !stillPinnedMessages.Any(m => m.Identity == x.Identity && m.MessageDigest == x.MessageDigest));

            // Convert PlainTcoMessage to MongoDbLogItem for the bulk update method.
            var mongoDbMessagesToAcknowledge = new List<MongoDbLogItem>(); 
            foreach (var message in messagesToAcknowledge)
            {
                var mongoDbLogItemId = await _mongoDbHelper.GetMongoDbLogItemIdFromPlainTcoMessage(message);
                if (mongoDbLogItemId.HasValue)
                {
                    var mongoDbMessage = new MongoDbLogItem
                    {
                        Id = mongoDbLogItemId.Value,
                    };
                    mongoDbMessagesToAcknowledge.Add(mongoDbMessage);
                }
            }

            // Bulk update the timestamp for all messages that are ready to be acknowledged.
            await _mongoDbHelper.BulkUpdateTimestampAcknowledgedAsync(mongoDbMessagesToAcknowledge);
        }


        public async Task AcknowledgeSingleMessage(ObjectId id)
        {
            var update = Builders<MongoDbLogItem>.Update.Set(m => m.TimeStampAcknowledged, DateTime.UtcNow);

            var messageFromId = await _mongoDbHelper.GetPlainTcoMessageFromId(id, MessageDisplay);
            var  messageStillPinned = await _messageProcessingHelper.GetStillPinnedSingleMsgAfterSettingItFalse(messageFromId);
            if (messageStillPinned != null)
            {
                var messageStillPinned2 = await _messageProcessingHelper.GetStillPinnedSingleMsgAfterSettingItFalse(messageFromId);
                if (messageStillPinned2 != null)
                {
                    return;
                }
            }
            var result = await _mongoDbHelper.UpdateTimeStampAcknowledgeAsync(id, update);
        }

        public async Task AutoAcknowledgeMessages(IEnumerable<MongoDbLogItem> msg)
        {
            var messagesInDbNoTimeStampAck = await _mongoDbHelper.QueryDb();
            var messagesFromPlc = MessageDisplay.ToList();
            var messageToAck = new List<MongoDbLogItem>();

            if (messagesInDbNoTimeStampAck != null)
            {
                foreach (var message in messagesInDbNoTimeStampAck)
                {
                    var identity = IdentityExtractor.ExtractIdentityFromRenderedMessage(message.RenderedMessage);

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
                await _mongoDbHelper.BulkUpdateTimestampAcknowledgedAsync(messageToAck);
            }
        }
    }

}
