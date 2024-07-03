using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TcoIo.Converters;
using TcoIo.Topology;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public TopologyObject FindMyConnectionPartner(
            string currentConnection,
            TopologyObject previousTopologyObject
        )
        {
            TopologyObject topology = new TopologyObject();
            if (currentConnection.StartsWith(previousTopologyObject.Name))
            {
                topology = previousTopologyObject;
            }
            else
            {
                //Find the topology object the current one is connected to and use its row and column for proper placement
                foreach (TopologyObject topologyObject in topologyObjects)
                {
                    //Find the box the current one is connected to
                    if (currentConnection.StartsWith(topologyObject.Name))
                    {
                        topology = topologyObject;
                        break;
                    }
                }
            }

            return topology;
        }
    }
}
