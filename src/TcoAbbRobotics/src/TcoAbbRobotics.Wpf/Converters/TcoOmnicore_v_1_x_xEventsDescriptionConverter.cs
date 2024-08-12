using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoAbbRobotics
{
    public class TcoOmnicore_v_1_x_xEventsDescriptionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                uint errorId = (uint)value;

                return TcoOmnicoreControlerEvents_v_1_x_x.Ids.ContainsKey(errorId)
                    ? TcoOmnicoreControlerEvents_v_1_x_x
                        .Ids.Where(key => key.Key == errorId)
                        .FirstOrDefault()
                        .Value
                    : "No error description available.";
            }
            catch (Exception)
            {
                throw;
                // swallow
            }

            return string.Empty;
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
