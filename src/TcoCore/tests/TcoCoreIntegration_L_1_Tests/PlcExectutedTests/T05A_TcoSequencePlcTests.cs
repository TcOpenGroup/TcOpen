using System;
using System.Threading;
using NUnit.Framework;
using TcoCore;
using TcoCoreTests;

namespace TcoCoreUnitTests.PlcExecutedTests
{
    public class T05_TcoSequenceTests
    {
        TcoSequenceTestContext tc = ConnectorFixture.Connector.MAIN._tcoSequenceTestContext;

        protected short initStepId = 32750;
        protected string initStepDescription = "---test---init---";
        protected short lastStepId = 32767;
        protected string lastStepDescription = "This is last step of the sequence";
        protected short cycleCount = 0;
        protected ushort restoreCycleCount_A = 0;
        protected ushort restoreCycleCount_N = 0;
        protected bool running;
        protected ushort numberOfSteps = 0;
        protected ushort initCycle = 100;
        protected ushort cycle = 0;
        protected short reqStep = 50;
        protected short reqStepNotExists = 300;
        protected short childState = 100;
        protected short plcCycle = 0;
        protected ulong onSequencerErrorCount_A = 0;
        protected ulong onSequencerErrorCount_N = 0;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
        }

        [SetUp]
        public void Setup()
        {
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._ClearPlcCycleCounter();
        }

        [TearDown]
        public void TearDown()
        {
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
        }

        [Test, Order((int)eTcoSequenceTests.NumberOfStepsCount)]
        public void T500_NumberOfStepsCount()
        {
            initStepId = 32765;
            initStepDescription = "---test---init---";
            numberOfSteps = 100;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            tc._totalStepNumber.Synchron = numberOfSteps;

            Assert.IsFalse(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.NumberOfStepsCount);
            Assert.IsTrue(tc._arranged.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron); //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual("---test---init---", tc._sut_A._currentStep.Description.Synchron); //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron); //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual("---test---init---", tc._sut_N._currentStep.Description.Synchron); //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared

            //After execution of this method, actual StepId should have value one, as step zero should finished, and sequence
            //should stay in step one, as step one should never finished in this case
            //StepDescritpion should have value "Step 1" as step zero should finished, and step one should never finished in this case
            //Sequencer method GetNumberOffFlowSteps() should return the value equal to value of the variable numberOfSteps
            //as it is number of the calls of the sequencer method Step()
            tc.ExecuteProbeRun((int)eTcoSequenceTests.NumberOfStepsCount, () => tc._done.Synchron);

            //tc._sut_A.UpdateCurrentStepDetails();
            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1. As the StepId uniqueness control has already been performed,
                tc._sut_A._currentStep.ID.Synchron
            ); //the step logic is executed
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes from "Step 0" to "Step 1". As the StepId uniqueness control has already
                tc._sut_A._currentStep.Description.Synchron
            ); //been performed, the step logic is executed.
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //tc._sut_N.UpdateCurrentStepDetails();
            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1. As the StepId uniqueness control has already been performed,
                tc._sut_N._currentStep.ID.Synchron
            ); //the step logic is executed
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes from "Step 0" to "Step 1". As the StepId uniqueness control has already
                tc._sut_N._currentStep.Description.Synchron
            ); //been performed, the step logic is executed.
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_N._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.ExecutionInOnePLCcycle)]
        public void T501_ExecutionInOnePLCcycle()
        {
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.ExecutionInOnePLCcycle);
            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId of the last executed step is equal to numberOfSteps - 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(), //Check if StepDescription of the last executed step is equal to "Step "+value of the variable numberOfSteps - 1
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId of the last executed step is equal to numberOfSteps  - 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(), //Check if StepDescription of the last executed step is equal to "Step "+value of the variable numberOfSteps - 1
                tc._sut_N._currentStep.Description.Synchron
            );
        }

        [Test, Order((int)eTcoSequenceTests.OnStepCompleted)]
        public void T502_OnStepCompleted()
        {
            short psc_A = tc._sut_A._onCompleteStepCount.Synchron; //Store the actual value of the calls of the method PostStepComplete().
            short psc_N = tc._sut_N._onCompleteStepCount.Synchron; //Store the actual value of the calls of the method PostStepComplete().
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.OnStepCompleted);

            Assert.AreEqual(
                psc_A + tc._sut_A._numberOfSteps.Synchron, //The actual value of the call of the method PostStepComplete() should increment exactly by number of the steps in the sequence, as each step is completed.
                tc._sut_A._onCompleteStepCount.Synchron
            );
            Assert.AreEqual(
                psc_N + tc._sut_N._numberOfSteps.Synchron, //The actual value of the call of the method PostStepComplete() should increment exactly by number of the steps in the sequence, as each step is completed.
                tc._sut_N._onCompleteStepCount.Synchron
            );
        }

        [Test, Order((int)eTcoSequenceTests.OnSequenceCompleted)]
        public void T503_OnSequenceCompleted()
        {
            short psc_A = tc._sut_A._onSequenceCompleteCount.Synchron; //Store the actual value of the calls of the method PostSequenceComplete().
            short psc_N = tc._sut_N._onSequenceCompleteCount.Synchron; //Store the actual value of the calls of the method PostSequenceComplete().

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.OnSequenceCompleted);

            Assert.AreEqual(
                psc_A + 1, //The actual value of the call of the method PostStepComplete() should increment exactly by number of the steps in the sequence, as each step is completed.
                tc._sut_A._onSequenceCompleteCount.Synchron
            );
            Assert.AreEqual(
                psc_N + 1, //The actual value of the call of the method PostStepComplete() should increment exactly by number of the steps in the sequence, as each step is completed.
                tc._sut_N._onSequenceCompleteCount.Synchron
            );
        }

        [Test, Order((int)eTcoSequenceTests.RestoreChildBetweenSteps)]
        public void T504_RestoreChildBetweenSteps()
        {
            ulong childStateChangeCount_A = tc._sut_A
                ._myTcoChildState
                ._requestedStateCount
                .Synchron; //Store the actual value of the changes into the requested state.
            ulong childRestoreCount_A = tc._sut_A._myTcoChildState._restoreCount.Synchron; //Store the actual value of the restores.
            ulong childStateChangeCount_N = tc._sut_N
                ._myTcoChildState
                ._requestedStateCount
                .Synchron; //Store the actual value of the changes into the requested state.
            ulong childRestoreCount_N = tc._sut_N._myTcoChildState._restoreCount.Synchron; //Store the actual value of the restores.
            tc._sut_A.SetupChildRequestedState(childState);
            tc._sut_N.SetupChildRequestedState(childState);
            tc._sut_A.SetChildState(childState);
            tc._sut_N.SetChildState(childState);
            Assert.IsFalse(tc._sut_A.IsAutoRestorable());
            Assert.IsTrue(tc._sut_A.HasAutoRestoreEnabled());
            Assert.IsTrue(tc._sut_A.ChildIsAutoRestorable());
            Assert.IsFalse(tc._sut_A.ChildHasAutoRestoreEnabled());
            Assert.IsFalse(tc._sut_N.IsAutoRestorable());
            Assert.IsFalse(tc._sut_N.HasAutoRestoreEnabled());
            Assert.IsFalse(tc._sut_N.ChildIsAutoRestorable());
            Assert.IsFalse(tc._sut_N.ChildHasAutoRestoreEnabled());

            Assert.IsFalse(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RestoreChildBetweenSteps);
            Assert.IsTrue(tc._arranged.Synchron);

            Assert.AreEqual(-1, tc._sut_A.GetChildState()); //As the parent sequencer has AutoRestore Enabled, the child is AutoRestorable.After querying for child state, it is restored to -1, as this child was not called in the previous PLC cycle.
            Assert.AreEqual(childState, tc._sut_N.GetChildState()); // As the parent sequencer has AutoRestore Disabled, the child is NonAutoRestorable.After querying for child state, it should not be restored to -1, and has to stay at its previous state.

            Assert.IsFalse(tc._done.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RestoreChildBetweenSteps);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                childStateChangeCount_A + numberOfSteps,
                tc._sut_A._myTcoChildState._requestedStateCount.Synchron
            );
            Assert.AreEqual(
                childRestoreCount_A + numberOfSteps - 1,
                tc._sut_A._myTcoChildState._restoreCount.Synchron
            );
            Assert.AreEqual(
                childStateChangeCount_N + 1,
                tc._sut_N._myTcoChildState._requestedStateCount.Synchron
            );
            Assert.AreEqual(childRestoreCount_N, tc._sut_N._myTcoChildState._restoreCount.Synchron);

            tc._sut_A.SetChildState(-1);
            tc._sut_N.SetChildState(-1);
        }

        [Test, Order((int)eTcoSequenceTests.OnStateChangeWithRestoreCallInside)]
        public void T505_OnStateChangeWithRestoreCallInside()
        {
            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RestoreSequencers);

            tc._arranged.Synchron = false;
            tc._done.Synchron = false;
            Assert.IsFalse(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.OnStateChangeWithRestoreCallInside);
            Assert.IsTrue(tc._arranged.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.OnStateChangeWithRestoreCallInside);

            Assert.AreEqual(
                1, //Check if StepId changes to 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual("Step 1", tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription changes to Step 1.
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_A._sequencerHasError.Synchron); //Check if seuencer has no error
            Assert.AreEqual(0, tc._sut_A._sequencerErrorId.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes to 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual("Step 1", tc._sut_N._currentStep.Description.Synchron); //Check if StepDescription changes to Step 1.
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_N._sequencerHasError.Synchron); //Check if seuencer has no error
            Assert.AreEqual(0, tc._sut_N._sequencerErrorId.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.OnStateChangeWithRestoreCallInside);

            Assert.AreEqual(
                0, //Check if StepId stays 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                0, //Check if current step status is None.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_A._sequencerHasError.Synchron); //Check if seuencer has no error
            Assert.AreEqual(0, tc._sut_A._sequencerErrorId.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                0, //Check if current step status is None.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_N._sequencerHasError.Synchron); //Check if seuencer has no error
            Assert.AreEqual(0, tc._sut_N._sequencerErrorId.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.MinStepId)]
        public void T506_MinStepId()
        {
            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;
            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            Assert.IsFalse(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MinStepId);
            Assert.IsTrue(tc._arranged.Synchron);

            tc._currentStepId.Synchron = -32768;
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MinStepId);
            Assert.IsTrue(tc._done.Synchron);
            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "STEP_ID TOO LOW: -32768! MINIMAL VALUE POSSIBLE: -32767!!!",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes to the expected error message.
            Assert.AreEqual(
                50, //Check if current step status is Error.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sut_A._sequencerHasError.Synchron); //Check if seuencer has error
            Assert.AreEqual(70, tc._sut_A._sequencerErrorId.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "STEP_ID TOO LOW: -32768! MINIMAL VALUE POSSIBLE: -32767!!!",
                tc._sut_N._currentStep.Description.Synchron
            ); //Check if StepDescription changes to the expected error message.
            Assert.AreEqual(
                50, //Check if current step status is Error.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sut_N._sequencerHasError.Synchron); //Check if seuencer has error
            Assert.AreEqual(70, tc._sut_N._sequencerErrorId.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            tc._arranged.Synchron = false;
            tc._done.Synchron = false;
            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._arranged.Synchron = false;
            tc._done.Synchron = false;

            Assert.IsFalse(tc._sut_A._sequencerHasError.Synchron);
            Assert.IsFalse(tc._sut_N._sequencerHasError.Synchron);

            Assert.IsFalse(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MinStepId);
            Assert.IsTrue(tc._arranged.Synchron);

            tc._currentStepId.Synchron = -32767;
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MinStepId);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId stays at 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual("Step 1", tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription changes to the expected step message.
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_A._sequencerHasError.Synchron); //Check if seuencer has no error
            Assert.AreEqual(0, tc._sut_A._sequencerErrorId.Synchron);

            Assert.AreEqual(
                1, //Check if StepId stays at 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual("Step 1", tc._sut_N._currentStep.Description.Synchron); //Check if StepDescription changes to the expected step message.
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_N._sequencerHasError.Synchron); //Check if seuencer has no error
            Assert.AreEqual(0, tc._sut_N._sequencerErrorId.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.MaxStepId)]
        public void T507_MaxStepId()
        {
            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;
            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            Assert.IsFalse(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MaxStepId);
            Assert.IsTrue(tc._arranged.Synchron);

            tc._currentStepId.Synchron = 32767;
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MaxStepId);

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "STEP_ID TOO HIGH: 32767! MAXIMAL VALUE POSSIBLE: 32766!!!",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes to the expected error message.
            Assert.AreEqual(
                50, //Check if current step status is Error.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sut_A._sequencerHasError.Synchron); //Check if seuencer has error
            Assert.AreEqual(80, tc._sut_A._sequencerErrorId.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "STEP_ID TOO HIGH: 32767! MAXIMAL VALUE POSSIBLE: 32766!!!",
                tc._sut_N._currentStep.Description.Synchron
            ); //Check if StepDescription changes to the expected error message.
            Assert.AreEqual(
                50, //Check if current step status is Error.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sut_N._sequencerHasError.Synchron); //Check if seuencer has error
            Assert.AreEqual(80, tc._sut_N._sequencerErrorId.Synchron);

            tc._arranged.Synchron = false;
            tc._done.Synchron = false;
            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._arranged.Synchron = false;
            tc._done.Synchron = false;

            Assert.IsFalse(tc._sut_A._sequencerHasError.Synchron);
            Assert.IsFalse(tc._sut_N._sequencerHasError.Synchron);

            Assert.IsFalse(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MaxStepId);
            Assert.IsTrue(tc._arranged.Synchron);

            tc._currentStepId.Synchron = 32766;
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MaxStepId);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual("Step 0", tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription changes to the expected step message.
            Assert.AreEqual(
                30, //Check if current step status is Error.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_A._sequencerHasError.Synchron); //Check if seuencer has no error
            Assert.AreEqual(0, tc._sut_A._sequencerErrorId.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual("Step 0", tc._sut_N._currentStep.Description.Synchron); //Check if StepDescription changes to the expected step message.
            Assert.AreEqual(
                30, //Check if current step status is Error.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_N._sequencerHasError.Synchron); //Check if seuencer has no error
            Assert.AreEqual(0, tc._sut_N._sequencerErrorId.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.RequestStepToFirstStepWithStepId0)]
        public void T508_RequestStepToFirstStepWithStepId0()
        {
            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;
            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepToFirstStepWithStepId0);
            Assert.IsTrue(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepToFirstStepWithStepId0);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(5, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual("Step 5", tc._sut_A._currentStep.Description.Synchron);
            Assert.AreEqual(40, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(5, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual("Step 5", tc._sut_N._currentStep.Description.Synchron);
            Assert.AreEqual(40, tc._sut_N._currentStep.Status.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepToFirstStepWithStepId0);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(0, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual("Step 0", tc._sut_A._currentStep.Description.Synchron);
            Assert.AreEqual(30, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(0, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual("Step 0", tc._sut_N._currentStep.Description.Synchron);
            Assert.AreEqual(30, tc._sut_N._currentStep.Status.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.RequestStep)]
        public void T509_RequestStep()
        {
            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStep);
            Assert.IsTrue(tc._arranged.Synchron);
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStep);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(0, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual("Step 0", tc._sut_A._currentStep.Description.Synchron);
            Assert.AreEqual(40, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(0, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual("Step 0", tc._sut_N._currentStep.Description.Synchron);
            Assert.AreEqual(40, tc._sut_N._currentStep.Status.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStep);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(-5, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual("Step -5", tc._sut_A._currentStep.Description.Synchron);
            Assert.AreEqual(30, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(-5, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual("Step -5", tc._sut_N._currentStep.Description.Synchron);
            Assert.AreEqual(30, tc._sut_N._currentStep.Status.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStep);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(0, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual("Step 0", tc._sut_A._currentStep.Description.Synchron);
            Assert.AreEqual(30, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(0, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual("Step 0", tc._sut_N._currentStep.Description.Synchron);
            Assert.AreEqual(30, tc._sut_N._currentStep.Status.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.CheckStepIdUniqueness)]
        public void T510_CheckStepIdUniqueness()
        {
            initStepId = 32750;
            initStepDescription = "---test---init---";
            lastStepId = 32765;
            lastStepDescription = "This is last step of the sequence";
            cycleCount = 0;
            restoreCycleCount_A = 0;
            restoreCycleCount_N = 0;
            numberOfSteps = 0;

            tc.ExecuteProbeRun((int)eTcoSequenceTests.RestoreSequencers, () => tc._done.Synchron);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            tc._sut_A._stepId.Synchron = lastStepId; //Set StepId of the last step in the PLC instance
            tc._sut_A._stepDescription.Synchron = lastStepDescription; //Set StepDescription of the last step in the PLC instance
            tc._sut_N._stepId.Synchron = lastStepId; //Set StepId of the last step in the PLC instance
            tc._sut_N._stepDescription.Synchron = lastStepDescription; //Set StepDescription of the last step in the PLC instance
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniqueness);
            Assert.IsTrue(tc._arranged.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_A._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            //No Step logic is executed, even if entering or transition conditions are met
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniqueness);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription does not change. As the StepId uniqueness control has not yet been performed,
            //the step logic is not executed, even if entering or transition conditions are met
            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not
                tc._sut_N._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron); //Check if StepDescription does not change. As the StepId uniqueness control has not yet been performed,

            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness
            //has already been performed
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniqueness);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                lastStepId, //Check if StepId changes from initStepId to lastStepId. As the StepId uniqueness control has already been performed,
                tc._sut_A._currentStep.ID.Synchron
            ); //the step logic is executed
            Assert.AreEqual(
                lastStepDescription, //Check if StepDescription changes from initStepDescription to lastStepDescription. As the StepId uniqueness control has already
                tc._sut_A._currentStep.Description.Synchron
            ); //been performed, the step logic is executed.
            Assert.AreEqual(
                40, //Check if current step status is Done.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                lastStepId, //Check if StepId changes from initStepId to lastStepId. As the StepId uniqueness control has already been performed,
                tc._sut_N._currentStep.ID.Synchron
            ); //the step logic is executed
            Assert.AreEqual(
                lastStepDescription, //Check if StepDescription changes from initStepDescription to lastStepDescription. As the StepId uniqueness control has already
                tc._sut_N._currentStep.Description.Synchron
            ); //been performed, the step logic is executed.
            Assert.AreEqual(
                40, //Check if current step status is Done.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.RestoreAlreadyCheckedSequence)]
        public void T512_RestoreAlreadyCheckedSequence()
        {
            //After Sequence restore, counting the steps so as the checking the StepId uniqueness has to be performed again
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence);
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc._sut_A._previousNumberOfSteps.Synchron); //Check if the number of the previous steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._previousNumberOfSteps.Synchron); //Check if the number of the previous steps in the sequence was succesfully cleared
        }

        [Test, Order((int)eTcoSequenceTests.NotUniqueStepId)]
        public void T513_NotUniqueStepId()
        {
            initStepId = 200;
            lastStepId = 500;

            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;

            tc._stepId.Synchron = lastStepId;
            tc._enabled.Synchron = true; //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription; //Set the StepDesciption of the last step in the PLC instance

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.NotUniqueStepId);
            Assert.IsTrue(tc._arranged.Synchron);

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            //No Step logic is executed, even if entering or transition conditions are met

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.NotUniqueStepId);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );

            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreNotEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription change from initStepDescription due to StepId uniqueness control error.
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if StepDescription change, due to StepId uniqueness control error to the expected error message
                    lastStepId.ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                tc._sut_A._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                10, //Check if the sequencer error is of the type StepIdHasBeenChanged
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not
                tc._sut_N._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreNotEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron); //Check if StepDescription change from initStepDescription due to StepId uniqueness control error.
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if StepDescription change, due to StepId uniqueness control error to the expected error message
                    lastStepId.ToString(),
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                tc._sut_N._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                10, //Check if the sequencer error is of the type StepIdHasBeenChanged
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness
            //has already been performed
            onSequencerErrorCount_A = //Store actual value of the OnSequencerError counter.
            tc._sut_A._onSequencerErrorCount.Synchron;
            onSequencerErrorCount_N = //Store actual value of the OnSequencerError counter.
            tc._sut_N._onSequencerErrorCount.Synchron;

            tc._sut_A._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness error
            //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._sut_N._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness error
            //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.NotUniqueStepId);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );

            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                10, //Check if the sequencer error is of the type NotUniqueStepId
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(true, tc._sut_A._runOneStep.Synchron); //Check if step logic was not entered due to sequence error, if yes _RunOneStep is to be reseted to false
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            //2Bremoved =>
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if StepDescription change, due to StepId uniqueness control error to the expected error message
                    lastStepId.ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                tc._sut_A._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.GreaterOrEqual(
                onSequencerErrorCount_A + 10, //Check if OnSequencerErrorCount has been incremented by 10-NotUniqueStepId
                tc._sut_A._onSequencerErrorCount.Synchron
            );

            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                10, //Check if the sequencer error is of the type NotUniqueStepId
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(true, tc._sut_A._runOneStep.Synchron); //Check if step logic was not entered due to sequence error, if yes _RunOneStep is to be reseted to false
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            //2Bremoved =>
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if StepDescription change, due to StepId uniqueness control error to the expected error message
                    lastStepId.ToString(),
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                tc._sut_N._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.GreaterOrEqual(
                onSequencerErrorCount_N + 10, //Check if OnSequencerErrorCount has been incremented by 10-NotUniqueStepId
                tc._sut_N._onSequencerErrorCount.Synchron
            );

            tc._done.Synchron = false;
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );

            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc._sut_A._previousNumberOfSteps.Synchron); //Check if the number of the previous steps in the sequence was succesfully cleared

            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._previousNumberOfSteps.Synchron); //Check if the number of the previous steps in the sequence was succesfully cleared
        }

        [Test, Order((int)eTcoSequenceTests.AfterErrorRestore)]
        public void T516_AfterErrorRestore()
        {
            initStepId = 800;
            lastStepId = 32765;

            tc._sut_A._stepId.Synchron = lastStepId;
            tc._sut_A.SetCurrentStep(initStepId, initStepDescription); //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._sut_A._enabled.Synchron = true; //Set step condition of the last step in the PLC instance to enabled
            tc._sut_A._stepDescription.Synchron = lastStepDescription; //Set StepDecription of the last step in the PLC instance
            tc._sut_A._runOneStep.Synchron = false; //Reset one step execution flag in the PLC testing instance
            tc._sut_A._runAllSteps.Synchron = false; //Reset all step execution flag in the PLC testing instance

            tc._sut_N._stepId.Synchron = lastStepId;
            tc._sut_N.SetCurrentStep(initStepId, initStepDescription); //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._sut_N._enabled.Synchron = true; //Set step condition of the last step in the PLC instance to enabled
            tc._sut_N._stepDescription.Synchron = lastStepDescription; //Set StepDecription of the last step in the PLC instance
            tc._sut_N._runOneStep.Synchron = false; //Reset one step execution flag in the PLC testing instance
            tc._sut_N._runAllSteps.Synchron = false; //Reset all step execution flag in the PLC testing instance

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorRestore);
            Assert.IsTrue(tc._arranged.Synchron);

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            //No Step logic is executed, even if entering or transition conditions are met
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorRestore);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(
                initStepDescription, //Check if StepDescription does not change. As the StepId uniqueness control has not yet been performed,
                tc._sut_A._currentStep.Description.Synchron
            ); //the step logic is not executed, even if entering or transition conditions are met
            Assert.AreEqual(
                false, //Check if sequence error has been reseted
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                0, //Check if the sequencer error type has been reseted to the type noerror
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not
                tc._sut_N._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(
                initStepDescription, //Check if StepDescription does not change. As the StepId uniqueness control has not yet been performed,
                tc._sut_N._currentStep.Description.Synchron
            ); //the step logic is not executed, even if entering or transition conditions are met
            Assert.AreEqual(
                false, //Check if sequence error has been reseted
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                0, //Check if the sequencer error type has been reseted to the type noerror
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60

            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness
            //has already been performed
            tc._sut_A._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness error
            //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._sut_N._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness error
            //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorRestore);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(false, tc._sut_A._runOneStep.Synchron); //Check if step logic was entered as sequence error has been already reseted. If _RunOneStep is reseted to false, step logic was entered and executed
            Assert.AreEqual(
                0, //Check if StepId changes from initStepId to first step StepId. As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sut_A._currentStep.ID.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                "Step number 0", //Check if StepDescription changes from initStepDescription to "Step number 0". As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sut_A._currentStep.Description.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                40, //Check if current step status is Done.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(false, tc._sut_N._runOneStep.Synchron); //Check if step logic was entered as sequence error has been already reseted. If _RunOneStep is reseted to false, step logic was entered and executed
            Assert.AreEqual(
                0, //Check if StepId changes from initStepId to first step StepId. As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sut_N._currentStep.ID.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                "Step number 0", //Check if StepDescription changes from initStepDescription to "Step number 0". As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sut_N._currentStep.Description.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                40, //Check if current step status is Done.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //During this third sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness
            //has already been performed
            tc._sut_A._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness error
            //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._sut_N._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness error
            //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorRestore);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(false, tc._sut_A._runOneStep.Synchron); //Check if step logic was entered as sequence error has been already reseted. If _RunOneStep is reseted to false, step logic was entered and executed
            Assert.AreEqual(
                1, //Check if StepId changes from initStepId to first step StepId. As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sut_A._currentStep.ID.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                "Step number 1", //Check if StepDescription changes from initStepDescription to "Step number 0". As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sut_A._currentStep.Description.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                40, //Check if current step status is Done.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(false, tc._sut_N._runOneStep.Synchron); //Check if step logic was entered as sequence error has been already reseted. If _RunOneStep is reseted to false, step logic was entered and executed
            Assert.AreEqual(
                1, //Check if StepId changes from initStepId to first step StepId. As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sut_N._currentStep.ID.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                "Step number 1", //Check if StepDescription changes from initStepDescription to "Step number 0". As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sut_N._currentStep.Description.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                40, //Check if current step status is Done.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.ChangeStepIdDuringExecution)]
        public void T520_ChangeStepIdDuringExecution()
        {
            initStepId = 32767;
            numberOfSteps = 100;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.ChangeStepIdDuringExecution);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared

            //After running this method, sequencer should stay in StepId 1, with StepDescription "Step 1"
            //with step status Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.ChangeStepIdDuringExecution);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1. As the StepId uniqueness control has already been performed,
                tc._sut_A._currentStep.ID.Synchron
            ); //the step logic is executed
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes from "Step 0" to "Step 1". As the StepId uniqueness control has already
                tc._sut_A._currentStep.Description.Synchron
            ); //been performed, the step logic is executed.
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1. As the StepId uniqueness control has already been performed,
                tc._sut_N._currentStep.ID.Synchron
            ); //the step logic is executed
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes from "Step 0" to "Step 1". As the StepId uniqueness control has already
                tc._sut_N._currentStep.Description.Synchron
            ); //been performed, the step logic is executed.
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_N._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId 2, with StepDescription "Step 2"
            //with step status Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.ChangeStepIdDuringExecution);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId changes from 1 to 2.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes from "Step 1" to "Step 2".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId changes from 1 to 2.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes from "Step 1" to "Step 2".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //After running this method, sequencer should stay in error, ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 2=>3"
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.ChangeStepIdDuringExecution);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId stays at value 2 as before
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 2=>3",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 2" to the expected error message
            Assert.AreEqual(
                "ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 2=>3",
                tc._sut_A._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                20, //Check if the sequencer error is of the type StepIdNumberChangedDuringExecution
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId stays at value 2 as before
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 2=>3",
                tc._sut_N._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 2" to the expected error message
            Assert.AreEqual(
                "ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 2=>3",
                tc._sut_N._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                20, //Check if the sequencer error is of the type StepIdNumberChangedDuringExecution
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.CommentOutPartOfRunningSequencer)]
        public void T524_CommentOutPartOfRunningSequencer()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CommentOutPartOfRunningSequencer);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            //with step status Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CommentOutPartOfRunningSequencer);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //After running this method, sequencer should stay in error,
            //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1", as part of th sequencer was commented out
            //so order of the step with StepId 3 changes from order 3 to order 1.

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CommentOutPartOfRunningSequencer);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sut_A._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sut_N._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sut_N._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CommentOutPartOfRunningSequencer);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes from 0 to 3.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 0" to "Step 3".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes from 0 to 3.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 0" to "Step 3".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.UncommentPartOfRunningSequencer)]
        public void T526_UncommentPartOfRunningSequencer()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.UncommentPartOfRunningSequencer);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            //with step status Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.UncommentPartOfRunningSequencer);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //After running this method, sequencer should stay in error,
            //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3", as part of the sequencer was uncommented
            //so order of the step with StepId 3 changes from order 1 to order 3.

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.UncommentPartOfRunningSequencer);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sut_A._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sut_N._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sut_N._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.OpenCloseSequence)]
        public void T530_OpenCloseSequence()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.OpenCloseSequence);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(0, tc._sut_A._currentStep.Order.Synchron);
            Assert.AreEqual(0, tc._sut_N._currentStep.Order.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.OpenCloseSequence);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(1, tc._sut_A.GetOrderOfTheCurrentlyExecutedStep());
            Assert.AreEqual(1, tc._sut_N.GetOrderOfTheCurrentlyExecutedStep());
        }

        [Test, Order((int)eTcoSequenceTests.RequestStepFromLowerToHigher)]
        public void T541_RequestStepFromLowerToHigher()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 100;
            tc._totalStepNumber.Synchron = numberOfSteps;
            reqStep = 50;
            tc._reqStep.Synchron = reqStep;
            reqStepNotExists = 300;
            tc._reqStepNotExists.Synchron = reqStepNotExists;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepFromLowerToHigher);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepFromLowerToHigher);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes to one
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes to "Step 1"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );

            Assert.AreEqual(
                1, //Check if StepId changes to one
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes to "Step 1"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_N._numberOfSteps.Synchron
            );

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepFromLowerToHigher);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                reqStep, //Check if StepId changes to reqStep
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + reqStep.ToString(), //Check if StepDescription changes to "Step " + reqStep
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                reqStep, //Check if StepId changes to reqStep
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + reqStep.ToString(), //Check if StepDescription changes to "Step " + reqStep
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.RequestStepFromHigherToLower)]
        public void T542_RequestStepFromHigherToLower()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepFromHigherToLower);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepFromHigherToLower);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                reqStep + 5, //Check if StepId changes to reqStep + 5
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (reqStep + 5).ToString(), //Check if StepDescription changes to "Step " + reqStep +5
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                reqStep + 5, //Check if StepId changes to reqStep + 5
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (reqStep + 5).ToString(), //Check if StepDescription changes to "Step " + reqStep +5
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepFromHigherToLower);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                reqStep, //Check if StepId changes to reqStep
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + reqStep.ToString(), //Check if StepDescription changes to "Step " + reqStep
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                reqStep, //Check if StepId changes to reqStep
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + reqStep.ToString(), //Check if StepDescription changes to "Step " + reqStep
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.RequestStepToNotExistingStep)]
        public void T544_RequestStepToNotExistingStep()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 100;
            tc._totalStepNumber.Synchron = numberOfSteps;
            reqStep = 50;
            tc._reqStep.Synchron = reqStep;
            reqStepNotExists = 300;
            tc._reqStepNotExists.Synchron = reqStepNotExists;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepToNotExistingStep);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepToNotExistingStep);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron);

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepToNotExistingStep);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron);

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepToNotExistingStep);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if StepDescription changes to the expected error message
                    reqStepNotExists.ToString()
                    + " DOES NOT EXIST",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if messenger returns the expected error message
                    reqStepNotExists.ToString()
                    + " DOES NOT EXIST",
                tc._sut_A._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                50, //Check if the sequencer error is of the type StepWithRequestedIdDoesNotExists
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if StepDescription changes to the expected error message
                    reqStepNotExists.ToString()
                    + " DOES NOT EXIST",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if messenger returns the expected error message
                    reqStepNotExists.ToString()
                    + " DOES NOT EXIST",
                tc._sut_N._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                50, //Check if the sequencer error is of the type StepWithRequestedIdDoesNotExists
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepToNotExistingStep);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);
        }

        [
            Test,
            Order((int)eTcoSequenceTests.RequestStepWhilePreviousRequestStepHasNotBeenYetProcessed)
        ]
        public void T547_RequestStepWhilePreviousRequestStepHasNotBeenYetProcessed()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 100;
            tc._totalStepNumber.Synchron = numberOfSteps;
            reqStep = 50;
            tc._reqStep.Synchron = reqStep;
            reqStepNotExists = 300;
            tc._reqStepNotExists.Synchron = reqStepNotExists;

            tc.ExecuteProbeRun(
                1,
                (int)eTcoSequenceTests.RequestStepWhilePreviousRequestStepHasNotBeenYetProcessed
            );
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(
                1,
                (int)eTcoSequenceTests.RequestStepWhilePreviousRequestStepHasNotBeenYetProcessed
            );
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if StepDescription changes to the expected error message
                    (reqStep + 10).ToString()
                    + " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: "
                    + reqStep.ToString()
                    + " HAS NOT BEEN YET PERFORMED!",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if messenger returns the expected error message
                    (reqStep + 10).ToString()
                    + " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: "
                    + reqStep.ToString()
                    + " HAS NOT BEEN YET PERFORMED!",
                tc._sut_A._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                60, //Check if the sequencer error is of the type SeveralRequestStep
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if StepDescription changes to the expected error message
                    (reqStep + 10).ToString()
                    + " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: "
                    + reqStep.ToString()
                    + " HAS NOT BEEN YET PERFORMED!",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if messenger returns the expected error message
                    (reqStep + 10).ToString()
                    + " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: "
                    + reqStep.ToString()
                    + " HAS NOT BEEN YET PERFORMED!",
                tc._sut_N._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                60, //Check if the sequencer error is of the type SeveralRequestStep
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.DisableStepEnabledAndActiveInPreviousPLCcycle)]
        public void T551_DisableStepEnabledAndActiveInPreviousPLCcycle()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(
                1,
                (int)eTcoSequenceTests.DisableStepEnabledAndActiveInPreviousPLCcycle
            );
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(
                1,
                (int)eTcoSequenceTests.DisableStepEnabledAndActiveInPreviousPLCcycle
            );
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes to one
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes to "Step 1"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );

            Assert.AreEqual(
                1, //Check if StepId changes to one
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes to "Step 1"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_N._numberOfSteps.Synchron
            );

            tc.ExecuteProbeRun(
                1,
                (int)eTcoSequenceTests.DisableStepEnabledAndActiveInPreviousPLCcycle
            );
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                10, //Check if current step status changes to Disabled
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                10, //Check if current step status changes to Disabled
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(
                1,
                (int)eTcoSequenceTests.DisableStepEnabledAndActiveInPreviousPLCcycle
            );
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId changes to two
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes to "Step 2"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId changes to two
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes to "Step 2"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.RequestStepCallingCyclically)]
        public void T560_RequestStepCallingCyclically()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initCycle = 100;
            tc._initReqStepCycle.Synchron = initCycle;
            numberOfSteps = 20;
            tc._totalStepNumber.Synchron = numberOfSteps;
            cycle = 20;
            tc._cycles.Synchron = cycle;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepCallingCyclically);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initCycle, tc._sut_A.GetRequestStepCycle()); //Check it the RequestStepCycle value was succesfully written
            Assert.AreEqual(initCycle, tc._sut_N.GetRequestStepCycle()); //Check it the RequestStepCycle value was succesfully written

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepCallingCyclically);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(0, tc._sut_A.GetRequestStepCycle()); //RequestStepCycle should be zero at this moment, as Open() method has been called, and
            Assert.AreEqual(0, tc._sut_N.GetRequestStepCycle()); //RequestStepCycle should be zero at this moment, as Open() method has been called, and

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepCallingCyclically);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(1, tc._sut_A.GetRequestStepCycle()); //RequestStepCycle should be one at this moment, as OpenSequence() method has been called, and RequestStep() method has been called in the previous PLC cycle
            Assert.AreEqual(1, tc._sut_N.GetRequestStepCycle()); //RequestStepCycle should be one at this moment, as OpenSequence() method has been called, and RequestStep() method has been called in the previous PLC cycle

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepCallingCyclically);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(0, tc._sut_A.GetRequestStepCycle()); //RequestStepCycle should be zero at this moment, as requested step has been already started
            Assert.AreEqual(0, tc._sut_N.GetRequestStepCycle()); //RequestStepCycle should be zero at this moment, as requested step has been already started

            Assert.AreEqual(
                10, //Check if StepId changes to 10
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 10", //Check if StepDescription changes to "Step 10"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                10, //Check if StepId changes to 10
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 10", //Check if StepDescription changes to "Step 10"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepCallingCyclically);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initCycle, tc._sut_A.GetRequestStepCycle()); //Check it the RequestStepCycle value was succesfully written
            Assert.AreEqual(initCycle, tc._sut_N.GetRequestStepCycle()); //Check it the RequestStepCycle value was succesfully written

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.RequestStepCallingCyclically);
            Assert.AreEqual(5, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(0, tc._sut_A._postOpenRequestStepCycle.Synchron); //RequestStepCycle should be zero at this moment, as Open() method has been called, and no active RequestStep() is "waiting in the queue"
            Assert.AreEqual(0, tc._sut_N._postOpenRequestStepCycle.Synchron); //RequestStepCycle should be zero at this moment, as Open() method has been called, and no active RequestStep() is "waiting in the queue"

            Assert.AreEqual(0, tc._sut_A._preCloseRequestStepCycle.Synchron); //RequestStepCycle should be zero at this moment, previous RequestStep() method has been completed, new RequestStep() method has been triggered again, but OpenSequence() method has not yet been called again.
            Assert.AreEqual(0, tc._sut_N._preCloseRequestStepCycle.Synchron); //RequestStepCycle should be zero at this moment, previous RequestStep() method has been completed, new RequestStep() method has been triggered again, but OpenSequence() method has not yet been called again.

            tc.ExecuteProbeRun(
                (ulong)cycle - 1,
                (int)eTcoSequenceTests.RequestStepCallingCyclically
            );
            Assert.AreEqual(cycle + 4, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(1, tc._sut_A._postOpenRequestStepCycle.Synchron); //RequestStepCycle should be one at this moment, as Open() method has been called, and one active RequestStep() to the Step 20 is "waiting in the queue"
            Assert.AreEqual(1, tc._sut_N._postOpenRequestStepCycle.Synchron); //RequestStepCycle should be one at this moment, as Open() method has been called, and one active RequestStep() to the Step 20 is "waiting in the queue"

            Assert.AreEqual(0, tc._sut_A._preCloseRequestStepCycle.Synchron); //RequestStepCycle should be zero at this moment, previous RequestStep() method has been completed, new RequestStep() method has been triggered again, but OpenSequence() method has not yet been called again.
            Assert.AreEqual(0, tc._sut_N._preCloseRequestStepCycle.Synchron); //RequestStepCycle should be zero at this moment, previous RequestStep() method has been completed, new RequestStep() method has been triggered again, but OpenSequence() method has not yet been called again.

            Assert.AreEqual(0, tc._sut_A.GetRequestStepCycle()); //RequestStepCycle should be zero at this moment, previous RequestStep() method has been completed
            //new RequestStep() method has been triggered again, but OpenSequence() method has not yet been called again.
            Assert.AreEqual(0, tc._sut_N.GetRequestStepCycle()); //RequestStepCycle should be zero at this moment, previous RequestStep() method has been completed
            //new RequestStep() method has been triggered again, but OpenSequence() method has not yet been called again.
            Assert.AreEqual(
                20, //Check if StepId changes to twenty
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 20", //Check if StepDescription changes to "Step 20"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                20, //Check if StepId changes to twenty
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 20", //Check if StepDescription changes to "Step 20"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.SetStepMode)]
        public void T570_SetStepMode()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32767;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 30;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.SetStepMode);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron); //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron); //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron); //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron); //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.SetStepMode);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays in 0. As the StepId uniqueness control control has already been performed,
                tc._sut_A._currentStep.ID.Synchron
            ); //the step logic could be executed, but no StepIn() method was called
            Assert.AreEqual(
                "(>Initial step<)", //Check if StepDescription stays in "(>Initial step<)". As the StepId uniqueness control control has already
                tc._sut_A._currentStep.Description.Synchron
            ); //been performed, the step logic could be executed but no StepIn() method was called.
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId stays in 0. As the StepId uniqueness control control has already been performed,
                tc._sut_N._currentStep.ID.Synchron
            ); //the step logic could be executed, but no StepIn() method was called
            Assert.AreEqual(
                "(>Initial step<)", //Check if StepDescription stays in "(>Initial step<)". As the StepId uniqueness control control has already
                tc._sut_N._currentStep.Description.Synchron
            ); //been performed, the step logic could be executed but no StepIn() method was called.
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_N._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.SetStepMode);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays in 0. Step logic was executed, but no another call of the method Step() has not been yet performed
                tc._sut_A._currentStep.ID.Synchron
            ); //so StepId stays at its last value.
            Assert.AreEqual(
                "Initial step", //Check if StepDescription changes from "(>Initial step<)" to "Initial step" as the step logic was executed and step is Done.
                tc._sut_A._currentStep.Description.Synchron
            ); //As no another call of the method Step() has not been yet performed, StepDescription stays at its last value.
            Assert.AreEqual(
                40, //Check if current step status has changed from ReadyToRun to Done.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId stays in 0. Step logic was executed, but no another call of the method Step() has not been yet performed
                tc._sut_N._currentStep.ID.Synchron
            ); //so StepId stays at its last value.
            Assert.AreEqual(
                "Initial step", //Check if StepDescription changes from "(>Initial step<)" to "Initial step" as the step logic was executed and step is Done.
                tc._sut_N._currentStep.Description.Synchron
            ); //As no another call of the method Step() has not been yet performed, StepDescription stays at its last value.
            Assert.AreEqual(
                40, //Check if current step status has changed from ReadyToRun to Done.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.SetStepMode);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "Initial step" to "(>Step 1<)".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status has changed from Done to ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "Initial step" to "(>Step 1<)".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_N._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status has changed from Done to ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.SetStepMode);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "Initial step" to "(>Step 1<)".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status has changed from Done to ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "Initial step" to "(>Step 1<)".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_N._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status has changed from Done to ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepMode)]
        public void T580_StepMode()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            numberOfSteps = 10;
            cycle = 0;
            tc._totalStepNumber.Synchron = numberOfSteps;
            initStepId = 32767;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepMode);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron); //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron); //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron); //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron); //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepMode);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays in 0. As the StepId uniqueness control control has already been performed,
                tc._sut_A._currentStep.ID.Synchron
            ); //the step logic could be executed, but no StepIn() method was called
            Assert.AreEqual(
                "(>Initial step<)", //Check if StepDescription stays in "(>Initial step<)". As the StepId uniqueness control control has already
                tc._sut_A._currentStep.Description.Synchron
            ); //been performed, the step logic could be executed but no StepIn() method was called.
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc.ExecuteProbeRun((ulong)numberOfSteps, (int)eTcoSequencerTests.StepMode);
            Assert.AreEqual(numberOfSteps + 1, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId changes to numberOfSteps - 1.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(), //Check if StepDescription changes to "Step " + numberOfSteps - 1.
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId changes to numberOfSteps - 1.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(), //Check if StepDescription changes to "Step " + numberOfSteps - 1.
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status is ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.InvalidMode)]
        public void T582_InvalidMode()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.InvalidMode);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.InvalidMode);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(numberOfSteps - 1, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(40, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(numberOfSteps - 1, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(),
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(40, tc._sut_N._currentStep.Status.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.InvalidMode);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(numberOfSteps, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + numberOfSteps.ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(30, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(numberOfSteps, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + numberOfSteps.ToString(),
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(30, tc._sut_N._currentStep.Status.Synchron);

            tc._sut_A.SetInvalidMode();
            tc._sut_N.SetInvalidMode();

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.InvalidMode);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(numberOfSteps, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(
                "INVALID MODE OF THE SEQUENCER!!!",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(50, tc._sut_A._currentStep.Status.Synchron);
            Assert.IsTrue(tc._sut_A._sequencerHasError.Synchron);

            Assert.AreEqual(numberOfSteps, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(
                "INVALID MODE OF THE SEQUENCER!!!",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(50, tc._sut_N._currentStep.Status.Synchron);
            Assert.IsTrue(tc._sut_N._sequencerHasError.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.StepInError)]
        public void T583_StepInError()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepInError);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepInError);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(numberOfSteps - 1, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(40, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(numberOfSteps - 1, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(),
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(40, tc._sut_N._currentStep.Status.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepInError);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(numberOfSteps, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + numberOfSteps.ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(30, tc._sut_A._currentStep.Status.Synchron);

            Assert.AreEqual(numberOfSteps, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + numberOfSteps.ToString(),
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(30, tc._sut_N._currentStep.Status.Synchron);

            tc._sut_A.SetCurrentStepToErrorState();
            tc._sut_N.SetCurrentStepToErrorState();

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepInError);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(numberOfSteps, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + numberOfSteps.ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(50, tc._sut_A._currentStep.Status.Synchron);
            Assert.IsFalse(tc._sut_A._sequencerHasError.Synchron);

            Assert.AreEqual(numberOfSteps, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(
                "Step " + numberOfSteps.ToString(),
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(50, tc._sut_N._currentStep.Status.Synchron);
            Assert.IsFalse(tc._sut_N._sequencerHasError.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.CheckStepIdUniquenessStepMode)]
        public void T590_CheckStepIdUniquenessStepMode()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            initStepDescription = "---test---init---";
            tc._currentStepDescription.Synchron = initStepDescription;
            lastStepId = 32758;
            tc._stepId.Synchron = lastStepId;
            lastStepDescription = "This is last step of the sequence";
            tc._stepDescription.Synchron = lastStepDescription;
            cycleCount = 0;
            restoreCycleCount_A = 0;
            restoreCycleCount_N = 0;
            numberOfSteps = 0;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniquenessStepMode);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_A._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniquenessStepMode);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_N._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron); //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,

            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniquenessStepMode);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                0, //Check if StepId changes from initStepId to 0. As the StepId uniqueness control control has already been performed,
                tc._sut_A._currentStep.ID.Synchron
            ); //the step logic is executed
            Assert.AreEqual(
                "(>Step number 0<)", //Check if StepDescription changes from initStepDescription to "(>Step number 0<)". As the StepId uniqueness control control has already
                tc._sut_A._currentStep.Description.Synchron
            ); //been performed, the step logic is executed.
            Assert.AreEqual(
                20, //Check if current step status is Done.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                0, //Check if StepId changes from initStepId to 0. As the StepId uniqueness control control has already been performed,
                tc._sut_N._currentStep.ID.Synchron
            ); //the step logic is executed
            Assert.AreEqual(
                "(>Step number 0<)", //Check if StepDescription changes from initStepDescription to "(>Step number 0<)". As the StepId uniqueness control control has already
                tc._sut_N._currentStep.Description.Synchron
            ); //been performed, the step logic is executed.
            Assert.AreEqual(
                20, //Check if current step status is Done.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniquenessStepMode);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                1, //Check if StepId changes, as StepId uniqueness control control has already been performed
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step number 1<)", //Check if HmiMessage changes, as StepId uniqueness control control has already been performed
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50, Error := 50

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._numberOfSteps.Synchron
            );
            Assert.AreEqual(
                1, //Check if StepId changes, as StepId uniqueness control control has already been performed
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step number 1<)", //Check if HmiMessage changes, as StepId uniqueness control control has already been performed
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50, Error := 50

            restoreCycleCount_A = tc._sut_A._restoreCycleCount.Synchron; //Store the actual sequence restore cycle counter
            restoreCycleCount_N = tc._sut_N._restoreCycleCount.Synchron; //Store the actual sequence restore cycle counter

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniquenessStepMode);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                restoreCycleCount_A + 1, //Check if only one restore of the sequencer was was performed, as it was expected
                tc._sut_A._restoreCycleCount.Synchron
            );
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_A._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            Assert.AreEqual(
                restoreCycleCount_N + 1, //Check if only one restore of the sequencer was was performed, as it was expected
                tc._sut_N._restoreCycleCount.Synchron
            );
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared
        }

        [Test, Order((int)eTcoSequenceTests.NotUniqueStepIdInStepMode)]
        public void T594_NotUniqueStepIdInStepMode()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 200;
            tc._currentStepId.Synchron = initStepId;
            initStepDescription = "---test---init---";
            tc._currentStepDescription.Synchron = initStepDescription;
            lastStepId = 500;
            tc._stepId.Synchron = lastStepId;
            lastStepDescription = "This is last step of the sequence";
            tc._stepDescription.Synchron = lastStepDescription;
            cycleCount = 0;
            restoreCycleCount_A = 0;
            restoreCycleCount_N = 0;
            numberOfSteps = 0;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniquenessStepMode);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness control
            //No Step logic is executed, even if entering or transition conditions are met
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniquenessStepMode);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreNotEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription change from initStepDescription due to StepId uniqueness control control error.
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if StepDescription change, due to StepId uniqueness control error to the expected error message
                    lastStepId.ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                tc._sut_A._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                10, //Check if the sequencer error is of the type UidUniqueness
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed
            tc._sut_A._runOneStep.Synchron = true;
            tc._sut_N._runOneStep.Synchron = true;
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CheckStepIdUniquenessStepMode);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                10, //Check if the sequencer error is of the type NotUniqueStepId
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(true, tc._sut_A._runOneStep.Synchron); //Check if step logic was not entered due to sequence error, if yes _RunOneStep is to be reseted to false
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if StepDescription change, due to StepId uniqueness control control error to the expected error message
                    lastStepId.ToString(),
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                tc._sut_A._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                10, //Check if the sequencer error is of the type NotUniqueStepId
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(true, tc._sut_N._runOneStep.Synchron); //Check if step logic was not entered due to sequence error, if yes _RunOneStep is to be reseted to false
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_N._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if StepDescription change, due to StepId uniqueness control control error to the expected error message
                    lastStepId.ToString(),
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "ERROR NOT UNIQUE STEP_ID "
                    + //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                tc._sut_N._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc._done.Synchron = false;
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );

            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc._sut_A._previousNumberOfSteps.Synchron); //Check if the number of the previous steps in the sequence was succesfully cleared

            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._previousNumberOfSteps.Synchron); //Check if the number of the previous steps in the sequence was succesfully cleared
        }

        [Test, Order((int)eTcoSequenceTests.AfterErrorResetInStepMode)]
        public void T597_AfterErrorResetInStepMode()
        {
            initStepId = 800;
            lastStepId = 32765;

            tc._sut_A._stepId.Synchron = lastStepId;
            tc._sut_A.SetCurrentStep(initStepId, initStepDescription); //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._sut_A._enabled.Synchron = true; //Set step condition of the last step in the PLC instance to enabled
            tc._sut_A._stepDescription.Synchron = lastStepDescription; //Set StepDecription of the last step in the PLC instance
            tc._sut_A._runOneStep.Synchron = false; //Reset one step execution flag in the PLC testing instance
            tc._sut_A._runAllSteps.Synchron = false; //Reset all step execution flag in the PLC testing instance

            tc._sut_N._stepId.Synchron = lastStepId;
            tc._sut_N.SetCurrentStep(initStepId, initStepDescription); //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._sut_N._enabled.Synchron = true; //Set step condition of the last step in the PLC instance to enabled
            tc._sut_N._stepDescription.Synchron = lastStepDescription; //Set StepDecription of the last step in the PLC instance
            tc._sut_N._runOneStep.Synchron = false; //Reset one step execution flag in the PLC testing instance
            tc._sut_N._runAllSteps.Synchron = false; //Reset all step execution flag in the PLC testing instance

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorResetInStepMode);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            //No Step logic is executed, even if entering or transition conditions are met
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorResetInStepMode);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(
                initStepDescription, //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,
                tc._sut_A._currentStep.Description.Synchron
            ); //the step logic is not executed, even if entering or transition conditions are met
            Assert.AreEqual(
                false, //Check if sequence error has been reseted
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                0, //Check if the sequencer error type has been reseted to the type noerror
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_N._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(
                initStepDescription, //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,
                tc._sut_N._currentStep.Description.Synchron
            ); //the step logic is not executed, even if entering or transition conditions are met
            Assert.AreEqual(
                false, //Check if sequence error has been reseted
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                0, //Check if the sequencer error type has been reseted to the type noerror
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60

            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed

            tc._sut_A._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness control error
            //In case of StepId uniqueness control error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._sut_N._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness control error
            //In case of StepId uniqueness control error occured it stays true, that means no entrance into Step body in the PLC has been performed

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorResetInStepMode);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                0, //Check if StepId changes from initStepId to first StepId. As the sequence error has been already reseted and the StepId uniqueness control control has been
                tc._sut_A._currentStep.ID.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                "(>Step number 0<)", //Check if StepDescription changes from initStepDescription to "(>Step number 0<)". As the sequence error has been already reseted and the StepId uniqueness control control has been
                tc._sut_A._currentStep.Description.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                0, //Check if StepId changes from initStepId to first StepId. As the sequence error has been already reseted and the StepId uniqueness control control has been
                tc._sut_N._currentStep.ID.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                "(>Step number 0<)", //Check if StepDescription changes from initStepDescription to "(>Step number 0<)". As the sequence error has been already reseted and the StepId uniqueness control control has been
                tc._sut_N._currentStep.Description.Synchron
            ); //already performed again after reset, the step logic is executed.
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //During this third sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed
            tc._sut_A._runAllSteps.Synchron = false; //Reset all step execution flag in the PLC testing instance
            tc._sut_A._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness control error
            //In case of StepId uniqueness control error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._sut_N._runAllSteps.Synchron = false; //Reset all step execution flag in the PLC testing instance
            tc._sut_N._runOneStep.Synchron = true; //This should be false after one PLC cycle run in case of not StepId uniqueness control error
            //In case of StepId uniqueness control error occured it stays true, that means no entrance into Step body in the PLC has been performed

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorResetInStepMode);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                0, //Check if StepId changes, to first StepId
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step number 0", //Check if StepId changes to "Step number 0"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status is Done.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                0, //Check if StepId changes, to first StepId
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step number 0", //Check if StepId changes to "Step number 0"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status is Done.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //During this fourth sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.AfterErrorResetInStepMode);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                1, //Check if StepId changes, to 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step number 1<)", //Check if StepId changes to "Step number 1"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status is Done.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                1, //Check if StepId changes, to 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step number 1<)", //Check if StepId changes to "Step number 1"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status is Done.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.InvalidModeDetailed)]
        public void T601_InvalidModeDetailed()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            initStepDescription = "---test---init---";
            tc._currentStepDescription.Synchron = initStepDescription;
            lastStepId = 32758;
            tc._stepId.Synchron = lastStepId;
            lastStepDescription = "This is last step of the sequence";
            tc._stepDescription.Synchron = lastStepDescription;
            cycleCount = 0;
            numberOfSteps = 0;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.InvalidModeDetailed);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_A._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.InvalidModeDetailed);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,
            //the step logic is not executed, even if entering or transition conditions are met

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_N._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron); //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,

            //During this second sequence run, Step logic should be executed under normal condition as steps counting and checking its StepId uniqueness
            //has already been performed. But as the sequencer mode is invalid, Step logic should not be entered.
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.InvalidModeDetailed);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(true, tc._sut_A._runOneStep.Synchron); //Check if step logic was not entered as sequence has IOnvalid mode. If _RunOneStep stays at true, step logic was not entered and executed.
            Assert.AreEqual(
                initStepId, //Check if StepId stays at initStepId. As the sequence has invalid mode the step logic should not be executed.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "INVALID MODE OF THE SEQUENCER!!!", //Check if StepDescription changes from initStepDescription to "INVALID MODE OF THE SEQUENCER!!!".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                50, //Check if current step status is Error.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sut_A._sequencerHasError.Synchron);

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(true, tc._sut_N._runOneStep.Synchron); //Check if step logic was not entered as sequence has IOnvalid mode. If _RunOneStep stays at true, step logic was not entered and executed.
            Assert.AreEqual(
                initStepId, //Check if StepId stays at initStepId. As the sequence has invalid mode the step logic should not be executed.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "INVALID MODE OF THE SEQUENCER!!!", //Check if StepDescription changes from initStepDescription to "INVALID MODE OF THE SEQUENCER!!!".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                50, //Check if current step status is Error.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sut_N._sequencerHasError.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.StepInErrorDetailed)]
        public void T604_StepInErrorDetailed()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            initStepDescription = "---test---init---";
            tc._currentStepDescription.Synchron = initStepDescription;
            lastStepId = 32758;
            tc._stepId.Synchron = lastStepId;
            lastStepDescription = "This is last step of the sequence";
            tc._stepDescription.Synchron = lastStepDescription;
            cycleCount = 0;
            numberOfSteps = 0;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepInErrorDetailed);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_A._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc._sut_N._previousNumberOfSteps.Synchron); //Check if the number of the previous steps was succesfully cleared

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepInErrorDetailed);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_A._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron); //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,
            //the step logic is not executed, even if entering or transition conditions are met

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(
                initStepId, //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not
                tc._sut_N._currentStep.ID.Synchron
            ); //executed, even if entering or transition conditions are met
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron); //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepInErrorDetailed);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreNotEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_A._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_A._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(true, tc._sut_A._runOneStep.Synchron); //Check if step logic was not entered as sequence has IOnvalid mode. If _RunOneStep stays at true, step logic was not entered and executed.

            Assert.AreEqual(
                initStepId, //Check if StepId stays at initStepId. As the sequence has invalid mode the step logic should not be executed.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if StepDescription stays at initStepDescription.
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                50, //Check if current step status is Error.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_A._sequencerHasError.Synchron);

            Assert.AreNotEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(
                tc._sut_N._numberOfSteps.Synchron, //Check if number of the stored steps is same as the number of the counted steps
                tc._sut_N._previousNumberOfSteps.Synchron
            );
            Assert.AreEqual(true, tc._sut_N._runOneStep.Synchron); //Check if step logic was not entered as sequence has IOnvalid mode. If _RunOneStep stays at true, step logic was not entered and executed.

            Assert.AreEqual(
                initStepId, //Check if StepId stays at initStepId. As the sequence has invalid mode the step logic should not be executed.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if StepDescription stays at initStepDescription.
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                50, //Check if current step status is Error.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sut_N._sequencerHasError.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.StepModeStepIn)]
        public void T611_StepModeStepIn()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32767;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepIn);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );

            //After running this method, sequencer should stay in StepId 0, with StepDescription "(>Initial step<)"
            //with step status Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepIn);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays 0. As the StepId uniqueness control control has already been performed, the step logic is executed.
                tc._sut_A._currentStep.ID.Synchron
            ); //But as no StepIn() method was called, sequence should stay in step 0
            Assert.AreEqual(
                "(>Initial step<)", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                tc._sut_A._currentStep.Description.Synchron
            ); //already been performed, the step logic is executed.
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId stays 0. As the StepId uniqueness control control has already been performed, the step logic is executed.
                tc._sut_N._currentStep.ID.Synchron
            ); //But as no StepIn() method was called, sequence should stay in step 0
            Assert.AreEqual(
                "(>Initial step<)", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                tc._sut_N._currentStep.Description.Synchron
            ); //already been performed, the step logic is executed.
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            for (cycle = 1; cycle <= 3; cycle++)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepIn);
                Assert.AreEqual(1 + cycle, tc._plcCycleCounter.Synchron);
                Assert.IsFalse(tc._done.Synchron);

                Assert.AreEqual(
                    cycle, //Check if StepId changes in each of the first 3 cycles as StepIn() method is called in each cycle
                    tc._sut_A._currentStep.ID.Synchron
                ); //and first 3 steps are completed immediately as they contain Await() method with value true
                Assert.AreEqual(
                    "(>Step "
                        + //Check if StepDescription changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        cycle.ToString()
                        + "<)", //and first 3 steps are completed immediately as they contain Await() method with value true
                    tc._sut_A._currentStep.Description.Synchron
                );
                Assert.AreEqual(
                    20, //Check if current step status is ReadyToRun.
                    tc._sut_A._currentStep.Status.Synchron
                ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

                Assert.AreEqual(
                    cycle, //Check if StepId changes in each of the first 3 cycles as StepIn() method is called in each cycle
                    tc._sut_N._currentStep.ID.Synchron
                ); //and first 3 steps are completed immediately as they contain Await() method with value true
                Assert.AreEqual(
                    "(>Step "
                        + //Check if StepDescription changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        cycle.ToString()
                        + "<)", //and first 3 steps are completed immediately as they contain Await() method with value true
                    tc._sut_N._currentStep.Description.Synchron
                );
                Assert.AreEqual(
                    20, //Check if current step status is ReadyToRun.
                    tc._sut_N._currentStep.Status.Synchron
                ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            }
            for (cycle = 4; cycle < numberOfSteps; cycle++)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepIn);
                Assert.AreEqual(1 + cycle, tc._plcCycleCounter.Synchron);
                Assert.IsFalse(tc._done.Synchron);

                Assert.AreEqual(
                    3, //Check if StepId changes to 3.
                    tc._sut_A._currentStep.ID.Synchron
                ); //But as no StepIn() method was called, sequence should stay in step 0
                Assert.AreEqual(
                    "Step 3", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                    tc._sut_A._currentStep.Description.Synchron
                ); //already been performed, the step logic is executed.
                Assert.AreEqual(
                    30, //Check if current step status is ReadyToRun.
                    tc._sut_A._currentStep.Status.Synchron
                ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

                Assert.AreEqual(
                    3, //Check if StepId changes to 3.
                    tc._sut_N._currentStep.ID.Synchron
                ); //But as no StepIn() method was called, sequence should stay in step 0
                Assert.AreEqual(
                    "Step 3", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                    tc._sut_N._currentStep.Description.Synchron
                ); //already been performed, the step logic is executed.
                Assert.AreEqual(
                    30, //Check if current step status is ReadyToRun.
                    tc._sut_N._currentStep.Status.Synchron
                ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            }
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepIn);
            Assert.AreEqual(1 + numberOfSteps, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes to 3.
                tc._sut_A._currentStep.ID.Synchron
            ); //But as no StepIn() method was called, sequence should stay in step 0
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                tc._sut_A._currentStep.Description.Synchron
            ); //already been performed, the step logic is executed.
            Assert.AreEqual(
                30, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes to 3.
                tc._sut_N._currentStep.ID.Synchron
            ); //But as no StepIn() method was called, sequence should stay in step 0
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                tc._sut_N._currentStep.Description.Synchron
            ); //already been performed, the step logic is executed.
            Assert.AreEqual(
                30, //Check if current step status is ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeChangeStepIdDuringExecution)]
        public void T612_StepModeChangeStepIdDuringExecution()
        {
            tc.ExecuteProbeRun(2, (int)eTcoSequenceTests.StepModeChangeStepIdDuringExecution);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                5, //Check if StepId changes to 5
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 3=>5",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 3=>5",
                tc._sut_A._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                20, //Check if the sequencer error is of the type UidNumberChangedDuringExecution
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                5, //Check if StepId changes to 5
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 3=>5",
                tc._sut_N._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 3=>5",
                tc._sut_N._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                20, //Check if the sequencer error is of the type UidNumberChangedDuringExecution
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeChangeStepIdDuringExecution);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.StepModeChangeStepOrder)]
        public void T615_StepModeChangeStepOrder()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32767;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeChangeStepOrder);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );

            //After running this method, sequencer should stay in StepId 0, with StepDescription "(>Initial step<)"
            //with step status Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeChangeStepOrder);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays 0. As the StepId uniqueness control control has already been performed, the step logic is executed.
                tc._sut_A._currentStep.ID.Synchron
            ); //But as no StepIn() method was called, sequence should stay in step 0
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes to "(>Step 0<)". As the StepId uniqueness control control has
                tc._sut_A._currentStep.Description.Synchron
            ); //already been performed, the step logic is executed.
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId stays 0. As the StepId uniqueness control control has already been performed, the step logic is executed.
                tc._sut_N._currentStep.ID.Synchron
            ); //But as no StepIn() method was called, sequence should stay in step 0
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes to "(>Step 0<)". As the StepId uniqueness control control has
                tc._sut_N._currentStep.Description.Synchron
            ); //already been performed, the step logic is executed.
            Assert.AreEqual(
                20, //Check if current step status is ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            for (cycle = 1; cycle <= 3; cycle++)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeChangeStepOrder);
                Assert.AreEqual(1 + cycle, tc._plcCycleCounter.Synchron);
                Assert.IsFalse(tc._done.Synchron);

                Assert.AreEqual(
                    cycle, //Check if StepId changes in each of the first 3 cycles as StepIn() method is called in each cycle
                    tc._sut_A._currentStep.ID.Synchron
                ); //and first 3 steps are completed immediately as they contain Await() method with value true
                Assert.AreEqual(
                    "(>Step "
                        + //Check if StepDescription changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        cycle.ToString()
                        + "<)", //and first 3 steps are completed immediately as they contain Await() method with value true
                    tc._sut_A._currentStep.Description.Synchron
                );
                Assert.AreEqual(
                    20, //Check if current step status is ReadyToRun.
                    tc._sut_A._currentStep.Status.Synchron
                ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

                Assert.AreEqual(
                    cycle, //Check if StepId changes in each of the first 3 cycles as StepIn() method is called in each cycle
                    tc._sut_N._currentStep.ID.Synchron
                ); //and first 3 steps are completed immediately as they contain Await() method with value true
                Assert.AreEqual(
                    "(>Step "
                        + //Check if StepDescription changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        cycle.ToString()
                        + "<)", //and first 3 steps are completed immediately as they contain Await() method with value true
                    tc._sut_N._currentStep.Description.Synchron
                );
                Assert.AreEqual(
                    20, //Check if current step status is ReadyToRun.
                    tc._sut_N._currentStep.Status.Synchron
                ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            }
            for (cycle = 4; cycle < numberOfSteps; cycle++)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeChangeStepOrder);
                Assert.AreEqual(1 + cycle, tc._plcCycleCounter.Synchron);
                Assert.IsFalse(tc._done.Synchron);

                Assert.AreEqual(
                    3, //Check if StepId changes to 3.
                    tc._sut_A._currentStep.ID.Synchron
                ); //But as no StepIn() method was called, sequence should stay in step 0
                Assert.AreEqual(
                    "Step 3", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                    tc._sut_A._currentStep.Description.Synchron
                ); //already been performed, the step logic is executed.
                Assert.AreEqual(
                    30, //Check if current step status is ReadyToRun.
                    tc._sut_A._currentStep.Status.Synchron
                ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

                Assert.AreEqual(
                    3, //Check if StepId changes to 3.
                    tc._sut_N._currentStep.ID.Synchron
                ); //But as no StepIn() method was called, sequence should stay in step 0
                Assert.AreEqual(
                    "Step 3", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                    tc._sut_N._currentStep.Description.Synchron
                ); //already been performed, the step logic is executed.
                Assert.AreEqual(
                    30, //Check if current step status is ReadyToRun.
                    tc._sut_N._currentStep.Status.Synchron
                ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            }
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeChangeStepOrder);
            Assert.AreEqual(1 + numberOfSteps, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes to 3.
                tc._sut_A._currentStep.ID.Synchron
            ); //But as no StepIn() method was called, sequence should stay in step 0
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                tc._sut_A._currentStep.Description.Synchron
            ); //already been performed, the step logic is executed.
            Assert.AreEqual(
                30, //Check if current step status is ReadyToRun.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes to 3.
                tc._sut_N._currentStep.ID.Synchron
            ); //But as no StepIn() method was called, sequence should stay in step 0
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has
                tc._sut_N._currentStep.Description.Synchron
            ); //already been performed, the step logic is executed.
            Assert.AreEqual(
                30, //Check if current step status is ReadyToRun.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeChangeStepOrder);
            Assert.AreEqual(2 + numberOfSteps, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>2",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>2",
                tc._sut_A._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type UidOrderChangedDuringExecution
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeChangeStepOrder);
            Assert.AreEqual(3 + numberOfSteps, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.StepModeCommentOutPartOfRunningSequencer)]
        public void T618_StepModeCommentOutPartOfRunningSequencer()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CommentOutPartOfRunningSequencer);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            //with step status Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CommentOutPartOfRunningSequencer);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //After running this method, sequencer should stay in error,
            //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1", as part of th sequencer was commented out
            //so order of the step with StepId 3 changes from order 3 to order 1.

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CommentOutPartOfRunningSequencer);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sut_A._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sut_N._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sut_N._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.CommentOutPartOfRunningSequencer);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes from 0 to 3.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 0" to "Step 3".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes from 0 to 3.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 0" to "Step 3".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeUncommentPartOfRunningSequencer)]
        public void T621_StepModeUncommentPartOfRunningSequencer()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.UncommentPartOfRunningSequencer);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            //with step status Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.UncommentPartOfRunningSequencer);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3.
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3.
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status is Running.
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            //After running this method, sequencer should stay in error,
            //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3", as part of the sequencer was uncommented
            //so order of the step with StepId 3 changes from order 1 to order 3.

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.UncommentPartOfRunningSequencer);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sut_A._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sut_A._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId stays at value 3 as before
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sut_N._currentStep.Description.Synchron
            ); //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual(
                "ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sut_N._messenger._mime.Text.Synchron
            ); //Check if messenger returns the expected error message
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                40, //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeRequestStepFromLowerToHigher)]
        public void T631_StepModeRequestStepFromLowerToHigher()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 100;
            tc._totalStepNumber.Synchron = numberOfSteps;
            reqStep = 50;
            tc._reqStep.Synchron = reqStep;
            reqStepNotExists = 300;
            tc._reqStepNotExists.Synchron = reqStepNotExists;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepFromLowerToHigher);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_A._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared

            Assert.AreEqual(
                initStepId, //Check if the initial StepId was written to the current step of the sequencer
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                initStepDescription, //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(0, tc._sut_N._numberOfSteps.Synchron); //Check if the number of the steps in the sequence was succesfully cleared

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepFromLowerToHigher);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId changes to zero
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes to "(>Step 0<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.AreEqual(
                numberOfSteps, //Check if number of the counted steps is equal to expected value
                tc._sut_A._numberOfSteps.Synchron
            );

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepFromLowerToHigher);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                reqStep, //Check if StepId changes to reqStep
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step " + reqStep.ToString() + "<)", //Check if StepDescription changes to "(>Step " + reqStep + "<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                reqStep, //Check if StepId changes to reqStep
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step " + reqStep.ToString() + "<)", //Check if StepDescription changes to "(>Step " + reqStep + "<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeRequestStepFromHigherToLower)]
        public void T633_StepModeRequestStepFromHigherToLower()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepFromHigherToLower);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(
                (ulong)reqStep + 5,
                (int)eTcoSequenceTests.StepModeRequestStepFromHigherToLower
            );
            Assert.AreEqual(reqStep + 5, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                reqStep + 5, //Check if StepId changes to reqStep + 5
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step " + (reqStep + 5).ToString() + "<)", //Check if StepDescription changes to "Step " + reqStep +5
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                reqStep + 5, //Check if StepId changes to reqStep + 5
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step " + (reqStep + 5).ToString() + "<)", //Check if StepDescription changes to "Step " + reqStep +5
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepFromHigherToLower);
            Assert.AreEqual(reqStep + 6, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                reqStep + 5, //Check if StepId stays at reqStep + 5
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (reqStep + 5).ToString(), //Check if StepDescription stays at "Step " + reqStep +5
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status changes to Done
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                reqStep + 5, //Check if StepId stays at reqStep + 5
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (reqStep + 5).ToString(), //Check if StepDescription stays at "Step " + reqStep +5
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step status changes to Done
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepFromHigherToLower);
            Assert.AreEqual(reqStep + 7, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                reqStep, //Check if StepId changes to reqStep
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + reqStep.ToString(), //Check if StepDescription changes to "Step " + reqStep
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                reqStep, //Check if StepId changes to reqStep
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + reqStep.ToString(), //Check if StepDescription changes to "Step " + reqStep
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeRequestStepToNotExistingStep)]
        public void T635_StepModeRequestStepToNotExistingStep()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 100;
            tc._totalStepNumber.Synchron = numberOfSteps;
            reqStep = 50;
            tc._reqStep.Synchron = reqStep;
            reqStepNotExists = 300;
            tc._reqStepNotExists.Synchron = reqStepNotExists;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepToNotExistingStep);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepToNotExistingStep);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron);

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepToNotExistingStep);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(initStepDescription, tc._sut_A._currentStep.Description.Synchron);

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(initStepDescription, tc._sut_N._currentStep.Description.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepToNotExistingStep);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(initStepId, tc._sut_A._currentStep.ID.Synchron);
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if StepDescription changes to the expected error message
                    reqStepNotExists.ToString()
                    + " DOES NOT EXIST",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if messenger returns the expected error message
                    reqStepNotExists.ToString()
                    + " DOES NOT EXIST",
                tc._sut_A._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                50, //Check if the sequencer error is of the type StepWithRequestedIdDoesNotExists
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(initStepId, tc._sut_N._currentStep.ID.Synchron);
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if StepDescription changes to the expected error message
                    reqStepNotExists.ToString()
                    + " DOES NOT EXIST",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if messenger returns the expected error message
                    reqStepNotExists.ToString()
                    + " DOES NOT EXIST",
                tc._sut_N._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                50, //Check if the sequencer error is of the type StepWithRequestedIdDoesNotExists
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeRequestStepToNotExistingStep);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);
        }

        [
            Test,
            Order(
                (int)
                    eTcoSequenceTests.StepModeRequestStepWhilePreviousRequestStepHasnotBeenYetProcessed
            )
        ]
        public void T640_StepModeRequestStepWhilePreviousRequestStepHasnotBeenYetProcessed()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            initStepId = 32750;
            tc._currentStepId.Synchron = initStepId;
            tc._currentStepDescription.Synchron = initStepDescription;
            numberOfSteps = 100;
            tc._totalStepNumber.Synchron = numberOfSteps;
            reqStep = 50;
            tc._reqStep.Synchron = reqStep;
            reqStepNotExists = 300;
            tc._reqStepNotExists.Synchron = reqStepNotExists;

            tc.ExecuteProbeRun(
                1,
                (int)eTcoSequenceTests.RequestStepWhilePreviousRequestStepHasNotBeenYetProcessed
            );
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(
                1,
                (int)eTcoSequenceTests.RequestStepWhilePreviousRequestStepHasNotBeenYetProcessed
            );
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if StepDescription changes to the expected error message
                    (reqStep + 10).ToString()
                    + " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: "
                    + reqStep.ToString()
                    + " HAS NOT BEEN YET PERFORMED!",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if messenger returns the expected error message
                    (reqStep + 10).ToString()
                    + " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: "
                    + reqStep.ToString()
                    + " HAS NOT BEEN YET PERFORMED!",
                tc._sut_A._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_A._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                60, //Check if the sequencer error is of the type SeveralRequestStep
                tc._sut_A._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if StepDescription changes to the expected error message
                    (reqStep + 10).ToString()
                    + " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: "
                    + reqStep.ToString()
                    + " HAS NOT BEEN YET PERFORMED!",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                "REQUESTED STEP_ID: "
                    + //Check if messenger returns the expected error message
                    (reqStep + 10).ToString()
                    + " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: "
                    + reqStep.ToString()
                    + " HAS NOT BEEN YET PERFORMED!",
                tc._sut_N._messenger._mime.Text.Synchron
            );
            Assert.AreEqual(
                true, //Check if sequence returns error
                tc._sut_N._sequencerHasError.Synchron
            );
            Assert.AreEqual(
                60, //Check if the sequencer error is of the type SeveralRequestStep
                tc._sut_N._sequencerErrorId.Synchron
            ); //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(
                50, //Check if step status is Error
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeDisabledStep)]
        public void T641_StepModeDisabledStep()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            numberOfSteps = 10;
            tc._totalStepNumber.Synchron = numberOfSteps;
            reqStep = 50;
            tc._reqStep.Synchron = reqStep;
            reqStepNotExists = 300;
            tc._reqStepNotExists.Synchron = reqStepNotExists;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeDisabledStep);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc._sut_A._stateChangeFrom.Synchron = -1;
            tc._sut_A._stateChangeTo.Synchron = -1;
            tc._sut_N._stateChangeFrom.Synchron = -1;
            tc._sut_N._stateChangeTo.Synchron = -1;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeDisabledStep);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            );

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            );

            tc.ExecuteProbeRun(2, (int)eTcoSequenceTests.StepModeDisabledStep);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(-1, tc._sut_A._stateChangeFrom.Synchron);
            Assert.AreEqual(-1, tc._sut_A._stateChangeTo.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(-1, tc._sut_N._stateChangeFrom.Synchron);
            Assert.AreEqual(-1, tc._sut_N._stateChangeTo.Synchron);

            tc._sut_A._onStateChangeCount.Synchron = 0;
            tc._sut_A._stateChangeFrom.Synchron = -1;
            tc._sut_A._stateChangeTo.Synchron = -1;
            tc._sut_N._onStateChangeCount.Synchron = 0;
            tc._sut_N._stateChangeFrom.Synchron = -1;
            tc._sut_N._stateChangeTo.Synchron = -1;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeDisabledStep);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(*Step 1*)", //Check if StepDescription changes from "(>Step 0<)" to "(*Step 1*)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                10, //Check if current step status changes to Disabled
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(0, tc._sut_A._onStateChangeCount.Synchron);
            Assert.AreEqual(-1, tc._sut_A._stateChangeFrom.Synchron);
            Assert.AreEqual(-1, tc._sut_A._stateChangeTo.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(*Step 1*)", //Check if StepDescription changes from "(>Step 0<)" to "(*Step 1*)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                10, //Check if current step status changes to Disabled
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(0, tc._sut_N._onStateChangeCount.Synchron);
            Assert.AreEqual(-1, tc._sut_N._stateChangeFrom.Synchron);
            Assert.AreEqual(-1, tc._sut_N._stateChangeTo.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeDisabledStep);
            Assert.AreEqual(5, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId changes from 0 to 2
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 2<)", //Check if StepDescription changes from "(>Step 0<)" to "(>Step 2<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to Disabled
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(0, tc._sut_A._onStateChangeCount.Synchron);
            Assert.AreEqual(-1, tc._sut_A._stateChangeFrom.Synchron);
            Assert.AreEqual(-1, tc._sut_A._stateChangeTo.Synchron);

            Assert.AreEqual(
                2, //Check if StepId changes from 0 to 2
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 2<)", //Check if StepDescription changes from "(>Step 0<)" to "(>Step 2<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to Disabled
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(0, tc._sut_N._onStateChangeCount.Synchron);
            Assert.AreEqual(-1, tc._sut_N._stateChangeFrom.Synchron);
            Assert.AreEqual(-1, tc._sut_N._stateChangeTo.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeDisabledStep);
            Assert.AreEqual(6, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId stays at 2
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes from "(>Step 2<)" to "Step 2"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Disabled
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(1, tc._sut_A._onStateChangeCount.Synchron);
            Assert.AreEqual(0, tc._sut_A._stateChangeFrom.Synchron);
            Assert.AreEqual(2, tc._sut_A._stateChangeTo.Synchron);

            Assert.AreEqual(
                2, //Check if StepId stays at 2
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes from "(>Step 2<)" to "Step 2"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step status changes to Disabled
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(1, tc._sut_N._onStateChangeCount.Synchron);
            Assert.AreEqual(0, tc._sut_N._stateChangeFrom.Synchron);
            Assert.AreEqual(2, tc._sut_N._stateChangeTo.Synchron);
        }

        [Test, Order((int)eTcoSequenceTests.StepModeStepForwardBackward)]
        public void T670_StepModeStepForwardBackward()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            numberOfSteps = 30;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId 1, with StepDescription ""(>Step 1<)", Step status should be ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId 2, with StepDescription ""(>Step 2<)", Step status should be ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId changes from 1 to 2
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 2<)", //Check if StepDescription changes from "(>Step 1<)" to "(>Step 2<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId changes from 1 to 2
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 2<)", //Check if StepDescription changes from "(>Step 1<)" to "(>Step 2<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId numberOfSteps - 1, that is the last step of the sequence
            //with StepDescription "(>Step + numberOfSteps - 1<)"
            //Step status should be ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId changes from 2 to numberOfSteps - 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step "
                    + (numberOfSteps - 1).ToString() //Check if StepDescription changes from "(>Step 2<)" to "(>Step + numberOfSteps - 1<)"
                    + "<)",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId changes from 2 to numberOfSteps - 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step "
                    + (numberOfSteps - 1).ToString() //Check if StepDescription changes from "(>Step 2<)" to "(>Step + numberOfSteps - 1<)"
                    + "<)",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId numberOfSteps - 1, that is the last step of the sequence
            //with StepDescription "(>Step + numberOfSteps - 1<)" as it is last step of the sequence and step forwards is not possible
            //Step status should be ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(5, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId stays at numberOfSteps - 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step "
                    + (numberOfSteps - 1).ToString() //Check if StepDescription stays at "(>Step + numberOfSteps - 1<)"
                    + "<)",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId stays at numberOfSteps - 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step "
                    + (numberOfSteps - 1).ToString() //Check if StepDescription stays at "(>Step + numberOfSteps - 1<)"
                    + "<)",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId numberOfSteps - 2, that is the before last step
            //of the sequence, with StepDescription "(>Step + numberOfSteps - 2<)" as it is before last step of the sequence
            //Step status should be ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(6, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                numberOfSteps - 2, //Check if StepId changes from numberOfSteps - 1 to  numberOfSteps - 2
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step "
                    + //Check if StepDescription changes from "(>Step + numberOfSteps - 1 <)" to  "(>Step + numberOfSteps - 2<)"
                    (numberOfSteps - 2).ToString()
                    + "<)",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                numberOfSteps - 2, //Check if StepId changes from numberOfSteps - 1 to  numberOfSteps - 2
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step "
                    + //Check if StepDescription changes from "(>Step + numberOfSteps - 1<)" to  "(>Step + numberOfSteps - 2<)"
                    (numberOfSteps - 2).ToString()
                    + "<)",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId 1, that is the before first step
            //of the sequence, with StepDescription "(>Step 1<)" with step status ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(7, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from numberOfSteps - 1 to 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "(>Step + numberOfSteps - 2<)" to  "(>Step 1<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                1, //Check if StepId changes from numberOfSteps - 1 to 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "(>Step + numberOfSteps - 2<)" to  "(>Step 1<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId 0, that is the first step
            //of the sequence, with StepDescription "(>Step 0<)" with step status ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(8, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId changes from 1 to 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "(>Step 1<)" to  "(>Step 0<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId changes from 1 to 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "(>Step 1<)" to  "(>Step 0<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId 0, that is the first step
            //of the sequence, so step backwards is not posssible. StepDescription should be "(>Step 0<)"
            //and step status ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepForwardBackward);
            Assert.AreEqual(9, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId changes from 1 to 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "(>Step 1<)" to  "(>Step 0<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId changes from 1 to 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "(>Step 1<)" to  "(>Step 0<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeStepInDetailed)]
        public void T681_StepModeStepInDetailed()
        {
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;

            numberOfSteps = 30;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepInDetailed);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepInDetailed);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId stays at 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId 1, with StepDescription ""(>Step 1<)"
            //Step status should be ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepInDetailed);
            Assert.AreEqual(2, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                1, //Check if StepId changes from 0 to 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 1<)", //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId numberOfSteps - 1, that is the last step of the sequence
            //with StepDescription "(>Step + numberOfSteps - 1<)"
            //Step status should be ReadyToRun
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepInDetailed);
            Assert.AreEqual(3, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId changes from 1 to numberOfSteps - 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step "
                    + (numberOfSteps - 1).ToString() //Check if StepDescription changes from "(>Step 1<)" to "(>Step + numberOfSteps - 1<)"
                    + "<)",
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId changes from 1 to numberOfSteps - 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step "
                    + (numberOfSteps - 1).ToString() //Check if StepDescription changes from "(>Step 1<)" to "(>Step + numberOfSteps - 1<)"
                    + "<)",
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step status stays at ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should stay in StepId numberOfSteps - 1
            //with StepDescription "(>Step + numberOfSteps - 1<)" with step status Done as CloseSequence() method has been called
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepInDetailed);
            Assert.AreEqual(4, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId changes from 1 to numberOfSteps-1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(), //Check if StepDescription changes from "(>Step 1<)" to "(>Step + numberOfSteps-1<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step changes to Done
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                numberOfSteps - 1, //Check if StepId changes from 1 to numberOfSteps-1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step " + (numberOfSteps - 1).ToString(), //Check if StepDescription changes from "(>Step 1<)" to "(>Step + numberOfSteps-1<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                40, //Check if current step changes to Done
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After running this method, sequencer should canges to StepId 0
            //with StepDescription "(>Step 0<)" with step status ReadyToRun as StepIn() method has been called
            //in last step of the sequence
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.StepModeStepInDetailed);
            Assert.AreEqual(5, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                0, //Check if StepId changes from numberOfSteps to 0
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "(>Step + numberOfSteps<)" to "(>Step 1<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step changes to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                0, //Check if StepId changes from numberOfSteps to 0
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 0<)", //Check if StepDescription changes from "(>Step + numberOfSteps<)" to "(>Step 1<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step changes to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.SwitchStepModeOnDuringStepExecution)]
        public void T691_SwitchStepModeOnDuringStepExecution()
        {
            numberOfSteps = 30;
            tc._totalStepNumber.Synchron = numberOfSteps;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.SwitchStepModeOnDuringStepExecution);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);
            //After running this method, sequencer should stay in StepId 1, with StepDescription "Step 1"
            //Step status should be Running
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.SwitchStepModeOnDuringStepExecution);
            Assert.AreEqual(1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                1, //Check if StepId changes to 1
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes to "Step 1"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step changes to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                1, //Check if StepId changes to 1
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 1", //Check if StepDescription changes to "Step 1"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step changes to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After execution of this method, actual StepId should have value of 2
            //StepDescription should have value of "(>Step 2<)" and step status should be ReadyToRun
            tc.ExecuteProbeRun(9, (int)eTcoSequenceTests.SwitchStepModeOnDuringStepExecution);
            Assert.AreEqual(10, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId changes from 1 to 2
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 2<)", //Check if StepDescription changes from "Step 1" "(>Step 2<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step changes from Running to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId changes from 1 to 2
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 2<)", //Check if StepDescription changes from "Step 1" "(>Step 2<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step changes from Running to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeStepForwardFromRunningStep)]
        public void T693_StepModeStepForwardFromRunningStep()
        {
            //After execution of this method, actual StepId should have value of 2
            //StepDescription should have value of "Step 2" and step status should be Running
            tc.ExecuteProbeRun(10, (int)eTcoSequenceTests.StepModeStepForwardFromRunningStep);
            Assert.AreEqual(10, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId stays 2
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes from "(>Step 2<) to "Step 2""
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step changes from Running to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId stays 2
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes from "(>Step 2<) to "Step 2""
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step changes from Running to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After execution of this method, actual StepId should have value of 3
            //StepDescription should have value of "(>Step 3<)" and step status should be ReadyToRun
            tc.ExecuteProbeRun(10, (int)eTcoSequenceTests.StepModeStepForwardFromRunningStep);
            Assert.AreEqual(20, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 3<)", //Check if StepDescription changes from "Step 2" to "(>Step 3<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step changes from Running to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId changes from 2 to 3
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 3<)", //Check if StepDescription changes from "Step 2" to "(>Step 3<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step changes from Running to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.StepModeStepBackwardFromRunningStep)]
        public void T695_StepModeStepBackwardFromRunningStep()
        {
            //After execution of this method, actual StepId should have value of 3
            //StepDescription should have value of "Step 3" and step status should be Running
            tc.ExecuteProbeRun(10, (int)eTcoSequenceTests.StepModeStepBackwardFromRunningStep);
            Assert.AreEqual(10, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            Assert.AreEqual(
                3, //Check if StepId stays 3
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "(>Step 3<) to "Step 3""
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step changes from Running to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                3, //Check if StepId stays 3
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 3", //Check if StepDescription changes from "(>Step 3<) to "Step 3""
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step changes from Running to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            //After execution of this method, actual StepId should have value of 2
            //StepDescription should have value of "(>Step 2<)" and step status should be ReadyToRun
            tc.ExecuteProbeRun(10, (int)eTcoSequenceTests.StepModeStepBackwardFromRunningStep);
            Assert.AreEqual(20, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId changes from 3 to 2
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 2<)", //Check if StepDescription changes from "Step 3" to "(>Step 2<)"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step changes from Running to ReadyToRun
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId changes from 3 to 2
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "(>Step 2<)", //Check if StepDescription changes from "Step 3" to "(>Step 2<)"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                20, //Check if current step changes from Running to ReadyToRun
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.SwitchStepModeStepOffFromReadyToRun)]
        public void T696_SwitchStepModeStepOffFromReadyToRun()
        {
            //After execution of this method, actual StepId should have value of 2
            //StepDescription should have value of "(>Step 2<)" and step status should be ReadyToRun
            tc.ExecuteProbeRun(10, (int)eTcoSequenceTests.SwitchStepModeStepOffFromReadyToRun);
            Assert.AreEqual(10, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId stays at 2
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes from "(>Step 2<)" to "Step 2"
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step changes from ReadyToRun to Running
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId stays at 2
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription changes from "(>Step 2<)" to "Step 2"
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step changes from ReadyToRun to Running
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.SwitchStepModeStepOnFromRunning)]
        public void T697_SwitchStepModeStepOnFromRunning()
        {
            //After execution of this method, actual StepId should have value of 2
            //StepDescription should have value of "Step 2" and step status should be Running
            tc.ExecuteProbeRun(10, (int)eTcoSequenceTests.SwitchStepModeStepOnFromRunning);
            Assert.AreEqual(10, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId stays at 2 as before
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription stays at "Step 2" as before
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step stays at Running as before
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId stays at 2 as before
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription stays at "Step 2" as before
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step stays at Running as before
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.SwitchStepModeStepOffFromRunning)]
        public void T698_SwitchStepModeStepOffFromRunning()
        {
            //After execution of this method, actual StepId should have value of 2
            //StepDescription should have value of "Step 2" and step status should be Running
            tc.ExecuteProbeRun(10, (int)eTcoSequenceTests.SwitchStepModeStepOffFromReadyToRun);
            Assert.AreEqual(10, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);

            Assert.AreEqual(
                2, //Check if StepId stays at 2 as before
                tc._sut_A._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription stays at "Step 2" as before
                tc._sut_A._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step stays at Running as before
                tc._sut_A._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

            Assert.AreEqual(
                2, //Check if StepId stays at 2 as before
                tc._sut_N._currentStep.ID.Synchron
            );
            Assert.AreEqual(
                "Step 2", //Check if StepDescription stays at "Step 2" as before
                tc._sut_N._currentStep.Description.Synchron
            );
            Assert.AreEqual(
                30, //Check if current step stays at Running as before
                tc._sut_N._currentStep.Status.Synchron
            ); //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order((int)eTcoSequenceTests.OnStateChangeWhenChangingModes)]
        public void T699_OnStateChangeWhenChangingModes()
        {
            //This test runs the whole sequence several times in cyclic mode then swith to step mode and performs StepIn several times, and then switch back to cyclic mode
            //and check if the number of the OnStateChange method calls, so as the PostStepComplete and PostSequenceComplete method calls was as expected.
            tc.ExecuteProbeRun(
                (int)eTcoSequenceTests.RestoreAlreadyCheckedSequence,
                () => tc._done.Synchron
            );
            tc._arranged.Synchron = false;
            tc._done.Synchron = false;

            numberOfSteps = 11;
            tc._totalStepNumber.Synchron = numberOfSteps;
            ushort cyclicCycles = 13;
            tc._cyclicCycles.Synchron = cyclicCycles;
            ushort stepInEvents = 17;
            tc._stepInEvents.Synchron = stepInEvents;

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.OnStateChangeWhenChangingModes);
            Assert.AreEqual(0, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._arranged.Synchron);
            Assert.IsFalse(tc._done.Synchron);

            tc._sut_A._onStateChangeCount.Synchron = 0;
            tc._sut_A._onCompleteStepCount.Synchron = 0;
            tc._sut_A._onSequenceCompleteCount.Synchron = 0;

            tc._sut_N._onStateChangeCount.Synchron = 0;
            tc._sut_N._onCompleteStepCount.Synchron = 0;
            tc._sut_N._onSequenceCompleteCount.Synchron = 0;
            //Cyclic mode
            tc.ExecuteProbeRun(cyclicCycles, (int)eTcoSequenceTests.OnStateChangeWhenChangingModes);
            Assert.AreEqual(cyclicCycles, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);
            Assert.AreEqual(0, tc._sut_A._modeController._mode.Synchron);
            Assert.AreEqual(0, tc._sut_N._modeController._mode.Synchron);

            ulong onStateChangeCount_A = tc._sut_A._onStateChangeCount.Synchron;
            Assert.AreEqual(
                tc._sut_A._sequenceCycles.Synchron * numberOfSteps - 1,
                onStateChangeCount_A
            );

            ulong onStateChangeCount_N = tc._sut_N._onStateChangeCount.Synchron;
            Assert.AreEqual(
                tc._sut_N._sequenceCycles.Synchron * numberOfSteps - 1,
                onStateChangeCount_A
            );

            //Step mode
            tc.ExecuteProbeRun(
                (ulong)stepInEvents + 1,
                (int)eTcoSequenceTests.OnStateChangeWhenChangingModes
            );
            Assert.AreEqual(cyclicCycles + stepInEvents + 1, tc._plcCycleCounter.Synchron);
            Assert.IsFalse(tc._done.Synchron);
            Assert.AreEqual(10, tc._sut_A._modeController._mode.Synchron);
            Assert.AreEqual(10, tc._sut_N._modeController._mode.Synchron);

            Assert.AreEqual(
                onStateChangeCount_A + stepInEvents,
                tc._sut_A._onStateChangeCount.Synchron
            );

            Assert.AreEqual(
                onStateChangeCount_N + stepInEvents,
                tc._sut_N._onStateChangeCount.Synchron
            );

            //Cyclic mode
            tc.ExecuteProbeRun(cyclicCycles, (int)eTcoSequenceTests.OnStateChangeWhenChangingModes);
            Assert.AreEqual(2 * cyclicCycles + stepInEvents + 1, tc._plcCycleCounter.Synchron);
            Assert.IsTrue(tc._done.Synchron);
            Assert.AreEqual(0, tc._sut_A._modeController._mode.Synchron);
            Assert.AreEqual(0, tc._sut_N._modeController._mode.Synchron);

            Assert.AreEqual(
                tc._sut_A._sequenceCycles.Synchron * numberOfSteps - 1,
                tc._sut_A._onStateChangeCount.Synchron
            );
            ulong PostStepCompleteCount_A =
                2 * (ulong)cyclicCycles * numberOfSteps
                + (ulong)Math.Ceiling((decimal)(stepInEvents / (numberOfSteps + 1)))
                    * numberOfSteps;
            Assert.AreEqual(PostStepCompleteCount_A, tc._sut_A._onCompleteStepCount.Synchron);
            ulong PostSequenceCompleteCount_A =
                2 * (ulong)cyclicCycles
                + (ulong)Math.Ceiling((decimal)(stepInEvents / (numberOfSteps + 1)));
            Assert.AreEqual(
                PostSequenceCompleteCount_A,
                tc._sut_A._onSequenceCompleteCount.Synchron
            );

            Assert.AreEqual(
                tc._sut_N._sequenceCycles.Synchron * numberOfSteps - 1,
                tc._sut_N._onStateChangeCount.Synchron
            );
            ulong PostStepCompleteCount_N =
                2 * (ulong)cyclicCycles * numberOfSteps
                + (ulong)Math.Ceiling((decimal)(stepInEvents / (numberOfSteps + 1)))
                    * numberOfSteps;
            Assert.AreEqual(PostStepCompleteCount_N, tc._sut_N._onCompleteStepCount.Synchron);
            ulong PostSequenceCompleteCount_N =
                2 * (ulong)cyclicCycles
                + (ulong)Math.Ceiling((decimal)(stepInEvents / (numberOfSteps + 1)));
            Assert.AreEqual(
                PostSequenceCompleteCount_N,
                tc._sut_N._onSequenceCompleteCount.Synchron
            );
        }

        [Test, Order((int)eTcoSequenceTests.MissedSequenceOpening)]
        public void T6100_MissedSequenceOpening()
        {
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MissedSequenceOpening);
            Assert.AreEqual(
                "Sequencer is not opened. You must call `Open` method before any step.",
                tc._sut_A._openingClosingErrorMessenger._mime.PlainMessage.Text
            );
            Assert.AreEqual(
                eMessageCategory.ProgrammingError,
                tc._sut_A._openingClosingErrorMessenger._mime.PlainMessage.CategoryAsEnum
            );
        }

        [Test, Order((int)eTcoSequenceTests.MissedSequenceClosing)]
        public void T6200_MissedSequenceClosing()
        {
            tc._sut_A._messenger.Clear();
            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.MissedSequenceClosing);
            Assert.AreEqual(
                "Sequencer is not closed. You must call `Close` method after last step call.",
                tc._sut_A._openingClosingErrorMessenger._mime.PlainMessage.Text
            );
            Assert.AreEqual(
                eMessageCategory.ProgrammingError,
                tc._sut_A._openingClosingErrorMessenger._mime.PlainMessage.CategoryAsEnum
            );
        }

        [Test, Order((int)eTcoSequenceTests.FreshStateEntry)]
        public void FreshStateEntry()
        {
            tc._sut_A._messenger.Clear();

            tc.ExecuteProbeRun(1, (int)eTcoSequenceTests.Initialize);
            tc.ExecuteProbeRun((int)eTcoSequenceTests.FreshStateEntry);

            Assert.AreEqual(true, tc.FreshStateEntry_enter_and_leave_must_be_true.Synchron);
            Assert.AreEqual(
                true,
                tc.FreshStateEntry_enter_and_two_cycles_on_first_must_be_true.Synchron
            );
            Assert.AreEqual(
                false,
                tc.FreshStateEntry_enter_and_two_cycles_on_second_must_be_false.Synchron
            );
            Assert.AreEqual(
                false,
                tc.FreshStateEntry_on_multiple_calls_but_not_first_must_be_false.Synchron
            );
            Assert.AreEqual(
                true,
                tc.FreshStateEntry_on_request_step_single_call_must_be_true.Synchron
            );
        }
    }
}
