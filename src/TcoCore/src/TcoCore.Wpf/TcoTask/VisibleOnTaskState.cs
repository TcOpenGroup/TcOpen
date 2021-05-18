using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using Vortex.Presentation.Wpf.Converters;

namespace TcoCore
{
    /// <summary>
    /// Will set Visibility to Visible if the value exist in ConverterParameter
    /// Converter parameter values are dash (-) separated and should contain only eTaskState states. ie Requested-Busy-Error or Ready-Done
    /// </summary>
    public class VisibleOnTaskState : BaseConverter
    {

        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var visibleStates = parameter.ToString().Split('-').Select(x => x.ToLower()).ToList();
                var taskState = (eTaskState)((short)value);
                string enumName = taskState.ToString().ToLower();
                if (visibleStates.Contains(enumName))
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            catch
            {
                //swallow
                return Visibility.Visible;
            }
        }
    }
}
