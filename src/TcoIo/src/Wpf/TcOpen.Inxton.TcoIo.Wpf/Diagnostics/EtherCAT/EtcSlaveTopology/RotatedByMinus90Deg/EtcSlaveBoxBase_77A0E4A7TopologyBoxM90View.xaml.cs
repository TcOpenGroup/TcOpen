using System.Windows;
using System.Windows.Controls;
using TcoIo.Converters.Utilities;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for fbSampleComponentView.xaml
    /// </summary>
    public partial class EtcSlaveBoxBase_77A0E4A7TopologyBoxM90View : UserControl
    {
        public EtcSlaveBoxBase_77A0E4A7TopologyBoxM90View()
        {
            InitializeComponent();
        }
        private void OpenBoxDetails(object sender, System.Windows.RoutedEventArgs e)
        {
            Window window = new Window();
            Grid grid = new Grid();
            EtcSlaveBoxBase_77A0E4A7TopologyBoxView box = new EtcSlaveBoxBase_77A0E4A7TopologyBoxView();
            box.DataContext = this.DataContext;
            grid.Children.Add(box);
            window.Content = grid;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.MaxHeight = 1200;
            window.Show();
        }

        private void tbName_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            tbName.FontSize = TextBlockUtils.UpdateFontSizeToFitTheTextBlockMaxWidth(tbName);
        }

    }
}
