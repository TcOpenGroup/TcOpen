using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using TcoCore.Wpf;
using TcoInspectors;

namespace TcOpen.Inxton.TcoInspectors.Wpf
{
    public class InspectorsResultColorConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = (eInspectorResult)Enum.ToObject(typeof(eInspectorResult), value);
                //(VortexCore.enumCheckResult)((int)value);

                switch (result)
                {
                    case eInspectorResult.NoAction:
                        return Brushes.Gray;
                    case eInspectorResult.Running:
                        return TcoColors.Accent;
                    case eInspectorResult.Passed:
                        return Brushes.Green;
                    case eInspectorResult.Failed:
                        return Brushes.Red;
                    case eInspectorResult.Inconclusive:
                        return Brushes.OrangeRed;
                    case eInspectorResult.Excluded:
                        return Brushes.Orange;
                    case eInspectorResult.Bypassed:
                        return Brushes.DarkOrange;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                // swallow
            }

            return Brushes.Gray;
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
