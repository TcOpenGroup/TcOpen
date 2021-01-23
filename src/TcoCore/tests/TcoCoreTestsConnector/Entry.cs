using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TcoCoreTests
{
    public static class Entry
    {
        const string amsId = "172.20.10.2.1.1";
        const int amsPort = 852;

        public static TcoCoreTests.TcoCoreTestsTwinController PlcTcoCoreTests { get; }
            = new TcoCoreTests.TcoCoreTestsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(amsId, amsPort, true));
    }
}
