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

        //private List<String> _displayFormats = new List<string> { "array of decimal numbers", "array of hexadecimal numbers", "string" };
        public eDisplayFormat CurrentDisplayFormat { get => _currentDisplayFormat; set => _currentDisplayFormat = value; }

        public TcoDataman_v_5_x_xServiceView()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void ResultData_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DisplayFormatDialog displayFormatDialog = new DisplayFormatDialog(CurrentDisplayFormat);
            displayFormatDialog.ShowDialog();
            this.CurrentDisplayFormat = displayFormatDialog.CurrentDisplayFormat;
            ((TcoDataman_v_5_x_xServiceViewModel)(this.DataContext)).CurrentDisplayFormat = this.CurrentDisplayFormat;
        }
    }
}
