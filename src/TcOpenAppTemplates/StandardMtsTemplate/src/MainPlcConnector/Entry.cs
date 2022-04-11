using System;
using Vortex.Adapters.Connector.Tc3.Adapter;
using Vortex.Connector;

namespace MainPlcConnector
{
    public class Entry
    {
        public static readonly string AmsId = Environment.GetEnvironmentVariable("Tc3Target");

        public static MainPlc.MainPlcTwinController Plc { get; }
            = new MainPlc.MainPlcTwinController(Tc3ConnectorAdapter.Create(AmsId, 851, true));

        private static MainPlc.MainPlcTwinController _plcDesign;
        public static MainPlc.MainPlcTwinController PlcDesign
        {
            get
            {
                if (_plcDesign == null) _plcDesign = new MainPlc.MainPlcTwinController(new ConnectorAdapter(typeof(DummyConnector)));
                return _plcDesign;
            }
        }
    }
}
