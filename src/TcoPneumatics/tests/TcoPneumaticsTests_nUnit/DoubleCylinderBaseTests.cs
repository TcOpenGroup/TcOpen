using System.IO;
using System.Reflection;
using NUnit.Framework;
using Tc.Prober.Recorder;
using Tc.Prober.Runners;
using TcoCore.Testing;
using TcoPneumatics;
using TcoPneumaticsTests;

namespace TcoPneumaticsTests
{
    public class DoubleCylinderBaseTests
    {
        DoubleCylinderBaseTestContext sut = ConnectorFixture
            .Connector
            .MAIN
            ._doubleCylinderBaseTests;

        [OneTimeSetUp()]
        public void OneTimeSetUp() { }

        [SetUp]
        public void TestSetup() { }

        [Test]
        [Timeout(10000)]
        public void Invalid()
        {
            sut.ExecuteProbeRun(1, (int)eCyclinderBaseTests.Invalid);
        }

        [Test]
        [Timeout(10000)]
        [TestCase(true)]
        [TestCase(false)]
        public void GetAtHome(bool signal)
        {
            sut._atHomeSignal.Synchron = signal;
            sut.ExecuteProbeRun(1, (int)eCyclinderBaseTests.GetAtHome);
            Assert.AreEqual(sut._atHomeSignal.Synchron, sut._get_AtHome.Synchron);
        }

        [Test]
        [Timeout(10000)]
        [TestCase(true)]
        [TestCase(false)]
        public void GetAtWork(bool signal)
        {
            sut._atWorkSignal.Synchron = signal;
            sut.ExecuteProbeRun(1, (int)eCyclinderBaseTests.GetAtWork);
            Assert.AreEqual(sut._atWorkSignal.Synchron, sut._get_AtWork.Synchron);
        }
    }
}
