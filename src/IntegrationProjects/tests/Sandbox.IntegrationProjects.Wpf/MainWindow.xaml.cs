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

namespace Sandbox.IntegrationProjects.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class TaskBusyToVisibleConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var val = (TcoCore.eTaskState)((short)value);
                switch (val)
                {                                            
                    case TcoCore.eTaskState.Requested:                       
                    case TcoCore.eTaskState.Busy:                        
                    case TcoCore.eTaskState.Error:
                        return Visibility.Visible;
                    case TcoCore.eTaskState.Done:
                    case TcoCore.eTaskState.Ready:
                        return Visibility.Hidden;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                // Swallow
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
