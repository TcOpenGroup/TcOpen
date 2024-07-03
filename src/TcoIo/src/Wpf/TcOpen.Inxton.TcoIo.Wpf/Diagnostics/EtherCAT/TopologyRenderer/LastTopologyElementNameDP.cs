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
        public string LastTopologyElementName
        {
            get
            {
                string name = "";
                TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(
                    () =>
                        name = ValidateFrameworkElement.Name(
                            (string)GetValue(LastTopologyElementNameProperty)
                        )
                );
                return name;
            }
            set
            {
                TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(
                    () =>
                        SetValue(
                            LastTopologyElementNameProperty,
                            ValidateFrameworkElement.Name(value)
                        )
                );
            }
        }

        // Using a DependencyProperty as the backing store for FirstTopologyElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastTopologyElementNameProperty =
            DependencyProperty.Register(
                "LastTopologyElementName",
                typeof(string),
                typeof(TopologyRenderer),
                new PropertyMetadata("")
            );
    }
}
