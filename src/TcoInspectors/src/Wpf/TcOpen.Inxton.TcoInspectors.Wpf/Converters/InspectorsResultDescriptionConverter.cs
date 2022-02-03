using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using TcoInspectors;

namespace TcOpen.Inxton.TcoInspectors.Wpf
{
    public class InspectorsResultDescriptionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = (eInspectorResult)Enum.ToObject(typeof(eInspectorResult), value);
                //(VortexCore.enumCheckResult)((int)value);

                return result.ToString();

            }
            catch (Exception)
            {

                // swallow
            }

            return string.Empty;
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
