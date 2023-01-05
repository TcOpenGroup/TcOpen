using MaterialDesignColors;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoCore
{
    /// <summary>
    /// Converts message category into corresponding foreground brush.
    /// </summary>
    public class ActiveMessageHighestCategoryForegroundBrush : MarkupExtension, IValueConverter
    {        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Color color = default;

                switch (((eMessageCategory)value))
                {
                    case eMessageCategory.Debug:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Green400, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Trace:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Green500, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Info:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Green700, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.TimedOut:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Green800, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Notification:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Green900, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Warning:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Yellow700, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Error:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Red600, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.ProgrammingError:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Red700, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Critical:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Red800, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Catastrophic:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Red900, out color);
                        return new SolidColorBrush(color);

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                //Swallow                
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
