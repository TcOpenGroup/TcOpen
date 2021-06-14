using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TcOpen.Inxton.TcoCore.Wpf.TcoComponent
{
    /// <summary>
    /// Interaction logic for TcoComponentView.xaml
    /// </summary>
    public partial class TcoComponentView : UserControl
    {
        public TcoComponentView()
        {
            InitializeComponent();
        }

        public UIElement ComponentHeader
        {
            get { return (UIElement)GetValue(ComponentHeaderProperty); }
            set { SetValue(ComponentHeaderProperty, value); }
        }

        public static readonly DependencyProperty ComponentHeaderProperty =
            DependencyProperty.Register("ComponentHeaderProperty", typeof(UIElement), typeof(TcoComponentView),
                new PropertyMetadata(null));



        public UIElement ComponentDetails
        {
            get { return (UIElement)GetValue(ComponentDetailsProperty); }
            set { SetValue(ComponentDetailsProperty, value); }
        }

        public static readonly DependencyProperty ComponentDetailsProperty =
            DependencyProperty.Register("ComponentDetails", typeof(UIElement), typeof(TcoComponentView),
                new PropertyMetadata(null));
    }
}
