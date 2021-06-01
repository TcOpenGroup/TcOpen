using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoDataTests
{
    public static class Entry
    {
        readonly static string AmdId = Environment.GetEnvironmentVariable("Tc3Target");

        public static TcoDataTestsTwinController TcoDataTests 
            = new TcoDataTestsTwinController(Tc3ConnectorAdapter.Create(AmdId, 852, true));
    }
}
