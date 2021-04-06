using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TcoCoreExamples
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
        static int TargetAmsPort = 853;

        public static TcoCoreExamples.TcoCoreExamplesTwinController PlcTcoCoreExamples { get; }
            = new TcoCoreExamples.TcoCoreExamplesTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
