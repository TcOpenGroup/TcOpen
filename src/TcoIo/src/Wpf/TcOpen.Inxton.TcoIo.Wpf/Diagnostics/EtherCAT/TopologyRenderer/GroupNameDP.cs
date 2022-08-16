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
        public string GroupName
        {
            get { return ValidateFrameworkElement.Name((string)GetValue(GroupNameProperty)); }
            set { SetValue(GroupNameProperty, ValidateFrameworkElement.Name(value)); }
        }

        // Using a DependencyProperty as the backing store for Group.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName",
                                        typeof(string),
                                        typeof(TopologyRenderer),
                                        new PropertyMetadata("Default group name"));
    }
}
