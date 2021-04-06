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

        [Test, Order(200)]
        public void T200_IdentitiesTest()
        {
            tc_A._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance 
            tc_B._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_B instance 

            Thread.Sleep(300);                                  //Time of the cyclical execution of the _TcoContextTest_A instance
            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            Thread.Sleep(500);                                  //Time of the cyclical execution of the _TcoContextTest_B instance
            tc_B._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.Greater(tc_A._startCycles.Synchron, 0);      //_startCycleCount should be greather then 0, as by calling the Run() method, also the Open() method should be called internally
            Assert.Greater(tc_A._endCycles.Synchron, 0);        //_endCycleCount should be greather then 0, as by calling the Run() method, also the Close() method should be called internally

            Assert.Greater(tc_B._startCycles.Synchron, 0);      //_startCycleCount should be greather then 0, as by calling the Run() method, also the Open() method should be called internally
            Assert.Greater(tc_B._endCycles.Synchron, 0);        //_endCycleCount should be greather then 0, as by calling the Run() method, also the Close() method should be called internally

            TcoObjectTest tc_A_to_A = tc_A._TcoObjectTest_A;
            TcoObjectTest tc_A_to_B = tc_A._TcoObjectTest_B;

            tc_A_to_A.ReadOutIdentities();                      // Read out identities into the test instance
            tc_A_to_B.ReadOutIdentities();                      // Read out identities into the test instance

            Assert.AreEqual(tc_A._MyIdentity.Synchron, tc_A_to_A._MyContextIdentity.Synchron);//Identity of the child's context (tc_A_to_A) is the same as the identity of the parent(tc_A)
            Assert.AreEqual(tc_A_to_A._MyContextIdentity.Synchron, tc_A_to_B._MyContextIdentity.Synchron);//Identity of the child's context (tc_A_to_A) is the same as the identity of the child's context (tc_A_to_B), as they have the same parent(tc_A)
            Assert.AreEqual(tc_A_to_A._MyParentIdentity.Synchron, tc_A_to_B._MyParentIdentity.Synchron);//Identity of the child's parent (tc_A_to_A) is the same as the identity of the child object's (tc_A_to_B), as they have the same parent(tc_A)
            Assert.AreEqual(tc_A_to_A._MyContextStartCount.Synchron, tc_A_to_B._MyContextStartCount.Synchron);//_startCycleCount of the child (tc_A_to_A) is the same as the _startCycleCount of the child (tc_A_to_B), as they have the same parent(tc_A)
            Assert.AreEqual(tc_A_to_A._MyContextEndCount.Synchron, tc_A_to_B._MyContextEndCount.Synchron);//_endCycleCount of the child (tc_A_to_A) is the same as the _endCycleCount of the child (tc_A_to_B), as they have the same parent(tc_A)
            Assert.AreNotEqual(tc_A._MyIdentity.Synchron, tc_A_to_A._MyIdentity.Synchron);//Identity of the child(tc_A_to_A) is different than the identity of the parent(tc_A), as they are both unique objects.
            Assert.AreNotEqual(tc_A._MyIdentity.Synchron, tc_A_to_B._MyIdentity.Synchron);//Identity of the child(tc_A_to_B) is different than the identity of the parent(tc_A), as they are both unique objects.

            TcoObjectTest tc_B_to_A = tc_B._TcoObjectTest_A;
            TcoObjectTest tc_B_to_B = tc_B._TcoObjectTest_B;

            tc_B_to_A.ReadOutIdentities();                      // Read out identities into the test instance
            tc_B_to_B.ReadOutIdentities();                      // Read out identities into the test instance

            Assert.AreEqual(tc_B._MyIdentity.Synchron, tc_B_to_A._MyContextIdentity.Synchron);//Identity of the child's context (tc_B_to_A) is the same as the identity of the parent(tc_B)
            Assert.AreEqual(tc_B_to_A._MyContextIdentity.Synchron, tc_B_to_B._MyContextIdentity.Synchron);//Identity of the child's context (tc_B_to_A) is the same as the identity of the child's context (tc_B_to_B), as they have the same parent(tc_B)
            Assert.AreEqual(tc_B_to_A._MyParentIdentity.Synchron, tc_B_to_B._MyParentIdentity.Synchron);//Identity of the child's parent (tc_B_to_A) is the same as the identity of the child object's (tc_B_to_B), as they have the same parent(tc_B)
            Assert.AreEqual(tc_B_to_A._MyContextStartCount.Synchron, tc_B_to_B._MyContextStartCount.Synchron);//_startCycleCount of the child (tc_B_to_A) is the same as the _startCycleCount of the child (tc_B_to_B), as they have the same parent(tc_B)
            Assert.AreEqual(tc_B_to_A._MyContextEndCount.Synchron, tc_B_to_B._MyContextEndCount.Synchron);//_endCycleCount of the child (tc_B_to_A) is the same as the _endCycleCount of the child (tc_B_to_B), as they have the same parent(tc_B)
            Assert.AreNotEqual(tc_B._MyIdentity.Synchron, tc_B_to_A._MyIdentity.Synchron);//Identity of the child(tc_B_to_A) is different as the identity of the parent(tc_B), as they are both unique objects.
            Assert.AreNotEqual(tc_B._MyIdentity.Synchron, tc_B_to_B._MyIdentity.Synchron);//Identity of the child(tc_B_to_B) is different as the identity of the parent(tc_B), as they are both unique objects.


            Assert.AreNotEqual(tc_A._MyIdentity.Synchron, tc_B._MyIdentity.Synchron);//Identity of the parent (tc_A) is different as the identity of the parent(tc_B), as they are both unique objects.
        }
    }
}