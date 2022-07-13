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
        public int CountOccupiedRows(TopologyObject connectionPartner)
        {
            int rows = 0;
            int parentIndex = topologyObjects.IndexOf(connectionPartner);
            int topologyObjectsCount = topologyObjects.Count;
            int rowsOccupied = 0;
            for (int i = parentIndex; i < topologyObjectsCount; i++)
            {
                rowsOccupied = Math.Max(rowsOccupied, topologyObjects[i].Row);
            }
            rows = rowsOccupied = rowsOccupied - connectionPartner.Row;
            return rows;
        }
    }
}
