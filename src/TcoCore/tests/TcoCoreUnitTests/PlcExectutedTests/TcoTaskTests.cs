using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcoCore;
using TcoCore.Testing;
using TcoCoreTests;

namespace TcoCoreUnitTests.PlcExecutedTests
{
    public class TcoTaskTests
    {
        TcoTaskTestContext tc = ConnectorFixture.Connector.MAIN._tcoTaskTestContext;

        [SetUp]
        public void Setup() { }

        [Test]
        public void ElapsedTypeMeasurement()
        {
            //! TEST WAS MOVED TO L1
            //#if DEBUG
            //            tc._elapsedTimeETA.Synchron = new TimeSpan(0, 0, 0, 0, 50);
            //            tc._runElapsedTimer.Synchron = false;
            //            var expected = tc._elapsedTimeETA.Synchron.Add(new TimeSpan(0, 0, 0, 0, 10)); // need to add one PLC cycle duration to the expected.

            //            tc.ExecuteProbeRun(3, 0);

            //            tc.ExecuteProbeRun((int)eTcoTaskTests.ElapsedTypeMeasurement, () => tc._to_A._sut_A._taskState.Synchron == (short)eTaskState.Done);
            //            Assert.AreEqual(expected, tc._elapsedTime.Synchron);
            //#endif
        }
    }
}
