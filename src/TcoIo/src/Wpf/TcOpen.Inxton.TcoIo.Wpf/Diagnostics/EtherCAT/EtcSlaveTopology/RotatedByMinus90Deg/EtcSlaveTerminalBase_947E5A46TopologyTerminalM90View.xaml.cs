using System.Windows;
using System.Windows.Controls;
using TcoIo.Converters.Utilities;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for fbSampleComponentView.xaml
    /// </summary>
    public partial class EtcSlaveTerminalBase_947E5A46TopologyTerminalM90View : UserControl
    {
        public EtcSlaveTerminalBase_947E5A46TopologyTerminalM90View()
        {
            InitializeComponent();
        }
        private void OpenTerminalDetails(object sender, System.Windows.RoutedEventArgs e)
        {
            Window window = new Window();
            Grid grid = new Grid();
            var model = DataContext.GetType().GetProperty("Model")?.GetValue(DataContext);
            string physics = model.GetType().GetProperty("AttributePhysics")?.GetValue(model).ToString();
            if (physics == "KYKY" || physics == "KY")
            {
                EtcSlaveTerminalBase_947E5A46SingleTerminalView box = new EtcSlaveTerminalBase_947E5A46SingleTerminalView();
                box.DataContext = this.DataContext ;
                grid.Children.Add(box);
            }
            else
            {
                EtcSlaveTerminalBase_947E5A46TopologyTerminalView box = new EtcSlaveTerminalBase_947E5A46TopologyTerminalView();
                box.DataContext = this.DataContext;
                grid.Children.Add(box);
            }
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
