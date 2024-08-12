using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class InfoDataStateToPortShort : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0x100_ = Communication port A
            //0x200_ = Communication port B
            //0x400_ = Communication port C
            //0x800_ = Communication port D
            string ret = "";
            if ((((int)(ushort)value) & 0x1000) != 0)
                ret = ret + "port A";
            if ((((int)(ushort)value) & 0x2000) != 0)
                ret = ret + "port B";
            if ((((int)(ushort)value) & 0x4000) != 0)
                ret = ret + "port C";
            if ((((int)(ushort)value) & 0x8000) != 0)
                ret = ret + "port D";
            return ret;
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
