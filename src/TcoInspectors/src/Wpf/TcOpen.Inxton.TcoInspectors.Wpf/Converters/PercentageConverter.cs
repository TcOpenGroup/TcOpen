using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcOpen.Inxton.TcoInspectors.Wpf.Converters
{
    public class PercentageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture
        )
        {
            double val = ConvertToDouble(value);

            if (parameter == null)
                return 0.5 * val;

            string[] split = parameter.ToString().Split('.');
            double parameterDouble =
                ConvertToDouble(split[0])
                + ConvertToDouble(split[1]) / (Math.Pow(10, split[1].Length));
            return val * parameterDouble;
        }

        private static double ConvertToDouble(object value)
        {
            return (value is double)
                ? (double)value
                : (value is IConvertible)
                    ? (value as IConvertible).ToDouble(null)
                    : double.Parse(value.ToString());
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture
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
