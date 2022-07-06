using System.Windows;
using System.Windows.Controls;

namespace TcoIo
{


    public partial class EtcMasterBase_62654B43TopologyDeviceView : UserControl
    {
        public EtcMasterBase_62654B43TopologyDeviceView()
        {
            InitializeComponent();
        }

        private void OpenDeviceDetails(object sender, System.Windows.RoutedEventArgs e)
        {
            Window window = new Window();
            Grid grid = new Grid();
            TcoEthercatMasterDeviceTopologyView device = new TcoEthercatMasterDeviceTopologyView();
            device.DataContext = this.DataContext;
            grid.Children.Add(device);
            window.Content = grid;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.MaxHeight = 1200;
            window.Show();
        }
    }
}
