using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using TcoCore;

namespace TcoCore
{
    internal static class ValueConverterExtension
    {
        public static Brush GetBrush(this IValueConverter _, string resourceKey, Brush fallbackBrush)
            => (Application.Current.TryFindResource(resourceKey) as Brush) ?? fallbackBrush;
    }

    public class MessageCategoryToBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var category = (eMessageCategory)value;

                switch (category)
                {
                    case eMessageCategory.Debug:
                    case eMessageCategory.Trace:
                    case eMessageCategory.Info:
                    case eMessageCategory.ProgrammingError:
                        return this.GetBrush("Secondary", Brushes.Black);
                    case eMessageCategory.TimedOut:
                    case eMessageCategory.Notification:
                        return this.GetBrush("Primary", Brushes.Black);
                    case eMessageCategory.Warning:      
                        return this.GetBrush("Alert", Brushes.Orange);
                    case eMessageCategory.Error:
                    case eMessageCategory.Critical:
                    case eMessageCategory.Catastrophic:
                        return this.GetBrush("Alert", Brushes.OrangeRed);

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