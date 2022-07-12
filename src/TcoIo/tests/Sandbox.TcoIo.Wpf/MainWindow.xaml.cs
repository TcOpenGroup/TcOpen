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

namespace Sandbox.TcoIo.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    ZoomSlider.Value = ZoomSlider.Value + e.Delta / 120 * ZoomSlider.SmallChange;
        //}

        //private void StackPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (Keyboard.Modifiers != ModifierKeys.Control)
        //        return;

        //    if (e.Delta > 0)
        //    {
        //        zoom = zoom * (1.0 +  e.Delta / 1200.0);
        //        (Stack.LayoutTransform as ScaleTransform).ScaleX = zoom;
        //        (Stack.LayoutTransform as ScaleTransform).ScaleY = zoom;
        //    }
        //    else if (e.Delta < 0)
        //    {
        //        zoom = zoom / (1.0 - e.Delta / 1200.0);
        //        (Stack.LayoutTransform as ScaleTransform).ScaleX = zoom;
        //        (Stack.LayoutTransform as ScaleTransform).ScaleY = zoom;
        //    }
        //    if (zoom > 8.0) zoom = 8.0;
        //    if (zoom < 0.125) zoom = 0.125;
        //}
    }
}
