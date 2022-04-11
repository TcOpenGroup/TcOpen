using System.Windows;
using System.Windows.Controls;
using TcoCore;
using TcOpen.Inxton;
using TcOpen.Inxton.TcoCore.Wpf;

namespace MainPlc
{
    /// <summary>
    /// Interaction logic for stMessageQueueView.xaml
    /// </summary>
    public partial class CUBaseMiniDiagnosticsView : UserControl
    {
        public CUBaseMiniDiagnosticsView()
        {
            InitializeComponent();
            this.DiagnosticsUpdateTimer();
        }

        private System.Timers.Timer messageUpdateTimer;
        private int diagnosticsDepth;

        private void DiagnosticsUpdateTimer()
        {
            if (messageUpdateTimer == null)
            {
                messageUpdateTimer = new System.Timers.Timer(updateRate);
                messageUpdateTimer.Elapsed += MessageUpdateTimer_Elapsed;
                messageUpdateTimer.AutoReset = true;
                messageUpdateTimer.Enabled = true;
            }
        }

        float updateRate = 2500;
        int updateCount = 0;

        private void MessageUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateMessages();
        }

        private TcoDiagnosticsViewModel _context { get { return this.DataContext as TcoDiagnosticsViewModel; } }

        private void UpdateMessages()
        {
           
            var inSight = false;
            TcoDiagnosticsViewModel MessageHandler = null;
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                if ((MessageHandler != null) && !MessageHandler.AutoUpdate)
                {
                    return;
                }

                inSight = UIElementAccessibilityHelper.IsInSight<Grid>(this.Element, this);
                if (inSight)
                {
                    MessageHandler = this.DataContext as TcoDiagnosticsViewModel;
                }
            });
            bool isAutoUpdate = MessageHandler == null ? false : MessageHandler.AutoUpdate;

            if (inSight && isAutoUpdate)
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                
                MessageHandler?.UpdateMessages();

                sw.Stop();
                updateRate = (updateRate + sw.ElapsedMilliseconds) / ++updateCount;
                messageUpdateTimer.Interval = updateRate < 100 ? 100 : updateRate + 100;               
            }

          

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                _context?.UpdateMessages();
            }
        }

        /// <summary>
        /// Gets or sets the diagnostics depth for the observed object.
        /// </summary>
        public int DiagnosticsDepth
        {
            get { return this._context.DiagnosticsDepth; }
            set { diagnosticsDepth = value; this._context.DiagnosticsDepth = value; }
        }
    }
}
