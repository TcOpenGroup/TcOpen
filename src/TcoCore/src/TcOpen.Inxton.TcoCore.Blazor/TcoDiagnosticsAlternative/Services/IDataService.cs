using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TcoCore;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services
{
    public interface IDataService
    {
        public IEnumerable<PlainTcoMessage> ActiveMessage { get; set; }
        public Task<(List<MongoDbLogItem> messages, long count)> GetDataAsycForActive(int itemsPerPage, int currentPage, eMessageCategory category, int? depthValue, DateTime? startDate = null, DateTime? endDate = null, string keyword = null);
        public Task<(List<MongoDbLogItem> messages, long count)> GetDataAsyncForArchive      (int itemsPerPage, int currentPage, eMessageCategory category, int? depthValue, DateTime? startDate = null, DateTime? endDate = null, string keyword = null);
        public Task AcknowledgeAllMessages(IEnumerable<MongoDbLogItem> msg);

        public Task AcknowledgeMessage(ulong identity, int messageDigest);
        Task TryAutoAcknowledgeMessages(IEnumerable<MongoDbLogItem> msg);
    }
}