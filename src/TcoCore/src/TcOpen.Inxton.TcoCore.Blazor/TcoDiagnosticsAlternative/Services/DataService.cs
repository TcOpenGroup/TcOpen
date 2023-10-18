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
            //List<PlainTcoMessage> messagesToUpdate = new List<PlainTcoMessage>();

            //foreach (var message in MessageDisplay.Where(m => m.Pinned)) 
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

        public async Task AcknowledgeSingleMessage(ObjectId id)
        {
            var update = Builders<MongoDbLogItem>.Update.Set(m => m.TimeStampAcknowledged, DateTime.UtcNow);

            var messageFromId = await _mongoDbHelper.GetPlainTcoMessageFromId(id, MessageDisplay);
            var  messageStillPinned = await _messageProcessingHelper.GetStillPinnedSingleMsgAfterSettingItFalse(messageFromId);

            if (messageStillPinned != null )
            {
                Console.WriteLine($"Still Active Message: {id}.");
                return;

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
                //await _mongoDbHelper.BulkUpdateTimestampAcknowledgedAsync(messageToAck);
            }
        }
    }

}
