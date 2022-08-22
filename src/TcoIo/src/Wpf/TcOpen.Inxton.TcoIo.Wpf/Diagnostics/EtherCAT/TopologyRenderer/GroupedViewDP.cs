using System.Windows;
using System.Windows.Controls;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public bool GroupedView
        {
            get 
            {
                bool _value = false;
                TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() => 
                    {
                        _value = (bool)GetValue(GroupedViewProperty); 
                    });
                return _value;
            }
            set { TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() => { SetValue(GroupedViewProperty, value); }); }
        }

        // Using a DependencyProperty as the backing store for GroupedView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupedViewProperty =
            DependencyProperty.Register("GroupedView", 
                                        typeof(bool), 
                                        typeof(TopologyRenderer), 
                                        new PropertyMetadata(false));
    }
}
