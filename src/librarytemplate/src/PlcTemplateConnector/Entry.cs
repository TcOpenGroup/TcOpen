using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace PlcTemplate
{
    public static class Entry
    {
        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 851;

        public static PlcTemplate.PlcTemplateTwinController PlcTemplatePlc 
            = new PlcTemplateTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
