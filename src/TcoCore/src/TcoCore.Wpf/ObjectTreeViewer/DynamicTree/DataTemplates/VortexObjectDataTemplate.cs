using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Vortex.Connector;

namespace Tco.Wpf.DynamicTree.DataTemplates
{
    public partial class VortexObjectDataTemplate 
    {
    }

    public class SymbolOrHumanReadableConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vob = value as IVortexObject;
            if (vob == null) return value;
            return string.IsNullOrEmpty(vob.HumanReadable) ? vob.Symbol : vob.HumanReadable;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
