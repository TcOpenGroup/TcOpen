using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoElements
{
    public static class Entry
    {
        readonly static string AmdId = Environment.GetEnvironmentVariable("Tc3Target");

        public static TcoElementsTwinController TcoElementsPlc 
            = new TcoElementsTwinController(Tc3ConnectorAdapter.Create(AmdId, 851, true));
    }
}
