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

                switch (category)
                {                    
                    case eMessageCategory.Debug:
                    case eMessageCategory.Trace:
                    case eMessageCategory.Info:
                        return TcoCore.Wpf.TcoColors.OnSecondary;                    
                    case eMessageCategory.TimedOut:
                    case eMessageCategory.Notification:
                        return TcoCore.Wpf.TcoColors.OnPrimary;
                    case eMessageCategory.Warning:
                        return TcoCore.Wpf.TcoColors.OnAccent;
                    case eMessageCategory.Error:
                    case eMessageCategory.Critical:
                    case eMessageCategory.Catastrophic:
                    case eMessageCategory.ProgrammingError:
                        return TcoCore.Wpf.TcoColors.OnError;

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

                switch (category)
                {
                    case eMessageCategory.Debug:
                    case eMessageCategory.Trace:
                    case eMessageCategory.Info:
                        return TcoCore.Wpf.TcoColors.Secondary;
                    case eMessageCategory.TimedOut:
                    case eMessageCategory.Notification:
                        return TcoCore.Wpf.TcoColors.Primary;
                    case eMessageCategory.Warning:
                        return TcoCore.Wpf.TcoColors.Accent;
                    case eMessageCategory.Error:
                    case eMessageCategory.Critical:
                    case eMessageCategory.Catastrophic:
                    case eMessageCategory.ProgrammingError:
                        return TcoCore.Wpf.TcoColors.Error;
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