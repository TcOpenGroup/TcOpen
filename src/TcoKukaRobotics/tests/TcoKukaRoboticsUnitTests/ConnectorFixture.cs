using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoKukaRoboticsUnitTests
{
    public class ConnectorFixture
    {
        public ConnectorFixture()
        {
            StartConnector();
        }

        private static TcoKukaRoboticsTests.TcoKukaRoboticsTestsTwinController _connector;
        public static TcoKukaRoboticsTests.TcoKukaRoboticsTestsTwinController Connector
        {
            get
            {
                if (_connector == null)
                {
                    StartConnector();
                }
                return _connector;
            }
        }

        public static void StartConnector()
        {
            var adapter = Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(
                TestSetupFixture.TargetAmsId,
                TestSetupFixture.TargetAmsPort,
                true
            );
            _connector = new TcoKukaRoboticsTests.TcoKukaRoboticsTestsTwinController(adapter);
            _connector.Connector.ReadWriteCycleDelay = 100;
            _connector.Connector.BuildAndStart();
        }
    }
}
