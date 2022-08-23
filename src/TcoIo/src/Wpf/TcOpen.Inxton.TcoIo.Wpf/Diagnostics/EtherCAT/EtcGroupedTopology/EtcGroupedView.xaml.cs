using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TcoIo.Converters;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoIo
{
    public partial class EtcGroupedView : UserControl
    {
        public EtcGroupedView()
        {
            InitializeComponent();
        }
        private void OpenDeviceDetails(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            Grid grid = new Grid();
            TopologyRenderer tr = new TopologyRenderer();
            EtcUngroupedViewData ungroupedViewData = (this.DataContext as EtcGroupedDataContext).UngroupedViewData as EtcUngroupedViewData;
            if(ungroupedViewData != null && ungroupedViewData.DataContext != null)
            {
                tr.GroupedView = false;
                tr.FirstTopologyElementName= ungroupedViewData.FirstTopologyElementName;
                tr.LastTopologyElementName = ungroupedViewData.LastTopologyElementName;
                tr.ExcludeSlavesConnectedToJunctionBox = ungroupedViewData.ExcludeSlavesConnectedToJunctionBox;
                tr.DataContext = ungroupedViewData.DataContext;
                grid.Children.Add(tr);
                window.Content = grid;
                window.SizeToContent = SizeToContent.WidthAndHeight;
                window.Show();
            }
        }
    }
}
