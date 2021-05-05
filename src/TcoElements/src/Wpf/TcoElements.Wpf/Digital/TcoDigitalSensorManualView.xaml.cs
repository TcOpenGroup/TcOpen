using System.ComponentModel;
using System.Windows.Controls;

namespace TcoElements
{
    /// <summary>
    /// Interaction logic for TcoDigitalSensorManualView.xaml
    /// </summary>
    public partial class TcoDigitalSensorManualView : UserControl
    {
        public TcoDigitalSensorManualView()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TcoDigitalSensor();
            }

            InitializeComponent();

        }
    }
}
