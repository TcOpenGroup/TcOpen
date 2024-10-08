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
    public partial class ArchivedMessages
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

        private DateTime DateLower { get; set; } = new DateTime(2022, 01, 01, 12, 0, 0);
        private DateTime DateUpper { get; set; }
        public bool IsDisabled { get; set; }
        public bool UseNative { get; set; }

        private string Keyword { get; set; } = "";
        private int _itemsPerPage = 15;
        int _currentPage = 1;
        int _totalPages = 10;
        public long TotalCount { get; set; }
        private IEnumerable<MongoDbLogItem> _messagesToDisplayArchive = new List<MongoDbLogItem>();

        protected override async Task OnInitializedAsync()
        {
           
            DropDownSelectedCategory = InitialCategory;
            DepthValue = InitalDepthValue;
            DateUpper = DateTime.Now;

            await GetDataFilteredAsyncForArchive();
        }

        protected override async Task OnParametersSetAsync()
        {
            await GetDataFilteredAsyncForArchive();
        }

        public async Task GetDataFilteredAsyncForArchive()
        {

            DateTime? startDate = DateLower;
            DateTime? endDate = DateUpper;
            string keyword = Keyword;
            eMessageCategory category = DropDownSelectedCategory;
            int _depthValue = DepthValue;

            var result = await DataService.GetDataAsyncForArchive(_itemsPerPage, _currentPage, category, _depthValue, startDate, endDate, keyword);

            TotalCount = result.count;
            _totalPages = (int)Math.Ceiling((double)TotalCount / _itemsPerPage);
            if (_totalPages < 1)
            {
                _totalPages = 1;
            }

           

            _messagesToDisplayArchive = result.messages.Where(x => x.DepthValue <= _depthValue).ToList();
        }

    }
}