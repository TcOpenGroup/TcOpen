using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public IVortexObject FindTopologyElement(
            string lastTopologyElement,
            IVortexObject obj,
            ref IVortexObject lastEvaluatedObject
        )
        {
            IVortexObject vortexObject = null;
            if (
                obj != null
                && ValidateFrameworkElement
                    .Name(obj.AttributeName)
                    .Equals(ValidateFrameworkElement.Name(lastTopologyElement))
            )
            {
                vortexObject = obj;
                lastEvaluatedObject = obj;
            }
            else
            {
                IEnumerable<IVortexObject> vortexObjects = obj.GetChildren()
                    .Where(i => i.GetType().GetProperty("AttributePhysics") != null);
                foreach (IVortexObject child in vortexObjects)
                {
                    IVortexObject lastChildObject = child;
                    vortexObject = FindTopologyElement(
                        lastTopologyElement,
                        child,
                        ref lastChildObject
                    );
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
