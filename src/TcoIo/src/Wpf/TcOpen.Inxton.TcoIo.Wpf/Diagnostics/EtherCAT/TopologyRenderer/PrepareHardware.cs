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
        public void PrepareHardware(IVortexObject obj)
        {

            if (obj != null)
            {
                UniformGrid hardware = new UniformGrid();
                WiringObject wiring = new WiringObject();
                Path path = new Path() { StrokeThickness = strokeThicknessDef };

                string currentPhysics = obj.GetType().GetProperty("AttributePhysics")?.GetValue(obj).ToString();
                string currentConnection = obj.GetType().GetProperty("AttributePreviousPort")?.GetValue(obj).ToString();
                string currentBoxType = obj.GetType().GetProperty("AttributeBoxType")?.GetValue(obj).ToString();

                ///////////////////////////////////////
                _name = obj.AttributeName;
                _conection = currentConnection;
                _boxtype = currentBoxType;
                _physics = currentPhysics;
                ///////////////////////////////////////


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

                FrameworkElement presentation = LazyRenderer.Get.CreatePresentation(PresentationType, obj) as FrameworkElement;
                presentation.HorizontalAlignment = HorizontalAlignment.Stretch;
                presentation.VerticalAlignment = VerticalAlignment.Stretch;

                if (presentation != null && (isMaster || isSlave))
                {
                    if(_name.Contains("Term 294"))
                    {

                    }
                    hardware.Children.Add(presentation);
                    hardware.Name = ValidateFrameworkElement.Name(obj.AttributeName);
                    if (isMaster)
                    {
                        column = 0;
                        //Increment row only in case of new master device
                        if (!string.IsNullOrEmpty(previousTopologyObject.Name))
                        {
                            maxrow++;
                            row = maxrow;
                            MaxPos_Y = MaxPos_Y + DimsDef.masterHeight + 15;
                            Pos_Y = MaxPos_Y;
                        }
                        Pos_X = DimsDef.masterWidth + 10;
                    }
                    // slave is the first item in the view
                    else if (isSlave && previousTopologyObject.Name == null) // slave is first in the view
                    {
                        //Add empty cell before slave
                        column = 0;
                        row = 0;
                        Pos_X = DimsDef.slaveWidth + 10.0;
                        UniformGrid emptyCell = new UniformGrid();
                        emptyCell.Width = DimsDef.slaveWidth + 10;
                        emptyCell.Height = DimsDef.slaveHeight + 15;
                        TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                        topologyObjects.Add(emptyTopologyObject);
                        //Direct connection to the previous device that is not visible (Y20)
                        CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y20);
                        column++;
                        Pos_X = Pos_X + DimsDef.slaveWidth + 10.0;
                    }
                    else if (isSlave)
                    {
                        if (currentPhysics != null && currentConnection != null)
                        {
                            TopologyObject connectionPartner = FindMyConnectionPartner(currentConnection , previousTopologyObject);
                            //First box after master
                            if (currentPhysics.StartsWith("Y") && previousTopologyObject.Name != null && currentConnection.StartsWith(previousTopologyObject.Name) && previousTopologyObject.Physics != null && currentConnection.EndsWith(previousTopologyObject.Physics))
                            {
                                //No empty cell in case of PLC backplane
                                if (!(currentBoxType != null && currentBoxType.StartsWith("EK1200")))
                                {
                                    //Add empty cell after master
                                    column++;
                                    Pos_X = Pos_X + DimsDef.slaveWidth + 10.0;
                                    UniformGrid emptyCell = new UniformGrid();
                                    emptyCell.Width = DimsDef.slaveWidth + 10;
                                    emptyCell.Height = DimsDef.slaveHeight + 15;
                                    TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                    topologyObjects.Add(emptyTopologyObject);
                                    //Direct connection to master (Y2Y)
                                    CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                }
                                //Check if previously added device box or terminal is that one the currently added is connected to
                                if (previousTopologyObject.Name != null && currentConnection.StartsWith(previousTopologyObject.Name))
                                {
                                    column++;
                                    Pos_X = Pos_X + DimsDef.slaveWidth + 10;
                                }
                            }
                            else
                            {
                                if (previousTopologyObject.Name != null)
                                {
                                    //Check if previously added device box or terminal is that one the currently added is connected to
                                    if (currentConnection.StartsWith(previousTopologyObject.Name))
                                    {
                                        column++;
                                        Pos_X = Pos_X + DimsDef.slaveWidth + 10;
                                    }
                                    if (previousTopologyObject.Physics != null)
                                    {
                                        //First box after 2-port junction box (port D) X1
                                        if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KYKY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("D"))
                                        {
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                            ShiftDownIfNeccessary("D");
                                            //Connection to the X1 of the junction box (Y2KYKY_X1)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X1);
                                        }
                                        //First box after 2-port junction box (port B) X2
                                        else if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KYKY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("B"))
                                        {
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                            ShiftDownIfNeccessary("B");
                                            //Connection to the X1 of the junction box (Y2KYKY_X2)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X2);
                                        }
                                        //First box after extension
                                        else if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("KY") && currentConnection.StartsWith(previousTopologyObject.Name))
                                        {
                                            //Add empty cell after extension box
                                            UniformGrid emptyCell = new UniformGrid();
                                            emptyCell.Width = DimsDef.slaveWidth + 10;
                                            emptyCell.Height = DimsDef.slaveHeight + 15;

                                            TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                            topologyObjects.Add(emptyTopologyObject);

                                            column++;
                                            Pos_X = Pos_X + DimsDef.slaveWidth + 10;
                                            //Connection to the extension box (Y2Y)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                        }
                                        //Box after box
                                        else if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("YY") && currentConnection.StartsWith(previousTopologyObject.Name))
                                        {
                                            //Add empty cell after box
                                            UniformGrid emptyCell = new UniformGrid();
                                            emptyCell.Width = DimsDef.slaveWidth + 10;
                                            emptyCell.Height = DimsDef.slaveHeight + 15;
                                            TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                            topologyObjects.Add(emptyTopologyObject);
                                            column++;
                                            Pos_X = Pos_X + DimsDef.slaveWidth + 10;
                                            //Connection to the box (Y2Y)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                        }
                                        //Box after box
                                        else if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("YY_K") && currentConnection.StartsWith(previousTopologyObject.Name))
                                        {
                                            //Add empty cell after box
                                            UniformGrid emptyCell = new UniformGrid();
                                            emptyCell.Width = DimsDef.slaveWidth + 10;
                                            emptyCell.Height = DimsDef.slaveHeight + 15;
                                            TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                            topologyObjects.Add(emptyTopologyObject);
                                            column++;
                                            Pos_X = Pos_X + DimsDef.slaveWidth + 10;
                                            //Connection to the box (Y2Y)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                        }
                                        //First box after YYYY box (port D) X51
                                        else if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("YYYY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("D"))
                                        {
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                            ShiftDownIfNeccessary("D");
                                            //Connection to the X1 of the junction box (Y2KYKY_X1)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X1);
                                        }
                                        //First box after 2-port junction box (port B) X2
                                        else if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("YYYY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("B"))
                                        {
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                            ShiftDownIfNeccessary("B");
                                            //Connection to the X1 of the junction box (Y2KYKY_X2)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X2);
                                        }
                                        //Box after box
                                        else if (currentPhysics.StartsWith("Y") && previousTopologyObject.Physics.Equals("YYYY") && currentConnection.StartsWith(previousTopologyObject.Name) && currentConnection.EndsWith("C"))
                                        {
                                            //Add empty cell after box
                                            UniformGrid emptyCell = new UniformGrid();
                                            emptyCell.Width = DimsDef.slaveWidth + 10;
                                            emptyCell.Height = DimsDef.slaveHeight + 15;
                                            TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                            topologyObjects.Add(emptyTopologyObject);
                                            column++;
                                            Pos_X = Pos_X + DimsDef.slaveWidth + 10;
                                            //Connection to the box (Y2Y)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
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
                                                    Pos_Y = topologyObject.Pos_Y;
                                                }
                                                //First box after 2-port junction box (port B) X2
                                                else if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("KYKY") && currentConnection.EndsWith("B"))
                                                {
                                                    int parentIndex = topologyObjects.IndexOf(topologyObject);
                                                    int topologyObjectsCount = topologyObjects.Count;
                                                    int rowsOccupied = 0;
                                                    for (int i = parentIndex; i < topologyObjectsCount; i++)
                                                    {
                                                        rowsOccupied = Math.Max(rowsOccupied, topologyObjects[i].Row);
                                                    }
                                                    rowsOccupied = rowsOccupied - topologyObject.Row;
                                                    column = topologyObject.Column + 1;
                                                    Pos_X = topologyObject.Pos_X + DimsDef.slaveWidth + 10;
                                                    row = row + 1;
                                                    Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;
                                                    ShiftDownIfNeccessary("B", rowsOccupied);
                                                    //Connection to the X2 of the junction box (Y2KYKY_X2)
                                                    CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X2);
                                                }
                                                //First box after coupler of the YKY type (port C) X2
                                                else if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("YKY") && currentConnection.EndsWith("C"))
                                                {
                                                    column = topologyObject.Column;
                                                    Pos_X = topologyObject.Pos_X;
                                                    row++;
                                                    Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;
                                                    ShiftDownIfNeccessary("C");
                                                    //Connection to the X2 of the coupler box (Y2YKY)
                                                    CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2YKY);
                                                }

                                                //First box after YYYY box (port D) X51
                                                else if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("YYYY") && currentConnection.StartsWith(topologyObject.Name) && currentConnection.EndsWith("D"))
                                                {
                                                    column = topologyObject.Column + 1;
                                                    Pos_X = topologyObject.Pos_X + DimsDef.slaveWidth + 10;
                                                    row++;
                                                    Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;


                                                    ShiftDownIfNeccessary("D");
                                                    //Connection to the X1 of the junction box (Y2KYKY_X1)
                                                    CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X1);
                                                }
                                                //First box after YYYY box (port B) X52
                                                else if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("YYYY") && currentConnection.StartsWith(topologyObject.Name) && currentConnection.EndsWith("B"))
                                                {
                                                    column = topologyObject.Column + 1;
                                                    Pos_X = topologyObject.Pos_X + DimsDef.slaveWidth + 10;
                                                    row++;
                                                    Pos_Y = Pos_Y + DimsDef.slaveHeight + 15;

                                                    ShiftDownIfNeccessary("B");
                                                    //Connection to the X1 of the junction box (Y2KYKY_X2)
                                                    CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X2);
                                                }
                                                //Box after box
                                                else if (currentPhysics.StartsWith("Y") && topologyObject.Physics.Equals("YYYY") && currentConnection.StartsWith(topologyObject.Name) && currentConnection.EndsWith("C"))
                                                {
                                                    column = topologyObject.Column + 1;
                                                    Pos_X = topologyObject.Pos_X + DimsDef.slaveWidth + 10;
                                                    row = topologyObject.Row;
                                                    Pos_Y = topologyObject.Pos_Y;

                                                    //Add empty cell after box
                                                    UniformGrid emptyCell = new UniformGrid();
                                                    emptyCell.Width = DimsDef.slaveWidth + 10;
                                                    emptyCell.Height = DimsDef.slaveHeight + 15;
                                                    TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                                    topologyObjects.Add(emptyTopologyObject);
                                                    column++;
                                                    Pos_X = Pos_X + DimsDef.slaveWidth + 10;
                                                    //Connection to the box (Y2Y)
                                                    CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                                }

                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    TopologyObject currentTopologyObject = new TopologyObject(obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, hardware, wiring);
                    topologyObjects.Add(currentTopologyObject);

                    maxrow = Math.Max(maxrow, row);
                    maxcolumn = Math.Max(maxcolumn, column);
                    MaxPos_X = Math.Max(MaxPos_X, Pos_X);
                    MaxPos_Y = Math.Max(MaxPos_Y, Pos_Y);
                    previousTopologyObject = currentTopologyObject;
                }
                foreach (var child in obj.GetChildren())
                {
                    PrepareHardware(child);
                }
            }
        }

    }
}
