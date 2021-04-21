using System.Windows;
using System.Windows.Controls;
using Vortex.Connector;

namespace Tco.Wpf
{
    public partial class DynamicTreeView : UserControl
    {
        public DynamicTreeView()
        {
            InitializeComponent();            
        }

        private TreeWrapperObject _dc;
        
        public TreeWrapperObject Dc
        {
            get
            {
                //if(_dc == null)
                {
                    _dc = new TreeWrapperObject(this.DataContext as IVortexObject);

                }
                return _dc;
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            tv.DataContext = Dc;           
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(DynamicTreeView), null);

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedItem = e.NewValue;
        }
    }

}
