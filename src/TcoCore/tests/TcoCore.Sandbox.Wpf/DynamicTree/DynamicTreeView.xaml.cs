using Tco.Wpf.DynamicTree.DataTemplates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vortex.Connector;

namespace inxton.vortex.framework.dynamictreeview.wpf.sandbox
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

      
    }

}
