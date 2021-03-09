using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TcoCoreTests
{
    public static class Entry
    {
        static string TargetAmsId = "172.20.10.104.1.1";
        static int TargetAmsPort = 852;
         static Entry() { 
        switch (System.Environment.MachineName)
            {
                case "WIN-8UTM78O6HB1":
                    TargetAmsId = "172.20.10.105.1.1";
                    TargetAmsPort = 852;
                    break;
                case "MTS2386":
                    TargetAmsId = "172.20.10.2.1.1";
                    TargetAmsPort = 852;
                    break;
                case "MTS_PK_LENOVUO_":
                    TargetAmsId = "172.20.10.223.1.1";
                    TargetAmsPort = 852;
                    break;
                case "MTS2216":
                    TargetAmsId = "172.20.10.104.1.1";
                    TargetAmsPort = 852;
                    break;
                default:
                    TargetAmsId = null;
                    TargetAmsPort = 852;
                    break;
            }
        }

        public static TcoCoreTests.TcoCoreTestsTwinController PlcTcoCoreTests { get; }
            = new TcoCoreTests.TcoCoreTestsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
    }
}
