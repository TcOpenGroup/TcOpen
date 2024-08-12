using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoUtilitiesTests;

namespace TcoBlinker
{
    public class Units
    {
        TcoUtilitiesTests.TcoBlinkerTestContext sut;

        [OneTimeSetUp]
        public void Setup()
        {
            Entry.TcoUtilitiesTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            sut = Entry.TcoUtilitiesTests.MAIN._testContext;
        }

        [Test]
        [TestCase(100, 200)]
        [TestCase(200, 100)]
        [TestCase(50, 20)]
        public void TcoBlinkerTest(int onTimeMs, int offTimeMs)
        {
            var onTime = TimeSpan.FromMilliseconds(onTimeMs);
            var offTime = TimeSpan.FromMilliseconds(offTimeMs);
            //-- Arrange
            sut._tcoBlinkerOnTime.Synchron = onTime;
            sut._tcoBlinkerOffTime.Synchron = offTime;

            sut.ExecuteProbeRun(1, (int)eTcoBlinkerTestList.Init);

            //-- Act
            sut.ExecuteProbeRun(
                (int)eTcoBlinkerTestList.TcoBlinker,
                () => sut._tcoBlinkerTestDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(onTime, sut._tcoBlinkerOnTimeDuration.Synchron);
            Assert.AreEqual(offTime, sut._tcoBlinkerOffTimeDuration.Synchron);
        }
    }
}
