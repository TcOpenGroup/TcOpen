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
        public Zoom zoom = new Zoom();
        public string PresentationType { get; set; }

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
            previousTopologyObject = new TopologyObject();
            topologyObjects = new ObservableCollection<TopologyObject>();
            this.PresentationType = "TopologyDevice-TopologyBaseM90-TopologyBoxM90-TopologyTerminalM90-TopologyEndTerminalM90";
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.DataContextChanged += TopologyRenderer_DataContextChanged;

            InitializeComponent();
        }


        private void TopologyRenderer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            PrepareHardware(this.DataContext as IVortexObject);

            Grid wiring = PrepareWiring() as Grid;
            this.stackPanel.Children.Clear();
            this.stackPanel.Children.Add(Render(wiring) as Grid);

        }

        private void stackPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
                return;

            if (e.Delta > 0)
            {
                zoom.ZoomValue = zoom.ZoomValue * (1.0 + e.Delta / 1200.0);
            }
            else if (e.Delta < 0)
            {
                zoom.ZoomValue = zoom.ZoomValue / (1.0 - e.Delta / 1200.0);
            }
            if (zoom.ZoomValue > 8.0) zoom.ZoomValue = 8.0;
            if (zoom.ZoomValue < 0.125) zoom.ZoomValue = 0.125;
            (stackPanel.LayoutTransform as ScaleTransform).ScaleX = zoom.ZoomValue;
            (stackPanel.LayoutTransform as ScaleTransform).ScaleY = zoom.ZoomValue;
        }

        public object Render(Grid wiringGrid = null)
        {
            Grid hardwareGrid = new Grid();
            wiringGrid = wiringGrid == null ? new Grid() : wiringGrid;

            if (!hardwareGrid.Children.Contains(wiringGrid))
            {
                hardwareGrid.Children.Add(wiringGrid);
                wiringGrid.SetValue(Grid.RowSpanProperty, maxrow + 1);
                wiringGrid.SetValue(Grid.ColumnSpanProperty, maxcolumn + 1);
            }

            foreach (TopologyObject topologyObject in topologyObjects)
            {
                UniformGrid hardware = topologyObject.Hardware;
                hardwareGrid.Children.Add(hardware);
                Grid.SetColumn(hardware, topologyObject.Column);
                Grid.SetRow(hardware, topologyObject.Row);
                hardwareGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                hardwareGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            ////////////////////////////////////////////
            ///Test zoom value
            ////////////////////////////////////////////

            TextBlock textBlock = new TextBlock();
            textBlock.SetBinding(TextBlock.TextProperty, new Binding() { Source = zoom, Path = new PropertyPath("ZoomValue"), Mode = BindingMode.OneWay });
            Grid.SetColumn(textBlock, 0);
            Grid.SetRow(textBlock, 1);
            hardwareGrid.Children.Add(textBlock);

            ////////////////////////////////////////////
            ///Temporary button just for debugging
            ////////////////////////////////////////////
#if DEBUG
            Button RefreshButton = new Button();
            RefreshButton.Content = "Refresh";
            RefreshButton.MaxWidth = 128;
            Grid.SetColumn(RefreshButton, 0);
            Grid.SetRow(RefreshButton, 0);
            zoom.ZoomValue = 1.0;
            (stackPanel.LayoutTransform as ScaleTransform).ScaleX = zoom.ZoomValue;
            (stackPanel.LayoutTransform as ScaleTransform).ScaleY = zoom.ZoomValue;

            RefreshButton.Click += RefreshButton_Click; ;

            hardwareGrid.Children.Add(RefreshButton);
#endif
            ////////////////////////////////////////////
            return hardwareGrid;
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
