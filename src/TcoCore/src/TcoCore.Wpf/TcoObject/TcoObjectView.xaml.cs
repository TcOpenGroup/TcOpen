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

namespace TcoCore
{
    /// <summary>
    /// Interaction logic for TcoTask.xaml
    /// </summary>
    public partial class TcoObjectView : UserControl
    {
        public TcoObjectView()
        {
            InitializeComponent();
        }
    }

    public class CountToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as IEnumerable<object>;
            return list?.Count() > 0 ? Visibility.Visible : Visibility.Collapsed;
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

    public class EmptyStringToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as string;
            return list?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
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

    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var @is = (bool)value;
                return @is ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception)
            {
                //!Swallow
            }
            
            return Visibility.Collapsed;
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
