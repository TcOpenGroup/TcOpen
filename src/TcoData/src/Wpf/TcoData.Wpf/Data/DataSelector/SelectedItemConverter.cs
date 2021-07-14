using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoData
{
    public class SelectedItemConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return 0 == String.CompareOrdinal(values[0].ToString(), values[1].ToString())
             ? FontWeight.FromOpenTypeWeight(700)
             : FontWeight.FromOpenTypeWeight(400);
            }
            catch
            {
                return FontWeight.FromOpenTypeWeight(400); ;
            }
         
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
