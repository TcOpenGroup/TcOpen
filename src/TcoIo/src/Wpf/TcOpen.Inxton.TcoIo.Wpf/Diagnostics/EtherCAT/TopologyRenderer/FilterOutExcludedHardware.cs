
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
        public void FilterOutExcludedHardware()
        {
            if (ExcludeSlavesConnectedToJunctionBox && !GroupedView && (!String.IsNullOrEmpty(FirstTopologyElementName) || !String.IsNullOrEmpty(LastTopologyElementName)))
            {
                if (LastElementRow != -1 && LastElementColumn != -1)
                {
                    ObservableCollection<TopologyObject> filteredTopologyObjects = new ObservableCollection<TopologyObject>();

                    foreach (TopologyObject topologyObject in topologyObjects)
                    {
                        if (topologyObject.Row <= LastElementRow)
                        {
                            filteredTopologyObjects.Add(topologyObject);
                        }
                    }
                    topologyObjects = filteredTopologyObjects;
                }
            }
        }
    }
}
