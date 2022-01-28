using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoInspectors;

namespace TcoInspectorsPlc
{
    public class Entry
    {
        public static TcoInspectorsTwinController TcoInspectorsPlc { get; } = 
            new TcoInspectorsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(Environment.GetEnvironmentVariable("Tc3Target"), 852, true));
    }
}
