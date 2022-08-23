
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
                firstTopologyElement = FindTopologyElement(FirstTopologyElementName, dt, ref lastEvaluatedObject);
                lastTopologyElement = FindTopologyElement(LastTopologyElementName, dt, ref lastEvaluatedObject);
                if (String.IsNullOrEmpty(LastTopologyElementName) || lastTopologyElement == null)
                {
                    lastTopologyElement = lastEvaluatedObject;
                    LastTopologyElementName = lastTopologyElement.AttributeName.ToString();
                }
                
                SubscribeToInfoDataState(dt, firstTopologyElement, lastTopologyElement, ExcludeSlavesConnectedToJunctionBox);
                FirstTopologyElementReached = false;
                LastTopologyElementReached = false;
            }
        }
    }
}
