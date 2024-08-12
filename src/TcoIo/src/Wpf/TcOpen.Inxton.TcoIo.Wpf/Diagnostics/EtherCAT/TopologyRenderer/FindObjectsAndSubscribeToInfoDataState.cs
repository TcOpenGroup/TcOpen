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
        public void FindObjectsAndSubscribeToInfoDataState(IVortexObject dt)
        {
            if (GroupedView == true)
            {
                FirstTopologyElementReached = false;
                LastTopologyElementReached = false;
                IVortexObject lastEvaluatedObject = null;
                if (String.IsNullOrEmpty(FirstTopologyElementName))
                {
                    FirstTopologyElementName = dt.AttributeName.ToString();
                }
                firstTopologyElement = FindTopologyElement(
                    FirstTopologyElementName,
                    dt,
                    ref lastEvaluatedObject
                );
                lastTopologyElement = FindTopologyElement(
                    LastTopologyElementName,
                    dt,
                    ref lastEvaluatedObject
                );
                if (String.IsNullOrEmpty(LastTopologyElementName) || lastTopologyElement == null)
                {
                    lastTopologyElement = lastEvaluatedObject;
                    LastTopologyElementName = lastTopologyElement.AttributeName.ToString();
                }

                SubscribeToInfoDataState(
                    dt,
                    firstTopologyElement,
                    lastTopologyElement,
                    ExcludeSlavesConnectedToJunctionBox
                );
                FirstTopologyElementReached = false;
                LastTopologyElementReached = false;
            }
        }
    }
}
