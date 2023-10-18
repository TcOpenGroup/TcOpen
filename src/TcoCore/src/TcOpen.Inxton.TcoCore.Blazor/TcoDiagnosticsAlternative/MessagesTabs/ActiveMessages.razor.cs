using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;
using global::TcoCore;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services;
using MongoDB.Bson;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.MessagesTabs
{
    public partial class ActiveMessages
    {
        [Parameter]
        public IDataService DataService { get; set; }

        [Parameter]
        public int MaxDiagnosticsDepth { get; set; }

        [Parameter]
        public int InitalDepthValue { get; set; }

        [Parameter]
        public int DepthValue { get; set; }

        [Parameter]
        public int DepthValueThreshold { get; set; }

        [Parameter]
        public Func<MongoDbLogItem, bool> GetMessageStatusPinned { get; set; }

        [Parameter]
        public eMessageCategory InitialCategory { get; set; }

        private eMessageCategory DropDownSelectedCategory { get; set; }

        private int _itemsPerPage = 5;
        int _currentPage = 1;
        int _totalPages = 10;
        private IEnumerable<MongoDbLogItem> _messagesToDisplay = new List<MongoDbLogItem>();

    protected override async Task OnInitializedAsync()
        {

            DropDownSelectedCategory = InitialCategory;
            DepthValue = InitalDepthValue;

            await GetDataFilteredAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await GetDataFilteredAsync();
            await AutoAcknowledgeMessages(_messagesToDisplay);
        }

        public async Task GetDataFilteredAsync()
        {
            DateTime? startDate = DateTime.Now.AddDays(-7); // Example: 7 days ago
            DateTime? endDate = DateTime.Now;
            string keyword = "";
            eMessageCategory category = DropDownSelectedCategory; // Example category
            int _depthValue = DepthValue;

            var result = await DataService.GetDataAsycForActive(_itemsPerPage, _currentPage, category, _depthValue, startDate, endDate, keyword);

            List<MongoDbLogItem> filteredItems = result.messages;
            long totalCount = result.count;
            _totalPages = (int)Math.Ceiling((double)totalCount / _itemsPerPage);
            if (_totalPages < 1)
            {
                _totalPages = 1;
            }
            _messagesToDisplay = result.messages;
        }

        private async Task AutoAcknowledgeMessages(IEnumerable<MongoDbLogItem> messages)
        {
            await DataService.AutoAcknowledgeMessages(messages);
        }

        private async Task AcknowledgeMessages()
        {
            await DataService.AcknowledgeAllMessages();
        }

        public async Task AcknowledgeMessage(ObjectId id)
        {
            await DataService.AcknowledgeSingleMessage(id);
        }
    }
}