using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoPneumaticsTests
{
    public class ConnectorFixture
    {
        public ConnectorFixture()
        {
            StartConnector();
        }

        private static TcoPneumaticsTests.TcoPneumaticsTestsTwinController _connector;
        public static TcoPneumaticsTests.TcoPneumaticsTestsTwinController Connector
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

            var adapter = Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(TestSetupFixture.TargetAmsId, TestSetupFixture.TargetAmsPort, true);


            var connectorTask = new System.Threading.Tasks.Task(() => {

                _connector = new TcoPneumaticsTests.TcoPneumaticsTestsTwinController(adapter);

                _connector.Connector.ReadWriteCycleDelay = 100;
                _connector.Connector.BuildAndStart();
            });

            connectorTask.Start();
            connectorTask.Wait();
        }
    }
}
