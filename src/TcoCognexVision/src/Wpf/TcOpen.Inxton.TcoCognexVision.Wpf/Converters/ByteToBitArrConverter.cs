using System;
using System.Globalization;
using System.Text;
using Vortex.Presentation.Wpf.Converters;

namespace TcoCognexVision.Converters
{
    public class ByteToBitArrConverter : BaseConverter
    {
        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte)
            {
                byte inByte = (byte)value;
                string strBin = System.Convert.ToString(inByte, 2).PadLeft(8, '0');
                return strBin;
            }
            else
            {
                return "N/A";
            }
        }
    }
}
