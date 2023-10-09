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
        /// <summary>
        /// DataService connects to the Mongo Database, given from the Blazor Context Manager
        /// It is responsible for Fetching, Updating, Filtering and so on
        /// </summary>
        [Inject]
        public DataService DataService { get; set; }

        /// <summary>
        /// DataCleanupService is responsible for limiting the entries to 5000 logs
        /// </summary>
        [Inject]
        public DataCleanupService DataCleanupService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Necessary for the Deserialization of the BSON Log Entries,
            //because there are different structures of Json Objects, which are handled differently
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

        public DateTime timetest { get; set; } = DateTime.Now;

        /// <summary>
        /// SetsUpdate Intervall via Consuming App
        /// </summary>
        public static int SetDiagnosticsUpdateInterval(int value) => _diagnosticsUpdateInterval = value;
        private static int _diagnosticsUpdateInterval { get; set; } = 300;
        private Timer messageUpdateTimer;

        //private IEnumerable<eMessageCategory> eMessageCategories => Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
        public string DiagnosticsStatus { get; set; } = "Diagnostics is not running";

        //Paging and Navigation
        private static int itemsPerPage { get; set; } = 20;
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
                MongdoDbLogItemFiltered = OrderMongoDbLogItems(DataService.CachedData);
                DataService.ExtractIdentity();
                DataService.FilterDataEntriesToUpdate();
                DataService.CategorizeMessages(ViewModel.MessageDisplay.ToList());
                await DataService.PurgeNewerSimilarMessages();
                await DataService.AutoAckNonPinnedMessages(ViewModel.MessageDisplay);

                StateHasChanged();
            });
        }

        //there is a big problem, which leads to alot of complicated stuff that
        // there is not real possibility of knowing, when a Message is still active.
        // Not the Property IsActive or anything else, in the TcoDiagnostics Component it gets solved without
        // just setting Pinned to false, and displaying the new Message.
        // i do not like that, several reasons. => then the origin Timestamp is wrong, Cycle is wrong.
        // but my understanding is, as long as the Error Cause is active, the Messages shall remain untouched.
        // Problem now, when i set pinned to false, and check which Messages still occur, the Logger creates new Logs.
        // => my Solution is, to have a Purge Method(), which purges from my DB newer Messages, when the Origin Error Message is not Acknowledged

        public async Task AcknowledgeAllMessages()
        {
            try
            {
                await DataService.TryAcknowledgeAllMessages(ViewModel.MessageDisplay);
                //foreach (var item in DataService.CachedDataEntriesToUpdate)
                //{
                //TcoAppDomain.Current.Logger.Information("All message acknowledged {@payload}", new { rootObject = ViewModel._tcoObject.HumanReadable, rootSymbol = ViewModel._tcoObject.Symbol });
                //}
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task AcknowledgeMessage(ulong? identity, int messageDigest)
        {
            try
            {
                await DataService.UpdateMessageInDb(identity, messageDigest);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

        private IEnumerable<MongoDbLogItem> OrderMongoDbLogItems(IEnumerable<MongoDbLogItem> items)
        {
            return items
                .Where(m => m.Properties?.sender?.Payload != null)
                .OrderByDescending(m => m.TimeStampAcknowledged.HasValue ? 0 : 1)
                .ThenByDescending(m => m.UtcTimeStamp)
                .ThenByDescending(m => m.TimeStampAcknowledged);
        }


        //Unsubscribe from the event when the component is disposed of
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
