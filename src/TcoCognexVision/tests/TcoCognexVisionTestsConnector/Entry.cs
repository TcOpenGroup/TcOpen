using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoCognexVisionTests
{
    public static class Entry
    {
        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 852;

        //public static TcoCognexVisionTestsTwinController TcoCognexVisionTestsPlc
        //    = new TcoCognexVisionTestsTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
        public static TcoCognexVisionTestsTwinController TcoCognexVisionTestsPlc
            = new TcoCognexVisionTestsTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
