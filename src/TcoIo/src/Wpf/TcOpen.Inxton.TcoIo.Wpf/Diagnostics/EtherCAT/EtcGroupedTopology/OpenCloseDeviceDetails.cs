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
        private void OpenDeviceDetails(object sender, RoutedEventArgs e)
        {
            EtcUngroupedViewData ungroupedViewData = (this.DataContext as EtcGroupedDataContext).UngroupedViewData as EtcUngroupedViewData;

            if (DetailWindow == null )
            {
                DetailWindow = new Window();
                DetailWindow.Closing += DetailWindow_Closing;
                DetailGrid = new Grid();
            }
            if (ungroupedViewData.UserView == null )
            {
                if(DetailTopologyRenderer == null)
                {
                    DetailTopologyRenderer = new TopologyRenderer();
                }
                if (ungroupedViewData != null && ungroupedViewData.DataContext != null)
                {
                    DetailTopologyRenderer.GroupedView = false;
                    DetailTopologyRenderer.FirstTopologyElementName = ungroupedViewData.FirstTopologyElementName;
                    DetailTopologyRenderer.LastTopologyElementName = ungroupedViewData.LastTopologyElementName;
                    DetailTopologyRenderer.ExcludeSlavesConnectedToJunctionBox = ungroupedViewData.ExcludeSlavesConnectedToJunctionBox;
                    DetailTopologyRenderer.DataContext = ungroupedViewData.DataContext;
                    if (DetailGrid.Parent == null)
                    {
                        DetailGrid.Children.Add(DetailTopologyRenderer);
                    }
                    DetailWindow.Content = DetailGrid;
                    DetailWindow.SizeToContent = SizeToContent.WidthAndHeight;
                    DetailWindow.Show();
                }
            }
            else
            {
                if (DetailUserView == null)
                {
                    DetailUserView = Activator.CreateInstance(ungroupedViewData.UserView) as FrameworkElement;
                }

                if (ungroupedViewData != null && ungroupedViewData.DataContext != null)
                {
                    DetailUserView.DataContext = ungroupedViewData.DataContext;
                    if (DetailGrid.Parent == null)
                    {
                        DetailGrid.Children.Add(DetailUserView);
                    }
                    DetailWindow.Content = DetailGrid;
                    DetailWindow.SizeToContent = SizeToContent.WidthAndHeight;
                    DetailWindow.Show();
                }
            }






        }

        private void DetailWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            (sender as Window)?.Hide();
        }
    }
}
