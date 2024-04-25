using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using TcoCognexVision.Converters;
using Vortex.Connector.ValueTypes;

namespace TcoCognexVision
{
    public partial class TcoDataman_v_5_x_xServiceView : UserControl
    {

        private eDisplayFormat _currentDisplayFormat;

        public eDisplayFormat CurrentDisplayFormat { get => _currentDisplayFormat; set => _currentDisplayFormat = value; }

        public TcoDataman_v_5_x_xServiceView()
        {
            InitializeComponent();
        }
       

        private void aButton_Decimal(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((TcoDataman_v_5_x_xViewModel)(this.DataContext) != null)
            {
                this.CurrentDisplayFormat = eDisplayFormat.Array_of_decimals;
                ((TcoDataman_v_5_x_xViewModel)(this.DataContext)).CurrentDisplayFormat = eDisplayFormat.Array_of_decimals;
            }
        }
        private void aButton_HexDecimal(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((TcoDataman_v_5_x_xViewModel)(this.DataContext) != null)
            {
                this.CurrentDisplayFormat = eDisplayFormat.Array_of_hexdecimals;
                ((TcoDataman_v_5_x_xViewModel)(this.DataContext)).CurrentDisplayFormat = eDisplayFormat.Array_of_hexdecimals;
            }
        }
        private void aButton_String(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((TcoDataman_v_5_x_xViewModel)(this.DataContext) != null)
            {
                this.CurrentDisplayFormat = eDisplayFormat.String;
                ((TcoDataman_v_5_x_xViewModel)(this.DataContext)).CurrentDisplayFormat = eDisplayFormat.String;
            }
        }
    }
}
