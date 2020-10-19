using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcOpen
{
    public class Entry
    {
        public static TcOpen.TcOpenTwinController TcOpen { get; } = new TcOpenTwinController(Tc3ConnectorAdapter.Create(851));
    }
}
