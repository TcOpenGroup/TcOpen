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
        public object PrepareWiring(Grid wiringGrid = null)
        {
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                wiringGrid = wiringGrid == null ? new Grid() : wiringGrid;
                PathFigure pathFigure = new PathFigure();
                LineSegment horizontal1 = new LineSegment();
                LineSegment horizontal2 = new LineSegment();
                LineSegment horizontal3 = new LineSegment();
                LineSegment vertical1 = new LineSegment();
                LineSegment vertical2 = new LineSegment();
                PathSegmentCollection wiring = new PathSegmentCollection();
                double x1, x2, x3, x4, x5, y1, y2, y3, y4, y5;

                foreach (TopologyObject topologyObject in topologyObjects)
                {
                    TopologyObject connectionPartner = new TopologyObject();
                    switch (topologyObject.Wiring.WiringType)
                    {
                        ////////////////////K2K//////////////////////////
                        case WiringObject.ConectionType.K2K:
                            break;

                        ////////////////////Y20//////////////////////////
                        case WiringObject.ConectionType.Y20:
                            x1 = topologyObject.Pos_X - DimsDef.slaveWidthWithBorders;
                            y1 = topologyObject.Pos_Y + DimsDef.slaveInput;
                            x2 = 0;
                            pathFigure = new PathFigure() { StartPoint = new Point(x1, y1) };
                            horizontal1 = new LineSegment() { Point = new Point(x2, y1) };

                            wiring = new PathSegmentCollection();
                            wiring.Add(horizontal1);

                            pathFigure.Segments = wiring;

                            PathFigureCollection pathFigureCollection = new PathFigureCollection();
                            pathFigureCollection.Add(pathFigure);

                            PathGeometry pathGeometry = new PathGeometry();
                            pathGeometry.Figures = pathFigureCollection;

                            topologyObject.Wiring.Path.Data = pathGeometry;

                            wiringGrid.Children.Add(topologyObject.Wiring.Path);
                            break;

                        ////////////////////Y2Y//////////////////////////
                        case WiringObject.ConectionType.Y2Y:
                            foreach (TopologyObject connectionPartnerPossible in topologyObjects)
                            {
                                if (topologyObject.Connection.StartsWith(connectionPartnerPossible.Name))
                                {
                                    connectionPartner = connectionPartnerPossible;
                                    break;
                                }
                            }
                            x1 = topologyObject.Pos_X - DimsDef.slaveWidthWithBorders;
                            y1 = topologyObject.Pos_Y + DimsDef.slaveInput;
                            x2 = connectionPartner.Pos_X;
                            pathFigure = new PathFigure() { StartPoint = new Point(x1, y1) };
                            horizontal1 = new LineSegment() { Point = new Point(x2, y1) };

                            wiring = new PathSegmentCollection();
                            wiring.Add(horizontal1);

                            pathFigure.Segments = wiring;

                            pathFigureCollection = new PathFigureCollection();
                            pathFigureCollection.Add(pathFigure);

                            pathGeometry = new PathGeometry();
                            pathGeometry.Figures = pathFigureCollection;


                            topologyObject.Wiring.Path.Data = pathGeometry;
                            wiringGrid.Children.Add(topologyObject.Wiring.Path);
                            break;

                        case WiringObject.ConectionType.Y2YKY:
                            foreach (TopologyObject connectionPartnerPossible in topologyObjects)
                            {
                                if (topologyObject.Connection.StartsWith(connectionPartnerPossible.Name))
                                {
                                    connectionPartner = connectionPartnerPossible;
                                    break;
                                }
                            }
                            x1 = topologyObject.Pos_X - DimsDef.slaveWidthWithBorders;
                            y1 = topologyObject.Pos_Y + DimsDef.slaveInput;
                            x2 = topologyObject.Pos_X + DimsDef.juntionOutputX1 - DimsDef.slaveWidthWithBorders;
                            y2 = connectionPartner.Pos_Y + DimsDef.slaveOutputFront + 10;

                            pathFigure = new PathFigure() { StartPoint = new Point(x1, y1) };
                            horizontal1 = new LineSegment() { Point = new Point(x2, y1) };
                            vertical1 = new LineSegment() { Point = new Point(x2, y2) };
                            horizontal2 = new LineSegment() { Point = new Point(x1, y2) };

                            wiring = new PathSegmentCollection();
                            wiring.Add(horizontal1);
                            wiring.Add(vertical1);
                            wiring.Add(horizontal2);

                            pathFigure.Segments = wiring;

                            pathFigureCollection = new PathFigureCollection();
                            pathFigureCollection.Add(pathFigure);

                            pathGeometry = new PathGeometry();
                            pathGeometry.Figures = pathFigureCollection;


                            topologyObject.Wiring.Path.Data = pathGeometry;
                            wiringGrid.Children.Add(topologyObject.Wiring.Path);
                            break;

                        ////////////////////Y2KYKY_X1//////////////////////////
                        case WiringObject.ConectionType.Y2KYKY_X1:
                            foreach (TopologyObject connectionPartnerPossible in topologyObjects)
                            {
                                if (topologyObject.Connection.StartsWith(connectionPartnerPossible.Name))
                                {
                                    connectionPartner = connectionPartnerPossible;
                                    break;
                                }
                            }
                            pathFigure = new PathFigure() { StartPoint = new Point(topologyObject.Pos_X - DimsDef.slaveWidthWithBorders, topologyObject.Pos_Y + DimsDef.slaveInput) };
                            horizontal1 = new LineSegment() { Point = new Point(connectionPartner.Pos_X + DimsDef.juntionOutputX1, topologyObject.Pos_Y + DimsDef.slaveInput) };
                            vertical1 = new LineSegment() { Point = new Point(connectionPartner.Pos_X + DimsDef.juntionOutputX1, connectionPartner.Pos_Y + DimsDef.slaveHeightWithBorders) };

                            wiring = new PathSegmentCollection();
                            wiring.Add(horizontal1);
                            wiring.Add(vertical1);

                            pathFigure.Segments = wiring;

                            pathFigureCollection = new PathFigureCollection();
                            pathFigureCollection.Add(pathFigure);

                            pathGeometry = new PathGeometry();
                            pathGeometry.Figures = pathFigureCollection;


                            topologyObject.Wiring.Path.Data = pathGeometry;
                            wiringGrid.Children.Add(topologyObject.Wiring.Path);
                            break;

                        ////////////////////Y2KYKY_X2//////////////////////////
                        case WiringObject.ConectionType.Y2KYKY_X2:
                            foreach (TopologyObject connectionPartnerPossible in topologyObjects)
                            {
                                if (topologyObject.Connection.StartsWith(connectionPartnerPossible.Name))
                                {
                                    connectionPartner = connectionPartnerPossible;
                                    break;
                                }
                            }
                            pathFigure = new PathFigure() { StartPoint = new Point(topologyObject.Pos_X - DimsDef.slaveWidthWithBorders, topologyObject.Pos_Y + DimsDef.slaveInput) };
                            horizontal1 = new LineSegment() { Point = new Point(connectionPartner.Pos_X + DimsDef.juntionOutputX2, topologyObject.Pos_Y + DimsDef.slaveInput) };
                            vertical1 = new LineSegment() { Point = new Point(connectionPartner.Pos_X + DimsDef.juntionOutputX2, connectionPartner.Pos_Y + DimsDef.slaveHeightWithBorders) };

                            wiring = new PathSegmentCollection();
                            wiring.Add(horizontal1);
                            wiring.Add(vertical1);

                            pathFigure.Segments = wiring;

                            pathFigureCollection = new PathFigureCollection();
                            pathFigureCollection.Add(pathFigure);

                            pathGeometry = new PathGeometry();
                            pathGeometry.Figures = pathFigureCollection;


                            topologyObject.Wiring.Path.Data = pathGeometry;
                            wiringGrid.Children.Add(topologyObject.Wiring.Path);
                            break;

                        default:
                            break;
                    }
                }
            });
            return wiringGrid;
        }

        private void CreateWiring(IVortexObject obj, ref Path path, ref WiringObject wiring, WiringObject.ConectionType conection)
        {
            var InfoData = obj.GetType().GetProperty("InfoData")?.GetValue(obj);
            if (InfoData != null)
            {
                var State = InfoData.GetType().GetProperty("State")?.GetValue(InfoData);
                if (State != null)
                {
                    Binding binding = new Binding { Source = State, Path = new PropertyPath("Cyclic"), Converter = new InfoDataStateToWireStroke(), Mode = BindingMode.OneWay };
                    path.SetBinding(Line.StrokeProperty, binding);
                    wiring.Path = path;
                    wiring.WiringType = conection;
                }
            }
        }
    }
}
