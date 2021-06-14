using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace IntegrationProjectsTests
{
    public static class Entry
    {
        const string AmdId = "172.20.10.2.1.1";

        public static IntegrationProjectsTestsTwinController IntegrationProjectsTests 
            = new IntegrationProjectsTestsTwinController(Tc3ConnectorAdapter.Create(AmdId, 852, true));
    }
}
