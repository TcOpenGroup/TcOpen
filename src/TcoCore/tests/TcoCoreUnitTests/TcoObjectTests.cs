using NUnit.Framework;
using System.Threading;
using TcoCoreTests;

namespace TcoCoreUnitTests
{

    public class T02_TcoObjectTests
    {

        TcoContextTest tc_A = ConnectorFixture.Connector.MAIN._tcoContextTest_A;
        TcoContextTest tc_B = ConnectorFixture.Connector.MAIN._tcoContextTest_B;
        
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(200)]
        public void T200_IdentitiesTest()
        {
            tc_A._callMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance 
            tc_B._callMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_B instance 

            Thread.Sleep(300);                                  //Time of the cyclical execution of the _TcoContextTest_A instance
            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            Thread.Sleep(500);                                  //Time of the cyclical execution of the _TcoContextTest_B instance
            tc_B._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.Greater(tc_A._startCycles.Synchron, 0);      //_startCycleCount should be greather then 0, as by calling the Run() method, also the Open() method should be called internally
            Assert.Greater(tc_A._endCycles.Synchron, 0);        //_endCycleCount should be greather then 0, as by calling the Run() method, also the Close() method should be called internally

            Assert.Greater(tc_B._startCycles.Synchron, 0);      //_startCycleCount should be greather then 0, as by calling the Run() method, also the Open() method should be called internally
            Assert.Greater(tc_B._endCycles.Synchron, 0);        //_endCycleCount should be greather then 0, as by calling the Run() method, also the Close() method should be called internally

            TcoObjectTest tc_A_to_A = tc_A._tcoObjectTest_A;
            TcoObjectTest tc_A_to_B = tc_A._tcoObjectTest_B;

            tc_A_to_A.ReadOutIdentities();                      // Read out identities into the test instance
            tc_A_to_B.ReadOutIdentities();                      // Read out identities into the test instance

            Assert.AreEqual(tc_A._myIdentity.Synchron, tc_A_to_A._myContextIdentity.Synchron);//Identity of the child's context (tc_A_to_A) is the same as the identity of the parent(tc_A)
            Assert.AreEqual(tc_A_to_A._myContextIdentity.Synchron, tc_A_to_B._myContextIdentity.Synchron);//Identity of the child's context (tc_A_to_A) is the same as the identity of the child's context (tc_A_to_B), as they have the same parent(tc_A)
            Assert.AreEqual(tc_A_to_A._myParentIdentity.Synchron, tc_A_to_B._myParentIdentity.Synchron);//Identity of the child's parent (tc_A_to_A) is the same as the identity of the child object's (tc_A_to_B), as they have the same parent(tc_A)
            Assert.AreEqual(tc_A_to_A._myContextStartCount.Synchron, tc_A_to_B._myContextStartCount.Synchron);//_startCycleCount of the child (tc_A_to_A) is the same as the _startCycleCount of the child (tc_A_to_B), as they have the same parent(tc_A)
            Assert.AreEqual(tc_A_to_A._myContextEndCount.Synchron, tc_A_to_B._myContextEndCount.Synchron);//_endCycleCount of the child (tc_A_to_A) is the same as the _endCycleCount of the child (tc_A_to_B), as they have the same parent(tc_A)
            Assert.AreNotEqual(tc_A._myIdentity.Synchron, tc_A_to_A._myIdentity.Synchron);//Identity of the child(tc_A_to_A) is different than the identity of the parent(tc_A), as they are both unique objects.
            Assert.AreNotEqual(tc_A._myIdentity.Synchron, tc_A_to_B._myIdentity.Synchron);//Identity of the child(tc_A_to_B) is different than the identity of the parent(tc_A), as they are both unique objects.

            TcoObjectTest tc_B_to_A = tc_B._tcoObjectTest_A;
            TcoObjectTest tc_B_to_B = tc_B._tcoObjectTest_B;

            tc_B_to_A.ReadOutIdentities();                      // Read out identities into the test instance
            tc_B_to_B.ReadOutIdentities();                      // Read out identities into the test instance

            Assert.AreEqual(tc_B._myIdentity.Synchron, tc_B_to_A._myContextIdentity.Synchron);//Identity of the child's context (tc_B_to_A) is the same as the identity of the parent(tc_B)
            Assert.AreEqual(tc_B_to_A._myContextIdentity.Synchron, tc_B_to_B._myContextIdentity.Synchron);//Identity of the child's context (tc_B_to_A) is the same as the identity of the child's context (tc_B_to_B), as they have the same parent(tc_B)
            Assert.AreEqual(tc_B_to_A._myParentIdentity.Synchron, tc_B_to_B._myParentIdentity.Synchron);//Identity of the child's parent (tc_B_to_A) is the same as the identity of the child object's (tc_B_to_B), as they have the same parent(tc_B)
            Assert.AreEqual(tc_B_to_A._myContextStartCount.Synchron, tc_B_to_B._myContextStartCount.Synchron);//_startCycleCount of the child (tc_B_to_A) is the same as the _startCycleCount of the child (tc_B_to_B), as they have the same parent(tc_B)
            Assert.AreEqual(tc_B_to_A._myContextEndCount.Synchron, tc_B_to_B._myContextEndCount.Synchron);//_endCycleCount of the child (tc_B_to_A) is the same as the _endCycleCount of the child (tc_B_to_B), as they have the same parent(tc_B)
            Assert.AreNotEqual(tc_B._myIdentity.Synchron, tc_B_to_A._myIdentity.Synchron);//Identity of the child(tc_B_to_A) is different as the identity of the parent(tc_B), as they are both unique objects.
            Assert.AreNotEqual(tc_B._myIdentity.Synchron, tc_B_to_B._myIdentity.Synchron);//Identity of the child(tc_B_to_B) is different as the identity of the parent(tc_B), as they are both unique objects.


            Assert.AreNotEqual(tc_A._myIdentity.Synchron, tc_B._myIdentity.Synchron);//Identity of the parent (tc_A) is different as the identity of the parent(tc_B), as they are both unique objects.
        }

        [Test,Order(300)]
        public void T300_EqualsTest()
        {
            Assert.IsTrue(tc_A._tcoObjectTest_Misc.EqualsTest(0)); //0 compares to own reference
            Assert.IsFalse(tc_A._tcoObjectTest_Misc.EqualsTest(1)); //1 compares to other object (A to B)
            Assert.IsFalse(tc_A._tcoObjectTest_Misc.EqualsTest(4)); //4 compares to other object (B to A)
            Assert.IsTrue(tc_A._tcoObjectTest_Misc.EqualsTest(2)); //2 compares same object instance

            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj1._someNumber.Synchron = 1;
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj1._someString.Synchron = "1";
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj2._someNumber.Synchron = 1;
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj2._someString.Synchron = "1";

            Assert.IsTrue(tc_A._tcoObjectTest_Misc.EqualsTest(3)); //3 compare two object with Equals override (compares values)

            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj2._someString.Synchron = "0";

            Assert.IsFalse(tc_A._tcoObjectTest_Misc.EqualsTest(3)); //3 compare two object with Equals override (compares values)
        }


        [Test, Order(400)]
        public void T400_EqualsOverrideTest()
        {
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj1._someNumber.Synchron = 0;
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj1._someString.Synchron = "";
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj2._someNumber.Synchron = 0;
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj2._someString.Synchron = "";

            Assert.IsTrue(tc_A._tcoObjectTest_Misc.EqualsOverrideTest());
          
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj1._someNumber.Synchron = 10;
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj1._someString.Synchron = "Hello";

            Assert.IsFalse(tc_A._tcoObjectTest_Misc.EqualsOverrideTest());

            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj2._someNumber.Synchron = 10;

            Assert.IsFalse(tc_A._tcoObjectTest_Misc.EqualsOverrideTest());
            
            tc_A._tcoObjectTest_Misc._tcoObjectEqualsTestObj2._someString.Synchron = "Hello";

            Assert.IsTrue(tc_A._tcoObjectTest_Misc.EqualsOverrideTest());
        }        
    }
}