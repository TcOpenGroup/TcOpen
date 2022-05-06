using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Vortex.Connector.ValueTypes
{
    public partial class OnlinerByteHWDiagLimitView : UserControl
    {
        public OnlinerByteHWDiagLimitView()
        {
            InitializeComponent();
        }
    }
    public class LimitToDescription : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Bit0: Value greater than limit
            //Bit1: Value smaller than limit
            string ret = "";
            if ((((int)(byte)value) & 0x01) != 0) ret = "Greater than limit value set.";
            else if ((((int)(byte)value) & 0x02) != 0) ret = "Smaller than limit value set.";
            return ret;
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
