using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoPneumatics
{
    public class Entry
    {
        public static TcoPneumatics.TcoPneumaticsTwinController TcoPneumaticsPlc { get; } = new TcoPneumaticsTwinController(Tc3ConnectorAdapter.Create("172.20.10.2.1.1",851, true));        
    }
}
