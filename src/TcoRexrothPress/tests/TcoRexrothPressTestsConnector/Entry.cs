using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoRexrothPressTests
{
    public static class Entry
    {
        static string TargetAmsId = "39.254.143.177.1.1"; //Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 852;

       
        public static TcoRexrothPressTestsTwinController TcoRexrothPressTestsPlc
            = new TcoRexrothPressTestsTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
