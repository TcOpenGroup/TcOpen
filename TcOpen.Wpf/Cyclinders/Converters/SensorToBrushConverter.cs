using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcOpen
{
    public class SensorToBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (bool)value ? Vortex.Presentation.Styling.Wpf.VortexResources.Primary : Vortex.Presentation.Styling.Wpf.VortexResources.OnPrimary;
            }
            catch (Exception)
            {
                return Vortex.Presentation.Styling.Wpf.VortexResources.Alert;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
