using System;

namespace TcoMitsubishiRoboticsTests
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

        public static TcoMitsubishiRoboticsTestsTwinController TcoMitsubishiRoboticsTestsPlc { get; }
            = new TcoMitsubishiRoboticsTestsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
