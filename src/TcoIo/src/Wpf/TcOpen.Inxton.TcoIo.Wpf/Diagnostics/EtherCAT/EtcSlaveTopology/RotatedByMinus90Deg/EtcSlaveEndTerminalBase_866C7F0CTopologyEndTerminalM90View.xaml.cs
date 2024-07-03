using System.Windows;
using System.Windows.Controls;
using TcoIo.Converters.Utilities;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for fbSampleComponentView.xaml
    /// </summary>
    public partial class EtcSlaveEndTerminalBase_866C7F0CTopologyEndTerminalM90View : UserControl
    {
        public EtcSlaveEndTerminalBase_866C7F0CTopologyEndTerminalM90View()
        {
            InitializeComponent();
        }

        private void OpenTerminalDetails(object sender, System.Windows.RoutedEventArgs e)
        {
            Window window = new Window();
            Grid grid = new Grid();
            var model = DataContext.GetType().GetProperty("Model")?.GetValue(DataContext);
            string physics = model
                .GetType()
                .GetProperty("AttributePhysics")
                ?.GetValue(model)
                .ToString();
            EtcSlaveEndTerminalBase_866C7F0CTopologyEndTerminalView box =
                new EtcSlaveEndTerminalBase_866C7F0CTopologyEndTerminalView();
            box.DataContext = this.DataContext;
            grid.Children.Add(box);
            window.Content = grid;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.MaxHeight = 1200;
            window.Show();
        }

        private void tbName_TargetUpdated(
            object sender,
            System.Windows.Data.DataTransferEventArgs e
        )
        {
            tbName.FontSize = TextBlockUtils.UpdateFontSizeToFitTheTextBlockMaxWidth(tbName);
        }
    }
}
