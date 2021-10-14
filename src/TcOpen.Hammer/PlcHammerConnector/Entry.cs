using Vortex.Adapters.Connector.Tc3.Adapter;

namespace PlcHammerConnector
{
    public class Entry
    {
        public static readonly string AmsId = "192.168.3.23.1.1";

        public static PlcHammer.PlcHammerTwinController PlcHammer { get; }
            = new PlcHammer.PlcHammerTwinController(Tc3ConnectorAdapter.Create(AmsId, 851, true));
    }
}
