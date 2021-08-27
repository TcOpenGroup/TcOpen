using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public partial class TcoDiagnosticsView
    {
        protected override void OnInitialized()
        {
            UpdateValuesOnChange(ViewModel._tcoObject);
        }

        private IEnumerable<eMessageCategory> eMessageCategories => Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
        private string Symbol { get; set; }
        public void OnSelectedMessage(PlainTcoMessage message)
        {
            ViewModel.SelectedMessage = message;
            if (ViewModel.SelectedMessage != null)
            {
                ViewModel.AffectedObject = (IVortexObject)ViewModel._tcoObject.GetConnector().IdentityProvider.GetVortexerByIdentity(ViewModel.SelectedMessage.Identity);
            }
        }

      

        private System.Timers.Timer messageUpdateTimer;
        private void DiagnosticsUpdateTimer()
        {
            if (messageUpdateTimer == null)
            {
                messageUpdateTimer = new System.Timers.Timer(2500);
                messageUpdateTimer.Elapsed += MessageUpdateTimer_Elapsed;
                messageUpdateTimer.AutoReset = true;
                messageUpdateTimer.Enabled = true;
            }
        }
        private void MessageUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            if ((ViewModel != null) && !ViewModel.AutoUpdate)
            {
                return;
            }

            ViewModel.UpdateMessages();
            InvokeAsync(StateHasChanged);

            
        }
    }
}
