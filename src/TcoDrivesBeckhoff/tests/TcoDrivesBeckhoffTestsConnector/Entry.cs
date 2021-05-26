using System;

namespace TcoDrivesBeckhoffTests
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

        public static TcoDrivesBeckhoffTestsTwinController TcoDrivesBeckhoffTestsPlc { get; }
            = new TcoDrivesBeckhoffTestsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
