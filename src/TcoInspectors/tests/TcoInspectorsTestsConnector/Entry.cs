using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoInspectorsTests
{
    public static class Entry
    {
        static string AmdId = Environment.GetEnvironmentVariable("Tc3Target");

        public static TcoInspectorsTestsTwinController TcoInspectorsTests
            = new TcoInspectorsTestsTwinController(Tc3ConnectorAdapter.Create(AmdId, 852, true));
    }
}
