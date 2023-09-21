using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using TcoCore.TcoDiagnosticsAlternative.LoggingToDb;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

using Vortex.Connector;

using static TcoCore.TcoDiagnosticsAlternativeViewModel;

namespace TcoCore
{
    public partial class TcoDiagnosticsAlternativeView
    {
        protected override void OnInitialized()
        {
            // If ViewModel is null, initialize it here
            var logger = new MongoLogger(); // Or get this from DI
            ViewModel = new TcoDiagnosticsAlternativeViewModel(logger, ViewModel._tcoObject);

            SetDefaultCategory(eMessageCategory.Warning);
            UpdateValuesOnChange(ViewModel._tcoObject);
            DiagnosticsUpdateTimer();
            StateHasChanged();
            AckMessages();
            uniqueMessages = UniqueMessages().ToList();
        }

        public static int SetDiagnosticsUpdateInterval(int value) => _diagnosticsUpdateInterval = value;
        private IEnumerable<eMessageCategory> eMessageCategories => Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
        private static int _diagnosticsUpdateInterval { get; set; } = 50;
        private Timer messageUpdateTimer;

        private async void MessageUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
                await InvokeAsync(() =>
                {
                    ViewModel.UpdateAndFetchMessages();
                    uniqueMessages = UniqueMessages().ToList();
                    StateHasChanged();
                });
        }

        private int currentPage = 1;
        private int itemsPerPage = 15;
        private List<PlainTcoMessageExtended> uniqueMessages;

        private int totalPages => (uniqueMessages.Count + itemsPerPage - 1) / itemsPerPage;

        private bool IsPreviousDisabled => currentPage == 1;
        private bool IsNextDisabled => currentPage == totalPages;

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

           private void DiagnosticsUpdateTimer()
        {
            if (messageUpdateTimer == null)
            {
                Console.WriteLine("Initializing timer.");
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


        private List<PlainTcoMessage> messages = new List<PlainTcoMessage>();

        private void OnNewMessageReceived(PlainTcoMessage newMessage)
        {
            // Add the new message to the list and re-render the component
            messages.Add(newMessage);
            StateHasChanged();
        }

        // Unsubscribe from the event when the component is disposed of
        public void Dispose()
        {
            ViewModel.NewMessageReceived -= OnNewMessageReceived;
        }

        private void AckMessages()
        {
            ViewModel.AcknowledgeMessages();
            StateHasChanged();
        }

        public IEnumerable<PlainTcoMessageExtended> UniqueMessages()
        {
            return ViewModel.DbMessageDisplay
                .Where(m => m.CategoryAsEnum >= MinMessageCategoryFilter)
                .GroupBy(m => new { m.Identity, m.Text, m.TimeStamp, m.Cycle })
                .Select(g => g.FirstOrDefault())
                .OrderByDescending(m => m.TimeStamp)
                .ThenByDescending(m => m.TimeStampAcknowledged.HasValue ? m.TimeStampAcknowledged.Value : DateTime.MinValue);
        }

        public string DiagnosticsMessage() => "Diag depth : " + DepthValue;
        public int MaxDiagnosticsDepth { get; set; } = 20;
        public static int _depthValue;
        public static int SetDefaultDepth(int item) => _depthValue = item;

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

        public eMessageCategory MinMessageCategoryFilter
        {
            get
            {
                return _minMessageCategoryFilter;
            }
            set
            {
                _minMessageCategoryFilter = value;
                ViewModel.MinMessageCategoryFilter = value;  // Update the ViewModel's property
            }
        }
        private eMessageCategory _minMessageCategoryFilter = DefaultCategory;


        public static eMessageCategory SetDefaultCategory(eMessageCategory item) => DefaultCategory = item;
        public static eMessageCategory DefaultCategory { get; set; } = eMessageCategory.Info;
    }
}
