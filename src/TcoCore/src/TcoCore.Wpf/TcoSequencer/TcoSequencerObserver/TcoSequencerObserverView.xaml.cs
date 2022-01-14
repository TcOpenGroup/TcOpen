using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoCore
{
    /// <summary>
    /// Interaction logic for TcoSequencerObserverView.xaml
    /// </summary>
    public partial class TcoSequencerObserverView : UserControl
    {
        public TcoSequencerObserverView()
        {
            InitializeComponent();
            SetTimer();
            this.DataContextChanged += TcoSequencerObserverView_DataContextChanged;
        }

        private void TcoSequencerObserverView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(!(this.DataContext is TcoSequencerObserverViewModel) && (this.DataContext is TcoSequencerObserver))
            {
                this.DataContext = new TcoSequencerObserverViewModel() { Model = this.DataContext };
            }
        }

        private System.Timers.Timer messageUpdateTimer;
        private void SetTimer()
        {
            if (messageUpdateTimer == null)
            {
                messageUpdateTimer = new System.Timers.Timer(1000);
                messageUpdateTimer.Elapsed += MessageUpdateTimer_Elapsed;
                messageUpdateTimer.AutoReset = true;
                messageUpdateTimer.Enabled = true;
            }
        }


        private void MessageUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var isInSight = false;
            TcoSequencerObserverViewModel VM = null;
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                isInSight = UIElementAccessibilityHelper.IsInSight<UserControl>(this, this);
                VM = this.DataContext as TcoSequencerObserverViewModel;
            });

            if (isInSight)
            {
                VM?.UpdateStepsTable();
            }
        }
    }
}
