using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Bson;

using TcoCore;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services
{
    public interface IDataService
    {
        public IEnumerable<PlainTcoMessage> MessageDisplay { get; set; }
        public Task<(List<MongoDbLogItem> messages, long count)> GetDataAsycForActive(int itemsPerPage, int currentPage, eMessageCategory category, int? depthValue, DateTime? startDate = null, DateTime? endDate = null, string keyword = null);
        public Task<(List<MongoDbLogItem> messages, long count)> GetDataAsyncForArchive      (int itemsPerPage, int currentPage, eMessageCategory category, int? depthValue, DateTime? startDate = null, DateTime? endDate = null, string keyword = null);
        public Task AcknowledgeAllMessages();
        public Task AcknowledgeSingleMessage(ObjectId id);
        public Task AutoAcknowledgeMessages(IEnumerable<MongoDbLogItem> msg);
    }
}