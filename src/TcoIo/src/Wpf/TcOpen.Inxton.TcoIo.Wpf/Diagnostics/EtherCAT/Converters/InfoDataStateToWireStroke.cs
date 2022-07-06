using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class InfoDataStateToWireStroke : MarkupExtension,  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //0x010_ = Slave not present
            //0x020_ = Slave signals link error
            //0x040_ = Slave signals missing link
            //0x080_ = Slave signals unexpected link
            //0x100_ = Communication port A
            //0x200_ = Communication port B
            //0x400_ = Communication port C
            //0x800_ = Communication port D    
            ResourceDictionary ColorResorce = new ResourceDictionary();
            ColorResorce.Source = new Uri("/TcOpen.Inxton.TcoIo.Wpf;component/diagnostics/ethercat/colors/colors.xaml", UriKind.RelativeOrAbsolute);
            Brush lime = new SolidColorBrush((Color)ColorResorce["InxtonLimeColor"]);
            Brush gray = new SolidColorBrush((Color)ColorResorce["InxtonGrayColor"]);
            Brush ret = lime;

            if ((((int)(ushort)value) & 0x0100) != 0) ret = TcoCore.Wpf.TcoColors.Error;
            if ((((int)(ushort)value) & 0x0200) != 0) ret = TcoCore.Wpf.TcoColors.Error;
            if ((((int)(ushort)value) & 0x0400) != 0 && (((int)(ushort)value) & 0x1000) != 0) ret = TcoCore.Wpf.TcoColors.Error;
            //if ((((int)(ushort)value) & 0x0800) != 0) ret = TcoCore.Wpf.TcoColors.Error;
            if ((ushort)value == 0) ret = gray;     // no data from slave

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
