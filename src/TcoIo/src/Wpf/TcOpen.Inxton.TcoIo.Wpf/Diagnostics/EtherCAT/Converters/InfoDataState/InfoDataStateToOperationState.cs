using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class InfoDataStateToOperationState : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0x___1 = Slave in 'INIT' state
            //0x___2 = Slave in 'PREOP' state
            //0x___3 = Slave in 'BOOT' state
            //0x___4 = Slave in 'SAFEOP' state
            //0x___8 = Slave in 'OP' state
            switch (((int)(ushort)value) & 0x000f)
            {
                case 1:
                    return "INIT";
                case 2:
                    return "PREOP";
                case 3:
                    return "BOOT";
                case 4:
                    return "SAFEOP";
                case 8:
                    return "OP";
                default:
                    return "";
            }
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
