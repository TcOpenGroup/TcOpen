using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MainPlc
{
    public class TaskStateToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TcoCore.eTaskState state = (TcoCore.eTaskState)Enum.Parse(typeof(TcoCore.eTaskState), value.ToString());

            switch (state)
            {
                case TcoCore.eTaskState.Ready:
                case TcoCore.eTaskState.Requested:
                    return Visibility.Collapsed;
                case TcoCore.eTaskState.Done:                    
                case TcoCore.eTaskState.Busy:
                case TcoCore.eTaskState.Error:
                    return Visibility.Visible;
                default:
                    return Visibility.Collapsed;
            }
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
