using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoIoBeckhoff
{
    public class Entry
    {
        public static TcoIoBeckhoff.TcoIoBeckhoffTwinController TcoPneumaticsPlc { get; } = new TcoIoBeckhoffTwinController(Tc3ConnectorAdapter.Create("172.20.10.2.1.1",851, true));        
    }
}
