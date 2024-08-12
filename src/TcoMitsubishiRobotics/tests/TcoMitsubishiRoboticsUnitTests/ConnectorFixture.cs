using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoMitsubishiRoboticsUnitTests
{
    public class ConnectorFixture
    {
        public ConnectorFixture()
        {
            StartConnector();
        }

        private static TcoMitsubishiRoboticsTests.TcoMitsubishiRoboticsTestsTwinController _connector;
        public static TcoMitsubishiRoboticsTests.TcoMitsubishiRoboticsTestsTwinController Connector
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
            _connector = new TcoMitsubishiRoboticsTests.TcoMitsubishiRoboticsTestsTwinController(
                adapter
            );
            _connector.Connector.ReadWriteCycleDelay = 100;
            _connector.Connector.BuildAndStart();
        }
    }
}
