using NUnit.Framework;
using System;
using System.Text;
using System.Threading;

namespace TcoCoreUnitTests
{

    public class T01_TcoContextTests
    {

        TcoCoreTests.TcoContextTest tc_A = TcoCoreUnitTests.ConnectorFixture.Connector.MAIN._tcoContextTest_A;
        TcoCoreTests.TcoContextTest tc_B = TcoCoreUnitTests.ConnectorFixture.Connector.MAIN._tcoContextTest_B;
        int Delay = 500;

        [SetUp]
        public void Setup()
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }


        [Test, Order(100)]
        public void T100_Plc_ContextADoesNotAffectContextB()
        {
            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 
            tc_B._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;

            tc_A._callMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance             

            Thread.Sleep(Delay);                                //Time of the cyclical execution of the _TcoContextTest_A instance

            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_diff = tc_A._startCycles.Synchron - AstrtCycles_0;//Calculate differencies between initial and actual counters values
            ulong AmainCycles_diff = tc_A._mainCycles.Synchron - AmainCycles_0;
            ulong AendCycles__diff = tc_A._endCycles.Synchron - AendCycles__0;

            Assert.Greater(tc_A._startCycles.Synchron, AstrtCycles_0);//All the actual values of the instance A must be greather than the initial values 
            Assert.Greater(tc_A._mainCycles.Synchron, AmainCycles_0);
            Assert.Greater(tc_A._endCycles.Synchron, AendCycles__0);

            Assert.AreEqual(AstrtCycles_diff, AmainCycles_diff);//All the values should have the same increment
            Assert.AreEqual(AmainCycles_diff, AendCycles__diff);


            Assert.AreEqual(BstrtCycles_0, tc_B._startCycles.Synchron); //All the actual values of the instance B must be the same than the initial values
            Assert.AreEqual(BmainCycles_0, tc_B._mainCycles.Synchron);
            Assert.AreEqual(BendCycles__0, tc_B._endCycles.Synchron);
        }

        [Test, Order(101)]
        public void T101_Plc_SoAsContextBDoesNotAffectContextA()
        {
            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 
            tc_B._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;

            tc_B._callMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_B instance             

            Thread.Sleep(3 * Delay);                            //Time of the cyclical execution of the _TcoContextTest_B instance

            tc_B._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong BstrtCycles_diff = tc_B._startCycles.Synchron - BstrtCycles_0;//Calculate differencies between initial and actual counters values
            ulong BmainCycles_diff = tc_B._mainCycles.Synchron - BmainCycles_0;
            ulong BendCycles__diff = tc_B._endCycles.Synchron - BendCycles__0;

            Assert.Greater(tc_B._startCycles.Synchron, BstrtCycles_0);//All the actual values of the instance B must be greather than the initial values 
            Assert.Greater(tc_B._mainCycles.Synchron, BmainCycles_0);
            Assert.Greater(tc_B._endCycles.Synchron, BendCycles__0);

            Assert.AreEqual(BstrtCycles_diff, BmainCycles_diff);//All the values should have the same increment
            Assert.AreEqual(BmainCycles_diff, BendCycles__diff);


            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);//All the actual values of the instance A must be the same than the initial values
            Assert.AreEqual(AmainCycles_0, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0, tc_A._endCycles.Synchron);
        }

        [Test, Order(102)]
        public void T102_NoPlcLogicIsRunning()
        {
            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 
            tc_B._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;


            Thread.Sleep(Delay);                                //During this time, no PLC logic should be executed

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);//All the actual values of the instance A must be the same than the initial values
            Assert.AreEqual(AmainCycles_0, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0, tc_A._endCycles.Synchron);

            Assert.AreEqual(BstrtCycles_0, tc_B._startCycles.Synchron);//All the actual values of the instance B must be the same than the initial values
            Assert.AreEqual(BmainCycles_0, tc_B._mainCycles.Synchron);
            Assert.AreEqual(BendCycles__0, tc_B._endCycles.Synchron);
        }

        [Test, Order(103)]
        public void T103_ContextDotMainCall()
        {
            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.CallMainFromUnitTest();                        //Calling only the Main() method of the TcoContext test instance

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);//Should be the same as before, as only Main() method is called. As neither the Open() nor the Close() methods are called, neither the _startCycleCount, not the _endCycleCount are incremented
            Assert.AreEqual(AmainCycles_0 + 1, tc_A._mainCycles.Synchron);//Only cycle counter of the Main() method should be incremented
            Assert.AreEqual(AendCycles__0, tc_A._endCycles.Synchron);//Should be the same as before, as only Main() method is called. As neither the Open() nor the Close() methods are called, neither the _startCycleCount, not the _endCycleCount are incremented
        }

        [Test, Order(104)]
        public void T104_ContextDotRunCall()
        {
            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.CallRunFromUnitTest();                         //Calling the Run() method of the TcoContext test instance. This method could call internaly Open(), Main() and Close() in the exact order as they are written.

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0 + 1, tc_A._startCycles.Synchron);//_startCycleCount should be also incremented, as by calling the Run() method, also the Open() method should be called internally
            Assert.AreEqual(AmainCycles_0 + 1, tc_A._mainCycles.Synchron);//cycle counter of the Main() method should be also incremented, as by calling the Run() method, also the Main() method should be called internally
            Assert.AreEqual(AendCycles__0 + 1, tc_A._endCycles.Synchron);//_endCycleCount should be also incremented, as by calling the Run() method, also the Close() method should be called internally
        }

        [Test, Order(105)]
        public void T105_MultipleContextDotMainCallUsingTestRunner()
        {
            ushort cycles = 10;
            ushort i = 0;

            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.RunUntilEndConditionIsMet(() =>                //Calling of the Open() method before the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet            
            {
                tc_A.CallMainFromUnitTest();                    //Calling only the Main() method
                i++;
            }, () => i >= cycles);                               //Calling of the Close() method after the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet    

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0 + cycles, tc_A._startCycles.Synchron);//_startCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Open() method should be called explicitly
            Assert.AreEqual(AmainCycles_0 + cycles, tc_A._mainCycles.Synchron);//cycle counter of the Main() method should be also incremented, as the Main() method is called explixitly.
            Assert.AreEqual(AendCycles__0 + cycles, tc_A._endCycles.Synchron);//_endCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Close() method should be called explicitly
        }

        [Test, Order(106)]
        public void T106_OnEntry()
        {
            ushort cycles = 10;
            ushort i = 0;

            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 


            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;
            ulong AonEntryCnt_0 = tc_A._onEntryCount.Synchron;

            tc_A.RunUntilEndConditionIsMet(() =>                //Calling of the Open() method before the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet            
            {
                tc_A.CallMainFromUnitTest();                    //Calling only the Main() method
                i++;
            }, () => i >= cycles);                               //Calling of the Close() method after the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet    

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0 + cycles, tc_A._startCycles.Synchron);//_startCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Open() method should be called explicitly
            Assert.AreEqual(AmainCycles_0 + cycles, tc_A._mainCycles.Synchron);//cycle counter of the Main() method should be also incremented, as the Main() method is called explixitly.
            Assert.AreEqual(AendCycles__0 + cycles, tc_A._endCycles.Synchron);//_endCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Close() method should be called explicitly
            Assert.AreEqual(AonEntryCnt_0, tc_A._onEntryCount.Synchron);//_onEntryCount should not increment, as only Open(), Main() and Close() methods should be called.


            AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            AmainCycles_0 = tc_A._mainCycles.Synchron;
            AendCycles__0 = tc_A._endCycles.Synchron;
            AonEntryCnt_0 = tc_A._onEntryCount.Synchron;

            for (i = 0; i < cycles; i++)
            {
                tc_A.CallRunFromUnitTest();
            }

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0 + cycles, tc_A._startCycles.Synchron);//_startCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Open() method should be called explicitly
            Assert.AreEqual(AmainCycles_0 + cycles, tc_A._mainCycles.Synchron);//cycle counter of the Main() method should be also incremented, as the Main() method is called explixitly.
            Assert.AreEqual(AendCycles__0 + cycles, tc_A._endCycles.Synchron);//_endCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Close() method should be called explicitly
            Assert.AreEqual(AonEntryCnt_0 + cycles, tc_A._onEntryCount.Synchron);//_onEntryCount should also increment, as OnEntry(),Open(), Main(), Close() and OnExit() methods should be called.

        }

        [Test, Order(107)]
        public void T107_OnExit()
        {
            ushort cycles = 20;
            ushort i = 0;

            tc_A._callMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 


            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;
            ulong AonExitCnt__0 = tc_A._onExitCount.Synchron;

            tc_A.RunUntilEndConditionIsMet(() =>                //Calling of the Open() method before the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet            
            {
                tc_A.CallMainFromUnitTest();                    //Calling only the Main() method
                i++;
            }, () => i >= cycles);                               //Calling of the Close() method after the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet    

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0 + cycles, tc_A._startCycles.Synchron);//_startCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Open() method should be called explicitly
            Assert.AreEqual(AmainCycles_0 + cycles, tc_A._mainCycles.Synchron);//cycle counter of the Main() method should be also incremented, as the Main() method is called explixitly.
            Assert.AreEqual(AendCycles__0 + cycles, tc_A._endCycles.Synchron);//_endCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Close() method should be called explicitly
            Assert.AreEqual(AonExitCnt__0, tc_A._onExitCount.Synchron);//_onExitCount should not increment, as only Open(), Main() and Close() methods should be called.


            AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            AmainCycles_0 = tc_A._mainCycles.Synchron;
            AendCycles__0 = tc_A._endCycles.Synchron;
            AonExitCnt__0 = tc_A._onExitCount.Synchron;

            for (i = 0; i < cycles; i++)
            {
                tc_A.CallRunFromUnitTest();
            }

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0 + cycles, tc_A._startCycles.Synchron);//_startCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Open() method should be called explicitly
            Assert.AreEqual(AmainCycles_0 + cycles, tc_A._mainCycles.Synchron);//cycle counter of the Main() method should be also incremented, as the Main() method is called explixitly.
            Assert.AreEqual(AendCycles__0 + cycles, tc_A._endCycles.Synchron);//_endCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Close() method should be called explicitly
            Assert.AreEqual(AonExitCnt__0 + cycles, tc_A._onExitCount.Synchron);//_onExitCount should also increment, as OnEntry(),Open(), Main(), Close() and OnExit() methods should be called.

        }
    }
}