using NUnit.Framework;
using TcoPneumatics;
using Tc.Prober.Runners;
using Tc.Prober.Recorder;
using System.Reflection;
using System.IO;
using TcoPneumaticsTests;
using TcoCore.Testing;

namespace TcoPneumaticsTests
{
    public class CylinderBaseTests
    {
        CylinderBaseTestContext sut = ConnectorFixture.Connector.MAIN._cylinderBaseTests;

        [OneTimeSetUp()]
        public void OneTimeSetUp()
        {

        }

        [SetUp]
        public void TestSetup()
        {

        }

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