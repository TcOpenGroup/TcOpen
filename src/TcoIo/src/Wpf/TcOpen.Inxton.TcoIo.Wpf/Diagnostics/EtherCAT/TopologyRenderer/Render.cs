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
        public object Render(Grid wiringGrid = null)
        {
            Grid hardwareGrid = new Grid() { Margin = new Thickness() };
            wiringGrid = wiringGrid == null ? new Grid() { Margin = new Thickness() } : wiringGrid;
            int gridRow = 0;
            int gridColumn = 0;
            TopologyObject lastTopologyObject = new TopologyObject();
            UniformGrid lastHardware = new UniformGrid();
            for (int i = 0; i <= maxrow; i++)
            {
                //hardwareGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                GridLength RowHeight = new GridLength(DimsDef.masterHeightWithBorders, GridUnitType.Pixel);
                hardwareGrid.RowDefinitions.Add(new RowDefinition() { Height = RowHeight});
            }
            for (int i = 0; i <= maxcolumn; i++)
            {
                hardwareGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                //GridLength ColumnWidth = new GridLength(DimsDef.slaveWidthWithBorders, GridUnitType.Pixel);
                //hardwareGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = ColumnWidth });
            }

            foreach (TopologyObject topologyObject in topologyObjects)
            {
                UniformGrid hardware = topologyObject.Hardware;
                hardwareGrid.Children.Add(hardware);
                Grid.SetColumn(hardware, topologyObject.Column);
                Grid.SetRow(hardware, topologyObject.Row);
                gridRow = Math.Max(gridRow, topologyObject.Row);
                gridColumn = Math.Max(gridColumn, topologyObject.Column);
                lastTopologyObject = topologyObject;
                lastHardware = hardware;
            }
            if (!hardwareGrid.Children.Contains(wiringGrid))
            {
                hardwareGrid.Children.Add(wiringGrid);
                if (hardwareGrid.RowDefinitions.Count > 0 && hardwareGrid.ColumnDefinitions.Count > 0)
                {
                    wiringGrid.SetValue(Grid.RowSpanProperty, hardwareGrid.RowDefinitions.Count);
                    wiringGrid.SetValue(Grid.ColumnSpanProperty, hardwareGrid.ColumnDefinitions.Count);
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

            //////////////////////////////////////////
            //Debug hardware grid rows/columns count
            //////////////////////////////////////////
            TextBlock rows = new TextBlock();
            rows.Text = "Row counts: " + hardwareGrid.RowDefinitions.Count + " : " + gridRow.ToString();
            Grid.SetColumn(rows, 0);
            Grid.SetRow(rows, 2);
            hardwareGrid.Children.Add(rows);

            TextBlock columns = new TextBlock();
            columns.Text = "Columns counts: " + hardwareGrid.ColumnDefinitions.Count + " : " + gridColumn.ToString();
            Grid.SetColumn(columns, 0);
            Grid.SetRow(columns, 3);
            hardwareGrid.Children.Add(columns);

            //////////////////////////////////////////

            //////////////////////////////////////////
            //Debug hardware grid rows/columns count
            //////////////////////////////////////////
            TextBlock lastTopologyObjectPositionReq = new TextBlock();
            lastTopologyObjectPositionReq.Text = "Last topology object position required: " + Environment.NewLine + "X: [" + lastTopologyObject.Pos_X.ToString() + "]," + Environment.NewLine + "Y [" + lastTopologyObject.Pos_Y.ToString() + "]";
            Grid.SetColumn(lastTopologyObjectPositionReq, 0);
            Grid.SetRow(lastTopologyObjectPositionReq, 4);
            hardwareGrid.Children.Add(lastTopologyObjectPositionReq);

            //TextBlock lastTopologyObjectPositionAct = new TextBlock();
            //lastTopologyObjectPositionAct.Text = "Last topology object position measured: " + Environment.NewLine + "X: [" + r.Width.ToString() + "]," + Environment.NewLine + "Y [" + r.Height.ToString() + "]";
            //Grid.SetColumn(lastTopologyObjectPositionAct, 0);
            //Grid.SetRow(lastTopologyObjectPositionAct, 5);
            //hardwareGrid.Children.Add(lastTopologyObjectPositionAct);

            //////////////////////////////////////////
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
            (grid.LayoutTransform as ScaleTransform).ScaleX = zoom.ZoomValue;
            (grid.LayoutTransform as ScaleTransform).ScaleY = zoom.ZoomValue;

            RefreshButton.Click += RefreshButton_Click; ;

            hardwareGrid.Children.Add(RefreshButton);
#endif
            ////////////////////////////////////////////
            return hardwareGrid;
        }


    }
}
