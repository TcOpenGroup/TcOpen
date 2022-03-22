using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TcoCore;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoCore
{
    /// <summary>
    /// Simple indicator of <see cref="TcoObject"/> status.    
    /// </summary>
    public partial class HealthIndicatorView : UserControl
    {
        /// <summary>
        /// Creates new instance of <see cref="HealthIndicatorView"/>
        /// </summary>
        public HealthIndicatorView()
        {
            InitializeComponent();
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
            var isInSight = false;
            TcoObjectMessageHandler MessageHandler = null;
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                isInSight = UIElementAccessibilityHelper.IsInSight<Grid>(this.Element, this);
                MessageHandler = this.DataContext as TcoObjectMessageHandler;
            });

            if (isInSight)
            {
                MessageHandler?.UpdateHealthInfo();
            }
        }                             
    }
}
