using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class InfoDataSyncUnitStateToOverallState : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0x___1 = At least one slave in 'INIT' state
            //0x___2 = At least one slave in 'PREOP' state
            //0x___3 = At least one slave in 'BOOT' state
            //0x___4 = At least one slave in 'SAFEOP' state
            //0x___8 = At least one slave in 'OP' state
            //0x001_ = At least one slave signals error
            //0x002_ = Invalid vendorId, productCode... read
            //0x004_ = Initialization error occurred
            //0x008_ = At least one slave disabled
            //0x010_ = At least one slave not present
            //0x020_ = At least one slave signals link error
            //0x040_ = At least one slave signals missing link
            //0x080_ = At least one slave signals unexpected link

            string ret = "";
            if ((((int)(ushort)value) & 0x0003) != 0) ret = ret + "At least one slave in 'BOOT' state" + Environment.NewLine;
            else if ((((int)(ushort)value) & 0x0001) != 0) ret = ret + "At least one slave in 'INIT' state" + Environment.NewLine;
            else if ((((int)(ushort)value) & 0x0002) != 0) ret = ret + "At least one slave in 'PREOP' state" + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0004) != 0) ret = ret + "At least one slave in 'SAFEOP' state" + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0008) != 0) ret = ret + "At least one slave in 'OP' state" + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0010) != 0) ret = ret + "At least one slave signals error," + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0020) != 0) ret = ret + "Invalid vendorId, productCode," + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0040) != 0) ret = ret + "Initialization error occurred," + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0080) != 0) ret = ret + "At least one slave disabled, " + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0100) != 0) ret = ret + "At least one slave not present; " + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0200) != 0) ret = ret + "At least one slave signals link error; " + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0400) != 0) ret = ret + "At least one slave signals missing link " + Environment.NewLine;
            if ((((int)(ushort)value) & 0x0800) != 0) ret = ret + "At least one slave signals unexpected link " + Environment.NewLine;
            if (ret.Contains(Environment.NewLine))
            {
                ret = ret.Substring(0, ret.LastIndexOf(Environment.NewLine));
            }
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
