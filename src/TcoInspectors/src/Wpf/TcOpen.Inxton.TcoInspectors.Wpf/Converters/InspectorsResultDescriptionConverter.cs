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

                switch (result)
                {
                    case eInspectorResult.NoAction:
                        return TcOpen.Inxton.TcoInspectors.Wpf.Properties.strings.NotInspected;
                    case eInspectorResult.Running:
                        return TcOpen.Inxton.TcoInspectors.Wpf.Properties.strings.InspectionIsRunning;
                    case eInspectorResult.Passed:
                        return TcOpen.Inxton.TcoInspectors.Wpf.Properties.strings.InspectionPassed;
                    case eInspectorResult.Failed:
                        return TcOpen.Inxton.TcoInspectors.Wpf.Properties.strings.InspectionFailed;
                    case eInspectorResult.Inconclusive:
                        return TcOpen.Inxton.TcoInspectors.Wpf.Properties.strings.InspectionInconclusive;
                    case eInspectorResult.Excluded:
                        return TcOpen.Inxton.TcoInspectors.Wpf.Properties.strings.InspectionDidRunButIsExcludedFromTheEvaluation;
                    case eInspectorResult.Bypassed:
                        return TcOpen.Inxton.TcoInspectors.Wpf.Properties.strings.InspectionWasBypassed;
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
