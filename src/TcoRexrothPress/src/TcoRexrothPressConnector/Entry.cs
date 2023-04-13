using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoRexrothPress
{
    public static class Entry
    {
        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 851;

        public static TcoRexrothPressTwinController TcoRexrothPressPlc 
            = new TcoRexrothPressTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
