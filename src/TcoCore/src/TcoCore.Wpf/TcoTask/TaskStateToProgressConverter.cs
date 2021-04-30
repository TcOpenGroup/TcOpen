using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoCore
{
    public class TaskStateToProgressConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var taskState = (eTaskState)((short)value);

                switch (taskState)
                {
                    case eTaskState.Ready:
                        return 0;
                    case eTaskState.Requested:
                        return 5;
                    case eTaskState.Busy:
                        return 35;
                    case eTaskState.Error:
                        return 0;
                    case eTaskState.Done:
                        return 100;
                    default:
                        return 0;
                }
            }
            catch (Exception)
            {
                // Swallow
            }

            return 0;
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
