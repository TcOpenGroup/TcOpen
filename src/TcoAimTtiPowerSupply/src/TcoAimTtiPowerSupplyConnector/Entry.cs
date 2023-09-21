using System;

namespace TcoAimTtiPowerSupply
{
    public class Entry
    {
        static string amsId = Environment.GetEnvironmentVariable("Tc3Target");
        const int amsPort = 851;

        public static TcoAimTtiPowerSupply.TcoAimTtiPowerSupplyTwinController TcoAimTtiPowerSupply { get; }
            = new TcoAimTtiPowerSupply.TcoAimTtiPowerSupplyTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(amsId, amsPort, true));
    }
}
