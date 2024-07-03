using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TcoDrivesBeckhoff
{
    public partial class TcoDriveSimpleServiceView : UserControl
    {
        public TcoDriveSimpleServiceView()
        {
            InitializeComponent();
        }
    }

    public class ErrorIdToDescriptionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                uint errorId = (uint)value;
                return NcErrors.Errors.ContainsKey(errorId)
                    ? NcErrors.Errors[errorId]
                    : "No error description available.";
            }
            catch (Exception)
            {
                throw;
                // swallow
            }

            return string.Empty;
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
