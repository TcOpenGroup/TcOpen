using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoCore
{
    public class TaskStateToBoolConverter : MarkupExtension, IValueConverter
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
                        return true;
                    case eTaskState.Ready:
                    case eTaskState.Error:
                    case eTaskState.Done:
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
