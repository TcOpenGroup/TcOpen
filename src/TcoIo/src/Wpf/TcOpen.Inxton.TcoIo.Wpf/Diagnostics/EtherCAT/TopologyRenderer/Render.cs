using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using TcoIo.Topology;

namespace TcoIo
{
    public partial class TopologyRenderer : UserControl
    {
        public object Render(Grid wiringGrid = null)
        {
            Grid hardwareGrid = new Grid() { Margin = new Thickness() };
            wiringGrid = wiringGrid == null ? new Grid() { Margin = new Thickness() } : wiringGrid;
            int gridRow = 0;
            int gridColumn = 0;
            TopologyObject firstTopologyObject = null;
            for (int i = 0; i <= maxrow; i++)
            {
                GridLength RowHeight = new GridLength(
                    DimsDef.masterHeightWithBorders,
                    GridUnitType.Pixel
                );
                hardwareGrid.RowDefinitions.Add(new RowDefinition() { Height = RowHeight });
            }
            for (int i = 0; i <= maxcolumn; i++)
            {
                hardwareGrid.ColumnDefinitions.Add(
                    new ColumnDefinition() { Width = GridLength.Auto }
                );
            }

            foreach (TopologyObject topologyObject in topologyObjects)
            {
                UniformGrid hardware = topologyObject.Hardware;
                hardwareGrid.Children.Add(hardware);
                Grid.SetColumn(hardware, topologyObject.Column);
                Grid.SetRow(hardware, topologyObject.Row);
                gridRow = Math.Max(gridRow, topologyObject.Row);
                gridColumn = Math.Max(gridColumn, topologyObject.Column);
                firstTopologyObject =
                    firstTopologyObject == null ? topologyObject : firstTopologyObject;
            }
            if (!hardwareGrid.Children.Contains(wiringGrid))
            {
                hardwareGrid.Children.Add(wiringGrid);
                if (
                    hardwareGrid.RowDefinitions.Count > 0
                    && hardwareGrid.ColumnDefinitions.Count > 0
                )
                {
                    wiringGrid.SetValue(Grid.RowSpanProperty, hardwareGrid.RowDefinitions.Count);
                    wiringGrid.SetValue(
                        Grid.ColumnSpanProperty,
                        hardwareGrid.ColumnDefinitions.Count
                    );
                }
            }

            ////////////////////////////////////////////
            ///Debug zoom value
            ////////////////////////////////////////////
            //TextBlock textBlock = new TextBlock();
            //textBlock.SetBinding(TextBlock.TextProperty, new Binding() { Source = zoom, Path = new PropertyPath("ZoomValue"), Mode = BindingMode.OneWay });
            //Grid.SetColumn(textBlock, 0);
            //Grid.SetRow(textBlock, 1);
            //hardwareGrid.Children.Add(textBlock);
            ////////////////////////////////////////////

            ////////////////////////////////////////////
            ////Debug hardware grid rows/columns count
            ////////////////////////////////////////////
            //TextBlock rows = new TextBlock();
            //rows.Text = "Row counts: " + hardwareGrid.RowDefinitions.Count + " : " + gridRow.ToString();
            //Grid.SetColumn(rows, 0);
            //Grid.SetRow(rows, 2);
            //hardwareGrid.Children.Add(rows);

            //TextBlock columns = new TextBlock();
            //columns.Text = "Columns counts: " + hardwareGrid.ColumnDefinitions.Count + " : " + gridColumn.ToString();
            //Grid.SetColumn(columns, 0);
            //Grid.SetRow(columns, 3);
            //hardwareGrid.Children.Add(columns);
            ////////////////////////////////////////////

            ////////////////////////////////////////////
            ////Debug last element position
            ////////////////////////////////////////////
            //TextBlock lastTopologyObjectPositionReq = new TextBlock();
            //lastTopologyObjectPositionReq.Text = "Last topology object position required: " + Environment.NewLine + "X: [" + lastTopologyObject.Pos_X.ToString() + "]," + Environment.NewLine + "Y [" + lastTopologyObject.Pos_Y.ToString() + "]";
            //Grid.SetColumn(lastTopologyObjectPositionReq, 0);
            //Grid.SetRow(lastTopologyObjectPositionReq, 4);
            //hardwareGrid.Children.Add(lastTopologyObjectPositionReq);
            ////////////////////////////////////////////

            ////////////////////////////////////////////
            ///Temporary button just for debugging
            ////////////////////////////////////////////
            Button RefreshButton = new Button();
            RefreshButton.Content = "Refresh";
            RefreshButton.MaxWidth = DimsDef.slaveWidth;
            RefreshButton.Padding = new Thickness(0);
            RefreshButton.Margin = new Thickness(0);

            Grid.SetColumn(RefreshButton, 0);
            Grid.SetRow(RefreshButton, 0);
            zoom = 1.0;
            (grid.LayoutTransform as ScaleTransform).ScaleX = zoom;
            (grid.LayoutTransform as ScaleTransform).ScaleY = zoom;

            RefreshButton.Click += RefreshButton_Click;
            ;

            //hardwareGrid.Children.Add(RefreshButton);
            ////////////////////////////////////////////
            return hardwareGrid;
        }

        ////////////////////////////////////////////
        ///Temporary event just for debugging
        ////////////////////////////////////////////
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            DependencyPropertyChangedEventArgs _e = new DependencyPropertyChangedEventArgs();
            FirstTopologyElementReached = false;
            LastTopologyElementReached = false;
            row = maxrow = column = 0;
            Pos_X = Pos_Y = MaxPos_Y = 0;
            FirstElementRow = LastElementRow = FirstElementColumn = LastElementColumn = -1;
            previousTopologyObject = new TopologyObject();
            topologyObjects = new ObservableCollection<TopologyObject>();
            firstTopologyElement = lastTopologyElement = null;
            dt = null;
            RenderCompleteTopology();
        }
        ////////////////////////////////////////////
    }
}
