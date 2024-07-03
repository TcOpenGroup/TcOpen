using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Vortex.Connector;

namespace TcoIo.Converters
{
    public class NonGenericTypeToVisibleConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return
                value
                    .GetType()
                    .GetProperties()
                    .Where(p => p.PropertyType.BaseType.IsGenericType)
                    .Count() == 1
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
