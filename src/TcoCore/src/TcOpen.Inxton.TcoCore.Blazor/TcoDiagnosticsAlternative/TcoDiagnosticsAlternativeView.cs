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
        }


        private static int _diagnosticsUpdateInterval { get; set; } = 100;
        private Timer messageUpdateTimer;

        private async void MessageUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await InvokeAsync(() =>
            {
                ViewModel.UpdateAndFetchMessages();
                StateHasChanged(); // Re-render the component
            });

        }


        private void DiagnosticsUpdateTimer()
        {
            if (messageUpdateTimer == null)
            {
                messageUpdateTimer = new Timer(_diagnosticsUpdateInterval);
                messageUpdateTimer.Elapsed += MessageUpdateTimer_Elapsed;
                messageUpdateTimer.AutoReset = true;
                messageUpdateTimer.Enabled = true;
            }
        }

        private List<PlainTcoMessage> messages = new List<PlainTcoMessage>();


        private void OnNewMessageReceived(PlainTcoMessage newMessage)
        {
            // Add the new message to the list and re-render the component
            messages.Add(newMessage);
            StateHasChanged();
        }

        private void FetchMessages()
        {
            ViewModel.UpdateMessages();
        }


        // Unsubscribe from the event when the component is disposed of
        public void Dispose()
        {
            ViewModel.NewMessageReceived -= OnNewMessageReceived;
        }
    }
}
