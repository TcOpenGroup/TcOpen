using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace PlcTemplateTests
{
    public static class Entry
    {
        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 852;

        public static PlcTemplateTestsTwinController PlcTemplateTests 
            = new PlcTemplateTestsTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
