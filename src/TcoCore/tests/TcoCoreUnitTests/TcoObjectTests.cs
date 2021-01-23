using NUnit.Framework;
using System.Threading;
using TcoCoreTests;

namespace TcoCoreUnitTests
{

    public class T02_TcoObjectTests
    {

        TcoContextTest tc_A = ConnectorFixture.Connector.MAIN._TcoContextTest_A;
        TcoContextTest tc_B = ConnectorFixture.Connector.MAIN._TcoContextTest_B;

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(001)]
        public void T001_IdentitiesTest()
        {
            tc_A._CallMyPlcInstance.Synchron = true;
            tc_B._CallMyPlcInstance.Synchron = true;

            Thread.Sleep(300);
            tc_A._CallMyPlcInstance.Synchron = false;

            Thread.Sleep(500);
            tc_B._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();
            tc_B.ReadOutCycleCounters();

            Assert.Greater(tc_A._startCycles.Synchron, 0);
            Assert.Greater(tc_A._endCycles.Synchron, 0);

            Assert.Greater(tc_B._startCycles.Synchron, 0);
            Assert.Greater(tc_B._endCycles.Synchron, 0);

            TcoObjectTest tc_A_to_A = tc_A._TcoObjectTest_A;
            TcoObjectTest tc_A_to_B = tc_A._TcoObjectTest_B;

            tc_A_to_A.ReadOutIdentities();
            tc_A_to_B.ReadOutIdentities();

            Assert.AreEqual(    tc_A._MyIdentity.Synchron,               tc_A_to_A._MyContextIdentity.Synchron);
            Assert.AreEqual(    tc_A_to_A._MyContextIdentity.Synchron,   tc_A_to_B._MyContextIdentity.Synchron);
            Assert.AreEqual(    tc_A_to_A._MyParentIdentity.Synchron,    tc_A_to_B._MyParentIdentity.Synchron);
            Assert.AreEqual(    tc_A_to_A._MyContextStartCount.Synchron, tc_A_to_B._MyContextStartCount.Synchron);
            Assert.AreEqual(    tc_A_to_A._MyContextEndCount.Synchron,   tc_A_to_B._MyContextEndCount.Synchron);
            Assert.AreNotEqual( tc_A._MyIdentity.Synchron,               tc_A_to_A._MyIdentity.Synchron);
            Assert.AreNotEqual( tc_A._MyIdentity.Synchron,               tc_A_to_B._MyIdentity.Synchron);

            TcoObjectTest tc_B_to_A = tc_B._TcoObjectTest_A;
            TcoObjectTest tc_B_to_B = tc_B._TcoObjectTest_B;

            tc_B_to_A.ReadOutIdentities();
            tc_B_to_B.ReadOutIdentities();

            Assert.AreEqual(    tc_B._MyIdentity.Synchron,              tc_B_to_A._MyContextIdentity.Synchron);
            Assert.AreEqual(    tc_B_to_A._MyContextIdentity.Synchron,  tc_B_to_B._MyContextIdentity.Synchron);
            Assert.AreEqual(    tc_B_to_A._MyParentIdentity.Synchron,   tc_B_to_B._MyParentIdentity.Synchron);
            Assert.AreEqual(    tc_B_to_A._MyContextStartCount.Synchron,tc_B_to_B._MyContextStartCount.Synchron);
            Assert.AreEqual(    tc_B_to_A._MyContextEndCount.Synchron,  tc_B_to_B._MyContextEndCount.Synchron);
            Assert.AreNotEqual( tc_B._MyIdentity.Synchron,              tc_B_to_A._MyIdentity.Synchron);
            Assert.AreNotEqual( tc_B._MyIdentity.Synchron,              tc_B_to_B._MyIdentity.Synchron);


            Assert.AreNotEqual(tc_A._MyIdentity.Synchron, tc_B._MyIdentity.Synchron);
        }
    }
}