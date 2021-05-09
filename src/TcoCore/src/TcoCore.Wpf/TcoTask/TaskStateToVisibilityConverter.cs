using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Vortex.Presentation.Wpf.Converters;

namespace TcoCore
{
    public class TaskStateToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var taskState = (eTaskState)((short)value);

                switch (taskState)
                {
                    case eTaskState.Requested:
                    case eTaskState.Busy:
                    case eTaskState.Error:
                        return Visibility.Visible;
                    case eTaskState.Ready:                    
                    case eTaskState.Done:
                        return Visibility.Hidden;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                // Swallow
            }

            return false;
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
