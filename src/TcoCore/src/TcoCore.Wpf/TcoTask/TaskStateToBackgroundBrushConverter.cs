using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using TcoCore.Wpf;

namespace TcoCore
{
    public class TaskStateToBackgroundBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value.GetType() == typeof(bool))
                {
                    var binaryTaskState = (bool)value;
                    return binaryTaskState ? TcoColors.Accent : TcoColors.Primary;
                }

                var taskState = (eTaskState)((short)value);

                switch (taskState)
                {
                    case eTaskState.Ready:
                    case eTaskState.Requested:
                    case eTaskState.Done:
                        return TcoColors.Primary;
                    case eTaskState.Busy:
                        return TcoColors.Accent;
                    case eTaskState.Error:
                        return TcoColors.Error;
                    default:
                        return Brushes.Gray;
                }
            }
            catch (Exception)
            {
                // Swallow
            }

            return 0;
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
