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
            get {return (bool)GetValue(ExcludeSlavesConnectedToJunctionBoxProperty);  }
            set {SetValue(ExcludeSlavesConnectedToJunctionBoxProperty, value); }
        }

        public static readonly DependencyProperty ExcludeSlavesConnectedToJunctionBoxProperty =
            DependencyProperty.Register("ExcludeSlavesConnectedToJunctionBox", 
                                        typeof(bool), 
                                        typeof(TopologyRenderer), 
                                        new PropertyMetadata(false));
    }
}
