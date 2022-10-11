using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoCognexVision
{
    public static class Entry
    {

        static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");

        public static TcoCognexVision.TcoCognexVisionTwinController TcoCognexVisionPlc 
            = new TcoCognexVisionTwinController(Tc3ConnectorAdapter.Create(TargetAmsId, 851, true));
    }
}
