using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Vortex.Presentation.Wpf.Converters;

namespace TcoCore
{
    public class SignalToBrushConverter : BaseConverter
    {
        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var signal = (bool)value;
                if (signal)
                {
                    return Application.Current.TryFindResource("SignalOnBrush") ?? Brushes.GreenYellow;
                }
                else
                {
                    return Application.Current.TryFindResource("SignalOffBrush") ?? Brushes.DimGray;
                }
            }
            catch (Exception e)
            {
                return Brushes.DarkGray;
            }
        }
    }
}
