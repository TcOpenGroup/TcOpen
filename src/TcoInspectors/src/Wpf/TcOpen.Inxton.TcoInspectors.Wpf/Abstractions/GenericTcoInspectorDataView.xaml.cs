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

namespace TcoInspectors
{
    /// <summary>
    /// Interaction logic for TcoInspectorDataControlView.xaml
    /// </summary>
    public partial class GenericTcoInspectorDataView : UserControl
    {
        public GenericTcoInspectorDataView()
        {
            InitializeComponent();
        }



        public string PresentationType
        {
            get { return (string)GetValue(PresentationTypeProperty); }
            set { SetValue(PresentationTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentationType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentationTypeProperty =
            DependencyProperty.Register("PresentationType", typeof(string), typeof(GenericTcoInspectorDataView), new PropertyMetadata("Display"));


    }
}
