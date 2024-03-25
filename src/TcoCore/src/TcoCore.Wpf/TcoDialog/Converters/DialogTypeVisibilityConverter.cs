using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoCore
{
    public class DialogTypeVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (eDialogType)Enum.Parse(typeof(eDialogType), value.ToString());

            switch (b)
            {
                case eDialogType.Undefined:
                    return Visibility.Collapsed;
                case eDialogType.Info:
                case eDialogType.Question: 
                case eDialogType.Warning:
                case eDialogType.Error:
                    return Visibility.Visible;
                default:
                    return Visibility.Visible;
            }
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
