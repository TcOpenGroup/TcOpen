using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoKukaRobotics
{
    public class TcoKrc4_v_5_x_xEventsDescriptionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                uint errorId = (uint)value;

                return TcoKrc4ControllerEvents_v_5_x_x.Ids.ContainsKey(errorId)
                    ? TcoKrc4ControllerEvents_v_5_x_x.Ids[errorId]
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
