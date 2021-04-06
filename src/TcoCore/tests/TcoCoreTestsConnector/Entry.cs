using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TcoCoreTests
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
        
        public static TcoCoreTests.TcoCoreTestsTwinController PlcTcoCoreTests { get; }
            = new TcoCoreTests.TcoCoreTestsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
