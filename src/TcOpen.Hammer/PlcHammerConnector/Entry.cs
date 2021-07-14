using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace PlcHammerConnector
{
    public class Entry
    {
        public readonly static string AmsId = Environment.GetEnvironmentVariable("Tc3Target");

        public static PlcHammer.PlcHammerTwinController PlcHammer { get; } 
            = new PlcHammer.PlcHammerTwinController(Tc3ConnectorAdapter.Create(AmsId, 851, true));
    }
}
