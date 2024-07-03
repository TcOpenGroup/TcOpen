using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoIo
{
    public static class Entry
    {
        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 851;

        public static TcoIo.TcoIoTwinController TcoIoPlc = new TcoIoTwinController(
            Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true)
        );
    }
}
