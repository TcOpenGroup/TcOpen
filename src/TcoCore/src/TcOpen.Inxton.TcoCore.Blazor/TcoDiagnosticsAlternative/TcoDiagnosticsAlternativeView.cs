using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using TcoCore.TcoDiagnosticsAlternative.LoggingToDb;

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

            UpdateValuesOnChange(ViewModel._tcoObject);
            DiagnosticsUpdateTimer();
            StateHasChanged();
            //AckMessages();
        }

        public static int SetDiagnosticsUpdateInterval(int value) => _diagnosticsUpdateInterval = value;
        private static int _diagnosticsUpdateInterval { get; set; } = 50;
        private Timer messageUpdateTimer;

        private async void MessageUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
                await InvokeAsync(() =>
                {
                    ViewModel.UpdateAndFetchMessages();
                    StateHasChanged();
                });
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
            ViewModel.AckAllMessagesPinned();
            StateHasChanged();
        }

        // Method to get unique messages
        public IEnumerable<PlainTcoMessage> GetUniqueMessages()
        {
            return ViewModel.DbMessageDisplay
                .GroupBy(m => new { m.Identity, m.Text, m.TimeStamp, m.Cycle })
                .Select(g => g.FirstOrDefault());
        }
    }
}
