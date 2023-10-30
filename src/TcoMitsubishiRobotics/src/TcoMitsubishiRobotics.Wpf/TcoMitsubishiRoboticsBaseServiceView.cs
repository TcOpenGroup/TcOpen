using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TcOpen.Inxton.Wpf;

namespace TcoMitsubishiRobotics
{

    public partial class TcoMitsubishiRoboticsBaseServiceView : UserControl
    {
        public TcoMitsubishiRoboticsBaseServiceView()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TcoCr800_v_1_x_xServiceViewModel();
            }

            InitializeComponent();
        }


       

    }

    public class EventIdToDescriptionConverter : MarkupExtension, IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                uint errorId = (uint)value;
                return ControllerEvents.Ids.ContainsKey(errorId) ? ControllerEvents.Ids[errorId] : "No error description available.";
            }
            catch (Exception)
            {
                throw;
                // swallow
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }


}
