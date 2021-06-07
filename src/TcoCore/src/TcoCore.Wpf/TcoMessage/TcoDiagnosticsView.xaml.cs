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
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoCore
{
    /// <summary>
    /// Interaction logic for stMessageQueueView.xaml
    /// </summary>
    public partial class TcoDiagnosticsView : UserControl
    {
        public TcoDiagnosticsView()
        {            
            InitializeComponent();
            this.DiagnosticsUpdateTimer();
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
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() => 
            {
                if ((MessageHandler != null) && !MessageHandler.AutoUpdate)
                {
                    return;
                }

                if (UIElementAccessibilityHelper.IsInSight<Grid>(this.Element, this))
                {
                    MessageHandler?.UpdateMessages();
                }
            });
        }
        private TcoDiagnosticsViewModel MessageHandler
        {
            get { return this.DataContext as TcoDiagnosticsViewModel; }
        }
    }
}
