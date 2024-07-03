using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoIoTests
{
    public static class Entry
    {
        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");

        static int TargetAmsPort = 852;

        public static TcoIoTestsTwinController TcoIoTests = new TcoIoTestsTwinController(
            Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true)
        );
    }
}
