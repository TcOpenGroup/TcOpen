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
    public partial class TcoDiagnosticsAlternativeView : IDisposable
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

        private IEnumerable<eMessageCategory> eMessageCategories => Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
        public string DiagnosticsStatus { get; set; } = "Diagnostics is not running";

        private int _totalPages;
        public int TotalPages => _totalPages;
        private static int _itemsPerPage = 20;
        private int _currentPage = 1;
        public bool IsFirstDisabled => _currentPage == 1;
        public bool IsLastDisabled => _currentPage == TotalPages;

        //Data and Lists, Logs from db, Messages from Plc
        private IEnumerable<MongoDbLogItem> _mongoDbLogItemFiltered = new List<MongoDbLogItem>();
        private List<PlainTcoMessage> _messagesPlc = new List<PlainTcoMessage>();
        public int CachedDataCount { get; private set; }

        private string ActiveMessagesCount() => "Aktive Messages : " + ViewModel._tcoObject.MessageHandler.ActiveMessagesCount;
        private string DiagnosticsMessage() => "Diag depth : " + DepthValue;
        private int _maxDiagnosticsDepth = 20;
        private static int _depthValue;
        public static int SetDefaultDepth(int item) => _depthValue = item;

        public eMessageCategory MinMessageCategoryFilter { get; set; } = DefaultCategory;
        public static eMessageCategory DefaultCategory { get; set; } = eMessageCategory.Info;

        public bool GetMessageStatusPinned(MongoDbLogItem message)
        {
            var matchingMessage = ViewModel.MessageDisplay.FirstOrDefault(
                m => m.Identity == message.Properties?.ExtractedIdentity &&
                m.MessageDigest == message.Properties?.sender?.Payload.MessageDigest);
            return matchingMessage?.Pinned ?? false;
        }

        /// <summary>
        /// SetsUpdate Intervall via Consuming App
        /// </summary>
        public static int SetDiagnosticsUpdateInterval(int value) => _diagnosticsUpdateInterval = value;
        private static int _diagnosticsUpdateInterval = 200;
        private Timer _messageUpdateTimer;

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

            _totalPages = await DataService.GetTotalPagesAsync(_itemsPerPage, MinMessageCategoryFilter);

            UpdateValuesOnChange(ViewModel._tcoObject);

            InitializeDiagnosticsUpdateTimer();
        }

        public void InitializeDiagnosticsUpdateTimer()
        {
            if (_messageUpdateTimer == null)
            {
                _messageUpdateTimer = new Timer(_diagnosticsUpdateInterval);
                _messageUpdateTimer.Elapsed += MessageUpdateTimer_Elapsed;
                _messageUpdateTimer.AutoReset = true;
                _messageUpdateTimer.Enabled = true;
            }
            else
            {
                Console.WriteLine("Timer already initialized.");
            }
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
                await DataService.AcknowledgeAllMessages(ViewModel.MessageDisplay);
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
                await DataService.AcknowledgeSingleMessage(identity, messageDigest, ViewModel.MessageDisplay);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void SetDefaultCategoryFilter(eMessageCategory category)
        {
            DefaultCategory = category;
        }

        public static void SetItemsPerPage(int value)
        {
            _itemsPerPage = value;
        }

        public void Dispose()
        {
            _messageUpdateTimer?.Dispose();
        }
        private async void MessageUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await InvokeAsync(async () =>
            {
                //DB Action
                await DataService.RefreshDataAsync( _itemsPerPage, MinMessageCategoryFilter, _currentPage);
                //Update plc Data
                await ViewModel.UpdateMessagesAsync();

                _mongoDbLogItemFiltered = OrderMongoDbLogItems(DataService.CachedData)
                         .Where(x => ViewModel.CalculateDepth(x) <= DepthValue);
                DataService.ExtractIdentity();
                await DataService.FilterDataEntriesToUpdate();
                DataService.CategorizeMessages(ViewModel.MessageDisplay.ToList());

                await DataService.PurgeNewerSimilarMessages();
                //await DataService.AutoAckNonPinnedMessages(ViewModel.MessageDisplay);

                StateHasChanged();
            });
        }

        private async Task PreviousPage()
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                _mongoDbLogItemFiltered = await DataService.GetDataAsync(_itemsPerPage, MinMessageCategoryFilter, _currentPage);
            }
        }

        private async Task NextPage()
        {
            if (_currentPage < TotalPages)
            {
                _currentPage++;
                _mongoDbLogItemFiltered = await DataService.GetDataAsync(_itemsPerPage, MinMessageCategoryFilter, _currentPage);
            }
        }

        public async Task FirstPage()
        {
            _currentPage = 1;
            _mongoDbLogItemFiltered = await DataService.GetDataAsync(_itemsPerPage, MinMessageCategoryFilter, _currentPage);
        }

        public async Task LastPage()
        {
            _currentPage = TotalPages;
            _mongoDbLogItemFiltered = await DataService.GetDataAsync(_itemsPerPage, MinMessageCategoryFilter, _currentPage);
        }

        private IEnumerable<MongoDbLogItem> OrderMongoDbLogItems(IEnumerable<MongoDbLogItem> items)
        {
            return items
                .Where(m => m.Properties?.sender?.Payload != null)
                .OrderByDescending(m => m.TimeStampAcknowledged.HasValue ? 0 : 1)
                .ThenByDescending(m => m.UtcTimeStamp)
                .ThenByDescending(m => m.TimeStampAcknowledged);
        }

        public int DepthValue
        {
            get
            {
                if (_depthValue == 0)
                    _depthValue = ViewModel._tcoObject.MessageHandler.DiagnosticsDepth;
                return _depthValue;
            }
            set
            {
                _depthValue = value;
                ViewModel._tcoObject.MessageHandler.DiagnosticsDepth = value;
            }
        }

    }
}
