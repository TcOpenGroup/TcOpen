using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlcAppExamples
{
    public static class Entry
    {
        const string amsId = "172.20.10.2.1.1";
        const int amsPort = 851;

        public static PlcAppExamples.PlcAppExamplesTwinController PlcAppExamples { get; }
            = new PlcAppExamples.PlcAppExamplesTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(amsId, amsPort, true));
    }
}
