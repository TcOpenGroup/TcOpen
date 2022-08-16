using System.Windows;
using System.Windows.Controls;
using Vortex.Connector;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public string FirstTopologyElementName
        {
            get 
            {
                string name = "";
                this.Dispatcher.Invoke(() => { name = ValidateFrameworkElement.Name((string)GetValue(FirstTopologyElementNameProperty)); });
                return name;
            }
            set 
            {
                this.Dispatcher.Invoke(() => SetValue(FirstTopologyElementNameProperty, ValidateFrameworkElement.Name(value)));
            }
        }

        // Using a DependencyProperty as the backing store for FirstTopologyElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstTopologyElementNameProperty =
            DependencyProperty.Register("FirstTopologyElementName",
                                        typeof(string),
                                        typeof(TopologyRenderer),
                                        new PropertyMetadata(""));
    }
}
