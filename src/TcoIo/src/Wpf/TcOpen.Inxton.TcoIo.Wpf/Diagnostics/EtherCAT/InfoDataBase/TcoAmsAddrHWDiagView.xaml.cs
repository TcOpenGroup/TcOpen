using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for fbSampleComponentView.xaml
    /// </summary>
    public partial class TcoAmsAddrHWDiagView : UserControl
    {
        public TcoAmsAddrHWDiagView()
        {
            InitializeComponent();
        }
    }
    public class AdsAddrToString : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TcoAmsAddr tcoAmsAddr = (TcoAmsAddr)value;
            return string.Format("{0}.{1}.{2}.{3}.{4}.{5}:{6}", tcoAmsAddr.netId[0].Synchron.ToString(), 
                                                                tcoAmsAddr.netId[1].Synchron.ToString(), 
                                                                tcoAmsAddr.netId[2].Synchron.ToString(), 
                                                                tcoAmsAddr.netId[3].Synchron.ToString(), 
                                                                tcoAmsAddr.netId[4].Synchron.ToString(), 
                                                                tcoAmsAddr.netId[5].Synchron.ToString(),
                                                                tcoAmsAddr.port.Synchron.ToString());
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
