using System;
using Vortex.Adapters.Connector.Tc3.Adapter;
using Vortex.Connector;

namespace PlcHammerConnector
{
    public class Entry
    {        
        public static readonly string AmsId = Environment.GetEnvironmentVariable("Tc3Target");

        public static PlcHammer.PlcHammerTwinController PlcHammer { get; }
            = new PlcHammer.PlcHammerTwinController(Tc3ConnectorAdapter.Create(AmsId, 851, true));

        private static PlcHammer.PlcHammerTwinController _plcHammerDesign;
        public static PlcHammer.PlcHammerTwinController PlcHammerDesign
        {
            get
            {
                if (_plcHammerDesign == null) _plcHammerDesign = new PlcHammer.PlcHammerTwinController(new ConnectorAdapter(typeof(DummyConnector)));
                return _plcHammerDesign;
            }
        }
    }
}
