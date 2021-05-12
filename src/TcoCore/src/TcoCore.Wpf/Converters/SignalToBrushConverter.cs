using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Vortex.Presentation.Wpf.Converters;

namespace TcoCore
{
    /// <summary>
    /// Will convert boolean values to Brush which represent whether signal is on, or off
    /// Using the MaterialDesignXaml toolkit http://materialdesigninxaml.net/  and it's color palette
    /// 
    /// Signal On is the Accent color from the palette, signal off si Dark color.
    /// https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/blob/master/MainDemo.Wpf/Palette.xaml
    /// </summary>
    public class SignalToBrushConverter : BaseConverter
    {
        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var signal = (bool)value;
                if (signal)
                {
                    return Application.Current.TryFindResource("SecondaryHueMidBrush") ?? Brushes.GreenYellow;
                }
                else
                {
                    return Application.Current.TryFindResource("PrimaryHueDarkBrush") ?? Brushes.DimGray;
                }
            }
            catch (Exception e)
            {
                return Brushes.DarkGray;
            }
        }
    }
}
