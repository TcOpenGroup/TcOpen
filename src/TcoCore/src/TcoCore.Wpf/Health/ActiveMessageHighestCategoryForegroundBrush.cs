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
                switch (((eMessageCategory)value))
                {
                    case eMessageCategory.All:
                    case eMessageCategory.Debug:
                    case eMessageCategory.Trace:
                    case eMessageCategory.Info:
                        return TcoCore.Wpf.TcoColors.Primary;
                    case eMessageCategory.TimedOut:
                    case eMessageCategory.Notification:
                    case eMessageCategory.Warning:
                        return TcoCore.Wpf.TcoColors.Accent;
                    case eMessageCategory.Error:
                    case eMessageCategory.ProgrammingError:
                    case eMessageCategory.Critical:
                    case eMessageCategory.Catastrophic:
                        return TcoCore.Wpf.TcoColors.Error;
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
