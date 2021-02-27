using NUnit.Framework;
using System.Threading;
using TcoCoreTests;

namespace TcoCoreUnitTests
{

    public class T03_TcoTaskTests
    {

        ushort cycles_A = 7;
        ushort cycles_B = 19;
        bool finished = false;
        ushort i = 0;

        TcoContextTest tc = ConnectorFixture.Connector.MAIN._TcoContextTest_A;

        ulong A_TaskInvokeCount_0;
        ulong B_TaskInvokeCount_0;
        ulong A_TaskInvokeRECount_0;
        ulong B_TaskInvokeRECount_0;
        ulong A_TaskExecuteCount_0;
        ulong B_TaskExecuteCount_0;
        ulong A_TaskExecuteRECount_0;
        ulong B_TaskExecuteRECount_0;
        ulong A_TaskDoneCount_0;
        ulong B_TaskDoneCount_0;
        ulong A_TaskDoneRECount_0;
        ulong B_TaskDoneRECount_0;

        ulong A_TaskInvokeCount_1;
        ulong B_TaskInvokeCount_1;
        ulong A_TaskInvokeRECount_1;
        ulong B_TaskInvokeRECount_1;
        ulong A_TaskExecuteCount_1;
        ulong B_TaskExecuteCount_1;
        ulong A_TaskExecuteRECount_1;
        ulong B_TaskExecuteRECount_1;
        ulong A_TaskDoneCount_1;
        ulong B_TaskDoneCount_1;
        ulong A_TaskDoneRECount_1;
        ulong B_TaskDoneRECount_1;

        ushort plccycles;
        [SetUp]
        public void Setup()
        {
            finished = false;
            i = 0;

            A_TaskInvokeCount_0 = tc._TcoObjectTest_A._TcoTaskTest_A._InvokeCounter.Synchron;
            B_TaskInvokeCount_0 = tc._TcoObjectTest_A._TcoTaskTest_B._InvokeCounter.Synchron;
            A_TaskInvokeRECount_0 = tc._TcoObjectTest_A._TcoTaskTest_A._InvokeRisingEdgeCounter.Synchron;
            B_TaskInvokeRECount_0 = tc._TcoObjectTest_A._TcoTaskTest_B._InvokeRisingEdgeCounter.Synchron;
            A_TaskExecuteCount_0 = tc._TcoObjectTest_A._TcoTaskTest_A._ExecuteCounter.Synchron;
            B_TaskExecuteCount_0 = tc._TcoObjectTest_A._TcoTaskTest_B._ExecuteCounter.Synchron;
            A_TaskExecuteRECount_0 = tc._TcoObjectTest_A._TcoTaskTest_A._ExecuteRisingEdgeCounter.Synchron;
            B_TaskExecuteRECount_0 = tc._TcoObjectTest_A._TcoTaskTest_B._ExecuteRisingEdgeCounter.Synchron;
            A_TaskDoneCount_0 = tc._TcoObjectTest_A._TcoTaskTest_A._DoneCounter.Synchron;
            B_TaskDoneCount_0 = tc._TcoObjectTest_A._TcoTaskTest_B._DoneCounter.Synchron;
            A_TaskDoneRECount_0 = tc._TcoObjectTest_A._TcoTaskTest_A._DoneRisingEdgeCounter.Synchron;
            B_TaskDoneRECount_0 = tc._TcoObjectTest_A._TcoTaskTest_B._DoneRisingEdgeCounter.Synchron;

            A_TaskInvokeCount_1 = 0;
            B_TaskInvokeCount_1 = 0;
            A_TaskInvokeRECount_1 = 0;
            B_TaskInvokeRECount_1 = 0;
            A_TaskExecuteCount_1 = 0;
            B_TaskExecuteCount_1 = 0;
            A_TaskExecuteRECount_1 = 0;
            B_TaskExecuteRECount_1 = 0;
            A_TaskDoneCount_1 = 0;
            B_TaskDoneCount_1 = 0;
            A_TaskDoneRECount_1 = 0;
            B_TaskDoneRECount_1 = 0;

            plccycles = 0;
        }

        public void GetCounterValues()
        {
            A_TaskInvokeCount_1 = tc._TcoObjectTest_A._TcoTaskTest_A._InvokeCounter.Synchron;
            B_TaskInvokeCount_1 = tc._TcoObjectTest_A._TcoTaskTest_B._InvokeCounter.Synchron;
            A_TaskInvokeRECount_1 = tc._TcoObjectTest_A._TcoTaskTest_A._InvokeRisingEdgeCounter.Synchron;
            B_TaskInvokeRECount_1 = tc._TcoObjectTest_A._TcoTaskTest_B._InvokeRisingEdgeCounter.Synchron;
            A_TaskExecuteCount_1 = tc._TcoObjectTest_A._TcoTaskTest_A._ExecuteCounter.Synchron;
            B_TaskExecuteCount_1 = tc._TcoObjectTest_A._TcoTaskTest_B._ExecuteCounter.Synchron;
            A_TaskExecuteRECount_1 = tc._TcoObjectTest_A._TcoTaskTest_A._ExecuteRisingEdgeCounter.Synchron;
            B_TaskExecuteRECount_1 = tc._TcoObjectTest_A._TcoTaskTest_B._ExecuteRisingEdgeCounter.Synchron;
            A_TaskDoneCount_1 = tc._TcoObjectTest_A._TcoTaskTest_A._DoneCounter.Synchron;
            B_TaskDoneCount_1 = tc._TcoObjectTest_A._TcoTaskTest_B._DoneCounter.Synchron;
            A_TaskDoneRECount_1 = tc._TcoObjectTest_A._TcoTaskTest_A._DoneRisingEdgeCounter.Synchron;
            B_TaskDoneRECount_1 = tc._TcoObjectTest_A._TcoTaskTest_B._DoneRisingEdgeCounter.Synchron;
        }

        public void CheckBothTaskInvokeCount(ushort count)
        {
            Assert.AreEqual(A_TaskInvokeCount_0 + count, A_TaskInvokeCount_1);
            Assert.AreEqual(B_TaskInvokeCount_0 + count, B_TaskInvokeCount_1);
            Assert.AreEqual(A_TaskInvokeRECount_0 + count, A_TaskInvokeRECount_1);
            Assert.AreEqual(B_TaskInvokeRECount_0 + count, B_TaskInvokeRECount_1);
        }

        public void CheckBothTaskExecuteRECount(ushort count)
        {
            Assert.AreEqual(A_TaskExecuteRECount_0 + count, A_TaskExecuteRECount_1);
            Assert.AreEqual(B_TaskExecuteRECount_0 + count, B_TaskExecuteRECount_1);
        }

        public void CheckBothTaskDoneRECount(ushort count)
        {
            Assert.AreEqual(A_TaskDoneRECount_0 + count, A_TaskDoneRECount_1);
            Assert.AreEqual(B_TaskDoneRECount_0 + count, B_TaskDoneRECount_1);
        }


        [Test, Order(300)]
        public void T300_TaskInvokeAndWaitForDone()
        {
            //Both tasks are triggered in the same plc cycle. The Invoke methods of the both tasks are still called cyclically. Task A should reach Done state sooner as the Task B, but it should not restarted again.
            Assert.Greater(cycles_B, cycles_A);

            tc._CallMyPlcInstance.Synchron = false;                                     //Switch off the cyclical execution of the tc instance 

            tc._TcoObjectTest_A._TcoTaskTest_A._CounterSetValue.Synchron = cycles_A;    //Assign _CounterSetValue to Task A 
            tc._TcoObjectTest_A._TcoTaskTest_B._CounterSetValue.Synchron = cycles_B;    //Assign _CounterSetValue to Task B

            tc._TcoObjectTest_A._TcoTaskTest_A.TriggerRestore();                        //Restore Task A
            tc._TcoObjectTest_A._TcoTaskTest_B.TriggerRestore();                        //Restore Task B

            tc._TcoObjectTest_A._TcoTaskTest_A.SetPreviousStateToIdle();                //Set previous state of the Task A to Idle
            tc._TcoObjectTest_A._TcoTaskTest_B.SetPreviousStateToIdle();                //Set previous state of the Task B to Idle

            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tc._TcoObjectTest_A._TcoTaskTest_A.TriggerInvoke();                     //Invoke of the Task A is cyclically called, even when Task A is in the Busy state
                tc._TcoObjectTest_A._TcoTaskTest_B.TriggerInvoke();                     //Invoke of the Task B is cyclically called, even when Task B is in the Busy state
                tc._TcoObjectTest_A.CallTaskInstancies();                               //Calling instancies of the Task A and Tak B in the PLC.
                tc._TcoObjectTest_A._TcoTaskTest_A.ReadOutState();                      //Reading out the state of the Task A
                tc._TcoObjectTest_A._TcoTaskTest_B.ReadOutState();                      //Reading out the state of the Task B
                finished = tc._TcoObjectTest_A._TcoTaskTest_A._IsDone.Synchron &&       //End condition, execution finished, when the both tasks are in Done state.
                            tc._TcoObjectTest_A._TcoTaskTest_B._IsDone.Synchron;
            }, () => finished);

            GetCounterValues();                                                         //Readout all counter values from test instance into the _1 variables
            CheckBothTaskInvokeCount(1);                                                //Both tasks should be triggered just once
            Assert.AreEqual(A_TaskExecuteCount_0 + cycles_A, A_TaskExecuteCount_1);     //Execution body of the Task A should run exactly cycles_A cycles
            Assert.AreEqual(B_TaskExecuteCount_0 + cycles_B, B_TaskExecuteCount_1);     //Execution body of the Task B should run exactly cycles_B cycles
            CheckBothTaskExecuteRECount(1);                                             //Both tasks starts executing just once

            Assert.Greater(A_TaskDoneCount_1 - A_TaskDoneCount_0, B_TaskDoneCount_1 - B_TaskDoneCount_0);//As the cycles_B is greater then cycle_A, TaskADoneCounter should have a bigger increment than TaskBDoneCounter
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);                  //As the cycles_B is greater then cycle_A, Task A is already in Done state, when Task B just reach it. As the execution is finished, when the both tasks are in Done state, TaskBDoneCounter should increment exactly by one only.

            CheckBothTaskDoneRECount(1);                                                //Both tasks reach done state just once
            Assert.AreEqual(cycles_B, plccycles);                                       //The execution should take exactly cycles_B cycles, as it is greather than cycles_A.
        }

        [Test, Order(301)]
        public void T301_TaskInvokeAfterDoneWithNoEmptyCycles()
        {
            //The Invoke methods of the both tasks are still called cyclically, after the both tasks has reached the Done state in the previous test.
            //As no empty cycle was called between this two tests, the both tasks should not change theirs states.
            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tc._TcoObjectTest_A._TcoTaskTest_A.TriggerInvoke();                     //Invoke of the Task A is cyclically called, even when Task A is in the Busy state
                tc._TcoObjectTest_A._TcoTaskTest_B.TriggerInvoke();                     //Invoke of the Task B is cyclically called, even when Task B is in the Busy state
                tc._TcoObjectTest_A.CallTaskInstancies();                               //Calling instancies of the Task A and Tak B in the PLC.
                tc._TcoObjectTest_A._TcoTaskTest_A.ReadOutState();                      //Reading out the state of the Task A
                tc._TcoObjectTest_A._TcoTaskTest_B.ReadOutState();                      //Reading out the state of the Task B
                finished = tc._TcoObjectTest_A._TcoTaskTest_A._IsDone.Synchron &&       //End condition, execution finished, when the both tasks are in Done state.
                            tc._TcoObjectTest_A._TcoTaskTest_B._IsDone.Synchron;
            }, () => finished);

            GetCounterValues();                                                         //Readout all counter values from test instance into the _1 variables
            CheckBothTaskInvokeCount(0);                                                //Both tasks should not be triggered again, as both task body are still called and no empty cycle was performed
            Assert.AreEqual(A_TaskExecuteCount_0, A_TaskExecuteCount_1);                //Execution body of the Task A  should not run, as it is already done
            Assert.AreEqual(B_TaskExecuteCount_0, B_TaskExecuteCount_1);                //Execution body of the Task B  should not run, as it is already done

            CheckBothTaskExecuteRECount(0);                                             //Neither Task A, nor Task B reach Executing state, as they are already in Done state.

            
            Assert.AreEqual(A_TaskDoneCount_0 + 1, A_TaskDoneCount_1);                  //Task done counter should increment only be 1, as Task A is already done
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);                  //Task done counter should increment only be 1, as Task B is already done

            CheckBothTaskDoneRECount(0);                                                //Neither Task A, nor Task B reach Done state, as they are already in Done state.
            Assert.AreEqual(1, plccycles);                                              //The execution should take exactly one cycle, end condition is already met before start.
        }

        [Test, Order(302)]
        public void T302_TaskInvokeAfterDoneWithOneEmptyCycle()
        {
            tc.AddEmptyCycle();

            tc.RunUntilEndConditionIsMet(() =>
            {
                tc.CallMainFromUnitTest();
                tc._TcoObjectTest_A._TcoTaskTest_A.TriggerInvoke();
                tc._TcoObjectTest_A._TcoTaskTest_B.TriggerInvoke();
                tc._TcoObjectTest_A.CallTaskInstancies();
                tc._TcoObjectTest_A._TcoTaskTest_A.ReadOutState();
                tc._TcoObjectTest_A._TcoTaskTest_B.ReadOutState();
                finished = tc._TcoObjectTest_A._TcoTaskTest_A._IsDone.Synchron &&
                            tc._TcoObjectTest_A._TcoTaskTest_B._IsDone.Synchron;
            }, () => finished);

            GetCounterValues();

            //Both tasks should be triggered just once
            CheckBothTaskInvokeCount(1);

            //Execution body of the Task A should run exactly cycles_A cycles
            Assert.AreEqual(A_TaskExecuteCount_0 + cycles_A, A_TaskExecuteCount_1);

            //Execution body of the Task B should run exactly cycles_B cycles
            Assert.AreEqual(B_TaskExecuteCount_0 + cycles_B, B_TaskExecuteCount_1);

            //Both tasks starts executing just once
            CheckBothTaskExecuteRECount(1);

            //As the cycles_B is greater then cycle_A, TaskADoneCounter should have a bigger increment than TaskBDoneCounter
            Assert.Greater(A_TaskDoneCount_1 - A_TaskDoneCount_0, B_TaskDoneCount_1 - B_TaskDoneCount_0);
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);

            //Both tasks reach done state just once
            CheckBothTaskDoneRECount(1);
        }

        [Test, Order(303)]
        public void T303_TaskInvokeAfterDoneWithAbortCall()
        {
            tc._TcoObjectTest_A._TcoTaskTest_A.TriggerAbort();
            tc._TcoObjectTest_A._TcoTaskTest_B.TriggerAbort();

            tc.RunUntilEndConditionIsMet(() =>
            {
                tc.CallMainFromUnitTest();
                tc._TcoObjectTest_A._TcoTaskTest_A.TriggerInvoke();
                tc._TcoObjectTest_A._TcoTaskTest_B.TriggerInvoke();
                tc._TcoObjectTest_A.CallTaskInstancies();
                tc._TcoObjectTest_A._TcoTaskTest_A.ReadOutState();
                tc._TcoObjectTest_A._TcoTaskTest_B.ReadOutState();
                finished = tc._TcoObjectTest_A._TcoTaskTest_A._IsDone.Synchron &&
                            tc._TcoObjectTest_A._TcoTaskTest_B._IsDone.Synchron;
            }, () => finished);


            GetCounterValues();

            //Both tasks should not be triggered
            CheckBothTaskInvokeCount(0);

            //Execution body of the Task A should not be called
            Assert.AreEqual(A_TaskExecuteCount_0 , A_TaskExecuteCount_1);

            //Execution body of the Task B should not be called
            Assert.AreEqual(B_TaskExecuteCount_0 , B_TaskExecuteCount_1);

            //Both tasks will not start
            CheckBothTaskExecuteRECount(0);

            //TaskADoneCounter so as the TaskBDoneCounter should increment just by one
            Assert.AreEqual(A_TaskDoneCount_0 + 1, A_TaskDoneCount_1);
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);

            //Both tasks are already done
            CheckBothTaskDoneRECount(0);
        }

        [Test, Order(304)]
        public void T304_TaskInvokeAfterDoneWithRestoreCall()
        {
            tc._TcoObjectTest_A._TcoTaskTest_A.TriggerRestore();
            tc._TcoObjectTest_A._TcoTaskTest_B.TriggerRestore();

            tc.RunUntilEndConditionIsMet(() =>
            {
                tc.CallMainFromUnitTest();
                tc._TcoObjectTest_A._TcoTaskTest_A.TriggerInvoke();
                tc._TcoObjectTest_A._TcoTaskTest_B.TriggerInvoke();
                tc._TcoObjectTest_A.CallTaskInstancies();
                tc._TcoObjectTest_A._TcoTaskTest_A.ReadOutState();
                tc._TcoObjectTest_A._TcoTaskTest_B.ReadOutState();
                finished = tc._TcoObjectTest_A._TcoTaskTest_A._IsDone.Synchron &&
                            tc._TcoObjectTest_A._TcoTaskTest_B._IsDone.Synchron;
            }, () => finished);

            GetCounterValues();

            //Both tasks should be triggered just once
            CheckBothTaskInvokeCount(1);

            //Execution body of the Task A should run exactly cycles_A cycles
            Assert.AreEqual(A_TaskExecuteCount_0 + cycles_A, A_TaskExecuteCount_1);

            //Execution body of the Task B should run exactly cycles_B cycles
            Assert.AreEqual(B_TaskExecuteCount_0 + cycles_B, B_TaskExecuteCount_1);

            //Both tasks starts executing just once
            CheckBothTaskExecuteRECount(1);

            //As the cycles_B is greater then cycle_A, TaskADoneCounter should have a bigger increment than TaskBDoneCounter
            Assert.Greater(A_TaskDoneCount_1 - A_TaskDoneCount_0, B_TaskDoneCount_1 - B_TaskDoneCount_0);
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);

            //Both tasks reach done state just once
            CheckBothTaskDoneRECount(1);
        }

        [Test, Order(305)]
        public void T305_TaskAbortDuringExecutionAndInvoke()
        {
            bool AtaskDone = false;

            tc.AddEmptyCycle();

            tc.RunUntilEndConditionIsMet(() =>
            {
                tc.CallMainFromUnitTest();
                tc._TcoObjectTest_A._TcoTaskTest_A.TriggerInvoke();
                tc._TcoObjectTest_A._TcoTaskTest_B.TriggerInvoke();
                tc._TcoObjectTest_A.CallTaskInstancies();
                tc._TcoObjectTest_A._TcoTaskTest_A.ReadOutState();
                tc._TcoObjectTest_A._TcoTaskTest_B.ReadOutState();
                if(tc._TcoObjectTest_A._TcoTaskTest_A._IsDone.Synchron && !AtaskDone)
                {
                    tc._TcoObjectTest_A._TcoTaskTest_B.TriggerAbort();
                    AtaskDone = true;
                }
                finished = tc._TcoObjectTest_A._TcoTaskTest_A._IsDone.Synchron &&
                            tc._TcoObjectTest_A._TcoTaskTest_B._IsDone.Synchron;

            }, () => finished);

            GetCounterValues();

            //TaskA invoke should be triggered once
            Assert.AreEqual(A_TaskInvokeCount_0 + 1, A_TaskInvokeCount_1);
            Assert.AreEqual(A_TaskInvokeRECount_0 + 1, A_TaskInvokeRECount_1);

            //TaskB invoke should be triggered twice
            Assert.AreEqual(B_TaskInvokeCount_0 + 2, B_TaskInvokeCount_1);
            Assert.AreEqual(B_TaskInvokeRECount_0 + 2, B_TaskInvokeRECount_1);

            //Execution body of the Task A should run exactly cycles_A cycles
            Assert.AreEqual(A_TaskExecuteCount_0 + cycles_A, A_TaskExecuteCount_1);

            //Execution body of the Task B should run exactly cycles_B + cycles_A cycles, as it was Aborted after cycles_A cycles and Invoked again
            Assert.AreEqual(B_TaskExecuteCount_0 + cycles_B + cycles_A, B_TaskExecuteCount_1);

            //TaskA executionshould should start once
            Assert.AreEqual(A_TaskExecuteRECount_0 + 1, A_TaskExecuteRECount_1);

            //TaskB executionshould should start twice
            Assert.AreEqual(B_TaskExecuteRECount_0 + 2, B_TaskExecuteRECount_1);

            //As the taskB is aborted and invoked again after taskA is done, difference between end (_1) and start (_0) counters of the both tasks should be exactle the cycles_B cycles
            Assert.AreEqual(A_TaskDoneCount_1 - A_TaskDoneCount_0 , B_TaskDoneCount_1 - B_TaskDoneCount_0 + cycles_B);
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);

            //Both tasks reach done state just once
            CheckBothTaskDoneRECount(1);
        }

        [Test, Order(310)]
        public void T310_TaskError()
        {
            TcoObjectTest to = tc._TcoObjectTest_A;
            TcoTaskTest tt = tc._TcoObjectTest_A._TcoTaskTest_A;

            tc.AddEmptyCycle();

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();
            });

            tc.MultipleCycleRun(() =>
            {
                tc.CallMainFromUnitTest();
                to.CallTaskInstancies();
                tt.ReadOutState();
            }, 3);

            Assert.IsFalse(tt._IsError.Synchron);
            Assert.IsTrue(tt._IsBusy.Synchron);
            Assert.IsFalse(tt._IsDone.Synchron);

            //Overwriting counter value leads to error 
            tt._CounterValue.Synchron = tt._CounterValue.Synchron + 5;

            tc.SingleCycleRun(() =>
            {
                tc.CallMainFromUnitTest();
                to.CallTaskInstancies();
                tt.ReadOutState();
            });

            Assert.IsTrue(tt._IsError.Synchron);
            Assert.IsFalse(tt._IsBusy.Synchron);
            Assert.IsFalse(tt._IsDone.Synchron);
        }

        [Test, Order(311)]
        public void T311_TaskInvokeAfterErrorNoRestoreNoEmptyCycles()
        {
            TcoObjectTest to = tc._TcoObjectTest_A;
            TcoTaskTest tt = tc._TcoObjectTest_A._TcoTaskTest_A;

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();
            });

            tc.MultipleCycleRun(() =>
            {
                tc.CallMainFromUnitTest();
                to.CallTaskInstancies();
                tt.ReadOutState();
            }, 3);

            Assert.IsTrue(tt._IsError.Synchron);
            Assert.IsFalse(tt._IsBusy.Synchron);
            Assert.IsFalse(tt._IsDone.Synchron);
        }

        [Test, Order(312)]
        public void T312_TaskInvokeAfterErrorNoRestoreOneEmptyCycle()
        {
            TcoObjectTest to = tc._TcoObjectTest_A;
            TcoTaskTest tt = tc._TcoObjectTest_A._TcoTaskTest_A;

            tc.AddEmptyCycle();

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();
            });

            tc.MultipleCycleRun(() =>
            {
                tc.CallMainFromUnitTest();
                to.CallTaskInstancies();
                tt.ReadOutState();
            }, 3);

            Assert.IsTrue(tt._IsError.Synchron);
            Assert.IsFalse(tt._IsBusy.Synchron);
            Assert.IsFalse(tt._IsDone.Synchron);
        }

        [Test, Order(313)]
        public void T313_TaskInvokeAfterErrorWithRestore()
        {
            TcoObjectTest to = tc._TcoObjectTest_A;
            TcoTaskTest tt = tc._TcoObjectTest_A._TcoTaskTest_A;

            tc.SingleCycleRun(() =>
            {
                tt.TriggerRestore();
                tt.TriggerInvoke();
                tc.CallMainFromUnitTest();
                to.CallTaskInstancies();
                tt.ReadOutState();
            });

            Assert.IsFalse(tt._IsError.Synchron);
            Assert.IsTrue(tt._IsBusy.Synchron);
            Assert.IsFalse(tt._IsDone.Synchron);
        }

        [Test, Order(314)]
        public void T314_TaskAbortDuringExecution()
        {
            TcoObjectTest to = tc._TcoObjectTest_A;
            TcoTaskTest tt = tc._TcoObjectTest_A._TcoTaskTest_A;

            tc.SingleCycleRun(() =>{tt.TriggerRestore();});

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();
                tc.CallMainFromUnitTest();
                to.CallTaskInstancies();
                tt.ReadOutState();
            });

            Assert.IsFalse(tt._IsError.Synchron);
            Assert.IsTrue(tt._IsBusy.Synchron);
            Assert.IsFalse(tt._IsDone.Synchron);

            tc.SingleCycleRun(() =>
            {
                tt.TriggerAbort();
                tc.CallMainFromUnitTest();
                to.CallTaskInstancies();
                tt.ReadOutState();
            });

            Assert.IsFalse(tt._IsError.Synchron);
            Assert.IsFalse(tt._IsBusy.Synchron);
            Assert.IsFalse(tt._IsDone.Synchron);
        }

        [Test, Order(315)]
        public void T315_TaskMessage()
        {
            TcoTaskTest tt = tc._TcoObjectTest_A._TcoTaskTest_A;

            string message = "Test error message";

            tt.PostMessage(message);

            Assert.AreEqual(message , tt.GetMessage());
        }

        [Test, Order(316)]
        public void T316_IdentitiesTest()
        {
            TcoObjectTest to = tc._TcoObjectTest_A;
            TcoTaskTest tt = tc._TcoObjectTest_A._TcoTaskTest_A;

            tc._CallMyPlcInstance.Synchron = true;

            Thread.Sleep(300);
            tc._CallMyPlcInstance.Synchron = false;

            tc.ReadOutCycleCounters();

            Assert.Greater(tc._startCycles.Synchron, 0);
            Assert.Greater(tc._endCycles.Synchron, 0);

            to.ReadOutIdentities();
            tt.ReadOutIdentities();

            Assert.AreEqual(tc._MyIdentity.Synchron, to._MyContextIdentity.Synchron);
            Assert.AreEqual(tc._MyIdentity.Synchron, tt._MyContextIdentity.Synchron);

            Assert.AreNotEqual(tc._MyIdentity.Synchron, to._MyIdentity.Synchron);
            Assert.AreNotEqual(tc._MyIdentity.Synchron, tt._MyIdentity.Synchron);

            Assert.AreNotEqual(to._MyIdentity.Synchron , tt._MyIdentity.Synchron);


        }

        [Test, Order(317)]
        public void T317_CheckAutoRestoreProperties()
        {
            TcoStateTest ts;
            bool ParentEnableAutoRestore;
            TcoTaskTest tt_a;
            TcoTaskTest tt_b;

            ts = tc._TcoObjectTest_A._TcoStateTest_A;
            tt_a = ts._TcoTaskTest_A;
            tt_b = ts._TcoTaskTest_B;
            ts.ReadOutAutoRestoreProperties();
            ParentEnableAutoRestore = ts._AutoRestoreToMyChildsEnabled.Synchron;
            tt_a.ReadOutAutoRestoreProperties();
            tt_b.ReadOutAutoRestoreProperties();
            Assert.AreEqual(ParentEnableAutoRestore,tt_a._AutoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ParentEnableAutoRestore,tt_b._AutoRestoreByMyParentEnabled.Synchron);

            ts = tc._TcoObjectTest_A._TcoStateTest_B;
            tt_a = ts._TcoTaskTest_A;
            tt_b = ts._TcoTaskTest_B;
            ts.ReadOutAutoRestoreProperties();
            ParentEnableAutoRestore = ts._AutoRestoreToMyChildsEnabled.Synchron;
            tt_a.ReadOutAutoRestoreProperties();
            tt_b.ReadOutAutoRestoreProperties();
            Assert.AreEqual(ParentEnableAutoRestore, tt_a._AutoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ParentEnableAutoRestore, tt_b._AutoRestoreByMyParentEnabled.Synchron);


            ts = tc._TcoObjectTest_B._TcoStateTest_A;
            tt_a = ts._TcoTaskTest_A;
            tt_b = ts._TcoTaskTest_B;
            ts.ReadOutAutoRestoreProperties();
            ParentEnableAutoRestore = ts._AutoRestoreToMyChildsEnabled.Synchron;
            tt_a.ReadOutAutoRestoreProperties();
            tt_b.ReadOutAutoRestoreProperties();
            Assert.AreEqual(ParentEnableAutoRestore, tt_a._AutoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ParentEnableAutoRestore, tt_b._AutoRestoreByMyParentEnabled.Synchron);

            ts = tc._TcoObjectTest_B._TcoStateTest_B;
            tt_a = ts._TcoTaskTest_A;
            tt_b = ts._TcoTaskTest_B;
            ts.ReadOutAutoRestoreProperties();
            ParentEnableAutoRestore = ts._AutoRestoreToMyChildsEnabled.Synchron;
            tt_a.ReadOutAutoRestoreProperties();
            tt_b.ReadOutAutoRestoreProperties();
            Assert.AreEqual(ParentEnableAutoRestore, tt_a._AutoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ParentEnableAutoRestore, tt_b._AutoRestoreByMyParentEnabled.Synchron);
        }

        [Test, Order(318)]
        public void T318_AutoRestoreOnStateChange()
        {
            TcoStateTest ts_a,ts_b;
            TcoTaskTest tt_a, tt_b;
            short cc_a,cc_b, ps_a,ps_b, ns_a,ns_b;

            ts_a = tc._TcoObjectTest_A._TcoStateTest_A;
            ts_b = tc._TcoObjectTest_A._TcoStateTest_B;
            tt_a = ts_a._TcoTaskTest_A;
            tt_b = ts_b._TcoTaskTest_B;

            ts_a.ReadOutAutoRestoreProperties();
            Assert.IsTrue(ts_a._AutoRestoreToMyChildsEnabled.Synchron);

            ts_b.ReadOutAutoRestoreProperties();
            Assert.IsFalse(ts_b._AutoRestoreToMyChildsEnabled.Synchron);

            ts_a.ReadOutState();
            ps_a = ts_a._MyState.Synchron;

            ts_b.ReadOutState();
            ps_b = ts_b._MyState.Synchron;

            tt_a._CounterSetValue.Synchron = 100;
            tt_b._CounterSetValue.Synchron = 100;
 
            tc.SingleCycleRun(() =>
            {
                tt_a.TriggerRestore();
                tt_b.TriggerRestore();
                tt_a.ReadOutState();
                tt_b.ReadOutState();
            });

            Assert.IsFalse(tt_a._IsBusy.Synchron);
            Assert.IsFalse(tt_b._IsBusy.Synchron);

            tc.AddEmptyCycle();

            tc.SingleCycleRun(() =>
            {
                tt_a.TriggerInvoke();
                tt_b.TriggerInvoke();
                tt_a.ExecutionBody();
                tt_b.ExecutionBody();
                tt_a.ReadOutState();
                tt_b.ReadOutState();
            });

            Assert.IsFalse(tt_a._IsBusy.Synchron);
            Assert.IsTrue(tt_b._IsBusy.Synchron);

            tc.SingleCycleRun(() =>
            {
                tt_a.TriggerInvoke();
                tt_b.TriggerInvoke();
                tt_a.ExecutionBody();
                tt_b.ExecutionBody();
                tt_a.ReadOutState();
                tt_b.ReadOutState();
            });

            Assert.IsTrue(tt_a._IsBusy.Synchron);
            Assert.IsTrue(tt_b._IsBusy.Synchron);

            cc_a = ts_a._OnStateChangeCounter.Synchron;
            ns_a = TestHelpers.RandomNumber(20, 100);
            Assert.AreNotEqual(ps_a, ns_a);

            cc_b = ts_b._OnStateChangeCounter.Synchron;
            ns_b = TestHelpers.RandomNumber(20, 100);
            Assert.AreNotEqual(ps_b, ns_b);

            tc.SingleCycleRun(() =>
            {
                ts_a.TriggerChangeState(ns_a);
                ts_b.TriggerChangeState(ns_b);
                tt_a.ExecutionBody();
                tt_b.ExecutionBody();
            });

            ts_a.ReadOutState();
            Assert.AreEqual(ns_a, ts_a._MyState.Synchron);
            Assert.AreEqual(cc_a + 1, ts_a._OnStateChangeCounter.Synchron);

            ts_b.ReadOutState();
            Assert.AreEqual(ns_b, ts_b._MyState.Synchron);
            Assert.AreEqual(cc_b + 1, ts_b._OnStateChangeCounter.Synchron);

            Assert.IsFalse(tt_a._IsBusy.Synchron);
            Assert.IsTrue(tt_b._IsBusy.Synchron);
        }
    }
}