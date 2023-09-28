using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

using Microsoft.AspNetCore.Components;
using Vortex.Presentation;
using MongoDB.Bson.Serialization;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services;

using PlcDocu.TcoCore;


using TcOpen.Inxton;
using TcOpen.Inxton.Input;



namespace TcoCore
{
    public partial class TcoDiagnosticsAlternativeView
    {

        [Inject]
        public DataService DataService { get; set; }

        [Inject]
        public DataCleanupService DataCleanupService { get; set; }

        //protected override void OnInitialized()
        //{
        //    ViewModel = new TcoDiagnosticsAlternativeViewModel(ViewModel._tcoObject, DataService);
        //    UpdateValuesOnChange(ViewModel._tcoObject);
        //    DiagnosticsUpdateTimer();
        //}

        protected override async Task OnInitializedAsync()
        {
            //ViewModel = new TcoDiagnosticsAlternativeViewModel(ViewModel._tcoObject, DataService);

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

        //SetsUpdate Intervall via Consuming App
        public static int SetDiagnosticsUpdateInterval(int value) => _diagnosticsUpdateInterval = value;
        private static int _diagnosticsUpdateInterval { get; set; } = 100;
        private Timer messageUpdateTimer;

        //private IEnumerable<eMessageCategory> eMessageCategories => Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
        public string DiagnosticsStatus { get; set; } = "Diagnostics is not running";

        //Paging and Navigation
        private static int itemsPerPage { get; set; } = 15;
        private int currentPage = 1;
        private int totalPages => (CachedDataCount + itemsPerPage - 1) / itemsPerPage;
        public bool IsFirstDisabled => currentPage == 1;
        public bool IsLastDisabled => currentPage == totalPages;

        //Data and Lists, Logs from db, Messages from Plc
        private IEnumerable<MongoDbLogItem> MongdoDbLogItemFiltered { get; set; } = new List<MongoDbLogItem>();
        private List<PlainTcoMessage> messages = new List<PlainTcoMessage>();
        public int CachedDataCount { get; private set; }


        //public string DiagnosticsMessage() => "Diag depth : " + DepthValue;
        //public int MaxDiagnosticsDepth { get; set; } = 20;
        //public static int _depthValue;
        //public static int SetDefaultDepth(int item) => _depthValue = item;

        private async void MessageUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await InvokeAsync(async () =>
            {
                //DB Action
                await DataService.RefreshDataAsync();
                //Update plc Data
                ViewModel.UpdateMessages();

                CachedDataCount = DataService.GetCachedDataCount();
                DataService.ExtractIdentity();
                DataService.OrderData();
                MongdoDbLogItemFiltered = DataService.CachedData
                     .Where(m => m.Properties?.sender?.Payload != null)
                     .OrderByDescending(m => m.TimeStampAcknowledged.HasValue ? 0 : 1)
                     .ThenByDescending(m => m.TimeStampAcknowledged)
                     .ThenByDescending(m => m.UtcTimeStamp);

                StateHasChanged();
            });
        }
    
        public static void SetItemsPerPage(int value)
        {
            itemsPerPage = value;
        }

        private void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
            }
        }

        private void NextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
            }
        }

        public void FirstPage()
        {
            currentPage = 1;
        }

        public void LastPage()
        {
            currentPage = totalPages;
        }

        private void OnNewMessageReceived(PlainTcoMessage newMessage)
        {
            // Add the new message to the list and re-render the component
            messages.Add(newMessage);
            StateHasChanged();
        }

        // Unsubscribe from the event when the component is disposed of
        //public void Dispose()
        //{
        //    ViewModel.NewMessageReceived -= OnNewMessageReceived;
        //}

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

    }
}
