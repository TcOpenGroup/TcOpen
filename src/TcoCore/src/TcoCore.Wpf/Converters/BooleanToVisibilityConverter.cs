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
            bool flag = false;
            try
            {
                if (value is bool)
                    flag = (bool)value;
                else
                {
                    if (value is bool?)
                    {
                        bool? flag2 = (bool?)value;
                        flag = (flag2.HasValue && flag2.Value);
                    }
                }

                //If false is passed as a converter parameter then reverse the value of input value
                if (parameter != null)
                {
                    bool par = true;
                    if ((bool.TryParse(parameter.ToString(), out par)) && (!par)) flag = !flag;
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
