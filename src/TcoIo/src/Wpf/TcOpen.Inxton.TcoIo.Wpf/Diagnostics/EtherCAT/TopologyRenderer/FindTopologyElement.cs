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
using System.Collections.Generic;
using System.Linq;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public IVortexObject FindTopologyElement(string lastTopologyElement, IVortexObject obj, ref IVortexObject lastEvaluatedObject)
        {
            IVortexObject vortexObject = null;
            if (obj != null && ValidateFrameworkElement.Name(obj.AttributeName).Equals(ValidateFrameworkElement.Name(lastTopologyElement)))
            {
                vortexObject = obj;
                lastEvaluatedObject = obj;
            }
            else
            {
                IEnumerable<IVortexObject> vortexObjects = obj.GetChildren().Where(i => i.GetType().GetProperty("AttributePhysics") != null );
                foreach (IVortexObject child in vortexObjects)
                {
                    IVortexObject lastChildObject = child;
                    vortexObject = FindTopologyElement(lastTopologyElement,child, ref lastChildObject);
                    lastEvaluatedObject = lastChildObject;
                    if (vortexObject != null)
                    {
                        break;
                    } 
                }
            }
            return vortexObject;
        }
    }
}
