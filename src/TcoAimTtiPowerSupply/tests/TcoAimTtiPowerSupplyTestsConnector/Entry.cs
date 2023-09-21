using System;

namespace TcoAimTtiPowerSupplyTests
{
    public static class Entry
    {

#if SET_ENV_VAR
        static Entry()
        {
            Environment.SetEnvironmentVariable("Tc3Target", YOUR_TARGER_SYSTEM AMS_ID);
        }
#endif

        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 852;

        public static TcoAimTtiPowerSupplyTestsTwinController TcoAimTtiPowerSupplyTestsPlc { get; }
            = new TcoAimTtiPowerSupplyTestsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
