using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoPneumaticsTests
{
    public class Entry
    {
        public static TcoPneumaticsTestsTwinController TcoPneumaticsTestsPlc { get; } = new TcoPneumaticsTestsTwinController(Tc3ConnectorAdapter.Create("172.20.10.2.1.1",852, true));        
    }
}
