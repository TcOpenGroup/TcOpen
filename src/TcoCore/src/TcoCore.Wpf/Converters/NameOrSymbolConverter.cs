using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using Vortex.Connector;

namespace TcoCore
{
    public class NameOrSymbolConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IVortexElement val = value as IVortexElement;
            return val != null ? string.IsNullOrEmpty(val.AttributeName) ? val.GetSymbolTail() : val.AttributeName : "Missing object information";
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
