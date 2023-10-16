using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;
using global::TcoCore;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services;
namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.MessagesTabs
{
    public partial class ActiveMessages
    {
        [Parameter]
        public IDataService DataService { get; set; }

        [Parameter]
        public int MaxDiagnosticsDepth { get; set; }
       
        //[Parameter]
        //public string DiagnosticsMessage { get; set; }

        [Parameter]
        public int DepthValue { get; set; }

        //[Parameter]
        //public Func<ulong?, int, Task> AcknowledgeMessageCallback { get; set; }

        //[Parameter]
        //public Func<Task> AcknowledgeAllMessages { get; set; }

        //[Parameter]
        //public string ActiveMessagesCount { get; set; }

        [Parameter]
        public Func<MongoDbLogItem, bool> GetMessageStatusPinned { get; set; }

        [Parameter]
        public eMessageCategory InitialCategory { get; set; }

        public eMessageCategory DropdownSelectedCategory
        {
            get => _dropdownSelectedCategory;
            set => _dropdownSelectedCategory = value;
        }


        private eMessageCategory _dropdownSelectedCategory;
        private int _itemsPerPage = 6;
        int _currentPage = 1;
        int _totalPages = 10;
        private IEnumerable<MongoDbLogItem> _messagesToDisplay = new List<MongoDbLogItem>();

        protected override async Task OnInitializedAsync()
        {
            if (_dropdownSelectedCategory == default(eMessageCategory))
            {
                _dropdownSelectedCategory = InitialCategory;
            }

            await GetDataFilteredAsync();
        }

        // Called from Parent?
        protected override async Task OnParametersSetAsync()
        {
            await GetDataFilteredAsync();
        }

        public async Task GetDataFilteredAsync()
        {

            DateTime? startDate = DateTime.Now.AddDays(-7); // Example: 7 days ago
            DateTime? endDate = DateTime.Now; 
            string keyword = "";
            eMessageCategory category = _dropdownSelectedCategory; // Example category

            var result = await DataService.GetDataGetDataAsyncForActive(_itemsPerPage,_currentPage, category,startDate, endDate, keyword);

            List<MongoDbLogItem> filteredItems = result.messages;
            long totalCount = result.count;
            _totalPages = (int)Math.Ceiling((double)totalCount / _itemsPerPage);
            _messagesToDisplay = result.messages;
        }

    }
}