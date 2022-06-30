using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Vortex.Connector;
using Vortex.Presentation.Wpf;
using TcoIo;
using System.Reflection;
using System.Collections.ObjectModel;
using TcOpen.Inxton.TcoIo.Wpf.Topology;

namespace TcOpen.Inxton.TcoIo.Wpf
{
    public class TopologyRenderer : UserControl
    {
        static int row = 0;
        static int column = 0;
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
            //this.Content = Render(this.DataContext as IVortexObject, ref row, ref column);
            this.Content = Render2(this.DataContext as IVortexObject, ref row, ref column, ref previousTopologyObject);
        }


        public object Render(IVortexObject obj, ref int row , ref int column , Grid mainGrid = null, UniformGrid cell = null, HardwareType previousType = HardwareType.Undefined)
        {

            if (obj != null)
            {
                mainGrid = mainGrid == null ? new Grid() : mainGrid;
                cell = cell == null ? new UniformGrid()  : cell;

                HardwareType currentType = new HardwareType();

                Type[] interfaces = obj.GetType().GetInterfaces();
                foreach (Type item in interfaces)
                {
                    if (item.Name.Contains("EtcMasterBase"))
                    {
                        currentType = HardwareType.EtcMasterBase;
                        break;
                    }
                    else if (item.Name.Contains("EtcSlaveBoxBase"))
                    {
                        currentType = HardwareType.EtcSlaveBoxBase;
                        break;
                    }
                    else if (item.Name.Contains("EtcSlaveTerminalBase"))
                    {
                        currentType = HardwareType.EtcSlaveTerminalBase;
                        break;
                    }
                    else if (item.Name.Contains("EtcSlaveEndTerminalBase"))
                    {
                        currentType = HardwareType.EtcSlaveEndTerminalBase;
                        break;
                    }
                }

                var presentation = LazyRenderer.Get.CreatePresentation(PresentationType, obj) as FrameworkElement;
                presentation.HorizontalAlignment = HorizontalAlignment.Stretch;
                presentation.VerticalAlignment = VerticalAlignment.Stretch;

                if (presentation != null && currentType != HardwareType.Undefined)
                {
                    cell.Children.Add(presentation);

                    mainGrid.Children.Add(cell);

                    if (currentType == HardwareType.EtcMasterBase /*&& (previousType == HardwareType.EtcSlaveBoxBase || previousType == HardwareType.EtcSlaveTerminalBase)*/)
                    {
                        column = 0;
                        row++;
                    }
                    else if (currentType == HardwareType.EtcSlaveBoxBase && previousType == HardwareType.EtcMasterBase)
                    {
                        column++;
                    }
                    else if (currentType == HardwareType.EtcSlaveBoxBase && previousType == HardwareType.EtcSlaveTerminalBase)
                    {
                        row++;
                    }
                    else if (currentType == HardwareType.EtcSlaveTerminalBase /*&& previousType == HardwareType.EtcSlaveTerminalBase*/)
                    {
                        column++;
                    }
                    else if (currentType == HardwareType.EtcSlaveEndTerminalBase)
                    {
                        column++;
                    }
                    Grid.SetColumn(cell, column);
                    Grid.SetRow(cell, row);
                    Grid.SetIsSharedSizeScope(mainGrid, true);
                    Grid.SetIsSharedSizeScope(cell, true);
                    cell.HorizontalAlignment = HorizontalAlignment.Stretch;
                    cell.VerticalAlignment = VerticalAlignment.Stretch;

                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    mainGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                    var physics = obj.GetType().GetProperty("Physics")?.GetValue(obj);
                }

                foreach (var child in obj.GetChildren())
                {
                    // cell new

                    Render(child, ref row, ref column, mainGrid, new UniformGrid(), currentType);
                }
            }
            return mainGrid;
        }

        public object Render2(IVortexObject obj, ref int row, ref int column, ref TopologyObject previousTopologyObject, Grid mainGrid = null, UniformGrid cell = null)
        {

            if (obj != null)
            {
                mainGrid = mainGrid == null ? new Grid() : mainGrid;
                cell = cell == null ? new UniformGrid() : cell;

                string currentPhysics = obj.GetType().GetProperty("AttributePhysics")?.GetValue(obj).ToString();
                string currentConnection = obj.GetType().GetProperty("AttributePreviousPort")?.GetValue(obj).ToString();

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
                    cell.Children.Add(presentation);

                    mainGrid.Children.Add(cell);

                    if (isMaster)
                    {
                        column = 0;
                        //Increment row only in case of new master device
                        if (!string.IsNullOrEmpty(previousTopologyObject.Name))
                        {
                            row++;
                        }
                    }
                    else if (isSlave)
                    {
                        //First box after master
                        if (currentPhysics != null && currentConnection != null && currentPhysics.StartsWith("Y") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith(previousTopologyObject.Physics))
                        {
                            //Add empty cell after master
                            column++;
                            UniformGrid emptyCell = new UniformGrid();
                            emptyCell.Width = DimsDef.emptyCellWidth;
                            emptyCell.Height = DimsDef.emptyCellHeight;

                            mainGrid.Children.Add(emptyCell);
                            Grid.SetColumn(emptyCell, column);
                            Grid.SetRow(emptyCell, row);
                        }
                        if (currentPhysics != null && currentConnection != null)
                        {
                            //Check if previously added device box or terminal is that one the currently added is connected to
                            if (currentConnection.StartsWith(previousTopologyObject.Name))
                            {
                                column++;
                            }
                            if (previousTopologyObject.Physics != null)
                            {
                                //First box after 2 port junction box (port D) X1
                                if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KYKY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("D"))
                                {
                                    row++;
                                }
                                //First box after 2 port junction box (port B) X2
                                if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KYKY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("B"))
                                {
                                    row++;
                                }
                                //First box after extension
                                if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KY") && currentConnection.StartsWith(previousTopologyObject.Name))
                                {
                                    //Add empty cell after extension box
                                    UniformGrid emptyCell = new UniformGrid();
                                    emptyCell.Width = DimsDef.emptyCellWidth;
                                    emptyCell.Height = DimsDef.emptyCellHeight;

                                    mainGrid.Children.Add(emptyCell);
                                    Grid.SetColumn(emptyCell, column);
                                    Grid.SetRow(emptyCell, row);
                                    column++;
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
                                            row = topologyObject.Row;
                                        }
                                        //First box after 2 port junction box (port B) X2
                                        if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("KYKY") && currentConnection.EndsWith("B"))
                                        {
                                            column = topologyObject.Column + 1;
                                            row++;
                                        }
                                        //First box after coupler of the YKY type (port C) X2
                                        if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("YKY") && currentConnection.EndsWith("C"))
                                        {
                                            column = topologyObject.Column;
                                            row++;
                                        }
                                        break;
                                    }
                                }

                            }
                        }

                    }


                    TopologyObject currentTopologyObject = new TopologyObject(obj.AttributeName, currentPhysics, currentConnection, row, column);
                    topologyObjects.Add(currentTopologyObject);

                    Grid.SetColumn(cell, column);
                    Grid.SetRow(cell, row);
                    Grid.SetIsSharedSizeScope(mainGrid, true);
                    Grid.SetIsSharedSizeScope(cell, true);
                    cell.HorizontalAlignment = HorizontalAlignment.Stretch;
                    cell.VerticalAlignment = VerticalAlignment.Stretch;

                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    mainGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                    previousTopologyObject = currentTopologyObject;
                }

                foreach (var child in obj.GetChildren())
                {
                    // cell new

                    Render2(child, ref row, ref column,ref previousTopologyObject, mainGrid, new UniformGrid());
                }
            }
            return mainGrid;
        }
    }

}
