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
        public IDataService DataService { get; set; }

        /// <summary>
        /// DataCleanupService is responsible for limiting the entries to 5000 logs
        /// </summary>
        [Inject]
        public DataCleanupService DataCleanupService { get; set; }

        private IEnumerable<eMessageCategory> eMessageCategories => Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
        public string DiagnosticsStatus { get; set; } = "Diagnostics is not running";

        private string ActiveMessagesCount() => "Aktive Messages : " + ViewModel._tcoObject.MessageHandler.ActiveMessagesCount;
        private string DiagnosticsMessage() => "Diag depth : " + DepthValue;

        private int _maxDiagnosticsDepth = 20;
        private static int _depthValue;
        //public static int SetDefaultDepth(int item) => _depthValue = item;

        //private static eMessageCategory _minMessageCategoryFilter;

        public static eMessageCategory MinMessageCategoryFilter{ get; set; }

        public static void SetMinMessageCategoryFilter(eMessageCategory category)
        {
                MinMessageCategoryFilter = category;
        }

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

            UpdateValuesOnChange(ViewModel._tcoObject);
            DataService.MessageDisplay = ViewModel.MessageDisplay;
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

        private async void MessageUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await InvokeAsync(async () =>
            {
                
                await ViewModel.UpdateMessagesAsync();
                DataService.MessageDisplay = ViewModel.MessageDisplay;

                StateHasChanged();
            });
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

        public void Dispose()
        {
            _messageUpdateTimer?.Dispose();
        }

    }
}
