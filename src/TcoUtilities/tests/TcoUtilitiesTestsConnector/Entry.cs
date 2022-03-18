using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoUtilitiesTests
{
    public static class Entry
    {
        private static string AmdId = Environment.GetEnvironmentVariable("Tc3Target");

        public static TcoUtilitiesTestsTwinController TcoUtilitiesTests 
            = new TcoUtilitiesTestsTwinController(Tc3ConnectorAdapter.Create(AmdId, 852, true));
    }
}
