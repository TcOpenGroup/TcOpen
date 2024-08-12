using System;
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
        public Type UserView
        {
            get
            {
                Type userView = null;
                TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(
                    () => userView = (Type)GetValue(UserViewProperty)
                );
                return userView;
            }
            set
            {
                TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(
                    () => SetValue(UserViewProperty, value as Type)
                );
            }
        }

        // Using a DependencyProperty as the backing store for FirstTopologyElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserViewProperty = DependencyProperty.Register(
            "UserView",
            typeof(Type),
            typeof(TopologyRenderer),
            new FrameworkPropertyMetadata(null, OnUserViewPropertyChanged)
        );

        private static void OnUserViewPropertyChanged(
            DependencyObject source,
            DependencyPropertyChangedEventArgs e
        )
        {
            ((TopologyRenderer)source).UserView = e.NewValue as Type;
        }
    }
}
