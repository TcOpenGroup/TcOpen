using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using TcoCore.Wpf;

namespace TcoCore
{
    public class TaskStateToForegroundBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value.GetType() == typeof(bool))
                {
                    var binaryTaskState = (bool)value;
                    return binaryTaskState ? TcoColors.OnAccent : TcoColors.OnPrimary;
                }

                var taskState = (eTaskState)((short)value);

                switch (taskState)
                {
                    case eTaskState.Ready:
                    case eTaskState.Requested:
                    case eTaskState.Done:
                        return TcoColors.OnPrimary;
                    case eTaskState.Busy:
                        return TcoColors.OnAccent;
                    case eTaskState.Error:
                        return TcoColors.OnError;
                    default:
                        return Brushes.Transparent;
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
