using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

using Microsoft.AspNetCore.Components;

using MongoDB.Bson.Serialization;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services;


namespace TcoCore
{
    public partial class TcoDiagnosticsAlternativeView
    {

        [Inject]
        public DataService DataService { get; set; }

        [Inject]
        public DataCleanupService DataCleanupService { get; set; }

        public static int MaxDatabaseEntries { get; set; } = 1000;
        public static int SetDiagnosticsUpdateInterval(int value) => _diagnosticsUpdateInterval = value;
        private IEnumerable<eMessageCategory> eMessageCategories => Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
        private static int _diagnosticsUpdateInterval { get; set; } = 100;
        private Timer messageUpdateTimer;
        private static int itemsPerPage { get; set; } = 20;
        private int currentPage = 1;

        private IEnumerable<MongoDbLogItem> MyData { get; set; } = new List<MongoDbLogItem>();


        protected override async Task OnInitializedAsync()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(SenderProperties)))
            {
                BsonClassMap.RegisterClassMap<SenderProperties>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }


            UpdateValuesOnChange(ViewModel._tcoObject);

            DiagnosticsUpdateTimer();
        }

        private void DiagnosticsUpdateTimer()
        {
            if (messageUpdateTimer == null)
            {
                //Console.WriteLine("Initializing timer.");
                messageUpdateTimer = new Timer(_diagnosticsUpdateInterval);
                messageUpdateTimer.Elapsed += MessageUpdateTimer_Elapsed;
                messageUpdateTimer.AutoReset = true;
                messageUpdateTimer.Enabled = true;
            }
            else
            {
                Console.WriteLine("Timer already initialized.");
            }
        }

        private async void MessageUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await InvokeAsync(async () =>
            {
                await DataService.RefreshDataAsync();
                DataService.ExtractIdentity();
                DataService.OrderData();
                MyData = DataService.CachedData
                     .Where(m => m.Properties?.sender?.Payload != null)
                     .OrderByDescending(m => m.TimeStampAcknowledged.HasValue ? 0 : 1)
                     .ThenByDescending(m => m.TimeStampAcknowledged)
                     .ThenByDescending(m => m.UtcTimeStamp);

                StateHasChanged();
            });
        }

        //public static void SetMaxDatabaseEntries ( int value)
        //{
        //    MaxDatabaseEntries = value;
        //}


        //public static void SetItemsPerPage(int value)
        //{
        //    itemsPerPage = value;
        //}

        //private void PreviousPage()
        //{
        //    if (currentPage > 1)
        //    {
        //        currentPage--;
        //    }
        //}

        //private void NextPage()
        //{
        //    if (currentPage < totalPages)
        //    {
        //        currentPage++;
        //    }
        //}

        //public void FirstPage()
        //{
        //    currentPage = 1;
        //}

        //public void LastPage()
        //{
        //    currentPage = totalPages;
        //}

        //public bool IsFirstDisabled => currentPage == 1;

        //public bool IsLastDisabled => currentPage == totalPages;

       

        //private List<PlainTcoMessage> messages = new List<PlainTcoMessage>();

        //private void OnNewMessageReceived(PlainTcoMessage newMessage)
        //{
        //    // Add the new message to the list and re-render the component
        //    messages.Add(newMessage);
        //    StateHasChanged();
        //}

        //// Unsubscribe from the event when the component is disposed of
        //public void Dispose()
        //{
        //    ViewModel.NewMessageReceived -= OnNewMessageReceived;
        //}

        //private void AckMessages()
        //{
        //    ViewModel.AcknowledgeMessages();
        //    StateHasChanged();
        //}

        //public IEnumerable<PlainTcoMessageExtended> UniqueMessages()
        //{
        //    return ViewModel.DbMessageDisplay
        //        .Where(m => m.CategoryAsEnum >= MinMessageCategoryFilter)
        //        .GroupBy(m => new { m.Identity, m.Text, m.TimeStamp, m.Cycle })
        //        .Select(g => g.FirstOrDefault())
        //        .OrderByDescending(m => m.TimeStamp)
        //        .ThenByDescending(m => m.TimeStampAcknowledged.HasValue ? m.TimeStampAcknowledged.Value : DateTime.MinValue);
        //}

        //public string DiagnosticsMessage() => "Diag depth : " + DepthValue;
        //public int MaxDiagnosticsDepth { get; set; } = 20;
        //public static int _depthValue;
        //public static int SetDefaultDepth(int item) => _depthValue = item;

        //public int DepthValue
        //{
        //    get
        //    {
        //        if (_depthValue == 0)
        //            _depthValue = ViewModel._tcoObject.MessageHandler.DiagnosticsDepth;
        //        return _depthValue;
        //    }
        //    set
        //    {
        //        _depthValue = value;
        //        ViewModel._tcoObject.MessageHandler.DiagnosticsDepth = value;
        //    }
        //}

        //public eMessageCategory MinMessageCategoryFilter
        //{
        //    get
        //    {
        //        return _minMessageCategoryFilter;
        //    }
        //    set
        //    {
        //        _minMessageCategoryFilter = value;
        //        ViewModel.MinMessageCategoryFilter = value;  // Update the ViewModel's property
        //    }
        //}
        //private eMessageCategory _minMessageCategoryFilter = DefaultCategory;


        //public static eMessageCategory SetDefaultCategory(eMessageCategory item) => DefaultCategory = item;
        //public static eMessageCategory DefaultCategory { get; set; } = eMessageCategory.All;

        //public int UniqueMessagesCount => uniqueMessages?.Count ?? 0;

        //public int DbMessageDisplayCount => ViewModel.DbMessageDisplay?.Count() ?? 0;

    }
}
