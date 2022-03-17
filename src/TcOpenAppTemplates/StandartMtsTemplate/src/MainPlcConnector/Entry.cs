using System;
using Vortex.Adapters.Connector.Tc3.Adapter;
using Vortex.Connector;

namespace MainPlcConnector
{
    public class Entry
    {
        public static readonly string AmsId = Environment.GetEnvironmentVariable("Tc3Target");

        public static MainPlc.MainPlcTwinController PlcHammer { get; }
            = new MainPlc.MainPlcTwinController(Tc3ConnectorAdapter.Create(AmsId, 851, true));

        private static MainPlc.MainPlcTwinController _plcHammerDesign;
        public static MainPlc.MainPlcTwinController PlcHammerDesign
        {
            get
            {
                if (_plcHammerDesign == null) _plcHammerDesign = new MainPlc.MainPlcTwinController(new ConnectorAdapter(typeof(DummyConnector)));
                return _plcHammerDesign;
            }
        }
    }
}
