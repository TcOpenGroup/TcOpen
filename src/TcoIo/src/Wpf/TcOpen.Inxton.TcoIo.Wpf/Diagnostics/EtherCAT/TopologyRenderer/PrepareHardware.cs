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
                            MaxPos_Y = MaxPos_Y + DimsDef.masterHeightWithBorders;
                            Pos_Y = MaxPos_Y;
                        }
                        Pos_X = DimsDef.masterWidthWithBorders;
                    }
                    // slave is the first item in the view
                    else if (isSlave && previousTopologyObject.Name == null) // slave is first in the view
                    {
                        //Add empty cell before slave
                        column = 0;
                        row = 0;
                        Pos_X = DimsDef.slaveWidthWithBorders;
                        UniformGrid emptyCell = new UniformGrid();
                        emptyCell.Width = DimsDef.slaveWidthWithBorders;
                        emptyCell.Height = DimsDef.slaveHeightWithBorders;
                        TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                        topologyObjects.Add(emptyTopologyObject);
                        //Direct connection to the previous device that is not visible (Y20)
                        CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y20);
                        column++;
                        Pos_X = Pos_X + DimsDef.slaveWidthWithBorders;
                    }
                    else if (isSlave)
                    {
                        if (currentPhysics != null && currentConnection != null)
                        {
                            TopologyObject connectionPartner = FindMyConnectionPartner(currentConnection , previousTopologyObject);
                            //First box after master
                            if (currentPhysics.StartsWith("Y") && connectionPartner.Name != null && currentConnection.StartsWith(connectionPartner.Name) && connectionPartner.Physics != null && currentConnection.EndsWith(connectionPartner.Physics))
                            {
                                //No empty cell in case of PLC backplane
                                if (!(currentBoxType != null && currentBoxType.StartsWith("EK1200")))
                                {
                                    //Add empty cell after master
                                    column++;
                                    Pos_X = Pos_X + DimsDef.slaveWidthWithBorders;
                                    UniformGrid emptyCell = new UniformGrid();
                                    emptyCell.Width = DimsDef.slaveWidthWithBorders;
                                    emptyCell.Height = DimsDef.slaveHeightWithBorders;
                                    TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                    topologyObjects.Add(emptyTopologyObject);
                                    //Direct connection to master (Y2Y)
                                    CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                }
                                column++;
                                Pos_X = Pos_X + DimsDef.slaveWidthWithBorders;
                            }
                            else
                            {
                                if (connectionPartner.Name != null)
                                {
                                    column = connectionPartner.Column + 1;
                                    Pos_X = connectionPartner.Pos_X + DimsDef.slaveWidthWithBorders;
                                    if (connectionPartner.Physics != null)
                                    {
                                        //First box after 2-port junction box (port D) X1
                                        if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("KYKY") && currentConnection.EndsWith("D"))
                                        {
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeightWithBorders;

                                            row = connectionPartner.Row + 1;
                                            Pos_Y = connectionPartner.Pos_Y + DimsDef.slaveHeightWithBorders;

                                            ShiftDownIfNeccessary("D", connectionPartner, 1);
                                            //Connection to the X1 of the junction box (Y2KYKY_X1)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X1);
                                        }
                                        //First box after 2-port junction box (port B) X2
                                        else if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("KYKY") && currentConnection.EndsWith("B"))
                                        {
                                            column = connectionPartner.Column + 1;
                                            Pos_X = connectionPartner.Pos_X + DimsDef.slaveWidthWithBorders;
                                            //row = row + 1;
                                            //Pos_Y = Pos_Y + DimsDef.slaveHeightWithBorders;
                                            row = connectionPartner.Row + 1;
                                            Pos_Y = connectionPartner.Pos_Y + DimsDef.slaveHeightWithBorders;
                                            ShiftDownIfNeccessary("B", connectionPartner,0);
                                            //Connection to the X2 of the junction box (Y2KYKY_X2)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X2);
                                        }
                                        //First box after extension
                                        else if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("KY"))
                                        {
                                            //Add empty cell after extension box
                                            UniformGrid emptyCell = new UniformGrid();
                                            emptyCell.Width = DimsDef.slaveWidthWithBorders;
                                            emptyCell.Height = DimsDef.slaveHeightWithBorders;

                                            TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                            topologyObjects.Add(emptyTopologyObject);

                                            column++;
                                            Pos_X = Pos_X + DimsDef.slaveWidthWithBorders;
                                            //Connection to the extension box (Y2Y)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                        }
                                        //Box after box
                                        else if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("YY"))
                                        {
                                            //Add empty cell after box
                                            UniformGrid emptyCell = new UniformGrid();
                                            emptyCell.Width = DimsDef.slaveWidthWithBorders;
                                            emptyCell.Height = DimsDef.slaveHeightWithBorders;
                                            TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                            topologyObjects.Add(emptyTopologyObject);
                                            column++;
                                            Pos_X = Pos_X + DimsDef.slaveWidthWithBorders;
                                            //Connection to the box (Y2Y)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                        }
                                        //Box after box
                                        else if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("YY_K"))
                                        {
                                            //Add empty cell after box
                                            UniformGrid emptyCell = new UniformGrid();
                                            emptyCell.Width = DimsDef.slaveWidthWithBorders;
                                            emptyCell.Height = DimsDef.slaveHeightWithBorders;
                                            TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                            topologyObjects.Add(emptyTopologyObject);
                                            column++;
                                            Pos_X = Pos_X + DimsDef.slaveWidthWithBorders;
                                            //Connection to the box (Y2Y)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                        }
                                        //First box after YYYY box (port D) X51
                                        else if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("YYYY") && currentConnection.EndsWith("D"))
                                        {
                                            //row++;
                                            //Pos_Y = Pos_Y + DimsDef.slaveHeightWithBorders;

                                            column = connectionPartner.Column + 1;
                                            Pos_X = connectionPartner.Pos_X + DimsDef.slaveWidthWithBorders;
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeightWithBorders;

                                            ShiftDownIfNeccessary("D", connectionPartner, 1);
                                            //Connection to the X1 of the junction box (Y2KYKY_X1)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X1);
                                        }
                                        //First box after 2-port junction box (port B) X2
                                        else if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("YYYY") && currentConnection.EndsWith("B"))
                                        {
                                            //row++;
                                            //Pos_Y = Pos_Y + DimsDef.slaveHeightWithBorders;

                                            column = connectionPartner.Column + 1;
                                            Pos_X = connectionPartner.Pos_X + DimsDef.slaveWidthWithBorders;
                                            row++;
                                            Pos_Y = Pos_Y + DimsDef.slaveHeightWithBorders;

                                            ShiftDownIfNeccessary("B", connectionPartner, 1);
                                            //Connection to the X1 of the junction box (Y2KYKY_X2)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2KYKY_X2);
                                        }
                                        //Box after box
                                        else if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("YYYY") && currentConnection.EndsWith("C"))
                                        {
                                            column = connectionPartner.Column + 1;
                                            Pos_X = connectionPartner.Pos_X + DimsDef.slaveWidthWithBorders;
                                            row = connectionPartner.Row;
                                            Pos_Y = connectionPartner.Pos_Y;

                                            //Add empty cell after box
                                            UniformGrid emptyCell = new UniformGrid();
                                            emptyCell.Width = DimsDef.slaveWidthWithBorders;
                                            emptyCell.Height = DimsDef.slaveHeightWithBorders;
                                            TopologyObject emptyTopologyObject = new TopologyObject("EmptyCellBefore" + obj.AttributeName, currentPhysics, currentConnection, row, column, Pos_X, Pos_Y, emptyCell);
                                            topologyObjects.Add(emptyTopologyObject);
                                            column++;
                                            Pos_X = Pos_X + DimsDef.slaveWidthWithBorders;
                                            //Connection to the box (Y2Y)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2Y);
                                        }
                                        //First terminal after junction box (port C) K bus
                                        else if (currentPhysics.StartsWith("K") && connectionPartner.Physics.Equals("KYKY") && currentConnection.EndsWith("C"))
                                        {
                                            column = connectionPartner.Column + 1;
                                            Pos_X = connectionPartner.Pos_X + DimsDef.slaveWidthWithBorders;
                                            row = connectionPartner.Row;
                                            Pos_Y = connectionPartner.Pos_Y;
                                        }
                                        //First box after coupler of the YKY type (port C) X2
                                        else if (currentPhysics.StartsWith("Y") && connectionPartner.Physics.Equals("YKY") && currentConnection.EndsWith("C"))
                                        {
                                            column = connectionPartner.Column;
                                            Pos_X = connectionPartner.Pos_X;
                                            //row++;
                                            //Pos_Y = Pos_Y + DimsDef.slaveHeightWithBorders;
                                            row = connectionPartner.Row + 1;
                                            Pos_Y = connectionPartner.Pos_Y + DimsDef.slaveHeightWithBorders;
                                            //ShiftDownIfNeccessary("C", CountOccupiedRows(connectionPartner) + 1);
                                            ShiftDownIfNeccessary("C", connectionPartner, 1);
                                            //Connection to the X2 of the coupler box (Y2YKY)
                                            CreateWiring(obj, ref path, ref wiring, WiringObject.ConectionType.Y2YKY);
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
