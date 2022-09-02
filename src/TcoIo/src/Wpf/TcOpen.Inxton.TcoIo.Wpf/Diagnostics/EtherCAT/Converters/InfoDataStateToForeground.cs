using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class InfoDataStateToForeground : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ushort)value != 8)
            {
                return TcoCore.Wpf.TcoColors.OnError;
            }
            else
            {
                ResourceDictionary ColorResorce = new ResourceDictionary();
                ColorResorce.Source = new Uri("/TcOpen.Inxton.TcoIo.Wpf;component/diagnostics/ethercat/colors/colors.xaml", UriKind.RelativeOrAbsolute);
                return new SolidColorBrush((Color)ColorResorce["OnBackgroundColor"]);
            }
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
