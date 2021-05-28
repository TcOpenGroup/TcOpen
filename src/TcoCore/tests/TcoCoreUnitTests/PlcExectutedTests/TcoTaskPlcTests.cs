using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCoreTests;
using TcoCore.Testing;
using TcoCore;
using System.Threading;

namespace TcoCoreUnitTests.PlcExecutedTests
{

    public class T03_TcoTaskTests
    {
        TcoTaskTestContext tc = ConnectorFixture.Connector.MAIN._tcoTaskTestContext;

        ushort cycles_A = 7;
        ushort cycles_B = 19;
 
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
        ulong A_onErrorCounter_0;
        ulong A_whileErrorCounter_0;

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
        ulong A_onErrorCounter_1;
        ulong A_whileErrorCounter_1;

        ulong plccycles;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            tc.ExecuteProbeRun(2, (int)eTcoTaskTests.CallTasksBodiesOnly);
        }

        [SetUp]
        public void Setup()
        {

            A_TaskInvokeCount_0 = tc._to_A._sut_A._invokeCounter.Synchron;
            B_TaskInvokeCount_0 = tc._to_A._sut_B._invokeCounter.Synchron;
            A_TaskInvokeRECount_0 = tc._to_A._sut_A._invokeRisingEdgeCounter.Synchron;
            B_TaskInvokeRECount_0 = tc._to_A._sut_B._invokeRisingEdgeCounter.Synchron;
            A_TaskExecuteCount_0 = tc._to_A._sut_A._executeCounter.Synchron;
            B_TaskExecuteCount_0 = tc._to_A._sut_B._executeCounter.Synchron;
            A_TaskExecuteRECount_0 = tc._to_A._sut_A._executeRisingEdgeCounter.Synchron;
            B_TaskExecuteRECount_0 = tc._to_A._sut_B._executeRisingEdgeCounter.Synchron;
            A_TaskDoneCount_0 = tc._to_A._sut_A._doneCounter.Synchron;
            B_TaskDoneCount_0 = tc._to_A._sut_B._doneCounter.Synchron;
            A_TaskDoneRECount_0 = tc._to_A._sut_A._doneRisingEdgeCounter.Synchron;
            B_TaskDoneRECount_0 = tc._to_A._sut_B._doneRisingEdgeCounter.Synchron;
            A_onStartCounter_0 = tc._to_A._sut_A._onStartCounter.Synchron;
            B_onStartCounter_0 = tc._to_A._sut_B._onStartCounter.Synchron;
            A_onErrorCounter_0 = tc._to_A._sut_A._onErrorCounter.Synchron;
            A_whileErrorCounter_0 = tc._to_A._sut_A._whileErrorCounter.Synchron;

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
            A_onErrorCounter_1 = 0;
            A_whileErrorCounter_1 = 0;
            plccycles = 0;
 

            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._plcCycleCounter.Synchron =0 ;

        }

        [TearDown]
        public void TearDown()
        {
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            tc.ExecuteProbeRun(2, (int)eTcoTaskTests.RestoreTasks);
        }


        public void GetCounterValues()
        {

            A_TaskInvokeCount_1 = tc._to_A._sut_A._invokeCounter.Synchron;
            B_TaskInvokeCount_1 = tc._to_A._sut_B._invokeCounter.Synchron;
            A_TaskInvokeRECount_1 = tc._to_A._sut_A._invokeRisingEdgeCounter.Synchron;
            B_TaskInvokeRECount_1 = tc._to_A._sut_B._invokeRisingEdgeCounter.Synchron;
            A_TaskExecuteCount_1 = tc._to_A._sut_A._executeCounter.Synchron;
            B_TaskExecuteCount_1 = tc._to_A._sut_B._executeCounter.Synchron;
            A_TaskExecuteRECount_1 = tc._to_A._sut_A._executeRisingEdgeCounter.Synchron;
            B_TaskExecuteRECount_1 = tc._to_A._sut_B._executeRisingEdgeCounter.Synchron;
            A_TaskDoneCount_1 = tc._to_A._sut_A._doneCounter.Synchron;
            B_TaskDoneCount_1 = tc._to_A._sut_B._doneCounter.Synchron;
            A_TaskDoneRECount_1 = tc._to_A._sut_A._doneRisingEdgeCounter.Synchron;
            B_TaskDoneRECount_1 = tc._to_A._sut_B._doneRisingEdgeCounter.Synchron;
            A_onStartCounter_1 = tc._to_A._sut_A._onStartCounter.Synchron;
            B_onStartCounter_1 = tc._to_A._sut_B._onStartCounter.Synchron;
            A_onErrorCounter_1 = tc._to_A._sut_A._onErrorCounter.Synchron;
            A_whileErrorCounter_1 = tc._to_A._sut_A._whileErrorCounter.Synchron;

            plccycles = tc._plcCycleCounter.Synchron;
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



        [Test, Order((int)eTcoTaskTests.TaskInvokeAndWaitForDone)]
        public void T300_TaskInvokeAndWaitForDone()
        {
            //Both tasks are triggered in the same plc cycle. The Invoke methods of the both tasks are still called cyclically. Task A should reach Done state sooner as the Task B, but it should not restarted again.
            Assert.Greater(cycles_B, cycles_A);
            //Arrange
            tc._to_A._sut_A._counterSetValue.Synchron = cycles_A;    //Assign _CounterSetValue to Task A 
            tc._to_A._sut_B._counterSetValue.Synchron = cycles_B;    //Assign _CounterSetValue to Task B
            //Act
            tc.ExecuteProbeRun((int)eTcoTaskTests.TaskInvokeAndWaitForDone, () => tc._done.Synchron);
            GetCounterValues();                                                         //Readout all counter values from test instance into the _1 variables
            CheckBothTaskInvokeCount(1);                                                //Both tasks should be triggered just once
            //Assert
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

        [Test, Order((int)eTcoTaskTests.TaskInvokeAfterDoneWithNoEmptyCycles)]
        public void T301_TaskInvokeAfterDoneWithNoEmptyCycles()
        {
            //The Invoke methods of the both tasks are still called cyclically, after the both tasks has reached the Done state in the previous test.
            //As no empty cycle was called between this two tests, the both tasks should not change theirs states.
            //Act
            tc.ExecuteProbeRun((int)eTcoTaskTests.TaskInvokeAfterDoneWithNoEmptyCycles, () => tc._done.Synchron);
            //Assert
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

        [Test, Order((int)eTcoTaskTests.TaskInvokeAfterDoneWithOneEmptyCycle)]
        public void T302_TaskInvokeAfterDoneWithOneEmptyCycle()
        {
            //The Invoke methods of the both tasks are still called cyclically, after the both tasks has reached the Done state in the before the previous test.
            //As one empty cycle was called between this two tests, the both tasks should be restarted again even from Done state.
            //Act
            tc.ExecuteProbeRun((int)eTcoTaskTests.TaskInvokeAfterDoneWithOneEmptyCycle, () => tc._done.Synchron);
            //Assert
            GetCounterValues();                                                         //Readout all counter values from test instance into the _1 variables
            CheckBothTaskInvokeCount(1);                                                //Both tasks should be triggered just once
            Assert.AreEqual(A_TaskExecuteCount_0 + cycles_A, A_TaskExecuteCount_1);     //Execution body of the Task A should run exactly cycles_A cycles
            Assert.AreEqual(B_TaskExecuteCount_0 + cycles_B, B_TaskExecuteCount_1);     //Execution body of the Task B should run exactly cycles_B cycles
            CheckBothTaskExecuteRECount(1);                                             //Both tasks starts executing just once
            Assert.Greater(A_TaskDoneCount_1 - A_TaskDoneCount_0, B_TaskDoneCount_1 - B_TaskDoneCount_0);//As the cycles_B is greater then cycle_A, TaskADoneCounter should have a bigger increment than TaskBDoneCounter
            Assert.AreEqual(B_TaskDoneCount_0 + 1, B_TaskDoneCount_1);                  //As the cycles_B is greater then cycle_A, Task A is already in Done state, when Task B just reach it. As the execution is finished, when the both tasks are in Done state, TaskBDoneCounter should increment exactly by one only.
            CheckBothTaskDoneRECount(1);                                                //Both tasks reach done state just once
            Assert.AreEqual(cycles_B, plccycles);                                     //The execution should take exactly cycles_B cycles, as it is greather than cycles_A. 
        }

        [Test, Order((int)eTcoTaskTests.TaskInvokeAfterDoneWithAbortCall)]
        public void T303_TaskInvokeAfterDoneWithAbortCall()
        {
            //The both tasks has reached the Done state in the previous test. The Abort() method are called on both tasks, but as both of them are already in Done state, the Abort() method should not change the state of the tasks.
            //Act
            tc.ExecuteProbeRun((int)eTcoTaskTests.TaskInvokeAfterDoneWithAbortCall, () => tc._done.Synchron);
            //Assert
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

        [Test, Order((int)eTcoTaskTests.TaskInvokeAfterDoneWithRestoreCall)]
        public void T304_TaskInvokeAfterDoneWithRestoreCall()
        {
            //The both tasks has reached the Done state in the before the previous test. The Restore() method should set the tasks into the Idle state from any state.
            //So calling Invoke() method after Restore() method should cause restarting the task.
            //Act
            tc.ExecuteProbeRun((int)eTcoTaskTests.TaskInvokeAfterDoneWithRestoreCall, () => tc._done.Synchron);
            //Assert
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

        [Test, Order((int)eTcoTaskTests.TaskAbortDuringExecutionAndInvoke)]
        public void T305_TaskAbortDuringExecutionAndInvoke()
        {
            //The Invoke() methods of the both tasks are still called cyclically, after the both tasks has reached the Done state in the previous test.
            //As one empty cycle was called between this two tests, the both tasks should be restarted again even from Done state.
            //The task A should finish sooner as task B because of cycles_A < cycles_B.
            //After reaching the Done state on the task A, the Abort() method is triggered on both tasks. This should not affect the task A as it is already in the Done state, 
            //but it should set the task B into the Idle state as it was in the Executing state at the moment of the call of the method Abort() and following Invoke() method are call should start it again.
            //Act
            tc.ExecuteProbeRun((int)eTcoTaskTests.TaskAbortDuringExecutionAndInvoke, () => tc._done.Synchron);
            //Assert
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

        [Test, Order((int)eTcoTaskTests.TaskError)]
        public void T310_TaskError()
        {
            //This test enters task into the Error state by ovewriting internal task counter "from outside", using ThrowWhen() method inside PLC test instance.
            //Arange
            tc._inUint.Synchron = 100;
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoTaskTests.TaskError);
            }
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);                                       //Task should be Busy, not Done, not in Error.
            Assert.IsTrue(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.AreEqual(6, tc._plcCycleCounter.Synchron);

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.TaskError);
            //Assert
            Assert.IsTrue(tc._to_A._sut_A._isError.Synchron);                                        //Task should be in Error, not Done, not Busy.
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.AreEqual(7, tc._plcCycleCounter.Synchron);
            GetCounterValues();
            Assert.AreEqual(A_onErrorCounter_0 + 1, A_onErrorCounter_1);

            //Arange
            cycles_A = 10;
            tc._inUint.Synchron = cycles_A;
            //Act
            tc.ExecuteProbeRun(cycles_A, (int)eTcoTaskTests.TaskError);
            //Assert
            GetCounterValues();
            Assert.AreEqual(A_onErrorCounter_0 + 1, A_onErrorCounter_1);
            Assert.IsTrue((A_whileErrorCounter_1 - A_whileErrorCounter_0) > (A_onErrorCounter_1 - A_onErrorCounter_0));
            Assert.AreEqual(A_whileErrorCounter_0 + cycles_A, A_whileErrorCounter_1);
            Assert.AreEqual(7+cycles_A, tc._plcCycleCounter.Synchron);

        }

        [Test, Order((int)eTcoTaskTests.TaskInvokeAfterErrorNoRestoreNoEmptyCycles)]
        public void T311_TaskInvokeAfterErrorNoRestoreNoEmptyCycles()
        {
            //Task should be in Error state after the previous test.
            //Restart the task by Invoke() call should not restart the task, as it is in the Error state and it must be restored before using Resore() method.
            //Arange
            cycles_A = 5;
            tc._inUint.Synchron = cycles_A;

            //Act
            tc.ExecuteProbeRun((ulong)(1+ cycles_A), (int)eTcoTaskTests.TaskInvokeAfterErrorNoRestoreNoEmptyCycles);

            //Assert
            Assert.IsTrue(tc._to_A._sut_A._isError.Synchron);                                        //Task should stay in Error, not Done, not Busy, as before.
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.AreEqual(1 + cycles_A, tc._plcCycleCounter.Synchron);
        }

        [Test, Order((int)eTcoTaskTests.TaskInvokeAfterErrorNoRestoreOneEmptyCycle)]
        public void T312_TaskInvokeAfterErrorNoRestoreOneEmptyCycle()
        {
            //Task should be in Error state after the before the previous test.
            //Restart the task by Invoke() call should not restart the task nor the empty cycle call, as the task is in the Error state and it must be restored before using Resore() method.

            //Arange
            cycles_A = 5;
            tc._inUint.Synchron = cycles_A;

            //Act
            tc.ExecuteProbeRun((ulong)(2 + cycles_A), (int)eTcoTaskTests.TaskInvokeAfterErrorNoRestoreOneEmptyCycle);
            //Assert
            Assert.IsTrue(tc._to_A._sut_A._isError.Synchron);                                        //Task should stay in Error, not Done, not Busy, as before.
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.AreEqual(2 + cycles_A, tc._plcCycleCounter.Synchron);
        }

        [Test, Order((int)eTcoTaskTests.TaskInvokeAfterErrorWithRestore)]
        public void T313_TaskInvokeAfterErrorWithRestore()
        {
            //Task should be in Error state after the previous tests.
            //Restore method shoukld be the only one way, how to get from the Error state of the task.

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.TaskInvokeAfterErrorWithRestore);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);                                       //Error should be reseted, Task should by in Busy state.
            Assert.IsTrue(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
        }

        [Test, Order((int)eTcoTaskTests.TaskAbortDuringExecution)]
        public void T314_TaskAbortDuringExecution()
        {
            //Act
            tc.ExecuteProbeRun(2, (int)eTcoTaskTests.TaskAbortDuringExecution);

            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);                                       //Task should be Busy, not in Error state, not Done.
            Assert.IsTrue(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.TaskAbortDuringExecution);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);                                       //Task should be not in Error state, not Busy, not Done.
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
        }

        [Test, Order((int)eTcoTaskTests.TaskMessage)]
        public void T315_TaskMessage()
        {
            //Arange
            string message = "Test error message";
            tc._inString.Synchron = message;
            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.TaskMessage);
            //Assert
            Assert.AreEqual(message, tc._retString.Synchron);                              //Check if message apears in the mime.
        }

        [Test, Order((int)eTcoTaskTests.IdentitiesTest)]
        public void T316_IdentitiesTest()
        {
            //Arange
            cycles_A = 300;
            tc._inUint.Synchron = cycles_A;


            //Act
            tc.ExecuteProbeRun((int)eTcoTaskTests.IdentitiesTest,()=>tc._done.Synchron);

            //Assert
            Assert.Greater(tc._startCycles.Synchron, 0);                                //StartCycleCounter should be greather than zero as test instance was running at least 300ms.
            Assert.Greater(tc._endCycles.Synchron, 0);                                  //EndCycleCounter should be greather than zero as test instance was running at least 300ms.
            Assert.AreEqual(tc._myIdentity.Synchron, tc._to_A._myContextIdentity.Synchron);   //Identity of the child's context (to) is the same as the identity of the parent(tc)
            Assert.AreEqual(tc._myIdentity.Synchron, tc._to_A._sut_A._myContextIdentity.Synchron);   //Identity of the grandchild's context (tt) is the same as the identity of the grandparent(tc)
            Assert.AreNotEqual(tc._myIdentity.Synchron, tc._to_A._myIdentity.Synchron);       //Identity of the child(to) is different than the identity of the parent(tc), as they are both unique objects.
            Assert.AreNotEqual(tc._myIdentity.Synchron, tc._to_A._sut_A._myIdentity.Synchron);       //Identity of the grandchild(tt) is different than the identity of its grandparent(tc), as they are both unique objects.
            Assert.AreNotEqual(tc._to_A._myIdentity.Synchron, tc._to_A._sut_A._myIdentity.Synchron);      //Identity of the grandchild(tt) is different than the identity of its parent(to), as they are both unique objects.
        }

        [Test, Order((int)eTcoTaskTests.CheckAutoRestoreProperties)]
        public void T317_CheckAutoRestoreProperties()
        {
            //tc._to_A._tSt_A, tc._to_A._tSt_B, tc._to_B._tSt_A and tc._to_B._tSt_B have different values of the EnableAutoRestore properties
            //Act
            tc.ExecuteProbeRun(5, (int)eTcoTaskTests.CheckAutoRestoreProperties);
            //Assert
            TcoTaskPlcTestState ts;
            TcoTaskPlcTest tt_a;
            TcoTaskPlcTest tt_b;

            //First case tc._tcoObjectTest_A._tcoStateTest_A
            ts = tc._to_A._tSt_A;                                                      //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tTt_A;                                                           //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tTt_B;                                                           //tt_b(Tco_Task) is a child object of the ts(TcoState)
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property.
                tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property. 
                tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Second case tc._tcoObjectTest_A._tcoStateTest_B
            ts = tc._to_A._tSt_B;                                                      //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tTt_A;                                                           //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tTt_B;                                                           //tt_b(Tco_Task) is a child object of the ts(TcoState)
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property.
                tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property. 
                tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Third case tc._tcoObjectTest_B._tcoStateTest_A
            ts = tc._to_B._tSt_A;                                                      //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tTt_A;                                                           //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tTt_B;                                                           //tt_b(Tco_Task) is a child object of the ts(TcoState)
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property.
                tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property. 
                tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Fourth case tc._tcoObjectTest_B._tcoStateTest_B
            ts = tc._to_B._tSt_B;                                                      //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tTt_A;                                                           //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tTt_B;                                                           //tt_b(Tco_Task) is a child object of the ts(TcoState)
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property.
                tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.AreEqual(ts._autoRestoreToMyChildsEnabled.Synchron,                  //The child's inherited auto restore property should be the same as the parent's dedicated auto restore property. 
                tt_b._autoRestoreByMyParentEnabled.Synchron);
        }

        [Test, Order((int)eTcoTaskTests.AutoRestoreOnStateChange)]
        public void T318_AutoRestoreOnStateChange()
        {
            //ts_a has EnableAutoRestore enabled, so his child tt_a should be AutoRestorable and on change of his parent state, it should restore itself.
            //ts_b has EnableAutoRestore disabled, so his child tt_b should not be AutoRestorable and on change of his parent state, it should not restore itself.

            TcoTaskPlcTestState ts_a, ts_b;
            TcoTaskPlcTest tt_a, tt_b;
            short cc_a, cc_b, is_a, is_b, ns_a, ns_b;

            ts_a = tc._to_A._tSt_A;                                                    //ts_a(TcoState) is a parent object for tt_a(Tco_Task).
            ts_b = tc._to_A._tSt_B;                                                    //ts_b(TcoState) is a parent object for tt_b(Tco_Task).

            tt_a = ts_a._tTt_A;                                                         //tt_a(Tco_Task) is a child object of the ts_a(TcoState).
            tt_b = ts_b._tTt_B;                                                         //tt_b(Tco_Task) is a child object of the ts_b(TcoState).

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.AutoRestoreOnStateChange);
            //Assert
            Assert.IsTrue(ts_a._autoRestoreToMyChildsEnabled.Synchron);                 //Check if ts_a has EnableAutoRestore enabled. This property is given by the PLC declaration.
            Assert.IsFalse(ts_b._autoRestoreToMyChildsEnabled.Synchron);                //Check if ts_b has EnableAutoRestore disabled. This property is given by the PLC declaration.
            is_a = ts_a._myState.Synchron;                                              //Store the parent initial state.
            is_b = ts_b._myState.Synchron;                                              //Store the parent initial state.

            //Arange
            tt_a._counterSetValue.Synchron = 100;                                       //Set the counter start value to the task A.
            tt_b._counterSetValue.Synchron = 100;                                       //Set the counter start value to the task B.
            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.AutoRestoreOnStateChange);
            //Assert
            Assert.IsFalse(tt_a._isBusy.Synchron);                                      //Task A should be in the Idle state.                  
            Assert.IsFalse(tt_b._isBusy.Synchron);                                      //Task B should be in the Idle state.                  

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.AutoRestoreOnStateChange);
            //Assert
            Assert.IsFalse(tt_a._isBusy.Synchron);                                      //Task A should be in the Idle state.
            Assert.IsTrue(tt_b._isBusy.Synchron);                                       //Task B should be in the Execution state.

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.AutoRestoreOnStateChange);
            //Assert
            Assert.IsTrue(tt_a._isBusy.Synchron);                                       //Task A should be in the Execution state now.                                       
            Assert.IsTrue(tt_b._isBusy.Synchron);                                       //Task B should be in the Execution state as before.

            //Arange
            cc_a = ts_a._onStateChangeCounter.Synchron;                                 //Store the value of the counter of the OnStateChange() method call of task A.
            ns_a = TestHelpers.RandomNumber((short)(is_a + 1), (short)(5 * (is_a + 1)));//Generate new random value of the new state.
            Assert.AreNotEqual(is_a, ns_a);                                             //New state should be different as the initial state.
            cc_b = ts_b._onStateChangeCounter.Synchron;                                 //Store the value of the counter of the OnStateChange() method call of task A.
            ns_b = TestHelpers.RandomNumber((short)(is_b + 1), (short)(5 * (is_b + 1)));//Generate new random value of the new state.
            Assert.AreNotEqual(is_b, ns_b);                                             //New state should be different as the initial state.
            ts_a._myState.Synchron = ns_a;
            ts_b._myState.Synchron = ns_b;

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.AutoRestoreOnStateChange);
            //Assert
            Assert.AreEqual(ns_a, ts_a._myState.Synchron);                              //Check if the state of the TcoState A has been changed into the new state ns_a.
            Assert.AreEqual(cc_a + 1, ts_a._onStateChangeCounter.Synchron);             //OnStateChange() method should be called just once, as only one change of the state has been performed on the TcoState A.
            Assert.AreEqual(ns_b, ts_b._myState.Synchron);                              //Check if the state of the TcoState B has been changed into the new state ns_b.
            Assert.AreEqual(cc_b + 1, ts_b._onStateChangeCounter.Synchron);             //OnStateChange() method should be called just once, as only one change of the state has been performed on the TcoState B.
            Assert.IsFalse(tt_a._isBusy.Synchron);                                      //Task A should change from the Executing state into the Idle state as Task A is AutoRestorable and its parent has changed its state.          
            Assert.IsTrue(tt_b._isBusy.Synchron);                                       //Task B should stay in the Executing state even if its parent has changed its state, as Task B is not AutoRestorable.          
        }

        [Test, Order((int)eTcoTaskTests.InvokeDisabledTask)]
        public void T320_InvokeDisabledTask()
        {
            //Disabled task is triggered. The task should stay in Ready state.
            //Arrange
            cycles_A = 10;
            tc._inUint.Synchron = cycles_A;
            tc._to_A._sut_A._counterSetValue.Synchron = cycles_A;                      //Assign _CounterSetValue to task
            //Act
            tc.ExecuteProbeRun((ulong)(cycles_A + 1), (int)eTcoTaskTests.InvokeDisabledTask);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isReady.Synchron);
            GetCounterValues();                                                        //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0, A_TaskInvokeCount_1);                 //Task should not be started
            Assert.AreEqual(A_TaskInvokeRECount_0, A_TaskInvokeRECount_1);             //Task should not be started
            Assert.AreEqual(A_TaskExecuteCount_0, A_TaskExecuteCount_1);               //Execution body of the task should not run.
            Assert.AreEqual(A_TaskExecuteRECount_0, A_TaskExecuteRECount_1);           //Task should not even enter into the Busy state.
            Assert.AreEqual(A_TaskDoneRECount_0, A_TaskDoneRECount_1);                 //Task should not even enter into the Done state.
            Assert.AreEqual(1 + cycles_A, plccycles);                                  //The execution should take exactly cycles_A cycles+1.
        }


        [Test, Order((int)eTcoTaskTests.DisableExecutingTask)]
        public void T321_DisableExecutingTask()
        {
            //Enabled task is started and than disabled. task is triggered. The task should get inte the Ready state.
            //Act
            tc.ExecuteProbeRun( 1, (int)eTcoTaskTests.DisableExecutingTask);
            //Assert
            Assert.IsTrue(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isReady.Synchron);

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableExecutingTask);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isError.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isReady.Synchron);
            GetCounterValues();                                                        //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0 + 1, A_TaskInvokeCount_1);             //Task should be started once
            Assert.AreEqual(A_TaskInvokeRECount_0 + 1, A_TaskInvokeRECount_1);         //Task should be started once
            Assert.AreEqual(A_TaskExecuteCount_0 + 1, A_TaskExecuteCount_1);           //Execution body of the task should enter once.
            Assert.AreEqual(A_TaskExecuteRECount_0 + 1, A_TaskExecuteRECount_1);       //Task should enter into the Busy state once.
            Assert.AreEqual(A_TaskDoneRECount_0, A_TaskDoneRECount_1);                 //Task should not enter into the Done state.

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableExecutingTask);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isReady.Synchron);

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableExecutingTask);
            //Assert
            Assert.IsTrue(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isReady.Synchron);
            GetCounterValues();                                                        //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0 + 2, A_TaskInvokeCount_1);             //Task should be started twice
            Assert.AreEqual(A_TaskExecuteCount_0 + 2, A_TaskExecuteCount_1);           //Execution body of the task should enter twice.
            Assert.AreEqual(A_TaskDoneRECount_0, A_TaskDoneRECount_1);                 //Task should not enter into the Done state.

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableExecutingTask);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isReady.Synchron);
            GetCounterValues();
        }


        [Test, Order((int)eTcoTaskTests.InvokeTaskThenDisable)]
        public void T322_InvokeTaskThenDisable()
        {
            //Enabled task is started and than disabled. task is triggered. The task should get inte the Ready state.
            //Arrange
            tc._to_A._sut_A._counterSetValue.Synchron = cycles_A;                                    //Assign _CounterSetValue to task

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.InvokeTaskThenDisable);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isReady.Synchron);
            GetCounterValues();                                                        //Readout all counter values from test instance into the _1 variables
            Assert.AreEqual(A_TaskInvokeCount_0 + 1, A_TaskInvokeCount_1);             //Task should be started once
            Assert.AreEqual(A_TaskInvokeRECount_0 + 1, A_TaskInvokeRECount_1);         //Task should be started once
            Assert.AreEqual(A_TaskExecuteCount_0, A_TaskExecuteCount_1);               //Execution body of the task should not run.
            Assert.AreEqual(A_TaskExecuteRECount_0, A_TaskExecuteRECount_1);           //Task should not even enter into the Busy state.
            Assert.AreEqual(A_TaskDoneRECount_0, A_TaskDoneRECount_1);                 //Task should not even enter into the Done state.
        }


        [Test, Order((int)eTcoTaskTests.DisableTaskInErrorStateEnableAndTriggerAgain)]
        public void T323_DisableTaskInErrorStateEnableAndTriggerAgain()
        {
            //Arrange
            tc._to_A._sut_A._counterSetValue.Synchron = cycles_A;                                    //Assign _CounterSetValue to task

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableTaskInErrorStateEnableAndTriggerAgain);
            //Assert
            Assert.IsTrue(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isReady.Synchron);

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableTaskInErrorStateEnableAndTriggerAgain);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isError.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isReady.Synchron);

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableTaskInErrorStateEnableAndTriggerAgain);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isError.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isReady.Synchron);

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableTaskInErrorStateEnableAndTriggerAgain);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isError.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isReady.Synchron);

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoTaskTests.DisableTaskInErrorStateEnableAndTriggerAgain);
            //Assert
            Assert.IsFalse(tc._to_A._sut_A._isBusy.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isDone.Synchron);
            Assert.IsFalse(tc._to_A._sut_A._isError.Synchron);
            Assert.IsTrue(tc._to_A._sut_A._isReady.Synchron);
        }


        [Test, Order((int)eTcoTaskTests.ElapsedTypeMeasurement)]
        public void T330_ElapsedTypeMeasurement()
        {
            tc._elapsedTimeETA.Synchron = new TimeSpan(0, 0, 0, 0, 50);
            tc._runElapsedTimer.Synchron = false;
            var expected = tc._elapsedTimeETA.Synchron.Add(new TimeSpan(0, 0, 0, 0, 10)); // need to add one PLC cycle duration to the expected.

            tc.ExecuteProbeRun((int)eTcoTaskTests.ElapsedTypeMeasurement, () => tc._to_A._sut_A._taskState.Synchron == (short)eTaskState.Done);
            Assert.AreEqual(expected, tc._elapsedTime.Synchron);
        }
    }
}
