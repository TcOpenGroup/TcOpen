using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vortex.Presentation.Wpf.Converters;

namespace TcoDrivesBeckhoff
{
    public class VisibleOnBool      : BaseConverter
    {
        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool _value = (bool)value;
                bool _shouldBvisible;
                Boolean.TryParse(parameter.ToString(), out _shouldBvisible);

                if(_value == _shouldBvisible)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            catch
            {
                //swallow
                return Visibility.Collapsed;
            }
        }
    }
}


