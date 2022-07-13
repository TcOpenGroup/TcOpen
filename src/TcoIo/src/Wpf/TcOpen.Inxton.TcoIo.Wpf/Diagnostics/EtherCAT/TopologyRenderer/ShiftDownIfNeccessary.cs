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
        private void ShiftDownIfNeccessary(string port, TopologyObject connectionPartner, int offset)
        {
            if (maxrow >= row)
            {
                int parentIndex = topologyObjects.IndexOf(connectionPartner);
                int topologyObjectsCount = topologyObjects.Count;
                int shiftedRow = 0;
                for (int i = parentIndex; i < topologyObjectsCount; i++)
                {
                    shiftedRow = Math.Max(shiftedRow, topologyObjects[i].Row);
                }
                shiftedRow = shiftedRow + offset;

                int minColumnFromMyRowDown = int.MaxValue;
                int maxColumnFromMyRowDown = 0;
                int maxColumnInMyRow = 0;
                int minColumnInMyRow = int.MaxValue;
                foreach (TopologyObject topologyObj in topologyObjects)
                {
                    if (topologyObj.Row > row)
                    {
                        maxColumnFromMyRowDown = Math.Max(maxColumnFromMyRowDown, topologyObj.Column);
                        minColumnFromMyRowDown = Math.Min(minColumnFromMyRowDown, topologyObj.Column);
                    }
                    if (topologyObj.Row == row)
                    {
                        maxColumnInMyRow = Math.Max(maxColumnInMyRow, topologyObj.Column);
                        minColumnInMyRow = Math.Min(minColumnInMyRow, topologyObj.Column);
                    }
                }

                if (!string.IsNullOrEmpty(port) && port.Equals("D"))
                {
                    //If there are already any objects lower then "me" or at "my" row, they have to be shifted down
                    foreach (TopologyObject topologyObj in topologyObjects)
                    {
                        if (topologyObj.Row >= row)
                        {
                            topologyObj.Row++;
                            topologyObj.Pos_Y = topologyObj.Pos_Y + DimsDef.slaveHeightWithBorders;
                            maxrow = Math.Max(maxrow, topologyObj.Row);
                            MaxPos_Y = Math.Max(MaxPos_Y, topologyObj.Pos_Y);
                        }
                    }
                }
                else
                {
                    //If there are already any objects right to "me" or at "my" column and lower then "me" or at "my" row at the same time,
                    //but no objects left to "me" and lower then "me or so that's "me" or at "my" row at the same time,
                    //that's "me" who has to be placed into the new added row after all the others.
                    if (column <= maxColumnInMyRow && column <= minColumnInMyRow && column <= maxColumnFromMyRowDown && column <= minColumnFromMyRowDown)
                    {
                        row = maxrow + 1;
                        Pos_Y = MaxPos_Y + DimsDef.slaveHeightWithBorders;
                        maxrow = Math.Max(maxrow, row);
                        MaxPos_Y = Math.Max(MaxPos_Y, Pos_Y);
                    }
                    else
                    {
                        row = shiftedRow;
                        Pos_Y = shiftedRow * (DimsDef.slaveHeightWithBorders);
                        maxrow = Math.Max(maxrow, row);
                        MaxPos_Y = Math.Max(MaxPos_Y, Pos_Y);
                        foreach (TopologyObject topologyObj in topologyObjects)
                        {
                            if (topologyObj.Row >= row)
                            {
                                topologyObj.Row++;
                                topologyObj.Pos_Y = topologyObj.Pos_Y + DimsDef.slaveHeightWithBorders;
                                maxrow = Math.Max(maxrow, topologyObj.Row);
                                MaxPos_Y = Math.Max(MaxPos_Y, topologyObj.Pos_Y);
                            }
                        }
                    }
                }
            }
        }

        private void ShiftDownIfNeccessary(string port, TopologyObject connectionPartner)
        {
            //ShiftDownIfNeccessary(port, 1);
            ShiftDownIfNeccessary(port, connectionPartner, 0);
        }


    }
}
