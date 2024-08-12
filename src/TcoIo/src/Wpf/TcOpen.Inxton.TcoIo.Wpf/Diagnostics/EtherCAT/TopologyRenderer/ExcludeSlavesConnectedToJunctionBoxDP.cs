using System.Windows;
using System.Windows.Controls;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public bool ExcludeSlavesConnectedToJunctionBox
        {
            get
            {
                bool ret = false;
                TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
                {
                    ret = (bool)GetValue(ExcludeSlavesConnectedToJunctionBoxProperty);
                });
                return ret;
            }
            set
            {
                TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(
                    () => SetValue(ExcludeSlavesConnectedToJunctionBoxProperty, value)
                );
            }
        }

        public static readonly DependencyProperty ExcludeSlavesConnectedToJunctionBoxProperty =
            DependencyProperty.Register(
                "ExcludeSlavesConnectedToJunctionBox",
                typeof(bool),
                typeof(TopologyRenderer),
                new PropertyMetadata(false)
            );
    }
}
