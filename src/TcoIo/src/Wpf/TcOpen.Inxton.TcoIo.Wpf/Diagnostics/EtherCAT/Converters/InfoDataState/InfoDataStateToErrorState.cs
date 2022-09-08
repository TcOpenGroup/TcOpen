using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class InfoDataStateToErrorState : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0x001_ = Slave signals error
            //0x002_ = Invalid vendorId, productCode... read
            //0x004_ = Initialization error occurred
            //0x008_ = Slave disabled
            string ret = "";
            if ((((int)(ushort)value) & 0x0010) != 0) ret = ret + "Error; ";
            if ((((int)(ushort)value) & 0x0020) != 0) ret = ret + "Invalid productCode; ";
            if ((((int)(ushort)value) & 0x0040) != 0) ret = ret + "Initialization error; ";
            if ((((int)(ushort)value) & 0x0080) != 0) ret = ret + "Slave disabled; ";
            return ret;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
