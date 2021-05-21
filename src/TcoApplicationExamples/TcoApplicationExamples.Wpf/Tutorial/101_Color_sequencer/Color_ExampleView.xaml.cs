using System;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Vortex.Presentation.Wpf;

namespace PlcAppExamples
{
    public partial class Color_ExampleView : UserControl
    {
        public Color_ExampleView()
        {
            InitializeComponent();
        }
    }
    public class Color_ExampleViewModel : RenderableViewModel
    {
        public Color_ExampleViewModel()
        {

        }

        public Color_Example Color_Example { get; set; }

        public override object Model { get => Color_Example; set => Color_Example = value as Color_Example; }
    }


    public class RgbConverter : MarkupExtension, IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var vals = values.ToList().Cast<bool>().Select(x => x ? 255 : 0).ToArray();
            var r = System.Convert.ToByte(vals[0]);
            var g = System.Convert.ToByte(vals[1]);
            var b = System.Convert.ToByte(vals[2]);
            return System.Windows.Media.Color.FromRgb(r, g, b);
        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(System.IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
