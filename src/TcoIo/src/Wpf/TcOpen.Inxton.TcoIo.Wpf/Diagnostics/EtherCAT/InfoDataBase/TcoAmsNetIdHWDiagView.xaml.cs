using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for fbSampleComponentView.xaml
    /// </summary>
    public partial class TcoAmsNetIdHWDiagView : UserControl
    {
        public TcoAmsNetIdHWDiagView()
        {
            InitializeComponent();
        }
    }

    public class AmsNetIdToString : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TcoAmsNetId tcoAmsNetId = (TcoAmsNetId)value;
            return string.Format(
                "{0}.{1}.{2}.{3}.{4}.{5}",
                tcoAmsNetId.netId[0].Synchron.ToString(),
                tcoAmsNetId.netId[1].Synchron.ToString(),
                tcoAmsNetId.netId[2].Synchron.ToString(),
                tcoAmsNetId.netId[3].Synchron.ToString(),
                tcoAmsNetId.netId[4].Synchron.ToString(),
                tcoAmsNetId.netId[5].Synchron.ToString()
            );
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
