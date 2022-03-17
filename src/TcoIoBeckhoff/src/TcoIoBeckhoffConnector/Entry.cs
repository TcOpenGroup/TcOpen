using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoIoBeckhoff
{
    public static class Entry
    {
        //const string AmdId = "172.20.10.2.1.1";

        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 851;

        public static TcoIoBeckhoff.TcoIoBeckhoffTwinController TcoIoBeckhoffPlc 
            = new TcoIoBeckhoffTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
