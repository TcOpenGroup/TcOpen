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

namespace TcoIo
{
    public class TopologyRenderer : UserControl
    {
        static int row = 0;
        static int column = 0;
        static double Pos_X = 0;
        static double Pos_Y = 0;
        static TopologyObject previousTopologyObject = new TopologyObject();
        static ObservableCollection<TopologyObject> topologyObjects = new ObservableCollection<TopologyObject>();

        public TopologyRenderer() : base()
        {
            this.DataContextChanged += TopologyRenderer_DataContextChanged;           
        }

        public string PresentationType { get; set; }

        public enum HardwareType
        {
            Undefined,
            EtcMasterBase,
            EtcSlaveBoxBase,
            EtcSlaveTerminalBase,
            EtcSlaveEndTerminalBase
        }

        private void TopologyRenderer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Content = Render(this.DataContext as IVortexObject, ref row, ref column, ref Pos_X, ref Pos_Y, ref previousTopologyObject);
        }

        public object Render(IVortexObject obj, ref int row, ref int column, ref double Pos_X, ref double Pos_Y, ref TopologyObject previousTopologyObject, Grid mainGrid = null, Grid subGrid = null, UniformGrid cell = null)
        {

            if (obj != null)
            {
                mainGrid = mainGrid == null ? new Grid() : mainGrid;
                subGrid = subGrid == null ? new Grid() : subGrid;
                cell = cell == null ? new UniformGrid() : cell;

                string currentPhysics = obj.GetType().GetProperty("AttributePhysics")?.GetValue(obj).ToString();
                string currentConnection = obj.GetType().GetProperty("AttributePreviousPort")?.GetValue(obj).ToString();
                string currentBoxType = obj.GetType().GetProperty("AttributeBoxType")?.GetValue(obj).ToString();

                Type[] interfaces = obj.GetType().GetInterfaces();

                bool isMaster = false;
                bool isSlave = false;


                foreach (Type item in interfaces)
                {
                    if (item.Name.Contains("EtcMasterBase"))
                    {
                        isMaster = true;
                        currentPhysics = "Master";
                        break;
                    }
                    else if (item.Name.Contains("EtcSlaveBase"))
                    {
                        isSlave = true;
                        break;
                    }
                }

                var presentation = LazyRenderer.Get.CreatePresentation(PresentationType, obj) as FrameworkElement;
                presentation.HorizontalAlignment = HorizontalAlignment.Stretch;
                presentation.VerticalAlignment = VerticalAlignment.Stretch;

                if (presentation != null && (isMaster || isSlave))
                {
                    if (!mainGrid.Children.Contains(subGrid))
                    {
                        mainGrid.Children.Add(subGrid);
                        subGrid.SetValue(Grid.RowSpanProperty, 100);
                        subGrid.SetValue(Grid.ColumnSpanProperty, 100);
                    }

                    cell.Children.Add(presentation);

                    if (isMaster)
                    {
                        column = 0;
                        //Increment row only in case of new master device
                        if (!string.IsNullOrEmpty(previousTopologyObject.Name))
                        {
                            row++;
                            Pos_Y = Pos_Y + DimsDef.masterHeight + 15;
                        }
                        Pos_X = DimsDef.masterWidth + 10;
                    }
                    else if (isSlave)
                    {
                        //First box after master
                        if (currentPhysics != null && currentConnection != null && currentPhysics.StartsWith("Y") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith(previousTopologyObject.Physics))
                        {
                            //No empty cell in case of PLC backplane
                            if (!(currentBoxType != null && currentBoxType.StartsWith("EK1200")))
                            {
                                //Add empty cell after master
                                column++;
                                Pos_X = Pos_X + DimsDef.slaveWidth + 10.0;
                                UniformGrid emptyCell = new UniformGrid();
                                emptyCell.Width = DimsDef.slaveWidth;
                                emptyCell.Height = DimsDef.slaveHeight;

                                mainGrid.Children.Add(emptyCell);
                                Grid.SetColumn(emptyCell, column);
                                Grid.SetRow(emptyCell, row);

                                var InfoData = obj.GetType().GetProperty("InfoData")?.GetValue(obj);
                                var State = InfoData.GetType().GetProperty("State")?.GetValue(InfoData);
                                if (State != null)
                                {
                                    //Direct connection to master
                                    Line line = new Line() { X1 = previousTopologyObject.Pos_X , X2 = previousTopologyObject.Pos_X + DimsDef.slaveWidth + 10 , Y1 = previousTopologyObject.Pos_Y + DimsDef.masterOutput + 5, Y2 = previousTopologyObject.Pos_Y + DimsDef.slaveInput + 5, StrokeThickness = 10 };
                                    Binding binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                    line.SetBinding(Line.StrokeProperty, binding);

                                    subGrid.Children.Add(line);
                                }
                            }
                        }
                        if (currentPhysics != null && currentConnection != null)
                        {
                            //Check if previously added device box or terminal is that one the currently added is connected to
                            if (currentConnection.StartsWith(previousTopologyObject.Name))
                            {
                                column++;
                                Pos_X = Pos_X + DimsDef.slaveWidth + 10;
                            }
                            if (previousTopologyObject.Physics != null)
                            {
                                //First box after 2 port junction box (port D) X1
                                if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KYKY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("D"))
                                {
                                    row++;
                                    Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                    var InfoData = obj.GetType().GetProperty("InfoData")?.GetValue(obj);
                                    var State = InfoData.GetType().GetProperty("State")?.GetValue(InfoData);
                                    if (State != null)
                                    {
                                        //Connection to the X1 of the junction box
                                        double x1 = previousTopologyObject.Pos_X + DimsDef.juntionOutputX1 - 5;
                                        double x2 = previousTopologyObject.Pos_X;
                                        double y1 = Pos_Y;
                                        double y2 = Pos_Y + DimsDef.slaveInput + 5;
                                        Line line = new Line() { X1 = x1 , X2 = x1 , Y1 = y1 , Y2 = y2 + 5, StrokeThickness = 10 };
                                        Binding binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                        line.SetBinding(Line.StrokeProperty, binding);
                                        subGrid.Children.Add(line);

                                        line = new Line() { X1 = x1 - 5, X2 = x2, Y1 = y2, Y2 = y2, StrokeThickness = 10 };
                                        binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                        line.SetBinding(Line.StrokeProperty, binding);
                                        subGrid.Children.Add(line);
                                    }
                                }
                                //First box after 2 port junction box (port B) X2
                                if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KYKY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("B"))
                                {
                                    row++;
                                    Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                    var InfoData = obj.GetType().GetProperty("InfoData")?.GetValue(obj);
                                    var State = InfoData.GetType().GetProperty("State")?.GetValue(InfoData);
                                    if (State != null)
                                    {
                                        //Connection to the X1 of the junction box
                                        double x1 = previousTopologyObject.Pos_X + DimsDef.juntionOutputX2 - 5;
                                        double x2 = previousTopologyObject.Pos_X;
                                        double y1 = Pos_Y;
                                        double y2 = Pos_Y + DimsDef.slaveInput + 5;
                                        Line line = new Line() { X1 = x1, X2 = x1, Y1 = y1, Y2 = y2 + 5, StrokeThickness = 10 };
                                        Binding binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                        line.SetBinding(Line.StrokeProperty, binding);
                                        subGrid.Children.Add(line);

                                        line = new Line() { X1 = x1 - 5, X2 = x2, Y1 = y2, Y2 = y2, StrokeThickness = 10 };
                                        binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                        line.SetBinding(Line.StrokeProperty, binding);
                                        subGrid.Children.Add(line);
                                    }
                                }
                                //First box after extension
                                if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KY") && currentConnection.StartsWith(previousTopologyObject.Name))
                                {
                                    //Add empty cell after extension box
                                    UniformGrid emptyCell = new UniformGrid();
                                    emptyCell.Width = DimsDef.slaveWidth;
                                    emptyCell.Height = DimsDef.slaveHeight;

                                    mainGrid.Children.Add(emptyCell);
                                    Grid.SetColumn(emptyCell, column);
                                    Grid.SetRow(emptyCell, row);
                                    column++;
                                    Pos_X = Pos_X + DimsDef.slaveWidth + 10;

                                    var InfoData = obj.GetType().GetProperty("InfoData")?.GetValue(obj);
                                    var State = InfoData.GetType().GetProperty("State")?.GetValue(InfoData);
                                    if (State != null)
                                    {
                                        //Connection to the extension box
                                        Line line = new Line() { X1 = previousTopologyObject.Pos_X, X2 = previousTopologyObject.Pos_X + DimsDef.slaveWidth + 10, Y1 = previousTopologyObject.Pos_Y + DimsDef.masterOutput + 5, Y2 = previousTopologyObject.Pos_Y + DimsDef.slaveInput + 5, StrokeThickness = 10 };
                                        Binding binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                        line.SetBinding(Line.StrokeProperty, binding);

                                        subGrid.Children.Add(line);
                                    }
                                }
                            }
                            //First box after 2 port junction port C
                            if (!currentConnection.StartsWith(previousTopologyObject.Name))
                            {
                                //Find the topology object the current one is connected to and use its row and column for proper placement
                                foreach (TopologyObject topologyObject in topologyObjects)
                                {
                                    //Find the box the current one is connected to 
                                    if (currentConnection.StartsWith(topologyObject.Name))
                                    {
                                        //First terminal after junction box (port C) K bus
                                        if (currentPhysics.StartsWith("K") && topologyObject.Physics.Equals("KYKY") && currentConnection.EndsWith("C"))
                                        {
                                            column = topologyObject.Column + 1;
                                            Pos_X = topologyObject.Pos_X + DimsDef.slaveWidth + 10;
                                            row = topologyObject.Row;
                                            Pos_Y = topologyObject.Pos_Y + 15;
                                        }
                                        //First box after 2 port junction box (port B) X2
                                        if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("KYKY") && currentConnection.EndsWith("B"))
                                        {
                                            column = topologyObject.Column + 1;
                                            Pos_X = topologyObject.Pos_X + DimsDef.slaveWidth + 10;
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                            var InfoData = obj.GetType().GetProperty("InfoData")?.GetValue(obj);
                                            var State = InfoData.GetType().GetProperty("State")?.GetValue(InfoData);
                                            if (State != null)
                                            {
                                                //Connection to the X1 of the junction box
                                                double x1 = topologyObject.Pos_X + DimsDef.juntionOutputX2 - 5;
                                                double x2 = topologyObject.Pos_X;
                                                double y1 = topologyObject.Pos_Y + DimsDef.slaveHeight + 15;
                                                double y2 = Pos_Y + DimsDef.slaveInput + 5;
                                                Line line = new Line() { X1 = x1, X2 = x1, Y1 = y1, Y2 = y2 + 5, StrokeThickness = 10 };
                                                Binding binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                                line.SetBinding(Line.StrokeProperty, binding);
                                                subGrid.Children.Add(line);

                                                line = new Line() { X1 = x1 - 5, X2 = x2, Y1 = y2, Y2 = y2, StrokeThickness = 10 };
                                                binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                                line.SetBinding(Line.StrokeProperty, binding);
                                                subGrid.Children.Add(line);
                                            }
                                        }
                                        //First box after coupler of the YKY type (port C) X2
                                        if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("YKY") && currentConnection.EndsWith("C"))
                                        {
                                            column = topologyObject.Column;
                                            Pos_X = topologyObject.Pos_X;
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                            var InfoData = obj.GetType().GetProperty("InfoData")?.GetValue(obj);
                                            var State = InfoData.GetType().GetProperty("State")?.GetValue(InfoData);
                                            if (State != null)
                                            {
                                                //Connection to the X1 of the junction box
                                                double x1 = topologyObject.Pos_X + DimsDef.juntionOutputX1 - 5 - DimsDef.slaveWidth - 15;
                                                double x2 = topologyObject.Pos_X;
                                                double y1 = topologyObject.Pos_Y + DimsDef.slaveOutputFront + 10;
                                                double y2 = Pos_Y + DimsDef.slaveInput + 5;
                                                Line line = new Line() { X1 = x2, X2 = x1-5, Y1 = y1, Y2 = y1, StrokeThickness = 10 };
                                                Binding binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                                line.SetBinding(Line.StrokeProperty, binding);
                                                subGrid.Children.Add(line);

                                                line = new Line() { X1 = x1, X2 = x1, Y1 = y1, Y2 = y2 + 5, StrokeThickness = 10 };
                                                binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                                line.SetBinding(Line.StrokeProperty, binding);
                                                subGrid.Children.Add(line);

                                                line = new Line() { X1 = x1 - 5, X2 = x2, Y1 = y2, Y2 = y2, StrokeThickness = 10 };
                                                binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                                                line.SetBinding(Line.StrokeProperty, binding);
                                                subGrid.Children.Add(line);
                                            }
                                        }
                                        break;
                                    }
                                }

                            }
                        }

                    }

                    mainGrid.Children.Add(cell);
                    TopologyObject currentTopologyObject = new TopologyObject(obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y);
                    topologyObjects.Add(currentTopologyObject);

                    Grid.SetColumn(cell, column);
                    Grid.SetRow(cell, row);

                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    mainGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                    previousTopologyObject = currentTopologyObject;
                }

                foreach (var child in obj.GetChildren())
                {
                    // cell new

                    Render(child, ref row, ref column, ref Pos_X, ref Pos_Y, ref previousTopologyObject, mainGrid, subGrid, new UniformGrid());
                }
            }
            return mainGrid;
        }
    }

}
