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
    public partial class TopologyRenderer : UserControl
    {
        static int row;
        static int maxrow;
        static int column;
        static int maxcolumn;
        static double Pos_X;
        static double MaxPos_X;
        static double Pos_Y;
        static double MaxPos_Y;
        static TopologyObject previousTopologyObject;
        static ObservableCollection<TopologyObject> topologyObjects;
        static double strokeThicknessDef = 10.0;
        static string _name;
        static string _conection;
        static string _boxtype;
        static string _physics;
        public double zoom = 1.0;
        public string PresentationType { get; set; }
        public ushort InfoDataState = 0;
        private IVortexObject dt;

        public TopologyRenderer()
        {
            row = 0;
            maxrow = 0;
            column = 0;
            maxcolumn = 0;
            Pos_X = 0;
            Pos_Y = 0;
            MaxPos_X = 0;
            MaxPos_Y = 0;
            InfoDataState = 0;
            previousTopologyObject = new TopologyObject();
            topologyObjects = new ObservableCollection<TopologyObject>();
            this.PresentationType = "TopologyDevice-TopologyBoxM90-TopologyTerminalM90-TopologyEndTerminalM90";
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.DataContextChanged += TopologyRenderer_DataContextChanged;

            InitializeComponent();
        }


        private void TopologyRenderer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext as IVortexObject != null)
            {
                dt = this.DataContext as IVortexObject;

                PrepareHardware(dt);

                Grid wiring = PrepareWiring() as Grid;
                this.grid.Children.Clear();
                this.grid.Children.Add(Render(wiring) as Grid);
            }
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Delta > 0)
                {
                    zoom = zoom * (1.0 + e.Delta / 1200.0);
                }
                else if (e.Delta < 0)
                {
                    zoom = zoom / (1.0 - e.Delta / 1200.0);
                }
                if (zoom > 8.0) zoom = 8.0;
                if (zoom < 0.125) zoom = 0.125;
                (grid.LayoutTransform as ScaleTransform).ScaleX = zoom;
                (grid.LayoutTransform as ScaleTransform).ScaleY = zoom;
                e.Handled = true;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                if (e.Delta > 0)
                {
                    scrollViewer.LineLeft();
                }
                else if (e.Delta < 0)
                {
                    scrollViewer.LineRight();
                }
                e.Handled = true;
            }
        }

            ////////////////////////////////////////////
            ///Temporary event just for debugging
            ////////////////////////////////////////////
            private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            DependencyPropertyChangedEventArgs _e = new DependencyPropertyChangedEventArgs();
            row = maxrow = column = 0;
            Pos_X = Pos_Y = MaxPos_Y = 0;
            previousTopologyObject = new TopologyObject();
            topologyObjects = new ObservableCollection<TopologyObject>();
            TopologyRenderer_DataContextChanged(sender, _e);
        }
        ////////////////////////////////////////////

    }
}
