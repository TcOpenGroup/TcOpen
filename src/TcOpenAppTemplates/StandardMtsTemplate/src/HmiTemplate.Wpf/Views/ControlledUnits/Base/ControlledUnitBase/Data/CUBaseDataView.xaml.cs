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
using TcoCore;

namespace MainPlc
{
    /// <summary>
    /// Interaction logic for CUBaseControlView.xaml
    /// </summary>
    public partial class CUBaseDataView : UserControl
    {
        public CUBaseDataView()
        {
            InitializeComponent();
            SetTimer();
        }

        private System.Timers.Timer messageUpdateTimer;
        private void SetTimer()
        {
            if (messageUpdateTimer == null)
            {
                messageUpdateTimer = new System.Timers.Timer(750);
                messageUpdateTimer.Elapsed += UpdateDataTimer_Elapsed;
                messageUpdateTimer.AutoReset = true;
                messageUpdateTimer.Enabled = true;
            }
        }

        private ProcessData ProcessData { get; set; }
        private dynamic CuProcessData { get; set; }

        private int c = 0;
        private void UpdateDataTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var isInSight = false;
            CUBaseControlViewModel context = null;
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                context = this.DataContext as CUBaseControlViewModel;
                isInSight = UIElementAccessibilityHelper.IsInSight<Grid>(this.Element, this);                
            });

            if (isInSight)
            {
                if (ProcessData == null) ProcessData = context?.Component.GetDescendants<ProcessData>().FirstOrDefault();
                if (CuProcessData == null) CuProcessData = context?.Component.GetChildren<CUProcessDataBase>().FirstOrDefault();
                               
                CuProcessData.FlushOnlineToShadow();                
                ProcessData.FlushOnlineToShadow();               
            }
        }
    }
}
