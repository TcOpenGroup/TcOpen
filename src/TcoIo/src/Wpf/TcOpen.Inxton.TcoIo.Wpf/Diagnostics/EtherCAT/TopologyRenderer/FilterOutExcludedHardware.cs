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
        public void FilterOutExcludedHardware()
        {
            if (
                ExcludeSlavesConnectedToJunctionBox
                && !GroupedView
                && (
                    !String.IsNullOrEmpty(FirstTopologyElementName)
                    || !String.IsNullOrEmpty(LastTopologyElementName)
                )
            )
            {
                if (LastElementRow != -1 && LastElementColumn != -1)
                {
                    ObservableCollection<TopologyObject> filteredTopologyObjects =
                        new ObservableCollection<TopologyObject>();

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
