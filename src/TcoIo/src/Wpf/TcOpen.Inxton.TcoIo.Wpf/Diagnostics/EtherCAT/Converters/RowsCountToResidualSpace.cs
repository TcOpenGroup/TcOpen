using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class RowsCountToResidualSpace : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            string[] separators = { Environment.NewLine };
            int rows = 0;
            if (!string.IsNullOrEmpty(s))
            {
                rows = s.Split(separators, StringSplitOptions.RemoveEmptyEntries).Length;
            }
            return 100.0 - 11.0 * rows;
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
