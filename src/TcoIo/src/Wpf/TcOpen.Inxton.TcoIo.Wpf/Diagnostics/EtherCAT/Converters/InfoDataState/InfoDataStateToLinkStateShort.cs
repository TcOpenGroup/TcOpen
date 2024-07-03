using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class InfoDataStateToLinkStateShort : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0x010_ = Slave not present
            //0x020_ = Slave signals link error
            //0x040_ = Slave signals missing link
            //0x080_ = Slave signals unexpected link
            string ret = "";
            if ((((int)(ushort)value) & 0x0100) != 0)
                ret = ret + "Slave not present; ";
            if ((((int)(ushort)value) & 0x0200) != 0)
                ret = ret + "Link error; ";
            if ((((int)(ushort)value) & 0x0400) != 0)
                ret = ret + "Missing link ";
            if ((((int)(ushort)value) & 0x0800) != 0)
                ret = ret + "Unexpected link ";
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
