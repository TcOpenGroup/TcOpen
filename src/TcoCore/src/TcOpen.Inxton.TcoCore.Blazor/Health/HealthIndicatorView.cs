using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore
{
    public partial class HealthIndicatorView
    {
        protected override void OnInitialized()
        {
            SetTimer();
        }

        private System.Timers.Timer messageUpdateTimer;

        private void SetTimer()
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
            Component?.UpdateHealthInfo();
            InvokeAsync(StateHasChanged);
        }
    }
}
