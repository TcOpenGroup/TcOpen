
using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace MainPlc
{
    public class HeartBeatToBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return Vortex.Presentation.Styling.Wpf.VortexResources.Primary;
            }
            return Vortex.Presentation.Styling.Wpf.VortexResources.Secondary;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
