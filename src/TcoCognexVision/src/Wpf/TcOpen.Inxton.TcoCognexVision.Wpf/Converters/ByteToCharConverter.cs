using System;
using System.Globalization;
using System.Text;
using Vortex.Presentation.Wpf.Converters;

namespace TcoCognexVision.Converters
{
    public class ByteToCharConverter : BaseConverter
    {
        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte)
            {
                if ((byte)value > 0)
                    return Encoding.UTF8.GetString(new byte[] { (byte)value });
                else return "N/A";
            }
            else
            {
                return "N/A";
            }
        }
    }
}
