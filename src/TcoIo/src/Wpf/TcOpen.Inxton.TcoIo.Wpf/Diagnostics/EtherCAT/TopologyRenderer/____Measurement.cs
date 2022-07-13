using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Vortex.Connector;
using Vortex.Presentation.Wpf;
using System.Collections.ObjectModel;
using TcoIo.Topology;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Data;
using TcoIo.Converters;
using System.Windows.Input;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public static class __Measurement
    {
        public static Rect GetAbsolutePlacement(this FrameworkElement element, bool relativeToScreen = false)
        {
            Rect ret = new Rect();
            //if (element.ActualHeight > 0 && element.ActualWidth > 0)
            if (!string.IsNullOrEmpty(element.Name))
            {
                var absolutePos = element.PointToScreen(new System.Windows.Point(0, 0));
                if (relativeToScreen)
                {
                    ret = new Rect(absolutePos.X, absolutePos.Y, element.ActualWidth, element.ActualHeight);
                }
                var posMW = Application.Current.MainWindow.PointToScreen(new System.Windows.Point(0, 0));
                absolutePos = new System.Windows.Point(absolutePos.X - posMW.X, absolutePos.Y - posMW.Y);
                ret = new Rect(absolutePos.X, absolutePos.Y, element.ActualWidth, element.ActualHeight);
            }
            return ret;
        }

    }
}
