using MaterialDesignColors;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using TcoCore;

namespace TcoCore
{
    public class MessageCategoryToForegroundBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var category = (eMessageCategory)value;

                Color color = default;
                switch (category)
                {
                  
                    case eMessageCategory.Debug:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo500, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.Trace:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo600, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.Info:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo700, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.TimedOut:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo800, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.Notification:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo900, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.Warning:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Yellow700, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.Error:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Red600, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.ProgrammingError:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Red700, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.Critical:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Red800, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    case eMessageCategory.Catastrophic:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Red900, out color);
                        return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
                    default:
                        break;
                }
            }
            catch
            {
                //++ Ignore
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

    public class MessageCategoryToBackgroundBrushConverter : MarkupExtension, IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var category = (eMessageCategory)value;

                Color color = default;
                switch (category)
                {
                    case eMessageCategory.Debug:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo400, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Trace:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo500, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Info:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo700, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.TimedOut:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo800, out color);
                        return new SolidColorBrush(color);
                    case eMessageCategory.Notification:
                        SwatchHelper.Lookup.TryGetValue(MaterialDesignColor.Indigo900, out color);
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
            catch
            {
                //++ Ignore
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