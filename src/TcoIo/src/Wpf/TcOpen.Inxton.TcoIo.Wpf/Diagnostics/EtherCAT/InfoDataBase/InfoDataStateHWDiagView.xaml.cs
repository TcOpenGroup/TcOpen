using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for fbSampleComponentView.xaml
    /// </summary>
    public partial class InfoDataStateHWDiagView : UserControl
    {
        public InfoDataStateHWDiagView()
        {
            InitializeComponent();
        }
    }

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

    public class InfoDataStateToErrorState : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0x001_ = Slave signals error
            //0x002_ = Invalid vendorId, productCode... read
            //0x004_ = Initialization error occurred
            //0x008_ = Slave disabled
            string ret = "";
            if ((((int)(ushort)value) & 0x0010) != 0)
                ret = ret + "Slave signals error; ";
            if ((((int)(ushort)value) & 0x0020) != 0)
                ret = ret + "Invalid vendorId, productCode; ";
            if ((((int)(ushort)value) & 0x0040) != 0)
                ret = ret + "Initialization error occurred; ";
            if ((((int)(ushort)value) & 0x0080) != 0)
                ret = ret + "Slave disabled; ";
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

    public class InfoDataStateToLinkState : MarkupExtension, IValueConverter
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
                ret = ret + "Slave signals link error; ";
            if ((((int)(ushort)value) & 0x0400) != 0)
                ret = ret + "Slave signals missing link ";
            if ((((int)(ushort)value) & 0x0800) != 0)
                ret = ret + "Slave signals unexpected link ";
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

    public class InfoDataStateToPort : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0x100_ = Communication port A
            //0x200_ = Communication port B
            //0x400_ = Communication port C
            //0x800_ = Communication port D
            string ret = "";
            if ((((int)(ushort)value) & 0x1000) != 0)
                ret = ret + "at port A";
            if ((((int)(ushort)value) & 0x2000) != 0)
                ret = ret + "at port B";
            if ((((int)(ushort)value) & 0x4000) != 0)
                ret = ret + "at port C";
            if ((((int)(ushort)value) & 0x8000) != 0)
                ret = ret + "at port D";
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

    public class InfoDataStateToBackground : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ushort)value != 8)
            {
                return TcoCore.Wpf.TcoColors.Error;
            }
            else
            {
                ResourceDictionary ColorResorce = new ResourceDictionary();
                ColorResorce.Source = new Uri(
                    "/TcOpen.Inxton.TcoIo.Wpf;component/diagnostics/ethercat/colors/colors.xaml",
                    UriKind.RelativeOrAbsolute
                );
                return new SolidColorBrush((Color)ColorResorce["InxtonGrayLightColor"]);
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

    public class InfoDataStateToForeground : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ushort)value != 8)
            {
                return TcoCore.Wpf.TcoColors.OnError;
            }
            else
            {
                return Brushes.Black;
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
