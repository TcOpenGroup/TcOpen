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

        TcoContextTest tc = ConnectorFixture.Connector.MAIN._tcoContextTest_A;

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
        ulong A_onStartCounter_0;
        ulong B_onStartCounter_0;

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
        ulong A_onStartCounter_1;
        ulong B_onStartCounter_1;

        ushort plccycles;


        [SetUp]
        public void Setup()
        {
            finished = false;
            i = 0;

            tc._tcoObjectTest_A._tcoTaskTest_A.Enable();
            tc._tcoObjectTest_A._tcoTaskTest_A.Enable();

            A_TaskInvokeCount_0 = tc._tcoObjectTest_A._tcoTaskTest_A._invokeCounter.Synchron;
            B_TaskInvokeCount_0 = tc._tcoObjectTest_A._tcoTaskTest_B._invokeCounter.Synchron;
            A_TaskInvokeRECount_0 = tc._tcoObjectTest_A._tcoTaskTest_A._invokeRisingEdgeCounter.Synchron;
            B_TaskInvokeRECount_0 = tc._tcoObjectTest_A._tcoTaskTest_B._invokeRisingEdgeCounter.Synchron;
            A_TaskExecuteCount_0 = tc._tcoObjectTest_A._tcoTaskTest_A._executeCounter.Synchron;
            B_TaskExecuteCount_0 = tc._tcoObjectTest_A._tcoTaskTest_B._executeCounter.Synchron;
            A_TaskExecuteRECount_0 = tc._tcoObjectTest_A._tcoTaskTest_A._executeRisingEdgeCounter.Synchron;
            B_TaskExecuteRECount_0 = tc._tcoObjectTest_A._tcoTaskTest_B._executeRisingEdgeCounter.Synchron;
            A_TaskDoneCount_0 = tc._tcoObjectTest_A._tcoTaskTest_A._doneCounter.Synchron;
            B_TaskDoneCount_0 = tc._tcoObjectTest_A._tcoTaskTest_B._doneCounter.Synchron;
            A_TaskDoneRECount_0 = tc._tcoObjectTest_A._tcoTaskTest_A._doneRisingEdgeCounter.Synchron;
            B_TaskDoneRECount_0 = tc._tcoObjectTest_A._tcoTaskTest_B._doneRisingEdgeCounter.Synchron;
            A_onStartCounter_0 = tc._tcoObjectTest_A._tcoTaskTest_A._onStartCounter.Synchron;
            B_onStartCounter_0 = tc._tcoObjectTest_A._tcoTaskTest_B._onStartCounter.Synchron;

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
            A_onStartCounter_1 = 0;
            B_onStartCounter_1 = 0;


            plccycles = 0;

        }

        public void GetCounterValues()
        {
            A_TaskInvokeCount_1 = tc._tcoObjectTest_A._tcoTaskTest_A._invokeCounter.Synchron;
            B_TaskInvokeCount_1 = tc._tcoObjectTest_A._tcoTaskTest_B._invokeCounter.Synchron;
            A_TaskInvokeRECount_1 = tc._tcoObjectTest_A._tcoTaskTest_A._invokeRisingEdgeCounter.Synchron;
            B_TaskInvokeRECount_1 = tc._tcoObjectTest_A._tcoTaskTest_B._invokeRisingEdgeCounter.Synchron;
            A_TaskExecuteCount_1 = tc._tcoObjectTest_A._tcoTaskTest_A._executeCounter.Synchron;
            B_TaskExecuteCount_1 = tc._tcoObjectTest_A._tcoTaskTest_B._executeCounter.Synchron;
            A_TaskExecuteRECount_1 = tc._tcoObjectTest_A._tcoTaskTest_A._executeRisingEdgeCounter.Synchron;
            B_TaskExecuteRECount_1 = tc._tcoObjectTest_A._tcoTaskTest_B._executeRisingEdgeCounter.Synchron;
            A_TaskDoneCount_1 = tc._tcoObjectTest_A._tcoTaskTest_A._doneCounter.Synchron;
            B_TaskDoneCount_1 = tc._tcoObjectTest_A._tcoTaskTest_B._doneCounter.Synchron;
            A_TaskDoneRECount_1 = tc._tcoObjectTest_A._tcoTaskTest_A._doneRisingEdgeCounter.Synchron;
            B_TaskDoneRECount_1 = tc._tcoObjectTest_A._tcoTaskTest_B._doneRisingEdgeCounter.Synchron;
            A_onStartCounter_1 = tc._tcoObjectTest_A._tcoTaskTest_A._onStartCounter.Synchron;
            B_onStartCounter_1 = tc._tcoObjectTest_A._tcoTaskTest_B._onStartCounter.Synchron;

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

            tc._callMyPlcInstance.Synchron = false;                                     //Switch off the cyclical execution of the tc instance 

            tc._tcoObjectTest_A._tcoTaskTest_A._counterSetValue.Synchron = cycles_A;    //Assign _CounterSetValue to Task A 
            tc._tcoObjectTest_A._tcoTaskTest_B._counterSetValue.Synchron = cycles_B;    //Assign _CounterSetValue to Task B

            tc._tcoObjectTest_A._tcoTaskTest_A.TriggerRestore();                        //Restore Task A
            tc._tcoObjectTest_A._tcoTaskTest_B.TriggerRestore();                        //Restore Task B

            tc._tcoObjectTest_A._tcoTaskTest_A.SetPreviousStateToReady();               //Set previous state of the Task A to Ready
            tc._tcoObjectTest_A._tcoTaskTest_B.SetPreviousStateToReady();               //Set previous state of the Task B to Ready

            
            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tc._tcoObjectTest_A._tcoTaskTest_A.TriggerInvoke();                     //Invoke of the Task A is cyclically called, even when Task A is in the Busy state
                tc._tcoObjectTest_A._tcoTaskTest_B.TriggerInvoke();                     //Invoke of the Task B is cyclically called, even when Task B is in the Busy state
                tc._tcoObjectTest_A.CallTaskInstancies();                               //Calling instancies of the Task A and Tak B in the PLC.
                tc._tcoObjectTest_A._tcoTaskTest_A.ReadOutState();                      //Reading out the state of the Task A
                tc._tcoObjectTest_A._tcoTaskTest_B.ReadOutState();                      //Reading out the state of the Task B
                finished = tc._tcoObjectTest_A._tcoTaskTest_A._isDone.Synchron &&       //End condition, execution finished, when the both tasks are in Done state.
                            tc._tcoObjectTest_A._tcoTaskTest_B._isDone.Synchron;
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

            Assert.AreEqual(A_onStartCounter_0 + 1, A_onStartCounter_1);
            Assert.AreEqual(B_onStartCounter_0 + 1, B_onStartCounter_1);
        }


        [Test, Order(301)]
        public void T301_TaskInvokeAfterDoneWithNoEmptyCycles()
        {
            //The Invoke methods of the both tasks are still called cyclically, after the both tasks has reached the Done state in the previous test.
            //As no empty cycle was called between this two tests, the both tasks should not change theirs states.
            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tc._tcoObjectTest_A._tcoTaskTest_A.TriggerInvoke();                     //Invoke of the Task A is cyclically called, even when Task A is in the Done state
                tc._tcoObjectTest_A._tcoTaskTest_B.TriggerInvoke();                     //Invoke of the Task B is cyclically called, even when Task B is in the Done state
                tc._tcoObjectTest_A.CallTaskInstancies();                               //Calling instancies of the Task A and Tak B in the PLC.
                tc._tcoObjectTest_A._tcoTaskTest_A.ReadOutState();                      //Reading out the state of the Task A
                tc._tcoObjectTest_A._tcoTaskTest_B.ReadOutState();                      //Reading out the state of the Task B
                finished = tc._tcoObjectTest_A._tcoTaskTest_A._isDone.Synchron &&       //End condition, execution finished, when the both tasks are in Done state.
                            tc._tcoObjectTest_A._tcoTaskTest_B._isDone.Synchron;
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
            //The Invoke methods of the both tasks are still called cyclically, after the both tasks has reached the Done state in the before the previous test.
            //As one empty cycle was called between this two tests, the both tasks should be restarted again even from Done state.

            tc.AddEmptyCycle();                                                         //Empty cycle between cyclically called the Invoke() methods of the both task should causes restarting the tasks even from Done state.

            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tc._tcoObjectTest_A._tcoTaskTest_A.TriggerInvoke();                     //Invoke of the Task A is cyclically called, even when Task A is in the Busy state
                tc._tcoObjectTest_A._tcoTaskTest_B.TriggerInvoke();                     //Invoke of the Task B is cyclically called, even when Task B is in the Busy state
                tc._tcoObjectTest_A.CallTaskInstancies();                               //Calling instancies of the Task A and Tak B in the PLC.
                tc._tcoObjectTest_A._tcoTaskTest_A.ReadOutState();                      //Reading out the state of the Task A
                tc._tcoObjectTest_A._tcoTaskTest_B.ReadOutState();                      //Reading out the state of the Task B
                finished = tc._tcoObjectTest_A._tcoTaskTest_A._isDone.Synchron &&       //End condition, execution finished, when the both tasks are in Done state.
                            tc._tcoObjectTest_A._tcoTaskTest_B._isDone.Synchron;
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

        [Test, Order(303)]
        public void T303_TaskInvokeAfterDoneWithAbortCall()
        {
            //The both tasks has reached the Done state in the previous test. The Abort() method are called on both tasks, but as both of them are already in Done state, the Abort() method should not change the state of the tasks.

            tc._tcoObjectTest_A._tcoTaskTest_A.TriggerAbort();                          //Triggering the Abort() method on the task A. As it is already in the Done state it should not affect it.
            tc._tcoObjectTest_A._tcoTaskTest_B.TriggerAbort();                          //Triggering the Abort() method on the task B. As it is already in the Done state it should not affect it.

            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tc._tcoObjectTest_A._tcoTaskTest_A.TriggerInvoke();                     //Invoke of the Task A is cyclically called, even when Task A is in the Done state
                tc._tcoObjectTest_A._tcoTaskTest_B.TriggerInvoke();                     //Invoke of the Task B is cyclically called, even when Task B is in the Done state
                tc._tcoObjectTest_A.CallTaskInstancies();                               //Calling instancies of the Task A and Tak B in the PLC.
                tc._tcoObjectTest_A._tcoTaskTest_A.ReadOutState();                      //Reading out the state of the Task A
                tc._tcoObjectTest_A._tcoTaskTest_B.ReadOutState();                      //Reading out the state of the Task B
                finished = tc._tcoObjectTest_A._tcoTaskTest_A._isDone.Synchron &&       //End condition, execution finished, when the both tasks are in Done state.
                            tc._tcoObjectTest_A._tcoTaskTest_B._isDone.Synchron;
            }, () => finished);

            GetCounterValues();                                                         //Readout all counter values from test instance into the _1 variables
            CheckBothTaskInvokeCount(0);                                                //Both tasks should not be triggered again, as both tasks have already reached the Done state and calling the Abort() method should not affect this state
            Assert.AreEqual(A_TaskExecuteCount_0, A_TaskExecuteCount_1);                //Execution body of the Task A  should not run, as it is already done
            Assert.AreEqual(B_TaskExecuteCount_0, B_TaskExecuteCount_1);                //Execution body of the Task B  should not run, as it is already done
            CheckBothTaskExecuteRECount(0);                                             //Neither Task A, nor Task B reach Executing state, as they are already in Done state.
            Assert.AreEqual(A_TaskDoneCount_0 + 1, A_TaskDoneCount_1);                  //Task done counter should increment only be 1, as Task A is already done
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);                  //Task done counter should increment only be 1, as Task B is already done
            CheckBothTaskDoneRECount(0);                                                //Neither Task A, nor Task B reach Done state, as they are already in Done state.
            Assert.AreEqual(1, plccycles);                                              //The execution should take exactly one cycle, end condition is already met before start.
        }

        [Test, Order(304)]
        public void T304_TaskInvokeAfterDoneWithRestoreCall()
        {
            //The both tasks has reached the Done state in the before the previous test. The Restore() method should set the tasks into the Idle state from any state.
            //So calling Invoke() method after Restore() method should cause restarting the task.

            tc._tcoObjectTest_A._tcoTaskTest_A.TriggerRestore();                        //Triggering the Restore() method on the task A that it is already in the Done state should set it into Idle state.
            tc._tcoObjectTest_A._tcoTaskTest_B.TriggerRestore();                        //Triggering the Restore() method on the task B that it is already in the Done state should set it into Idle state.

            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tc._tcoObjectTest_A._tcoTaskTest_A.TriggerInvoke();                     //Invoke of the Task A is cyclically called, even when Task A is in the Done state
                tc._tcoObjectTest_A._tcoTaskTest_B.TriggerInvoke();                     //Invoke of the Task B is cyclically called, even when Task B is in the Done state
                tc._tcoObjectTest_A.CallTaskInstancies();                               //Calling instancies of the Task A and Tak B in the PLC.
                tc._tcoObjectTest_A._tcoTaskTest_A.ReadOutState();                      //Reading out the state of the Task A
                tc._tcoObjectTest_A._tcoTaskTest_B.ReadOutState();                      //Reading out the state of the Task B
                finished = tc._tcoObjectTest_A._tcoTaskTest_A._isDone.Synchron &&       //End condition, execution finished, when the both tasks are in Done state.
                            tc._tcoObjectTest_A._tcoTaskTest_B._isDone.Synchron;
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

        [Test, Order(305)]
        public void T305_TaskAbortDuringExecutionAndInvoke()
        {
            //The Invoke() methods of the both tasks are still called cyclically, after the both tasks has reached the Done state in the previous test.
            //As one empty cycle was called between this two tests, the both tasks should be restarted again even from Done state.
            //The task A should finish sooner as task B because of cycles_A < cycles_B.
            //After reaching the Done state on the task A, the Abort() method is triggered on both tasks. This should not affect the task A as it is already in the Done state, 
            //but it should set the task B into the Idle state as it was in the Executing state at the moment of the call of the method Abort() and following Invoke() method are call should start it again.
            bool AtaskDone = false;

            tc.AddEmptyCycle();                                                         //Empty cycle between cyclically called the Invoke() methods of the both task should causes restarting the tasks even from Done state.

            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tc._tcoObjectTest_A._tcoTaskTest_A.TriggerInvoke();                     //Invoke of the Task A is cyclically called, even when Task A is in the Done state
                tc._tcoObjectTest_A._tcoTaskTest_B.TriggerInvoke();                     //Invoke of the Task B is cyclically called, even when Task B is in the Done state
                tc._tcoObjectTest_A.CallTaskInstancies();                               //Calling instancies of the Task A and Tak B in the PLC.
                tc._tcoObjectTest_A._tcoTaskTest_A.ReadOutState();                      //Reading out the state of the Task A
                tc._tcoObjectTest_A._tcoTaskTest_B.ReadOutState();                      //Reading out the state of the Task B
                if (tc._tcoObjectTest_A._tcoTaskTest_A._isDone.Synchron && !AtaskDone)  //At the moment of this condition is met, Task A should be in the Done state, and Task B should be in th Executing state.
                {
                    tc._tcoObjectTest_A._tcoTaskTest_A.TriggerAbort();                  //Triggering the Abort() method on the task A. As it is already in the Done state it should not affect it.
                    tc._tcoObjectTest_A._tcoTaskTest_B.TriggerAbort();                  //Triggering the Abort() method on the task B. As it is still in th Execution state it should this should set it to Idle.
                    AtaskDone = true;
                }
                finished = tc._tcoObjectTest_A._tcoTaskTest_A._isDone.Synchron &&       //End condition, execution finished, when the both tasks are in Done state.
                            tc._tcoObjectTest_A._tcoTaskTest_B._isDone.Synchron;
            }, () => finished);

            GetCounterValues();                                                         //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0 + 1, A_TaskInvokeCount_1);          //Task A invoke should be triggered only once
            Assert.AreEqual(A_TaskInvokeRECount_0 + 1, A_TaskInvokeRECount_1);        //Task A invoke should be triggered only once
            Assert.AreEqual(B_TaskInvokeCount_0 + 2, B_TaskInvokeCount_1);          //Task B invoke should be triggered twice as it was aborted and started again
            Assert.AreEqual(B_TaskInvokeRECount_0 + 2, B_TaskInvokeRECount_1);        //Task B invoke should be triggered twice as it was aborted and started again
            Assert.AreEqual(A_TaskExecuteCount_0 + cycles_A, A_TaskExecuteCount_1);     //Execution body of the Task A should run exactly cycles_A cycles
            Assert.AreEqual(B_TaskExecuteCount_0 + cycles_B + cycles_A, B_TaskExecuteCount_1);//Execution body of the Task B should run exactly cycles_A +  cycles_B cycles, as it was started again after cycles_A  cycles
            Assert.AreEqual(A_TaskExecuteRECount_0 + 1, A_TaskExecuteRECount_1);        //Task A execution should should start only once
            Assert.AreEqual(B_TaskExecuteRECount_0 + 2, B_TaskExecuteRECount_1);        //Task B execution should should start twice
            Assert.AreEqual(A_TaskDoneCount_1 - A_TaskDoneCount_0,                      //As the taskB is aborted and invoked again after taskA is done, difference between end (_1) and start (_0) counters of the both tasks should be exactle the cycles_B cycles 
                B_TaskDoneCount_1 - B_TaskDoneCount_0 + cycles_B);
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);                  //As the cycles_B is greater then cycle_A, Task A is already in Done state, when Task B just reach it. As the execution is finished, when the both tasks are in Done state, TaskBDoneCounter should increment exactly by one only.
            CheckBothTaskDoneRECount(1);                                                //Both tasks reach done state just once
            Assert.AreEqual(cycles_A + cycles_B, plccycles);                            //The execution should take exactly cycles_A + cycles_B cycles, as Task B takes exactly cycles_B cycles, but it was restarted after cycles_A cycles.
        }

        [Test, Order(310)]
        public void T310_TaskError()
        {
            //This test enters task into the Error state by ovewriting internal task counter "from outside", using ThrowWhen() method inside PLC test instance.

            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;
            ulong onErrorCounter_0 = tt._onErrorCounter.Synchron;
            ulong whileErrorCounter_0 = tt._whileErrorCounter.Synchron;

            tc.AddEmptyCycle();                                                         //Empty cycle between cyclically called the Invoke() methods of the both task should causes restarting the tasks even from Done state.

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke() method should change the task state into the Request state
            });

            tc.MultipleCycleRun(() =>
            {
                to.CallTaskInstancies();                                                //Calling instance body at least once should change task state from Request to Executing
                tt.ReadOutState();
            }, 3);

            Assert.IsFalse(tt._isError.Synchron);                                       //Task should be Busy, not Done, not in Error.
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);

            tt._counterValue.Synchron = tt._counterValue.Synchron + 5;                  //Overwriting the counter value from "outside" force task to Error state

            tc.SingleCycleRun(() =>
            {
                to.CallTaskInstancies();
                tt.ReadOutState();
            });

            Assert.IsTrue(tt._isError.Synchron);                                        //Task should be in Error, not Done, not Busy.
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            ulong onErrorCounter_1 = tt._onErrorCounter.Synchron;
            ulong whileErrorCounter_1 = tt._whileErrorCounter.Synchron;

            Assert.AreEqual(onErrorCounter_0 + 1, onErrorCounter_1);

            cycles_A = 10;

            tc.MultipleCycleRun(() =>
            {
                to.CallTaskInstancies();
                tt.ReadOutState();
            }, cycles_A);

            onErrorCounter_1 = tt._onErrorCounter.Synchron;
            whileErrorCounter_1 = tt._whileErrorCounter.Synchron;

            Assert.AreEqual(onErrorCounter_0 + 1, onErrorCounter_1);
            Assert.IsTrue((whileErrorCounter_1 - whileErrorCounter_0) > (onErrorCounter_1 - onErrorCounter_0));
            Assert.AreEqual(whileErrorCounter_0 + cycles_A, whileErrorCounter_1);

        }

        [Test, Order(311)]
        public void T311_TaskInvokeAfterErrorNoRestoreNoEmptyCycles()
        {
            //Task should be in Error state after the previous test.
            //Restart the task by Invoke() call should not restart the task, as it is in the Error state and it must be restored before using Resore() method.

            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke method should enter the task into the Request state in case of: Idle state or Done state and at least one empty cycle performed.
            });                                                                         //It must not restart the task from the Error state

            tc.MultipleCycleRun(() =>
            {
                to.CallTaskInstancies();
                tt.ReadOutState();
            }, 3);

            Assert.IsTrue(tt._isError.Synchron);                                        //Task should stay in Error, not Done, not Busy, as before.
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
        }

        [Test, Order(312)]
        public void T312_TaskInvokeAfterErrorNoRestoreOneEmptyCycle()
        {
            //Task should be in Error state after the before the previous test.
            //Restart the task by Invoke() call should not restart the task nor the empty cycle call, as the task is in the Error state and it must be restored before using Resore() method.

            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;

            tc.AddEmptyCycle();                                                         //Empty cycle must not affect the Error state of the task

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke must not restart the task from the Error state
            });

            tc.MultipleCycleRun(() =>
            {
                to.CallTaskInstancies();
                tt.ReadOutState();
            }, 3);

            Assert.IsTrue(tt._isError.Synchron);                                        //Task should stay in Error, not Done, not Busy, as before.
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
        }

        [Test, Order(313)]
        public void T313_TaskInvokeAfterErrorWithRestore()
        {
            //Task should be in Error state after the previous tests.
            //Restore method shoukld be the only one way, how to get from the Error state of the task.

            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;

            tc.SingleCycleRun(() =>
            {
                tt.TriggerRestore();                                                    //Restore() method should set the task into the Idle state from any state.
                tt.TriggerInvoke();                                                     //Invoke() method should change the task state into the Request state
                to.CallTaskInstancies();                                                //Calling instance body at least once should change task state from Request to Executing
                tt.ReadOutState();
            });

            Assert.IsFalse(tt._isError.Synchron);                                       //Error should be reseted, Task should by in Busy state.
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
        }

        [Test, Order(314)]
        public void T314_TaskAbortDuringExecution()
        {
            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;

            tc.SingleCycleRun(() => { tt.TriggerRestore(); });                             //Restore() method should set the task into the Idle state from any state.

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke() method should change the task state into the Request state
                to.CallTaskInstancies();                                                //Calling instance body at least once should change task state from Request to Executing
                tt.ReadOutState();
            });

            Assert.IsFalse(tt._isError.Synchron);                                       //Task should be Busy, not in Error state, not Done.
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);

            tc.SingleCycleRun(() =>
            {
                tt.TriggerAbort();                                                      //Calling Abort() method during execution should restore the task ang set it to Idle state.
                to.CallTaskInstancies();
                tt.ReadOutState();
            });

            Assert.IsFalse(tt._isError.Synchron);                                       //Task should be not in Error state, not Busy, not Done.
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
        }

        [Test, Order(315)]
        public void T315_TaskMessage()
        {
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;

            string message = "Test error message";

            tt.PostMessage(message);                                                    //Force the error message to the task instence

            Assert.AreEqual(message, tt.GetMessage());                                 //Check if message apears in the mime.
        }

        [Test, Order(316)]
        public void T316_IdentitiesTest()
        {
            TcoObjectTest to = tc._tcoObjectTest_A;                                     //to is the child of the tc
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;                        //tt is the child of the to and grandchild of the tc

            tc._callMyPlcInstance.Synchron = true;                                      //Switch on the cyclical execution of the tc instance 

            Thread.Sleep(300);                                                          //Time of the cyclical execution of the test instance
            tc._callMyPlcInstance.Synchron = false;                                     //Switch off the cyclical execution of the tc instance 

            tc.ReadOutCycleCounters();                                                  //Read out actual cycle counters values into the test instance

            Assert.Greater(tc._startCycles.Synchron, 0);                                //StartCycleCounter should be greather than zero as test instance was running at least 300ms.
            Assert.Greater(tc._endCycles.Synchron, 0);                                  //EndCycleCounter should be greather than zero as test instance was running at least 300ms.

            to.ReadOutIdentities();                                                     //Readout identities into the test instance
            tt.ReadOutIdentities();                                                     //Readout identities into the test instance

            Assert.AreEqual(tc._myIdentity.Synchron, to._myContextIdentity.Synchron);   //Identity of the child's context (to) is the same as the identity of the parent(tc)
            Assert.AreEqual(tc._myIdentity.Synchron, tt._myContextIdentity.Synchron);   //Identity of the grandchild's context (tt) is the same as the identity of the grandparent(tc)

            Assert.AreNotEqual(tc._myIdentity.Synchron, to._myIdentity.Synchron);       //Identity of the child(to) is different than the identity of the parent(tc), as they are both unique objects.
            Assert.AreNotEqual(tc._myIdentity.Synchron, tt._myIdentity.Synchron);       //Identity of the grandchild(tt) is different than the identity of its grandparent(tc), as they are both unique objects.
            Assert.AreNotEqual(to._myIdentity.Synchron, tt._myIdentity.Synchron);      //Identity of the grandchild(tt) is different than the identity of its parent(to), as they are both unique objects.
        }

        [Test, Order(317)]
        public void T317_CheckAutoRestoreProperties()
        {
            //tc._tcoObjectTest_A._tcoStateTest_A, tc._tcoObjectTest_A._tcoStateTest_B, tc._tcoObjectTest_B._tcoStateTest_A and tc._tcoObjectTest_B._tcoStateTest_B have different values of the EnableAutoRestore properties
            TcoStateTest ts;
            TcoTaskTest tt_a;
            TcoTaskTest tt_b;

            //First case tc._tcoObjectTest_A._tcoStateTest_A
            ts = tc._tcoObjectTest_A._tcoStateTest_A;                                   //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tcoTaskTest_A;                                                   //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tcoTaskTest_B;                                                   //tt_b(Tco_Task) is a child object of the ts(TcoState)
            ts.ReadOutAutoRestoreProperties();                                          //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent
            tt_a.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            tt_b.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property.
                tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property. 
                tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Second case tc._tcoObjectTest_A._tcoStateTest_B
            ts = tc._tcoObjectTest_A._tcoStateTest_B;                                   //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tcoTaskTest_A;                                                   //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tcoTaskTest_B;                                                   //tt_b(Tco_Task) is a child object of the ts(TcoState)
            ts.ReadOutAutoRestoreProperties();                                          //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent
            tt_a.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            tt_b.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property.
                tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property. 
                tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Third case tc._tcoObjectTest_B._tcoStateTest_A
            ts = tc._tcoObjectTest_B._tcoStateTest_A;                                   //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tcoTaskTest_A;                                                   //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tcoTaskTest_B;                                                   //tt_b(Tco_Task) is a child object of the ts(TcoState)
            ts.ReadOutAutoRestoreProperties();                                          //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent
            tt_a.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            tt_b.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property.
                tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property. 
                tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Fourth case tc._tcoObjectTest_B._tcoStateTest_B
            ts = tc._tcoObjectTest_B._tcoStateTest_B;                                   //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tcoTaskTest_A;                                                   //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tcoTaskTest_B;                                                   //tt_b(Tco_Task) is a child object of the ts(TcoState)
            ts.ReadOutAutoRestoreProperties();                                          //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent
            tt_a.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            tt_b.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property.
                tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property. 
                tt_b._autoRestoreByMyParentEnabled.Synchron);
        }

        [Test, Order(318)]
        public void T318_AutoRestoreOnStateChange()
        {
            //ts_a has EnableAutoRestore enabled, so his child tt_a should be AutoRestorable and on change of his parent state, it should restore itself.
            //ts_b has EnableAutoRestore disabled, so his child tt_b should not be AutoRestorable and on change of his parent state, it should not restore itself.

            TcoStateTest ts_a, ts_b;
            TcoTaskTest tt_a, tt_b;
            short cc_a, cc_b, is_a, is_b, ns_a, ns_b;

            ts_a = tc._tcoObjectTest_A._tcoStateTest_A;                                 //ts_a(TcoState) is a parent object for tt_a(Tco_Task).
            ts_b = tc._tcoObjectTest_A._tcoStateTest_B;                                 //ts_b(TcoState) is a parent object for tt_b(Tco_Task).

            tt_a = ts_a._tcoTaskTest_A;                                                 //tt_a(Tco_Task) is a child object of the ts_a(TcoState).
            tt_b = ts_b._tcoTaskTest_B;                                                 //tt_b(Tco_Task) is a child object of the ts_b(TcoState).

            ts_a.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent.
            Assert.IsTrue(ts_a._autoRestoreToMyChildsEnabled.Synchron);                 //Check if ts_a has EnableAutoRestore enabled. This property is given by the PLC declaration.

            ts_b.ReadOutAutoRestoreProperties();                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent.
            Assert.IsFalse(ts_b._autoRestoreToMyChildsEnabled.Synchron);                //Check if ts_b has EnableAutoRestore disabled. This property is given by the PLC declaration.

            ts_a.ReadOutState();                                                        //Readout parent state into the testing instance.
            is_a = ts_a._myState.Synchron;                                              //Store the parent initial state.

            ts_b.ReadOutState();                                                        //Readout parent state into the testing instance.
            is_b = ts_b._myState.Synchron;                                              //Store the parent initial state.

            tt_a._counterSetValue.Synchron = 100;                                       //Set the counter start value to the task A.
            tt_b._counterSetValue.Synchron = 100;                                       //Set the counter start value to the task B.

            tc.SingleCycleRun(() =>
            {
                tt_a.TriggerRestore();                                                  //By calling the method Restore(), the task A should get into the state Idle.
                tt_b.TriggerRestore();                                                  //By calling the method Restore(), the task B should get into the state Idle.
                tt_a.ReadOutState();                                                    //Readout task A state into the testing instance.                                                    
                tt_b.ReadOutState();                                                    //Readout task B state into the testing instance.                                                    
            });

            Assert.IsFalse(tt_a._isBusy.Synchron);                                      //Task A should be in the Idle state.                  
            Assert.IsFalse(tt_b._isBusy.Synchron);                                      //Task B should be in the Idle state.                  

            tc.SingleCycleRun(() =>                                                     //By calling the Invoke() methods on the both tasks, both of them should get into the Request state.
            {                                                                           //By calling the bodies of the both tasks should get from Request into the Executing state.
                tt_a.TriggerInvoke();                                                   //As Task A is AutoRestorable and its body was not called in the previous Plc cycle, its Restore() method is called,
                tt_b.TriggerInvoke();                                                   //so it should be in the Idle state after this Plc cycle.
                tt_a.ExecutionBody();                                                   //As Task B is not AutoRestorable does not matter if its body was called in the previous Plc cycle or not.
                tt_b.ExecutionBody();                                                   //After this Plc cycle it should by in the Execution state.
                tt_a.ReadOutState();
                tt_b.ReadOutState();
            });

            Assert.IsFalse(tt_a._isBusy.Synchron);                                      //Task A should be in the Idle state.
            Assert.IsTrue(tt_b._isBusy.Synchron);                                       //Task B should be in the Execution state.

            tc.SingleCycleRun(() =>
            {
                tt_a.TriggerInvoke();                                                   //Task A needs to be started again, as it was restored before due to its body was not called cyclically in the previous Plc cycle.
                tt_a.ExecutionBody();
                tt_b.ExecutionBody();
                tt_a.ReadOutState();
                tt_b.ReadOutState();
            });

            Assert.IsTrue(tt_a._isBusy.Synchron);                                       //Task A should be in the Execution state now.                                       
            Assert.IsTrue(tt_b._isBusy.Synchron);                                       //Task B should be in the Execution state as before.

            cc_a = ts_a._onStateChangeCounter.Synchron;                                 //Store the value of the counter of the OnStateChange() method call of task A.
            ns_a = TestHelpers.RandomNumber((short)(is_a + 1), (short)(5 * (is_a + 1)));//Generate new random value of the new state.
            Assert.AreNotEqual(is_a, ns_a);                                             //New state should be different as the initial state.

            cc_b = ts_b._onStateChangeCounter.Synchron;                                 //Store the value of the counter of the OnStateChange() method call of task A.
            ns_b = TestHelpers.RandomNumber((short)(is_b + 1), (short)(5 * (is_b + 1)));//Generate new random value of the new state.
            Assert.AreNotEqual(is_b, ns_b);                                             //New state should be different as the initial state.

            tc.SingleCycleRun(() =>
            {
                ts_a.TriggerChangeState(ns_a);                                          //Change the state of the TcoState A into the new state ns_a, different from the initial state is_a.
                ts_b.TriggerChangeState(ns_b);                                          //Change the state of the TcoState B into the new state ns_b, different from the initial state is_b.
                tt_a.ExecutionBody();
                tt_b.ExecutionBody();
            });

            ts_a.ReadOutState();
            Assert.AreEqual(ns_a, ts_a._myState.Synchron);                              //Check if the state of the TcoState A has been changed into the new state ns_a.
            Assert.AreEqual(cc_a + 1, ts_a._onStateChangeCounter.Synchron);             //OnStateChange() method should be called just once, as only one change of the state has been performed on the TcoState A.

            ts_b.ReadOutState();
            Assert.AreEqual(ns_b, ts_b._myState.Synchron);                              //Check if the state of the TcoState B has been changed into the new state ns_b.
            Assert.AreEqual(cc_b + 1, ts_b._onStateChangeCounter.Synchron);             //OnStateChange() method should be called just once, as only one change of the state has been performed on the TcoState B.

            Assert.IsFalse(tt_a._isBusy.Synchron);                                      //Task A should change from the Executing state into the Idle state as Task A is AutoRestorable and its parent has changed its state.          
            Assert.IsTrue(tt_b._isBusy.Synchron);                                       //Task B should stay in the Executing state even if its parent has changed its state, as Task B is not AutoRestorable.          
        }

        [Test, Order(320)]
        public void T320_InvokeDisabledTask()
        {
            //Disabled task is triggered. The task should stay in Ready state.
            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;
            tc._callMyPlcInstance.Synchron = false;                                     //Switch off the cyclical execution of the tc instance 
            tt._counterSetValue.Synchron = cycles_A;                                    //Assign _CounterSetValue to task
            tt.TriggerRestore();                                                        //Restore task
            tt.Disable();                                                               //Disable task

            tc.RunUntilEndConditionIsMet(() =>
            {
                plccycles++;
                tt.TriggerInvoke();                                                     //Invoke of the task is cyclically called, even when task is in the Busy state
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            }, () => plccycles >= cycles_A);

            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tt._isReady.Synchron);
            GetCounterValues();                                                         //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0, A_TaskInvokeCount_1);                 //Task should not be started
            Assert.AreEqual(A_TaskInvokeRECount_0, A_TaskInvokeRECount_1);             //Task should not be started
            Assert.AreEqual(A_TaskExecuteCount_0, A_TaskExecuteCount_1);               //Execution body of the task should not run.
            Assert.AreEqual(A_TaskExecuteRECount_0, A_TaskExecuteRECount_1);           //Task should not even enter into the Busy state.
            Assert.AreEqual(A_TaskDoneRECount_0, A_TaskDoneRECount_1);                 //Task should not even enter into the Done state.
            Assert.AreEqual(cycles_A, plccycles);                                       //The execution should take exactly cycles_A cycles.
        }

        [Test, Order(321)]
        public void T321_DisableExecutingTask()
        {
            //Enabled task is started and than disabled. task is triggered. The task should get inte the Ready state.
            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;
            tc._callMyPlcInstance.Synchron = false;                                     //Switch off the cyclical execution of the tc instance 
            tt._counterSetValue.Synchron = cycles_A;                                    //Assign _CounterSetValue to task

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke of the task is cyclically called, even when task is in the Busy state
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });

            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);

            tc.SingleCycleRun(() =>
            {
                tt.Disable();                                                           //Disable task.
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });

            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsTrue(tt._isError.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            GetCounterValues();                                                        //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0 + 1, A_TaskInvokeCount_1);             //Task should be started once
            Assert.AreEqual(A_TaskInvokeRECount_0 + 1, A_TaskInvokeRECount_1);         //Task should be started once
            Assert.AreEqual(A_TaskExecuteCount_0 + 1, A_TaskExecuteCount_1);           //Execution body of the task should enter once.
            Assert.AreEqual(A_TaskExecuteRECount_0 + 1, A_TaskExecuteRECount_1);       //Task should enter into the Busy state once.
            Assert.AreEqual(A_TaskDoneRECount_0, A_TaskDoneRECount_1);                 //Task should not enter into the Done state.

            tc.SingleCycleRun(() =>
            {
                tt.TriggerRestore();
                tt.Enable();                                                            //Enable task. It should not start again without Invoke() methoh call.
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });

            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tt._isReady.Synchron);
            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke of the task is cyclically called, even when task is in the Busy state
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });

            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            GetCounterValues();                                                        //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0 + 2, A_TaskInvokeCount_1);             //Task should be started twice
            Assert.AreEqual(A_TaskExecuteCount_0 + 2, A_TaskExecuteCount_1);           //Execution body of the task should enter twice.
            Assert.AreEqual(A_TaskDoneRECount_0, A_TaskDoneRECount_1);                 //Task should not enter into the Done state.

            tc.SingleCycleRun(() =>
            {
                tt.TriggerRestore();
                to.CallTaskInstancies();
                tt.ReadOutState();
            });
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tt._isReady.Synchron);
            GetCounterValues();
        }

        [Test, Order(322)]
        public void T322_InvokeTaskThenDisable()
        {
            //Enabled task is started and than disabled. task is triggered. The task should get inte the Ready state.
            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;
            tc._callMyPlcInstance.Synchron = false;                                     //Switch off the cyclical execution of the tc instance 
            tt._counterSetValue.Synchron = cycles_A;                                    //Assign _CounterSetValue to task

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke of the task is cyclically called, even when task is in the Busy state
                tt.Disable();                                                           //Disable task.
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tt._isReady.Synchron);
            GetCounterValues();                                                        //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0 + 1, A_TaskInvokeCount_1);             //Task should be started once
            Assert.AreEqual(A_TaskInvokeRECount_0 + 1, A_TaskInvokeRECount_1);         //Task should be started once
            Assert.AreEqual(A_TaskExecuteCount_0, A_TaskExecuteCount_1);               //Execution body of the task should not run.
            Assert.AreEqual(A_TaskExecuteRECount_0, A_TaskExecuteRECount_1);           //Task should not even enter into the Busy state.
            Assert.AreEqual(A_TaskDoneRECount_0, A_TaskDoneRECount_1);                 //Task should not even enter into the Done state.
        }

        [Test, Order(323)]
        public void T323_DisableTaskInErrorStateEnableAndTriggerAgain()
        {
            TcoObjectTest to = tc._tcoObjectTest_A;
            TcoTaskTest tt = tc._tcoObjectTest_A._tcoTaskTest_A;
            tc._callMyPlcInstance.Synchron = false;                                     //Switch off the cyclical execution of the tc instance 
            tt._counterSetValue.Synchron = cycles_A;                                    //Assign _CounterSetValue to task

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke of the task is cyclically called, even when task is in the Busy state
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);

            tt._counterValue.Synchron = tt._counterValue.Synchron + 5;                  //Overwriting _CounterValue from outside cause entering the task into the Error state.


            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke of the task is cyclically called, even when task is in the Busy state
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsTrue(tt._isError.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);

            tc.SingleCycleRun(() =>
            {
                tt.Disable();                                                           //Disable task.
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsTrue(tt._isError.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);

            tc.SingleCycleRun(() =>
            {
                tt.TriggerInvoke();                                                     //Invoke of the task is cyclically called, even when task is in the Busy state
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsTrue(tt._isError.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);

            tc.SingleCycleRun(() =>
            {
                tt.TriggerRestore();                                                    //Restore is the only way how to get from Error state.
                tt.TriggerInvoke();                                                     //Invoke of the task is cyclically called, even when task is in the Busy state
                to.CallTaskInstancies();                                                //Calling instance of the task in the PLC.
                tt.ReadOutState();                                                      //Reading out the state of the task
            });
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tt._isReady.Synchron);
        }
    }
}

