using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoPneumatics
{
    public abstract partial class fbComponent
    {     
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            ComponentsCollector.Get.AddComponent(this);
        }
    }
}
