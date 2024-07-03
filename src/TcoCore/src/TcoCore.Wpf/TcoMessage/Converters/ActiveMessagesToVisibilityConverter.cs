using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoCore
{
    /// <summary>
    /// Converts to <see cref="Visibility"/>; visible when the value of is greater than 0; otherwise hidden.
    /// </summary>
    public class ActiveMessagesToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ((int)value) > 0 ? Visibility.Visible : Visibility.Hidden;
            }
            catch (Exception)
            {
                //Swallow
            }

            return Visibility.Hidden;
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
