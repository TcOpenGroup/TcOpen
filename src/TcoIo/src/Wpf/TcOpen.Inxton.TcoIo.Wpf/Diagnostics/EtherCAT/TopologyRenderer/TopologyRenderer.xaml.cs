using System.Windows;
using System.Windows.Controls;
using Vortex.Connector;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Controls.Primitives;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        static int row, maxrow, column, maxcolumn = 0;
        static double Pos_X , MaxPos_X , Pos_Y , MaxPos_Y = 0.0;
        TopologyObject previousTopologyObject;
        ObservableCollection<TopologyObject> topologyObjects;
        static double strokeThicknessDef = 10.0;
        static string _name , _conection , _boxtype, _physics;
        public double zoom = 1.0;
        public ushort SummaryInfoDataState = 0;
        private IVortexObject dt;
        public List<GroupedViewItemObject> groupedViewItems = new List<GroupedViewItemObject>();
        public bool FirstTopologyElementReached , LastTopologyElementReached , syncUnitError = false;
        static int FirstElementRow , LastElementRow , FirstElementColumn , LastElementColumn = -1;
        IVortexObject firstTopologyElement, lastTopologyElement;

        public TopologyRenderer()
        {
            row = maxrow = column = maxcolumn = 0;
            Pos_X = Pos_Y = MaxPos_X = MaxPos_Y = 0;
            SummaryInfoDataState = 0;
            FirstElementRow = LastElementRow = FirstElementColumn = LastElementColumn = -1;
            firstTopologyElement = lastTopologyElement = null;
            previousTopologyObject = new TopologyObject();
            topologyObjects = new ObservableCollection<TopologyObject>();
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.DataContextChanged += TopologyRenderer_DataContextChanged;
            InitializeComponent();
        }

        private void TopologyRenderer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RenderCompleteTopology();
        }

        private void RenderCompleteTopology()
        {
            if (this.DataContext as IVortexObject != null)
            {
                dt = this.DataContext as IVortexObject;
                FirstTopologyElementReached = false;
                LastTopologyElementReached = false;
                groupedViewItems = new List<GroupedViewItemObject>();

                FindObjectsAndSubscribeToInfoDataState(dt);

                PrepareHardware(dt);

                FilterOutExcludedHardware();

                Grid wiring = PrepareWiring() as Grid;
                this.grid.Children.Clear();
                this.grid.Children.Add(Render(wiring) as Grid);
            }
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (GroupedView == false)
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
            else
            {
                if(this.grid != null && this.grid.Children != null)
                {
                    Grid child_0 = this.grid.Children[0] as Grid;
                    if(child_0 != null)
                    {
                        UniformGrid child_0_1 = child_0.Children[1] as UniformGrid;
                        if (child_0_1 != null)
                        {
                            EtcGroupedView child_0_1_0 = child_0_1.Children[0] as EtcGroupedView;
                            if (child_0_1_0 != null)
                            {
                                ScrollViewer groupScrollViewer = child_0_1_0.groupViewScrollViewer as ScrollViewer;
                                if(groupScrollViewer != null)
                                {
                                    if (e.Delta > 0)
                                    {
                                        groupScrollViewer.LineUp();
                                    }
                                    else if (e.Delta < 0)
                                    {
                                        groupScrollViewer.LineDown();
                                    }
                                    e.Handled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
