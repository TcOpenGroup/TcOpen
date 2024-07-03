using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using TcoInspectors;

namespace TcOpen.Inxton.TcoInspectors.Wpf
{
    public class InspectorsIconConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = (eInspectorResult)Enum.ToObject(typeof(eInspectorResult), value);

                switch (result)
                {
                    case eInspectorResult.NoAction:
                        return MaterialDesignThemes.Wpf.PackIconKind.Minus;
                    case eInspectorResult.Running:
                        return MaterialDesignThemes.Wpf.PackIconKind.Run;
                    case eInspectorResult.Passed:
                        return MaterialDesignThemes.Wpf.PackIconKind.Check;
                    case eInspectorResult.Failed:
                        return MaterialDesignThemes.Wpf.PackIconKind.AlphabetXCircle;
                    case eInspectorResult.Inconclusive:
                        return MaterialDesignThemes.Wpf.PackIconKind.AlertCircle;
                    case eInspectorResult.Excluded:
                        return MaterialDesignThemes.Wpf.PackIconKind.Exclusion;
                    case eInspectorResult.Bypassed:
                        return MaterialDesignThemes.Wpf.PackIconKind.TransitSkip;
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
