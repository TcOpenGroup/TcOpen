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
        public Task<IEnumerable<MongoDbLogItem>> GetDataGetDataAsyncForArchive(int itemsPerPage, eMessageCategory category, int currentPage);
        public Task<(List<MongoDbLogItem> messages, long count)> GetDataGetDataAsyncForActive(int itemsPerPage, int currentPage, eMessageCategory category = eMessageCategory.All, DateTime? startDate = null, DateTime? endDate = null, string keyword = null);

        public Task AcknowledgeAllMessages();
        public Task AcknowledgeMessage(ulong identity, int messageDigest);
    }
}