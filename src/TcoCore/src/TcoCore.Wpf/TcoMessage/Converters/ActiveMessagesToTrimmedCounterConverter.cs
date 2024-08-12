using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoCore
{
    /// <summary>
    /// Converts to message counter to max 9+, when less displays the current count.
    /// </summary>
    public class ActiveMessagesToTrimmedCounterConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ((int)value) > 9 ? "9+" : value;
            }
            catch (Exception)
            {
                //Swallow
            }

            return 0;
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
