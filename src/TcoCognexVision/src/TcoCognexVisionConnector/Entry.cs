using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoCognexVision
{
    public static class Entry
    {
        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 851;

        public static TcoCognexVisionTwinController TcoCognexVisionPlc 
            = new TcoCognexVisionTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
