using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoCore
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = false;
            try 
            { 
                if (value is bool)
                {
                    flag = (bool)value;
                }
                else if (value is bool?)
                {
                    var nullable = (bool?)value;
                    flag = nullable.GetValueOrDefault();
                }
                if (parameter != null)
                {
                    if (bool.Parse((string)parameter))
                    {
                        flag = !flag;
                    }
                }
        }
            catch 
            {
                //swalow
            }
            return flag ? Visibility.Visible : Visibility.Collapsed;
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
