using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace PlcTcProberTestsConnector
{
    public static class Entry
    {
        public static PlcTcProberTests.PlcTcProberTestsTwinController Plc { get; } = new PlcTcProberTests.PlcTcProberTestsTwinController(Tc3ConnectorAdapter.Create(851));
    }
}
