using NUnit.Framework;
using System;
using System.Threading;
using TcoCore;
using TcoCoreTests;

namespace TcoCoreUnitTests
{

    public class T05_TcoSequencerAutoRestorableTests
    {

        TcoSequencerAutoRestorableTest tc = ConnectorFixture.Connector.MAIN._tcoSequencerAutoRestorableTest;

        protected short initStepId = 32750;
        protected string initStepDescription = "---test---init---";
        protected short lastStepId = 32767;
        protected string lastStepDescription = "This is last step of the sequence";
        protected short cycleCount = 0;
        protected short restoreCycleCount = 0;
        protected bool running;
        protected ushort numberOfSteps = 0;
        protected ushort initCycle = 100;
        protected ushort cycle = 0;
        protected short reqStep = 50;
        protected short reqStepNotExists = 300;
        protected short childState = 100;
        protected short plcCycle = 0;
        protected ulong onSequencerErrorCount = 0;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            tc._callMyPlcInstance.Synchron = true;                      //Switch on cyclicall calling the PLC code of this instance
            while (!tc._init.Synchron) { }
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._finishStep.Synchron = false;                            //Reset finish step flag in the PLC testing instance
            tc._restore.Synchron = false;                               //Reset sequence restore flag in the PLC testing instance
            tc._sequencer._callRestoreInOnStateChange.Synchron = false; //Reset calling Restore() in OnStateChange() method.
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            tc._callMyPlcInstance.Synchron = false;                     //Switch off cyclicall calling the PLC code of this instance
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._finishStep.Synchron = false;                            //Reset finish step flag in the PLC testing instance
            tc._restore.Synchron = false;                               //Reset sequence restore flag in the PLC testing instance
            tc._sequencer._callRestoreInOnStateChange.Synchron = false; //Reset calling Restore() in OnStateChange() method.
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
        }

        [Test, Order(500)]
        public void T500_ExternalSequenceNumberOfStepsCount()
        {
            initStepId = 32765;
            initStepDescription = "---test---init---";
            numberOfSteps = 100;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SingleCycleRun(() => tc.SetCyclicMode());                //Set sequencer into the cyclic mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepID uniqueness is performed on next sequence execution
            tc.ClearNumberOfSteps();                                    //Clear internal flow step counters, so number of steps is going to be counted again on next sequence run
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc.UpdateCurrentStepDetails();

            Assert.AreEqual(initStepId,
                tc._sequencer._currentStepId.Synchron);                 //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual("---test---init---",
                tc._sequencer._currentStepDescription.Synchron);        //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps in the sequence was succesfully cleared

            tc.SequencerSingleCycleRun(() =>                            //After execution of this method, actual StepId should have value one, as step zero should finished, and sequence
            {                                                           //should stay in step one, as step one should never finished in this case
                                                                        //StepDescritpion should have value "Step 1" as step zero should finished, and step one should never finished in this case
                if (tc.Step(0, true, "Initial step"))                   //Sequencer method GetNumberOffFlowSteps() should return the value equal to value of the variable numberOfSteps 
                {                                                       //as it is number of the calls of the sequencer method Step()
                    tc.StepCompleteWhen(true);
                }

                for (short i = 1; i <= numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1. As the StepId uniqueness control has already been performed, 
                tc._sequencer._currentStepId.Synchron);                 //the step logic is executed                    
            Assert.AreEqual("Step 1",                                   //Check if StepDescription changes from "Step 0" to "Step 1". As the StepId uniqueness control has already
                tc._sequencer._currentStepDescription.Synchron);        //been performed, the step logic is executed.
            Assert.AreEqual(numberOfSteps,                              //Check if number of the counted steps is equal to expected value
                tc.GetNumberOfStepsInSequence());
            Assert.AreEqual(30,                                         //Check if current step status is Running.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50

        }

        [Test, Order(501)]
        public void T501_ExternalSequenceExecutionInOnePLCcycle()
        {
            tc.SequencerSingleCycleRun(() =>                            //After execution this method actual StepId should have value equal to the value of the variable numberOfSteps
            {                                                           //as each step should finished as each contains StepCompleteWhen method call with value TRUE
                                                                        //StepDescription should have value "Step "+value of the variable numberOfSteps
                if (tc.Step(0, true, "Initial step"))                   //This test simulate one PLC cycle inside that the sequence of numberOfSteps steps, with all transition conditions
                {                                                       //met. All steps of such a sequence should be executed in one PLC cycle.
                    tc.StepCompleteWhen(true);
                }

                for (short i = 1; i <= numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps,                              //Check if StepId of the last executed step is equal to numberOfSteps 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + numberOfSteps.ToString(),         //Check if StepDescription of the last executed step is equal to "Step "+value of the variable numberOfSteps
                tc._sequencer._currentStepDescription.Synchron);
        }

        [Test, Order(502)]
        public void T502_ExternalSequenceOnStepCompleted()
        {
            short psc = tc._sequencer._onCompleteStepCount.Synchron;    //Store the actual value of the calls of the method PostStepComplete().
            tc.SequencerSingleCycleRun(() =>                            //After execution this method actual value of the calls of the method PostStepComplete() should be incremented by the value of the variable numberOfSteps
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }

                for (short i = 1; i <= numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }

                    // Test CurrentStep property;
                    tc.GetCurrentStepPropTest();
                    var plain = tc._setCurrentStepPropTestData.CreatePlainerType();
                    tc._setCurrentStepPropTestData.FlushPlainToOnline(plain);
                    tc.GetCurrentStepPropTest();
                    tc._setCurrentStepPropTestData.FlushOnlineToPlain(plain);
                    
                    Assert.AreEqual((short)i, plain.ID);
                    Assert.AreEqual("Step " + i.ToString(), plain.Description);
                    Assert.AreEqual(true, plain.Enabled);
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            Assert.AreEqual(psc + tc.GetNumberOfStepsInSequence(),      //The actual value of the call of the method PostStepComplete() should increment exactly by number of the steps in the sequence, as each step is completed.
                tc._sequencer._onCompleteStepCount.Synchron);
        }

        [Test, Order(503)]
        public void T503_ExternalSequenceOnSequenceCompleted()
        {
            short psc = tc._sequencer._onSequenceCompleteCount.Synchron;//Store the actual value of the calls of the method PostSequenceComplete().
            tc.SequencerSingleCycleRun(() =>                            //After execution this method actual value of the calls of the method PostSequenceComplete() should be incremented by one.
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }

                for (short i = 1; i <= numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            Assert.AreEqual(psc + 1,                                    //The actual value of the call of the method PostStepComplete() should increment exactly by number of the steps in the sequence, as each step is completed.
                tc._sequencer._onSequenceCompleteCount.Synchron);
        }

        [Test, Order(504)]
        public void T504_ExternalRestoreChildBetweenSteps()
        {
            tc.SingleCycleRun(() =>
            {
                Assert.IsFalse(tc.IsAutoRestorable());
                Assert.IsTrue(tc.HasAutoRestoreEnabled());
                Assert.IsTrue(tc.ChildIsAutoRestorable());
                Assert.IsFalse(tc.ChildHasAutoRestoreEnabled());
                tc.SetChildState(childState);                           //Set the child state to the desired state. As the parent sequencer has AutoRestore Enabled, the child is AutoRestorable.
                Assert.AreEqual(-1, tc.GetChildState());                //After querying for child state, it is restored to -1, as this child was not called in the previous PLC cycle.
            });

            tc.SequencerSingleCycleRun(() =>
            {
                tc.SetChildState(childState);                           //Set the child to the desired state.
                Assert.AreEqual(childState, tc.GetChildState());        //Check if the child state has been changed to desired state.
                if (tc.Step(0, true, "Initial step"))
                {
                    Assert.AreEqual(-1, tc.GetChildState());            //Child should change its state to -1 due to Restore() method call as the parent Sequencer has changed its state.
                    tc.SetChildState(childState);                       //Set the child to the desired state.
                    Assert.AreEqual(childState, tc.GetChildState());    //Check if the child state has been changed to desired state.
                    tc.StepCompleteWhen(true);                          //Complete step casuse that at the next Step() method call, Sequencer change its state from 0 to 1. Child state should be restored frm the value of childState to -1. 
                }
                for (short i = 1; i <= numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        Assert.AreEqual(-1, tc.GetChildState());        //Child should change its state to -1 due to Restore() method call as the parent Sequencer has changed its state.
                        tc.SetChildState(childState);                   //Set the child to the desired state.
                        Assert.AreEqual(childState, tc.GetChildState());//Check if the child state has been changed to desired state.
                        tc.StepCompleteWhen(true);                      //Complete step casuse that at the next Step() method call, Sequencer change its state from i to i + 1. Child state should be restored frm the value of childState to -1. 
                    }
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
        }

        [Test, Order(505)]
        public void T505_ExternalSequenceOnStateChangeWithRestoreCallInside()
        {
            numberOfSteps = 10;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SingleCycleRun(() => tc.SetCyclicMode());                //Set sequencer into the cyclic mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepID uniqueness is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the testing instance

            tc._sequencer._callRestoreInOnStateChange.Synchron = false; //Reset calling Restore() in OnStateChange() method.

            tc.SequencerSingleCycleRun(() =>
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }

                for (short i = 1; i <= numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes to 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 1",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes to Step 1.        
            Assert.AreEqual(30,                                         //Check if current step status is Running.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sequencer._sequencerHasError.Synchron);  //Check if seuencer has no error
            Assert.AreEqual(0,
                tc._sequencer._sequencerErrorId.Synchron);

            tc._sequencer._callRestoreInOnStateChange.Synchron = true;  //Set calling Restore() in OnStateChange() method.

            tc.SequencerSingleCycleRun(() =>
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }

                for (short i = 1; i <= numberOfSteps; i++)
                {
                    if(tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays 0
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(0,                                          //Check if current step status is None.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sequencer._sequencerHasError.Synchron);  //Check if seuencer has no error
            Assert.AreEqual(0,
                tc._sequencer._sequencerErrorId.Synchron);

            tc._sequencer._callRestoreInOnStateChange.Synchron = false; //Reset calling Restore() in OnStateChange() method.

        }

        [Test, Order(506)]
        public void T506_ExternalSequenceTooLowStepId()
        {
            numberOfSteps = 10;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SingleCycleRun(() => tc.SetCyclicMode());                //Set sequencer into the cyclic mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepID uniqueness is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the testing instance

            tc.SequencerSingleCycleRun(() =>                            
            {                                                          
                if (tc.Step(-32768, true, "Initial step"))           
                {                                                
                    tc.StepCompleteWhen(true);
                }

                for (short i = 1; i <= numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays at 0
                tc._sequencer._currentStepId.Synchron);                                     
            Assert.AreEqual("STEP_ID TOO LOW: -32768! MINIMAL VALUE POSSIBLE: -32767!!!",                                   
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes to the expected error message.        
            Assert.AreEqual(50,                                         //Check if current step status is Error.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sequencer._sequencerHasError.Synchron);   //Check if seuencer has error
            Assert.AreEqual(70,                                         
                tc._sequencer._sequencerErrorId.Synchron);       

        }

        [Test, Order(507)]
        public void T507_ExternalSequenceTooHighStepId()
        {
            numberOfSteps = 10;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SingleCycleRun(() => tc.SetCyclicMode());                //Set sequencer into the cyclic mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepID uniqueness is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the testing instance

            tc.SequencerSingleCycleRun(() =>                         
            {                                                         
                for (short i = 0; i <= numberOfSteps-1; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }

                if (tc.Step(32767, true, "Last step"))
                {
                    tc.StepCompleteWhen(true);
                }

            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays at 0
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("STEP_ID TOO HIGH: 32767! MAXIMAL VALUE POSSIBLE: 32766!!!",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes to the expected error message.        
            Assert.AreEqual(50,                                         //Check if current step status is Error.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sequencer._sequencerHasError.Synchron);   //Check if seuencer has error
            Assert.AreEqual(80,
                tc._sequencer._sequencerErrorId.Synchron);
        }

        [Test, Order(508)]
        public void T508_ExternalSequenceRequestStepToFirstStepWithStepId0()
        {
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SingleCycleRun(() => tc.SetCyclicMode());                //Set sequencer into the cyclic mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepID uniqueness is performed on next sequence execution
            tc.SetNumberOfSteps(10);                                    //Set numberOfSteps to the testing instance

            tc.SequencerSingleCycleRun(() =>
            {
                for (short i = 0; i < 5; i++)
                {
                    if(tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }

                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(4,tc._sequencer._currentStepId.Synchron);
                Assert.AreEqual("Step 4",tc._sequencer._currentStepDescription.Synchron); 
                Assert.AreEqual(40,tc._sequencer._currentStepStatus.Synchron); 
                
                if (tc.Step(5, true, "Step 5" ))
                {
                    tc.RequestStep(0);
                }
                for (short i = 6; i <= 10; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(5, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 5", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40, tc._sequencer._currentStepStatus.Synchron);

            tc.SequencerSingleCycleRun(() =>
            {
                for (short i = 0; i <= 10; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 0", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30, tc._sequencer._currentStepStatus.Synchron);

        }

        [Test, Order(509)]
        public void T509_ExternalSequenceRequestStep()
        {
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SingleCycleRun(() => tc.SetCyclicMode());                //Set sequencer into the cyclic mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepID uniqueness is performed on next sequence execution
            tc.SetNumberOfSteps(10);                                    //Set numberOfSteps to the testing instance

            tc.SequencerSingleCycleRun(() =>
            {
                for (short i = -5; i < 0; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }

                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(-1, tc._sequencer._currentStepId.Synchron);
                Assert.AreEqual("Step -1", tc._sequencer._currentStepDescription.Synchron);
                Assert.AreEqual(40, tc._sequencer._currentStepStatus.Synchron);

                if (tc.Step(0, true, "Step 0"))
                {
                    tc.RequestStep(-5);
                }
                for (short i = 1; i <= 5; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 0", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40, tc._sequencer._currentStepStatus.Synchron);

            tc.SequencerSingleCycleRun(() =>
            {

                for (short i = -5; i <= 5; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(-5, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step -5", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30, tc._sequencer._currentStepStatus.Synchron);

            tc.SequencerSingleCycleRun(() =>
            {
                if (tc.Step(-5, true, "Step 0"))
                {
                    tc.RequestStep(0);
                }
                for (short i = -4; i <= 5; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 0", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30, tc._sequencer._currentStepStatus.Synchron);
        }

        [Test, Order(510)]
        public void T510_PLCSequenceCheckUniquenessFirstCycle()
        {
            initStepId = 32750;
            initStepDescription = "---test---init---";
            lastStepId = 32765;
            lastStepDescription = "This is last step of the sequence";
            cycleCount = 0;
            restoreCycleCount = 0;
            numberOfSteps = 0;

            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsNotChecked();                               //Set sequence as not checked, so StepId uniqueness is going to be performed on next sequence run
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._stepID.Synchron = lastStepId;                           //Set StepId of the last step in the PLC instance
            tc._enabled.Synchron = true;                                //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription;         //Set StepDescription of the last step in the PLC instance
            tc._restore.Synchron = false;                                 //Reset sequence restore flag in the PLC testing instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = true;                            //Set all step execution flag in the PLC testing instance. If this value so as the _FinishStep are true, 
                                                                        //all steps are executed and finished in one PLC cycle
            tc._finishStep.Synchron = true;                             //Set all step execution flag in the PLC testing instance. If this value so as the _RunOneStep are true, 
                                                                        //all steps are executed and finished in one PLC cycle. 

            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if the initial StepId was written to the current step of the sequencer
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(initStepDescription,                        //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc.GetPreviousNumberOfStepsInSequence());//Check if the number of the previous steps was succesfully cleared

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            //No Step logic is executed, even if entering or transition conditions are met 
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
            Assert.AreEqual(initStepDescription,
                tc._sequencer._currentStepDescription.Synchron);         //Check if StepDescription does not change. As the StepId uniqueness control has not yet been performed, 
                                                                         //the step logic is not executed, even if entering or transition conditions are met 
        }

        [Test, Order(511)]
        public void T511_PLCSequenceCheckUniquenessSecondCycle()
        {
            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness
            //has already been performed
            numberOfSteps = tc.GetNumberOfStepsInSequence();            //Get the counted number of steps in sequence
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(lastStepId,                                 //Check if StepId changes from initStepId to lastStepId. As the StepId uniqueness control has already been performed, 
                tc._sequencer._currentStepId.Synchron);                 //the step logic is executed                    
            Assert.AreEqual(lastStepDescription,                        //Check if StepDescription changes from initStepDescription to lastStepDescription. As the StepId uniqueness control has already
                tc._sequencer._currentStepDescription.Synchron);        //been performed, the step logic is executed.
            Assert.AreEqual(40,                                         //Check if current step status is Done.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(512)]
        public void T512_PLCSequenceRestoreAlreadyCheckedSequence()
        {
            //After Sequence restore, counting the steps so as the checking the StepId uniqueness has to be performed again
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            restoreCycleCount = tc._restoreCycleCount.Synchron;         //Store the actual sequence restore cycle counter
            tc._restore.Synchron = true;                                //Set sequence restore flag in the PLC testing instance. If this value is true, PLC restores the sequencer in the 
            running = true;                                             //PLC instance. Once it is done, PLC reset this variable to false.
            while (running)
            {
                running = tc._restore.Synchron;
            }
            Assert.AreEqual(restoreCycleCount + 1,                      //Check if only one restore of the sequencer was was performed, as it was expected
                tc._restoreCycleCount.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc.GetPreviousNumberOfStepsInSequence());//Check if the number of the previous steps in the sequence was succesfully cleared
        }

        [Test, Order(513)]
        public void T513_PLCSequenceNotUniqueStepIdFirstCycle()
        {
            initStepId = 200;
            lastStepId = 500;

            tc._stepID.Synchron = lastStepId;
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as StepDescription to the current step of the sequencer
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._stepID.Synchron = lastStepId;                           //Set the StepId of the last step in the PLC instance
            tc._enabled.Synchron = true;                                //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription;         //Set thw StepDesciption of the last step in the PLC instance
            tc._restore.Synchron = false;                               //Reset sequence restore flag in the PLC testing instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            //No Step logic is executed, even if entering or transition conditions are met 
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
                                                                        //2Bremoved =>
            Assert.AreNotEqual(initStepDescription,
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription change from initStepDescription due to StepId uniqueness control error. 
            Assert.AreEqual("ERROR NOT UNIQUE STEP_ID " +               //Check if StepDescription change, due to StepId uniqueness control error to the expected error message
                    lastStepId.ToString(),
                    tc._sequencer._currentStepDescription.Synchron);
            //<= 2Bremoved
            Assert.AreEqual("ERROR NOT UNIQUE STEP_ID " +               //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                    tc.GetTextOfTheMostImportantMessage());
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(10,                                         //Check if the sequencer error is of the type StepIdHasBeenChanged
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(514)]
        public void T514_PLCSequenceNotUniqueStepIdSecondCycle()
        {
            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness
            //has already been performed
            numberOfSteps = tc.GetNumberOfStepsInSequence();            //Get the counted number of steps in sequence
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness error
                                                                        //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            onSequencerErrorCount =                                     //Store actual value of the OnSequencerError counter.
                tc._sequencer._onSequencerErrorCount.Synchron;
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(10,                                         //Check if the sequencer error is of the type NotUniqueStepId
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(true, tc._sequencer._runOneStep.Synchron);  //Check if step logic was not entered due to sequence error, if yes _RunOneStep is to be reseted to false         
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
                                                                        //2Bremoved =>
            Assert.AreEqual("ERROR NOT UNIQUE STEP_ID " +               //Check if StepDescription change, due to StepId uniqueness control error to the expected error message
                    lastStepId.ToString(),
                    tc._sequencer._currentStepDescription.Synchron);
            //<= 2Bremoved
            Assert.AreEqual("ERROR NOT UNIQUE STEP_ID " +               //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                    tc.GetTextOfTheMostImportantMessage());
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.GreaterOrEqual(onSequencerErrorCount + 10,           //Check if OnSequencerErrorCount has been incremented by 10-NotUniqueStepId
                tc._sequencer._onSequencerErrorCount.Synchron);
        }

        [Test, Order(515)]
        public void T515_PLCSequenceNotUniqueStepIdRestore()
        {
            tc._finishStep.Synchron = true;
            //After Sequence restore, counting the steps so as the checking the StepId uniqueness has to be performed again
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            restoreCycleCount = tc._restoreCycleCount.Synchron;         //Store the actual sequence restore cycle counter
            tc._restore.Synchron = true;                                //Set sequence restore flag in the PLC testing instance. If this value is true, PLC restores the sequencer in the 
            running = true;                                             //PLC instance. Once it is done, PLC reset this variable to false.
            while (running)
            {
                running = tc._restore.Synchron;
            }
            Assert.AreEqual(restoreCycleCount + 1,                      //Check if only one restore of the sequencer was was performed, as it was expected
                tc._restoreCycleCount.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps in the sequence was succesfully cleared
            Assert.AreEqual(0, tc.GetPreviousNumberOfStepsInSequence());//Check if the number of the previous steps in the sequence was succesfully cleared
        }

        [Test, Order(516)]
        public void T516_PLCSequenceAfterErrorRestoreFirstCycle()
        {
            initStepId = 800;
            lastStepId = 32765;

            tc._stepID.Synchron = lastStepId;
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._stepID.Synchron = lastStepId;                           //Set StepId of the last step in the PLC instance
            tc._enabled.Synchron = true;                                //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription;         //Set StepDecription of the last step in the PLC instance
            tc._restore.Synchron = false;                               //Reset sequence restore flag in the PLC testing instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness
            //No Step logic is executed, even if entering or transition conditions are met 
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
            Assert.AreEqual(initStepDescription,                        //Check if StepDescription does not change. As the StepId uniqueness control has not yet been performed,
                tc._sequencer._currentStepDescription.Synchron);        //the step logic is not executed, even if entering or transition conditions are met      
            Assert.AreEqual(false,                                      //Check if sequence error has been reseted
                tc.SequencerHasError());
            Assert.AreEqual(0,                                          //Check if the sequencer error type has been reseted to the type noerror
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
        }

        [Test, Order(517)]
        public void T517_PLCSequenceAfterErrorRestoreSecondCycle()
        {
            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness
            //has already been performed
            numberOfSteps = tc.GetNumberOfStepsInSequence();            //Get the counted number of steps in sequence
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness error
                                                                        //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(false, tc._sequencer._runOneStep.Synchron); //Check if step logic was entered as sequence error has been already reseted. If _RunOneStep is reseted to false, step logic was entered and executed
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId changes from initStepId to first step StepId. As the sequence error has been already reseted and the StepId uniqueness control has been 
                tc._sequencer._currentStepId.Synchron);                 //already performed again after reset, the step logic is executed.                    
            Assert.AreEqual("Step number 0",                            //Check if StepDescription changes from initStepDescription to "Step number 0". As the sequence error has been already reseted and the StepId uniqueness control has been
                tc._sequencer._currentStepDescription.Synchron);        //already performed again after reset, the step logic is executed. 
            Assert.AreEqual(40,                                         //Check if current step status is Done.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(518)]
        public void T518_PLCSequenceAfterErrorRestoreThirdCycle()
        {
            //During this third sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness
            //has already been performed
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness error
                                                                        //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(false, tc._sequencer._runOneStep.Synchron); //Check if step logic was entered as sequence error has been already reseted. If _RunOneStep is reseted to false, step logic was entered and executed
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1.  
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step number 1",                            //Check if StepId changes from "Step number 0" to "Step number 1". 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status is Done.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(520)]
        public void T520_ChangeStepIdDuringExecutionRunFirstCycle()
        {
            initStepId = 32767;
            numberOfSteps = 100;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness is performed on next sequence execution
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if the initial StepId was written to the current step of the sequencer
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(initStepDescription,                        //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared
                                                                        //After running this method, sequencer should stay in StepId 1, with StepDescription "Step 1"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (short i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1. As the StepId uniqueness control has already been performed, 
                tc._sequencer._currentStepId.Synchron);                 //the step logic is executed                    
            Assert.AreEqual("Step 1",                                   //Check if StepDescription changes from "Step 0" to "Step 1". As the StepId uniqueness control has already
                tc._sequencer._currentStepDescription.Synchron);        //been performed, the step logic is executed.
            Assert.AreEqual(numberOfSteps,                              //Check if number of the counted steps is equal to expected value
                tc.GetNumberOfStepsInSequence());
            Assert.AreEqual(30,                                         //Check if current step status is Running.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(521)]
        public void T521_ChangeStepIdDuringExecutionRunSecondCycle()
        {
            //After running this method, sequencer should stay in StepId 2, with StepDescription "Step 2"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(2, true, "Step 2"))
                {

                }
                if (tc.Step(3, true, "Step 3"))
                {

                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId changes from 1 to 2.  
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 2",                                   //Check if StepDescription changes from "Step 1" to "Step 2".
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status is Running.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(522)]
        public void T522_ChangeStepIdDuringExecutionOccure()
        {
            //After running this method, sequencer should stay in error,
            tc.SequencerSingleCycleRun(() =>                            //ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 2=>3"
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(3, true, "Step 2"))                         //The StepId of the currently running step changes hero from 2 => 3
                {

                }
                if (tc.Step(2, true, "Step 3"))
                {

                }
                for (ushort i = 2; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId stays at value 2 as before  
                tc._sequencer._currentStepId.Synchron);
            //2Bremoved =>
            Assert.AreEqual("ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 2=>3",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes from value "Step 2" to the expected error message
                                                                        //<= 2Bremoved
            Assert.AreEqual("ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 2=>3",
                tc.GetTextOfTheMostImportantMessage());                 //Check if messenger returns the expected error message
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(20,                                         //Check if the sequencer error is of the type StepIdNumberChangedDuringExecution
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(523)]
        public void T523_ResetErrorRunAgainAndPrepareForCommentOut()
        {
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness is performed on next sequence execution
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer

            //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))                         //This section will be commented out in the next test   //
                {                                                       //                                                      //                                     
                    tc.StepCompleteWhen(true);                          //                                                      //
                }                                                       //                                                      //
                if (tc.Step(2, true, "Step 2"))                         //                                                      //
                {                                                       //                                                      //
                    tc.StepCompleteWhen(true);                          //                                                      //
                }                                                       //This section will be commented out in the next test   //
                if (tc.Step(3, true, "Step 3"))
                {

                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId changes from 2 to 3.  
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 3",                                   //Check if StepDescription changes from "Step 2" to "Step 3".
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status is Running.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(524)]
        public void T524_CommentOutPartOfRunningSequencer()
        {
            //After running this method, sequencer should stay in error,
            tc.SequencerSingleCycleRun(() =>                            //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1", as part of th sequencer was commented out
            {                                                           //so order of the step with StepId 3 changes from order 3 to order 1.
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                //if (tc.Step(1, true, "Step 1"))                       //This section was uncommented in the previous test     //
                //{                                                     //                                                      //                                     
                //    tc.StepCompleteWhen(true);                        //                                                      //
                //}                                                     //                                                      //
                //if (tc.Step(2, true, "Step 2"))                       //                                                      //
                //{                                                     //                                                      //
                //    tc.StepCompleteWhen(true);                        //                                                      //
                //}                                                     //This section was uncommented in the previous test     //
                if (tc.Step(3, true, "Step 3"))
                {

                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId stays at value 3 as before  
                tc._sequencer._currentStepId.Synchron);
            //2Bremoved =>
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes from value "Step 3" to the expected error message
                                                                        //<= 2Bremoved
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc.GetTextOfTheMostImportantMessage());                 //Check if messenger returns the expected error message
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(40,                                         //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(525)]
        public void T525_ResetErrorRunAgainAndPrepareForUncomment()
        {
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness is performed on next sequence execution
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run

            //After running this method, sequencer should stay in SteId 3, with StepDescription "Step 3"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {

                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                //if (tc.Step(1, true, "Step 1"))                       //This section will be uncommented in the next test     //
                //{                                                     //                                                      //                                     
                //    tc.StepCompleteWhen(true);                        //                                                      //
                //}                                                     //                                                      //
                //if (tc.Step(2, true, "Step 2"))                       //                                                      //
                //{                                                     //                                                      //
                //    tc.StepCompleteWhen(true);                        //                                                      //
                //}                                                     //This section will be uncommented in the next test     //
                if (tc.Step(3, true, "Step 3"))
                {

                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId changes from 0 to 3.  
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 3",                                   //Check if StepDescription changes from "Step 0" to "Step 3".
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status is Running.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(526)]
        public void T526_UncommentPartOfRunningSequencer()
        {
            //After running this method, sequencer should stay in error,
            tc.SequencerSingleCycleRun(() =>                            //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3", as part of the sequencer was uncommented
            {                                                           //so order of the step with StepId 3 changes from order 1 to order 3.
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))                         //This section was be commented out in the previous test//
                {                                                       //                                                      //                                     
                    tc.StepCompleteWhen(true);                          //                                                      //
                }                                                       //                                                      //
                if (tc.Step(2, true, "Step 2"))                         //                                                      //
                {                                                       //                                                      //
                    tc.StepCompleteWhen(true);                          //                                                      //
                }                                                       //This section was be commented out in the previous test//
                if (tc.Step(3, true, "Step 3"))
                {

                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId stays at value 3 as before  
               tc._sequencer._currentStepId.Synchron);
            //2Bremoved =>
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes from value "Step 3" to the expected error message
                                                                        //<= 2Bremoved
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc.GetTextOfTheMostImportantMessage());                 //Check if messenger returns the expected error message
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(40,                                         //Check if the sequencer error is of the type OrderOfTheStepHasBeenChanged
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(530)]
        public void T530_OpenCloseSequenceFirstCycle()
        {
            numberOfSteps = 10;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.ClearNumberOfSteps();                                    //Clear internal flow step counters, so number of steps is going to be counted again on next sequence execution

            tc.SequencerSingleCycleRun(() =>
            {
                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(0, tc._sequencer._currentStepOrder.Synchron);
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                tc.UpdateCurrentStepDetails();
                //Assert.AreEqual(0, tc._sequencer._currentStepOrder.Synchron);
                Assert.AreEqual(1, tc.GetOrderOfTheCurrentlyExecutedStep());
                for (short i = 1; i <= numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            tc.GetOrderOfTheCurrentlyEvaluatedStep();
            Assert.AreEqual(1, tc._sequencer._currentStepOrder.Synchron);//_currentStepOrder should stay at value of one, as step 1 has not been yet completed
            Assert.AreEqual(numberOfSteps + 1,                          //Check if OrderOfTheCurrentlyEvaluatedStep that represent total number of calls of the method Step() in this sequence is equal to 
                tc.GetOrderOfTheCurrentlyEvaluatedStep());              //expected value

        }

        [Test, Order(531)]
        public void T531_OpenCloseSequenceSecondCycle()
        {
            //After running this method, sequencer should stay in StepId numberOfSteps, with StepDescription 
            tc.SequencerSingleCycleRun(() =>                            //"Step " + numberOfSteps, with step status Done
            {
                tc.UpdateCurrentStepDetails();

                Assert.AreEqual(1, tc._sequencer._currentStepOrder.Synchron);//_currentStepOrder should have value of one as before no other step was completed
                Assert.AreEqual(0, tc.GetOrderOfTheCurrentlyEvaluatedStep());//OrderOfTheCurrentlyEvaluatedStep should have value of zero after call of the method Open(), as no calls of the method Step()
                if (tc.Step(0, true, "Initial step"))                   //has been yet performed
                {
                    tc.StepCompleteWhen(true);
                }
                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(1, tc._sequencer._currentStepOrder.Synchron);//_currentStepOrder should have value of one as before no other step was completed
                Assert.AreEqual(1, tc.GetOrderOfTheCurrentlyEvaluatedStep());//OrderOfTheCurrentlyEvaluatedStep  should have value of one as just one call of the method Step() was performed after call of the method Open()

                for (ushort i = 1; i <= numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(numberOfSteps + 1,                      //OrderOfTheCurrentlyExecutedStep should have value of numberOfSteps + 1 as all steps was completed
                    tc.GetOrderOfTheCurrentlyExecutedStep());
                Assert.AreEqual(numberOfSteps + 1,                      //OrderOfTheCurrentlyEvaluatedStep should have value of numberOfSteps + 1 as it is number of calls of the method Step()
                    tc.GetOrderOfTheCurrentlyEvaluatedStep());          //after call of the method Open()
                tc.SequenceComplete();
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0, tc.GetOrderOfTheCurrentlyExecutedStep());//OrderOfTheCurrentlyExecutedStep should have value of zero as it has been reseted by call of the method Close()
            Assert.AreEqual(numberOfSteps + 1,                          //OrderOfTheCurrentlyEvaluatedStep should have value of numberOfSteps + 1 as it is number of calls of the method Step()
                tc.GetOrderOfTheCurrentlyEvaluatedStep());
            Assert.AreEqual(numberOfSteps,                              //Check if StepId stays at the last step StepId.  
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + numberOfSteps.ToString(),         //Check if StepDescription stays at the last step StepDescription.
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status is Done.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(532)]
        public void T532_OpenCloseSequenceThirdCycle()
        {
            //After running this method, sequencer should stay in StepId numberOfSteps, with StepDescription
            tc.SequencerSingleCycleRun(() =>                            //"Step " + numberOfSteps, with step status Done
            {
                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(0,                                      //OrderOfTheCurrentlyExecutedStep should have value of zero as it has been reseted by call of the method SequenceComplete() in  
                    tc.GetOrderOfTheCurrentlyExecutedStep());           //the previous test 
                Assert.AreEqual(0,
                    tc.GetOrderOfTheCurrentlyEvaluatedStep());          //OrderOfTheCurrentlyEvaluatedStep should have value of zero as new call of the method Open() has been performed 
            });
            tc.UpdateCurrentStepDetails();

            Assert.AreEqual(0, tc.GetOrderOfTheCurrentlyEvaluatedStep());//OrderOfTheCurrentlyEvaluatedStep should have value of zero as it was reseted by call of the method Open() 
            Assert.AreEqual(numberOfSteps,                              //Check if StepId stays at the last step StepId as before, as no new call of the method Step() has been performed  
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + numberOfSteps.ToString(),         //Check if StepDescription stays at the last step StepDescription as before, as no new call of the method Step() has been performed 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status is Done  as before, as no new call of the method Step() has been performed 
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(533)]
        public void T533_OpenCloseSequenceFourthCycle()
        {
            //After running this method, sequencer should stay in StepId 0, with StepDesription
            tc.SequencerSingleCycleRun(() =>                            //"Step 0" , with step status Running
            {
                if (tc.Step(0, true, "Step 0"))
                {

                }
            });
            tc.UpdateCurrentStepDetails();

            Assert.AreEqual(1,
                tc.GetOrderOfTheCurrentlyEvaluatedStep());              //OrderOfTheCurrentlyEvaluatedStep should have value of one as it was reseted by call of the method Open(), but method call Step() increments this value
            Assert.AreEqual(0,                                          //Check if StepId changes to zero 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 0",                                   //Check if StepDescription changes to "Step 0" 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(540)]
        public void T540_PrepareSequenceForRequestStep()
        {
            initStepId = 32750;
            numberOfSteps = 100;
            reqStep = 50;
            reqStepNotExists = 300;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence execution
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if the initial StepId was written to the current step of the sequencer
               tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(initStepDescription,                        //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps in the sequence was succesfully cleared
                                                                        //After running this method, sequencer should stay in StepId 1, with StepDesription "Step 1"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes to one 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 1",                                   //Check if StepDescription changes to "Step 1" 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.AreEqual(numberOfSteps,                              //Check if number of the counted steps is equal to expected value
                tc.GetNumberOfStepsInSequence());
        }

        [Test, Order(541)]
        public void T541_RequestStepFromLowerToHigher()
        {
            //After running this method, sequencer should stay in StepId reqStep, with StepDesription "Step "+reqStep
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                tc.RequestStep(reqStep);
                for (short i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId changes to reqStep 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + reqStep.ToString(),               //Check if StepDescription changes to "Step " + reqStep 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(542)]
        public void T542_RequestStepFromHigherToLowerFirstCycle()
        {
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
                                                                        //After running this method, sequencer should stay in StepId reqStep+5, with StepDescription "Step "+reqStep+5
            tc.SequencerSingleCycleRun(() =>                            //with step status Running. Request step to the reqstep value will be performed in next PLC cycle as this 
            {                                                           //is "jump backwards" case

                for (short i = 0; i < reqStep + 5; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                for (short i = (short)(reqStep + 5); i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                tc.RequestStep(reqStep);
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep + 5,                                //Check if StepId changes to reqStep + 5
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + (reqStep + 5).ToString(),         //Check if StepDescription changes to "Step " + reqStep +5 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(543)]
        public void T543_RequestStepFromHigherToLowerSecondCycle()
        {
            //After running this method, sequencer should stay in StepId reqStep, with StepDescription "Step "+reqStep
            tc.SequencerSingleCycleRun(() =>                            //with step status Running. Request step to the reqstep value from the previous PLC cycle is going to be
            {                                                           //performed in this PLC cycle as this is "jump backwards" case

                for (short i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId changes to reqStep 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + reqStep.ToString(),               //Check if StepDescription changes to "Step " + reqStep 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(544)]
        public void T544_RequestStepToNotExistingStepFirstCycle()
        {
            //After running this method, sequencer should stay in StepId reqStep, with StepDescription "Step "+reqStep
            tc.SequencerSingleCycleRun(() =>                            //with step status Running. Request step to the reqStepNotExists has not yet been performed. 
            {                                                           //Searching for reqStepNotExists, if such a step exists after calling method RequestStep(), it should be
                                                                        //found in this first PLC cycle. If such s step exists before calling method RequestStep(), it should be found
                tc.RequestStep(reqStepNotExists);                       //in second PLC cycle. If such a step does not exists, it should be discovered in the third PLC cycle
                for (short i = 0; i < numberOfSteps; i++)               //after calling Open() method
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId stays reqStep as before
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + reqStep.ToString(),               //Check if StedDescription stays in "Step " + reqStep as before
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status stays in Running as before
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(545)]
        public void T545_RequestStepToNotExistingStepSecondCycle()
        {
            //After running this method, sequencer should stay in StepId reqStep, with StepDescription "Step "+reqStep
            tc.SequencerSingleCycleRun(() =>                            //with step status Running. Request step to the reqStepNotExists has not yet been performed.
            {                                                           //Searching for reqStepNotExists, if such a step exists after calling method RequestStep(), it should be
                                                                        //found in previous PLC cycle. If such s step exists before calling method RequestStep(), it should be found
                for (short i = 0; i < numberOfSteps; i++)              //in this second PLC cycle. If such a step does not exists, it should be discovered in the third PLC cycle
                {                                                       //after calling Open() method
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            Assert.AreEqual(reqStep,                                    //Check if StepId stays reqStep as before
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + reqStep.ToString(),               //Check if StepId stays in "Step " + reqStep as before
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status stays in Running as before
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(546)]
        public void T546_RequestStepToNotExistingStepThirdCycle()
        {
            //After running this method, sequencer should return an error "REQUESTED STEP_ID DOES NOT EXIST"
            tc.SequencerSingleCycleRun(() =>                            //Request step to the reqStepNotExists should be processed in this PLC cycle.
            {                                                           //Searching for reqStepNotExists should finished. If such a step does not exists, 
                                                                        //it should be discovered in the this third PLC cycle after calling OpenSequence() method
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId stays reqStep as before
                tc._sequencer._currentStepId.Synchron);
            //2Bremoved =>
            Assert.AreEqual("REQUESTED STEP_ID: " +                     //Check if StepDescription changes from value "Step " + reqstep to the expected error message
                reqStepNotExists.ToString() + " DOES NOT EXIST",
                tc._sequencer._currentStepDescription.Synchron);
            //<= 2Bremoved
            Assert.AreEqual("REQUESTED STEP_ID: " +                     //Check if messenger returns the expected error message
                reqStepNotExists.ToString() + " DOES NOT EXIST",
                tc.GetTextOfTheMostImportantMessage());
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(50,                                         //Check if the sequencer error is of the type StepWithRequestedIdDoesNotExists
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(547)]
        public void T547_RequestStepWhilePreviousRequestStepHasNotBeenYetProcessed()
        {
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
                                                                        //After running this method, sequencer should stay in error as second RequestStep() method has is 
            tc.SequencerSingleCycleRun(() =>                            //going to be called, while previous is not yet perfromed()
            {
                for (short i = 0; i < reqStep + 5; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                tc.RequestStep(reqStep);                                //First RequestStep() method call. It is not going to be executed in this PLC cycle, as it is "jump backwards" case
                for (short i = (short)(reqStep + 5); i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                tc.RequestStep((short)(reqStep + 10));                 //Second RequestStep() method call. 
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            //2Bremoved
            Assert.AreEqual("REQUESTED STEP_ID: " +                     //Check if StepDescription changes to the expected error message
                (reqStep + 10).ToString() +
                " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: " +
                 reqStep.ToString() + " HAS NOT BEEN YET PERFORMED!",
                 tc._sequencer._currentStepDescription.Synchron);
            //<= 2Bremoved
            Assert.AreEqual("REQUESTED STEP_ID: " +                     //Check if messenger returns the expected error message
                (reqStep + 10).ToString() +
                " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: " +
                 reqStep.ToString() + " HAS NOT BEEN YET PERFORMED!",
                 tc.GetTextOfTheMostImportantMessage());
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(60,                                         //Check if the sequencer error is of the type SeveralRequestStep
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(550)]
        public void T550_PrepareForDisabledStep()
        {
            numberOfSteps = 10;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
                                                                        //After running this method, sequencer should stay in StepId 1, with StepDescription "Step 1"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (short i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes to one 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 1",                                   //Check if StepDescription changes to "Step 1" 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.AreEqual(numberOfSteps,                              //Check if number of the counted steps is equal to expected value
                tc.GetNumberOfStepsInSequence());
        }

        [Test, Order(551)]
        public void T551_DisableStepEnabledAndActiveInPreviousPLCcycle()
        {
            //After running this method, sequencer should change to StepId 2, with StepDescription "Step 2"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running, as previously runnig Step 1 has changed to disabled
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                tc.Step(1, false, "Initial step");
                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(10,                                     //Check if current step status changes to Disabled
                    tc._sequencer._currentStepStatus.Synchron);         //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
                for (short i = 2; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId changes to two 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 2",                                   //Check if StepDescription changes to "Step 2" 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(560)]
        public void T560_RequestStepCallingCyclicallyFirstCycle()
        {
            initCycle = 100;
            cycle = 0;
            numberOfSteps = 20;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance
            cycle = tc.SetRequestStepCycle(initCycle);                  //Set RequestStepCycle value to the PLC instance and store its value

            Assert.AreEqual(initCycle, cycle);                          //Check it the RequestStepCycle value was succesfully written

            tc.SequencerSingleCycleRun(() =>
            {
                Assert.AreEqual(0, tc.GetRequestStepCycle());           //RequestStepCycle should be zero at this moment, as Open() method has been called, and
                if (tc.Step(0, true, "Initial step"))                   //no active RequestStep() is "waiting in the queue"
                {
                    tc.RequestStep(10);                                 //New RequestStep() method call. After next Open() method call RequestStepCycle value
                }                                                       //should be incremented
            });
        }

        [Test, Order(561)]
        public void T561_RequestStepCallingCyclicallySecondCycle()
        {
            cycle = 0;
            //After running this method, sequencer should change to StepId 10, with StepDescription "Step 10"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running, as Step 10 was requested by RequestStep() method in the previous
            {                                                           //PLC cycle
                Assert.AreEqual(1, tc.GetRequestStepCycle());           //RequestStepCycle should be one at this moment, as OpenSequence() method has been called, and
                tc.Step(0, true, "Step 0");                             //RequestStep() method has been called in the previous PLC cycle
                tc.Step(20, true, "Step 20");
                if (tc.Step(10, true, "Step 10"))
                {
                    cycle++;
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1, cycle);                                  //Check if step logic was executed, if not cycle stays at value zero.
            Assert.AreEqual(0, tc.GetRequestStepCycle());               //RequestStepCycle should be zero at this moment, as requested step has been already started  
            Assert.AreEqual(10,                                         //Check if StepId changes to two 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 10",                                  //Check if StepDescription changes to "Step 2" 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(562)]
        public void T562_RequestStepCallingCyclicallyNext20Cycle()
        {
            cycle = tc.SetRequestStepCycle(initCycle);                 //Set RequestStepCycle value to the PLC instance and store its value
            Assert.AreEqual(initCycle, cycle);                         //Check it the RequestStepCycle value was succesfully written
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //Step 10 is active from the previous PLC cycle, so at first call of this method Step 10 is executed
            {                                                           //and new RequestStep() to step 20 is requested. At second call and all next calls Step 20 is executed 
                                                                        //and new RequestStep() to step 20 is requested.
                if (cycle == 0)                                         //First call
                {
                    Assert.AreEqual(0, tc.GetRequestStepCycle());       //RequestStepCycle should be zero at this moment, as Open() method has been called, and
                }                                                       //no active RequestStep() is "waiting in the queue"
                else                                                    //All the others call
                {
                    Assert.AreEqual(1, tc.GetRequestStepCycle());       //RequestStepCycle should be one at this moment, as Open() method has been called, and
                }                                                       //one active RequestStep() to the Step 20 is "waiting in the queue"
                                                                        //This does not produce error, as the previous StepId of the previously requested step (20) is same as the 
                                                                        //StepId of the newly requested StepId
                tc.Step(0, true, "Step 0");
                if (tc.Step(20, true, "Step 20"))
                {
                    cycle++;
                    tc.RequestStep(20);
                }
                if (tc.Step(10, true, "Step 10"))
                {
                    cycle++;
                    tc.RequestStep(20);
                }
                Assert.AreEqual(0, tc.GetRequestStepCycle());           //RequestStepCycle should be zero at this moment, previous RequestStep() method has been completed
                                                                        //new RequestStep() method has been triggered again, but OpenSequence() method has not yet been called again.
            }, endCondition: () => cycle > 21);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0, tc.GetRequestStepCycle());               //RequestStepCycle should be zero at this moment, previous RequestStep() method has been completed               
                                                                        //new RequestStep() method has been triggered again, but OpenSequence() method has not yet been called again.
            Assert.AreEqual(20,                                         //Check if StepId changes to twenty 
               tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 20",                                  //Check if StepDescription changes to "Step 20" 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
        }

        [Test, Order(570)]
        public void T570_SetStepMode()
        {
            initStepId = 32767;
            numberOfSteps = 30;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,
                tc._sequencer._currentStepId.Synchron);                 //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual(initStepDescription,
                tc._sequencer._currentStepDescription.Synchron);        //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared

            tc.SequencerSingleCycleRun(() =>                            //After execution of this method, actual StepId should have value zero, as step mode is active and no StepIn() method was called
            {                                                           //StepDescription should have value "(>Initial step<)" as step zero is in order of the execution, but as the step mode is active  
                                                                        //and no StepIn() method was called
                if (tc.Step(0, true, "Initial step"))                   //Sequencer method GetNumberOffStepsInSequence() should return the value equal to value of the variable numberOfSteps 
                {                                                       //as it is number of the calls of the sequencer method Step()
                    tc.StepCompleteWhen(true);                          //Actual step should stay in status ReadyToRun
                }
                for (short i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays in 0. As the StepId uniqueness control control has already been performed, 
                tc._sequencer._currentStepId.Synchron);                 //the step logic could be executed, but no StepIn() method was called                   
            Assert.AreEqual("(>Initial step<)",                         //Check if StepDescription stays in "(>Initial step<)". As the StepId uniqueness control control has already
                tc._sequencer._currentStepDescription.Synchron);        //been performed, the step logic could be executed but no StepIn() method was called.
            Assert.AreEqual(numberOfSteps,                              //Check if number of the counted steps is equal to expected value
                tc.GetNumberOfStepsInSequence());
            Assert.AreEqual(20,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(571)]
        public void T571_StepModeCycleWithStepInMethodCalled()
        {
            tc.SequencerSingleCycleRun(() =>                            //After execution of this method, actual StepId should have value one, as step mode is active and StepIn() method was called
            {                                                           //StepDescription should have value "(>Step 1<)" as step one is in order of the execution, the step mode is active and  
                                                                        //StepIn() method was called
                tc.StepIn();                                            //Call of the method StepIn() executes logic of the step that is in order of the execution and in RedyToRun state                                            
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(0,                                      //Check if StepId stays in 0. Step logic was executed, but no another call of the method Step() has not been yet performed
                    tc._sequencer._currentStepId.Synchron);             //so StepId stays at its last value.                
                Assert.AreEqual("Initial step",                         //Check if StepDescription changes from "(>Initial step<)" to "Initial step" as the step logic was executed and step is Done.
                    tc._sequencer._currentStepDescription.Synchron);    //As no another call of the method Step() has not been yet performed, StepDescription stays at its last value.
                Assert.AreEqual(40,                                     //Check if current step status has changed from ReadyToRun to Done.
                    tc._sequencer._currentStepStatus.Synchron);         //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
                if (tc.Step(1, true, "Step 1"))

                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 2; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1.
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 1<)",                               //Check if StepDescription changes from "Initial step" to "(>Step 1<)".
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(numberOfSteps,                              //Check if number of the counted steps is equal to expected value
                tc.GetNumberOfStepsInSequence());
            Assert.AreEqual(20,                                         //Check if current step status has changed from Done to ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(572)]
        public void T572_StepModeCycleWithoutStepInMethodCalled()
        {
            tc.SequencerSingleCycleRun(() =>                            //After execution of this method, actual StepId should have value same as before as step mode is active and no StepIn() method  
            {                                                           //is called in this PLC cycle. StepDescription should have value same as before as step mode is on and no StepIn() method is
                                                                        //called in this PLC cycle.
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1.
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 1<)",                               //Check if StepDescription changes from "Initial step" to "(>Step 1<)".
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(numberOfSteps,                              //Check if number of the counted steps is equal to expected value
                tc.GetNumberOfStepsInSequence());
            Assert.AreEqual(20,                                         //Check if current step status has changed from Done to ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        //[Test, Order(573)]
        //public void T573_UnforceStepModeCallWhileForceStepModeSignalStaysTrue()
        //{
        //    tc.SequencerSingleCycleRun(() => tc.SetStepMode());                 //This set sequencer into the step mode
        //    tc.UnforceStepMode();                                       //UnforceStep mode call
        //                                                                //Force step mode signal remains true, and UnforceStepMode method is called. Sequencer should stay in step mode
        //    tc.SequencerSingleCycleRun(() =>
        //    {
        //        if (tc.Step(0, true, "Initial step"))
        //        {
        //            tc.StepCompleteWhen(true);
        //        }
        //        for (ushort i = 1; i < numberOfSteps; i++)
        //        {
        //            if (tc.Step((short)i, true, "Step " + i.ToString()))
        //            {
        //                tc.StepCompleteWhen(true);
        //            }
        //        }
        //        if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
        //        {
        //            tc.SequenceComplete();
        //        }
        //    });
        //    Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1.
        //        tc._sequencer._currentStepId.Synchron);
        //    Assert.AreEqual("(>Step 1<)",                               //Check if StepDescription changes from "Initial step" to "(>Step 1<)".
        //        tc._sequencer._currentStepDescription.Synchron);
        //    Assert.AreEqual(numberOfSteps,                       //Check if number of the counted steps is equal to expected value
        //        tc.GetNumberOfStepsInSequence());
        //    Assert.AreEqual(20,                                         //Check if current step status has changed from Done to ReadyToRun.
        //        tc._sequencer._currentStepStatus.Synchron);       //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        //}

        //[Test, Order(574)]
        //public void T574_UnforceStepModeCallAfterForceStepModeSignalBecomesFalse()
        //{
        //    tc.SingleCycleRun(() => tc.SequencerForceStep(false));        //ForceStep mode signal falls to false
        //    tc.AddEmptyCycle();
        //    tc.UnforceStepMode();                                       //UnforceStep mode call

        //    tc.AddEmptyCycle();
        //    tc.AddEmptyCycle();
        //    //Force step mode signal falls to false, and UnforceStepMode method is called. Sequencer should switch back to cyclic mode
        //    tc.SequencerSingleCycleRun(() =>                            //So after execution of this method, actual StepId should have value of numberOfSteps, StepDescription should have value
        //    {                                                           //of "Step " + numberOfSteps, and step status should be in Done state.
        //        if (tc.Step(0, true, "Initial step"))
        //        {
        //            tc.StepCompleteWhen(true);
        //        }
        //        for (ushort i = 1; i < numberOfSteps; i++)
        //        {
        //            if (tc.Step((short)i, true, "Step " + i.ToString()))
        //            {
        //                tc.StepCompleteWhen(true);
        //            }
        //        }
        //        if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
        //        {
        //            tc.SequenceComplete();
        //        }
        //    });
        //    Assert.AreEqual(numberOfSteps,                       //Check if StepId changes from 1 to numberOfSteps.
        //        tc._sequencer._currentStepId.Synchron);
        //    Assert.AreEqual("Step " + numberOfSteps.ToString(),  //Check if StepDescription changes from "(>Step 1<)" to "Step " + numberOfSteps.
        //        tc._sequencer._currentStepDescription.Synchron);
        //    Assert.AreEqual(40,                                         //Check if current step status has changed from Done to Done.
        //        tc._sequencer._currentStepStatus.Synchron);       //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        //    tc.AddEmptyCycle();
        //}

        [Test, Order(580)]
        public void T580_ExternalSequenceStepModeFirstCycle()
        {
            numberOfSteps = 10;
            cycle = 0;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,
                tc._sequencer._currentStepId.Synchron);                 //Check if the initial StepId was written to the current step of the sequencer
            Assert.AreEqual(initStepDescription,
                tc._sequencer._currentStepDescription.Synchron);        //Check if the initial StepDescription was written to the current step of the sequencer
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared

            tc.SequencerSingleCycleRun(() =>                            //After execution of this method, actual StepId should have value zero, as stepmode is on and no StepIn() method was called
            {                                                           //StepDescription should have value "(>Initial step<)" as step zero is in order of the execution, but as the stepmode is on and  
                                                                        //no StepIn() method was called
                if (tc.Step(0, true, "Initial step"))                   //Sequencer method GetNumberOfStepsInSequence() should return the value equal to value of the variable numberOfSteps 
                {                                                       //as it is number of the calls of the sequencer method Step()
                    tc.StepCompleteWhen(true);                          //Actual step should stay in status ReadyToRun
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays in 0. As the StepId uniqueness control control has already been performed, 
                tc._sequencer._currentStepId.Synchron);                 //the step logic could be executed, but no StepIn() method was called                   
            Assert.AreEqual("(>Initial step<)",                         //Check if StepDescription stays in "(>Initial step<)". As the StepId uniqueness control control has already
                tc._sequencer._currentStepDescription.Synchron);        //been performed, the step logic could be executed but no StepIn() method was called.
            Assert.AreEqual(numberOfSteps,                              //Check if number of the counted steps is equal to expected value
                tc.GetNumberOfStepsInSequence());
            Assert.AreEqual(20,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(581)]
        public void T581_ExternalSequenceStepModeNextNcyclesWithStepInMethodCall()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of numberOfSteps, as stepmode is on and StepIn() 
            {                                                           //method is called in each PLC cycle. StepDescription should have value of "Step " + numberOfSteps as stepmode is on
                                                                        //and StepIn() method is called in each PLC cycle. Step status should have state Done
                tc.StepIn();
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }

                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.StepIn();

                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }

                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
                cycle++;
            }, endCondition: () => cycle > numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps,                              //Check if StepId changes to numberOfSteps.  
                tc._sequencer._currentStepId.Synchron);                 //                
            Assert.AreEqual("Step " + numberOfSteps.ToString(),         //Check if StepDescription changes to "Step " + numberOfSteps. 
                tc._sequencer._currentStepDescription.Synchron);        //
            Assert.AreEqual(40,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(582)]
        public void T582_ExternalInvalidMode()
        {
            numberOfSteps = 10;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SingleCycleRun(() => tc.SetCyclicMode());                //Set sequencer into the cyclic mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepID uniqueness is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                                    //Set numberOfSteps to the testing instance

            tc.SequencerSingleCycleRun(() =>
            {
                for (short i = 0; i < numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }

                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(numberOfSteps - 1, tc._sequencer._currentStepId.Synchron);
                Assert.AreEqual("Step " + (numberOfSteps - 1).ToString(), tc._sequencer._currentStepDescription.Synchron);
                Assert.AreEqual(40, tc._sequencer._currentStepStatus.Synchron);
                tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString());
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + numberOfSteps.ToString(), tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30, tc._sequencer._currentStepStatus.Synchron);

            tc.SetInvalidMode();
            tc.SequencerSingleCycleRun(() =>
            {
                for (short i = 0; i <= numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("INVALID MODE OF THE SEQUENCER!!!", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(50, tc._sequencer._currentStepStatus.Synchron);
            Assert.IsTrue(tc._sequencer._sequencerHasError.Synchron);

        }

        [Test, Order(583)]
        public void T583_ExternalStepInError()
        {
            numberOfSteps = 10;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SingleCycleRun(() => tc.SetCyclicMode());                //Set sequencer into the cyclic mode
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepID uniqueness is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                                    //Set numberOfSteps to the testing instance

            tc.SequencerSingleCycleRun(() =>
            {
                for (short i = 0; i < numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }

                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(numberOfSteps - 1, tc._sequencer._currentStepId.Synchron);
                Assert.AreEqual("Step " + (numberOfSteps - 1).ToString(), tc._sequencer._currentStepDescription.Synchron);
                Assert.AreEqual(40, tc._sequencer._currentStepStatus.Synchron);
                tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString());
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + numberOfSteps.ToString(), tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30, tc._sequencer._currentStepStatus.Synchron);

            tc.SetCurrentStepToErrorState();
            tc.SequencerSingleCycleRun(() =>
            {
                for (short i = 0; i <= numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });

            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps, tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + numberOfSteps.ToString(), tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(50, tc._sequencer._currentStepStatus.Synchron);
            Assert.IsFalse(tc._sequencer._sequencerHasError.Synchron);
        }

        [Test, Order(590)]
        public void T590_PLCSequenceCheckUniquenessStepModeFirstCycle()
        {
            initStepId = 32750;
            initStepDescription = "---test---init---";
            lastStepId = 32758;
            lastStepDescription = "This is last step of the sequence";
            cycleCount = 0;
            restoreCycleCount = 0;
            numberOfSteps = 0;

            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsNotChecked();                               //Set sequence as not checked, so StepId uniqueness control is going to be performed on next sequence run
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._stepID.Synchron = lastStepId;                           //Set StepId of the last step in the PLC instance
            tc._enabled.Synchron = true;                                //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription;         //Set StepDescription of the last step in the PLC instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = true;                            //Set all step execution flag in the PLC testing instance. If this value so as the _FinishStep are true, 
                                                                        //all steps are executed and finished in one PLC cycle
            tc._finishStep.Synchron = true;                             //Set all step execution flag in the PLC testing instance. If this value so as the _RunOneStep are true, 
                                                                        //all steps are executed and finished in one PLC cycle. If this value so as the _RunOneStep are true 
            tc._restore.Synchron = false;                                 //Reset sequence restore flag in the PLC testing instance
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if the initial StepId was written to the current step of the sequencer
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(initStepDescription,                        //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc.GetPreviousNumberOfStepsInSequence());//Check if the number of the previous steps was succesfully cleared

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness 
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
            Assert.AreEqual(initStepDescription,
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed, 
                                                                        //the step logic is not executed, even if entering or transition conditions are met
        }

        [Test, Order(591)]
        public void T591_PLCSequenceCheckUniquenessStepModeSecondCycle()
        {
            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed
            numberOfSteps = tc.GetNumberOfStepsInSequence();            //Get the counted number of steps in sequence
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,						                    //Check if StepId changes from initStepId to 0. As the StepId uniqueness control control has already been performed, 
                tc._sequencer._currentStepId.Synchron);                 //the step logic is executed                    
            Assert.AreEqual("(>Step number 0<)",                        //Check if StepDescription changes from initStepDescription to "(>Step number 0<)". As the StepId uniqueness control control has already
                tc._sequencer._currentStepDescription.Synchron);        //been performed, the step logic is executed.
            Assert.AreEqual(20,                                         //Check if current step status is Done.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(592)]
        public void T592_PLCSequenceCheckUniquenessStepModeThirdCycle()
        {
            cycleCount = tc._cycleCount.Synchron;
            tc.StepIn();
            tc._runPLCinstanceOnce.Synchron = true;
            running = true;
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(cycleCount + 1,                             //Check if only one PLC cycle was performed 
                tc._cycleCount.Synchron);
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(1,                                          //Check if StepId changes, as StepId uniqueness control control has already been performed
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step number 1<)",                        //Check if HmiMessage changes, as StepId uniqueness control control has already been performed
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50, Error := 50
        }

        [Test, Order(593)]
        public void T593_PLCSequenceResetSequenceInStepMode()
        {
            //After Sequence reset, counting the steps so as the checking the StepId uniqueness control has to be performed again
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            restoreCycleCount = tc._restoreCycleCount.Synchron;         //Store the actual sequence restore cycle counter
            tc._restore.Synchron = true;                                //Set sequence restore flag in the PLC testing instance. If this value is true, PLC resets the sequencer in the 
            running = true;                                             //PLC instance. Once it is done, PLC reset this variable to false.
            while (running)
            {
                running = tc._restore.Synchron;
            }
            Assert.AreEqual(restoreCycleCount + 1,                      //Check if only one restore of the sequencer was was performed, as it was expected
                tc._restoreCycleCount.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc.GetPreviousNumberOfStepsInSequence());//Check if the number of the previous steps was succesfully cleared
        }

        [Test, Order(594)]
        public void T594_PLCSequenceNotUniqueStepIdInStepModeFirstCycle()
        {
            initStepId = 200;
            lastStepId = 500;

            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._stepID.Synchron = lastStepId;                           //Set StepId of the last step in the PLC instance
            tc._enabled.Synchron = true;                                //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription;         //Set StepDescription of the last step in the PLC instance
            tc._restore.Synchron = false;                                 //Reset sequence restore flag in the PLC testing instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness control
            //No Step logic is executed, even if entering or transition conditions are met 
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
            Assert.AreNotEqual(initStepDescription,
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription change from initStepDescription due to StepId uniqueness control control error. 
            Assert.AreEqual("ERROR NOT UNIQUE STEP_ID " +               //Check if StepDescription change, due to StepId uniqueness control error to the expected error message
                    lastStepId.ToString(),
                    tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual("ERROR NOT UNIQUE STEP_ID " +               //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                    tc.GetTextOfTheMostImportantMessage());
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(10,                                         //Check if the sequencer error is of the type UidUniqueness
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(595)]
        public void T595_PLCSequenceNotUniqueStepIdInStepModeSecondCycle()
        {
            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed
            numberOfSteps = tc.GetNumberOfStepsInSequence();            //Get the counted number of steps in sequence
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness control error
                                                                        //In case of StepId uniqueness control error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(10,                                         //Check if the sequencer error is of the type NotUniqueStepId
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(true, tc._sequencer._runOneStep.Synchron);  //Check if step logic was not entered due to sequence error, if yes _RunOneStep is to be reseted to false         
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
            Assert.AreEqual("ERROR NOT UNIQUE STEP_ID " +               //Check if StepDescription change, due to StepId uniqueness control control error to the expected error message
                    lastStepId.ToString(),
                    tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual("ERROR NOT UNIQUE STEP_ID " +               //Check if messenger returns the expected error message
                    lastStepId.ToString(),
                    tc.GetTextOfTheMostImportantMessage());
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(596)]
        public void T596_PLCSequenceResetInStepModeAfterNotUniqueStepIdError()
        {
            //After Sequence reset, counting the steps so as the checking the StepId uniqueness control has to be performed again
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            restoreCycleCount = tc._restoreCycleCount.Synchron;         //Store the actual sequence restore cycle counter
            tc._restore.Synchron = true;                                //Set sequence restore flag in the PLC testing instance. If this value is true, PLC resets the sequencer in the 
            running = true;                                             //PLC instance. Once it is done, PLC reset this variable to false.
            while (running)
            {
                running = tc._restore.Synchron;
            }
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(restoreCycleCount + 1,                      //Check if only one restore of the sequencer was was performed, as it was expected
                tc._restoreCycleCount.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc.GetPreviousNumberOfStepsInSequence());//Check if the number of the previous steps was succesfully cleared
        }

        [Test, Order(597)]
        public void T597_PLCSequenceAfterErrorResetInStepModeFirstCycle()
        {
            initStepId = 800;
            lastStepId = 32765;

            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._stepID.Synchron = lastStepId;                           //Set StepId of the last step in the PLC instance
            tc._enabled.Synchron = true;                                //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription;         //Set StepDescription of the last step in the PLC instance
            tc._restore.Synchron = false;                                 //Reset sequence restore flag in the PLC testing instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness control
            //No Step logic is executed, even if entering or transition conditions are met 
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
            Assert.AreEqual(initStepDescription,                        //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed,
                tc._sequencer._currentStepDescription.Synchron);        //the step logic is not executed, even if entering or transition conditions are met      
            Assert.AreEqual(false,                                      //Check if sequence error has been reseted
                tc.SequencerHasError());
            Assert.AreEqual(0,                                          //Check if the sequencer error type has been reseted to the type noerror
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
        }

        [Test, Order(598)]
        public void T598_PLCSequenceAfterErrorResetInStepModeSecondCycle()
        {
            //During this second sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed
            numberOfSteps = tc.GetNumberOfStepsInSequence();            //Get the counted number of steps in sequence
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness control error
                                                                        //In case of StepId uniqueness control error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(0,                                          //Check if StepId changes from initStepId to first StepId. As the sequence error has been already reseted and the StepId uniqueness control control has been 
                tc._sequencer._currentStepId.Synchron);                 //already performed again after reset, the step logic is executed.                    
            Assert.AreEqual("(>Step number 0<)",                        //Check if StepDescription changes from initStepDescription to "(>Step number 0<)". As the sequence error has been already reseted and the StepId uniqueness control control has been
                tc._sequencer._currentStepDescription.Synchron);        //already performed again after reset, the step logic is executed. 
            Assert.AreEqual(20,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(599)]
        public void T599_PLCSequenceAfterErrorResetInStepModeThirdCycle()
        {
            //During this third sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness control error
                                                                        //In case of StepId uniqueness control error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc.StepIn();                                                //Triggers StepIn() method
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(0,                                          //Check if StepId changes, to first StepId
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step number 0",                            //Check if StepId changes to "Step number 0"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status is Done.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(600)]
        public void T600_PLCSequenceAfterErrorResetInStepModeFourthCycle()
        {
            //During this fourth sequence run, Step logic is already executed as steps counting and checking its StepId uniqueness control
            //has already been performed
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness control error
                                                                        //In case of StepId uniqueness control error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(1,                                          //Check if StepId changes, to 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step number 1<)",                        //Check if StepId changes to "Step number 1"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status is Done.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(601)]
        public void T601_PLCSequencePrepareForInvalidMode()
        {
            initStepId = 32750;
            initStepDescription = "---test---init---";
            lastStepId = 32758;
            lastStepDescription = "This is last step of the sequence";
            cycleCount = 0;
            restoreCycleCount = 0;
            numberOfSteps = 0;

            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsNotChecked();                               //Set sequence as not checked, so StepId uniqueness control is going to be performed on next sequence run
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._stepID.Synchron = lastStepId;                           //Set StepId of the last step in the PLC instance
            tc._enabled.Synchron = true;                                //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription;         //Set StepDescription of the last step in the PLC instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = true;                            //Set all step execution flag in the PLC testing instance. If this value so as the _FinishStep are true, 
                                                                        //all steps are executed and finished in one PLC cycle
            tc._finishStep.Synchron = true;                             //Set all step execution flag in the PLC testing instance. If this value so as the _RunOneStep are true, 
                                                                        //all steps are executed and finished in one PLC cycle. If this value so as the _RunOneStep are true 
            tc._restore.Synchron = false;                                 //Reset sequence restore flag in the PLC testing instance
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if the initial StepId was written to the current step of the sequencer
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(initStepDescription,                        //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc.GetPreviousNumberOfStepsInSequence());//Check if the number of the previous steps was succesfully cleared

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness 
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
            Assert.AreEqual(initStepDescription,
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed, 
                                                                        //the step logic is not executed, even if entering or transition conditions are met
        }

        [Test, Order(602)]
        public void T602_PLCSequenceInvalidSequencerMode()
        {
            tc.SetInvalidMode();                                        //During this second sequence run, Step logic should be executed under normal condition as steps counting and checking its StepId uniqueness
                                                                        //has already been performed. But as the sequencer mode is invalid, Step logic should not be entered.
            numberOfSteps = tc.GetNumberOfStepsInSequence();            //Get the counted number of steps in sequence
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness error
                                                                        //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(true, tc._sequencer._runOneStep.Synchron); //Check if step logic was not entered as sequence has IOnvalid mode. If _RunOneStep stays at true, step logic was not entered and executed.
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if StepId stays at initStepId. As the sequence has invalid mode the step logic should not be executed. 
                tc._sequencer._currentStepId.Synchron);                 
            Assert.AreEqual("INVALID MODE OF THE SEQUENCER!!!",         //Check if StepDescription changes from initStepDescription to "INVALID MODE OF THE SEQUENCER!!!".
                tc._sequencer._currentStepDescription.Synchron);       
            Assert.AreEqual(50,                                         //Check if current step status is Error.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsTrue(tc._sequencer._sequencerHasError.Synchron);
        }

        [Test, Order(603)]
        public void T603_PLCSequencePrepareForStepInError()
        {
            initStepId = 32750;
            initStepDescription = "---test---init---";
            lastStepId = 32758;
            lastStepDescription = "This is last step of the sequence";
            cycleCount = 0;
            restoreCycleCount = 0;
            numberOfSteps = 0;

            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsNotChecked();                               //Set sequence as not checked, so StepId uniqueness control is going to be performed on next sequence run
            tc.ClearNumberOfSteps();                                    //Clear internal step counters, so number of steps is going to be counted again on next sequence run
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc._runPLCinstanceOnce.Synchron = false;                    //Reset one time calling of the PLC testing instance
            tc._runPLCinstanceCyclicaly.Synchron = false;               //Reset cyclical calling of the PLC testing instance
            tc._stepID.Synchron = lastStepId;                           //Set StepId of the last step in the PLC instance
            tc._enabled.Synchron = true;                                //Set step condition of the last step in the PLC instance to enabled
            tc._stepDescription.Synchron = lastStepDescription;         //Set StepDescription of the last step in the PLC instance
            tc._runOneStep.Synchron = false;                            //Reset one step execution flag in the PLC testing instance
            tc._runAllSteps.Synchron = true;                            //Set all step execution flag in the PLC testing instance. If this value so as the _FinishStep are true, 
                                                                        //all steps are executed and finished in one PLC cycle
            tc._finishStep.Synchron = true;                             //Set all step execution flag in the PLC testing instance. If this value so as the _RunOneStep are true, 
                                                                        //all steps are executed and finished in one PLC cycle. If this value so as the _RunOneStep are true 
            tc._restore.Synchron = false;                                 //Reset sequence restore flag in the PLC testing instance
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if the initial StepId was written to the current step of the sequencer
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(initStepDescription,                        //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(0, tc.GetNumberOfStepsInSequence());        //Check if the number of the steps was succesfully cleared
            Assert.AreEqual(0, tc.GetPreviousNumberOfStepsInSequence());//Check if the number of the previous steps was succesfully cleared

            //During this first sequence run, number of steps should be counting and checking its StepId uniqueness 
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(initStepId,                                 //Check if StepId does not change. As the StepId uniqueness control control has not yet been performed, the step logic is not 
                        tc._sequencer._currentStepId.Synchron);         //executed, even if entering or transition conditions are met                     
            Assert.AreEqual(initStepDescription,
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription does not change. As the StepId uniqueness control control has not yet been performed, 
                                                                        //the step logic is not executed, even if entering or transition conditions are met
        }

        [Test, Order(604)]
        public void T604_PLCSequenceStepInError()
        {
            tc.SetCurrentStepToErrorState();                            //During this second sequence run, Step logic should be executed under normal condition as steps counting and checking its StepId uniqueness
                                                                        //has already been performed. But as the scurrent step is in error state, Step logic should not be entered.
            numberOfSteps = tc.GetNumberOfStepsInSequence();            //Get the counted number of steps in sequence
            cycleCount = tc._cycleCount.Synchron;                       //Store the actual PLC cycle counter
            tc._runAllSteps.Synchron = false;                           //Reset all step execution flag in the PLC testing instance
            tc._runOneStep.Synchron = true;                             //This should be false after one PLC cycle run in case of not StepId uniqueness error
                                                                        //In case of StepId uniqueness error occured it stays true, that means no entrance into Step body in the PLC has been performed
            tc._runPLCinstanceOnce.Synchron = true;                     //Set one time calling of the PLC testing instance. If this value is true, PLC runs one PLC cycle, at the end PLC
            running = true;                                             //reset this variable to false
            while (running)
            {
                running = tc._runPLCinstanceOnce.Synchron;
            }
            Assert.AreEqual(cycleCount + 1, tc._cycleCount.Synchron);   //Check if only one PLC cycle was performed, as it was expected    
            Assert.AreNotEqual(0, tc.GetNumberOfStepsInSequence());     //Check if some steps inside PLC sequence were counted
            Assert.AreEqual(tc.GetNumberOfStepsInSequence(),            //Check if number of the stored steps is same as the number of the counted steps
                               tc.GetPreviousNumberOfStepsInSequence());
            Assert.AreEqual(true, tc._sequencer._runOneStep.Synchron); //Check if step logic was not entered as sequence has IOnvalid mode. If _RunOneStep stays at true, step logic was not entered and executed.
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if StepId stays at initStepId. As the sequence has invalid mode the step logic should not be executed. 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(initStepDescription,                        //Check if StepDescription stays at initStepDescription.
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(50,                                         //Check if current step status is Error.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30, Done:= 40, Error := 50
            Assert.IsFalse(tc._sequencer._sequencerHasError.Synchron);
        }

        [Test, Order(610)]
        public void T610_StepModeFirstCycle()
        {
            initStepId = 32767;
            numberOfSteps = 10;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(initStepId,                                 //Check if the initial StepId was written to the current step of the sequencer
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual(initStepDescription,                        //Check if the initial StepDescription was written to the current step of the sequencer
                tc._sequencer._currentStepDescription.Synchron);
            //After running this method, sequencer should stay in StepId 0, with StepDescription "(>Initial step<)"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays 0. As the StepId uniqueness control control has already been performed, the step logic is executed.
                tc._sequencer._currentStepId.Synchron);                 //But as no StepIn() method was called, sequence should stay in step 0                  
            Assert.AreEqual("(>Initial step<)",                         //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has 
                tc._sequencer._currentStepDescription.Synchron);        //already been performed, the step logic is executed. 
            Assert.AreEqual(20,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(611)]
        public void T611_StepModeStepIn()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            {                                                           //with step status Running
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                // Step 0
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 1
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 2
                if (tc.Step(2, true, "Step 2"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 3
                if (tc.Step(3, true, "Step 3"))
                {
                    tc.StepCompleteWhen(false);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }

                if (cycle <= 3)
                {
                    tc.UpdateCurrentStepDetails();
                    Assert.AreEqual(cycle,                              //Check if StepId changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        tc._sequencer._currentStepId.Synchron);         //and first 3 steps are completed immediately as they contain Await() method with value true
                    Assert.AreEqual("(>Step " +                         //Check if StepDescription changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        cycle.ToString() + "<)",                        //and first 3 steps are completed immediately as they contain Await() method with value true
                        tc._sequencer._currentStepDescription.Synchron);
                    Assert.AreEqual(20,                                 //Check if current step status is ReadyToRun.
                        tc._sequencer._currentStepStatus.Synchron);     //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
                }
            }, endCondition: () => cycle >= numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId changes to 3. 
                tc._sequencer._currentStepId.Synchron);                 //But as no StepIn() method was called, sequence should stay in step 0                  
            Assert.AreEqual("Step 3",                                   //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has 
                tc._sequencer._currentStepDescription.Synchron);        //already been performed, the step logic is executed. 
            Assert.AreEqual(30,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(612)]
        public void T612_StepModeChangeStepIdDuringExecution()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in error,
            {                                                           //ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 3=>5"
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                // Step 0
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 1
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 2
                if (tc.Step(2, true, "Step 2"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 3 => 5
                if (tc.Step(5, true, "Step 3"))
                {
                    tc.StepCompleteWhen(false);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            }, endCondition: () => cycle >= numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(5,                                          //Check if StepId changes to 5
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 3=>5",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual("ERROR STEP_ID CHANGED DURING STEP EXECUTION FROM: 3=>5",
                    tc.GetTextOfTheMostImportantMessage());             //Check if messenger returns the expected error message
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(20,                                         //Check if the sequencer error is of the type UidNumberChangedDuringExecution
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(613)]
        public void T613_StepModeRestoreAfterChangeStepIdDuringExecutionError()
        {
            cycle = 0;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer

            tc.SequencerSingleCycleRun(() =>
            {
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
        }

        [Test, Order(614)]
        public void T614_StepModePrepareSequenceBeforeChangeOrderTest()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            {                                                           //with step status Running
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                // Step 0
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 1
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 2
                if (tc.Step(2, true, "Step 2"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 3
                if (tc.Step(3, true, "Step 3"))
                {
                    tc.StepCompleteWhen(false);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }

                if (cycle <= 3)
                {
                    tc.UpdateCurrentStepDetails();
                    Assert.AreEqual(cycle,                              //Check if StepId changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        tc._sequencer._currentStepId.Synchron);         //and first 3 steps are completed immediately as they contain Await() method with value true
                    Assert.AreEqual("(>Step " +                         //Check if StepDescription changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        cycle.ToString() + "<)",                        //and first 3 steps are completed immediately as they contain Await() method with value true
                        tc._sequencer._currentStepDescription.Synchron);
                    Assert.AreEqual(20,                                 //Check if current step status is ReadyToRun.
                        tc._sequencer._currentStepStatus.Synchron);     //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
                }
            }, endCondition: () => cycle >= numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId changes to 3. 
                tc._sequencer._currentStepId.Synchron);                 //But as no StepIn() method was called, sequence should stay in step 0                  
            Assert.AreEqual("Step 3",                                   //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has 
                tc._sequencer._currentStepDescription.Synchron);        //already been performed, the step logic is executed. 
            Assert.AreEqual(30,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(615)]
        public void T615_StepModeChangeStepOrder()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in error,
            {                                                           //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>2
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                // Step 0
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 1
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 3
                if (tc.Step(3, true, "Step 3"))
                {
                    tc.StepCompleteWhen(false);
                }
                // Step 2
                if (tc.Step(2, true, "Step 2"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            }, endCondition: () => cycle >= numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId stays at value 3 as before
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>2",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>2",
                tc.GetTextOfTheMostImportantMessage());                 //Check if messenger returns the expected error message
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(40,                                         //Check if the sequencer error is of the type UidOrderChangedDuringExecution
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(616)]
        public void T616_StepModeRestoreAfterChangeStepError()
        {
            cycle = 0;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer

            tc.SequencerSingleCycleRun(() =>
            {
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
        }

        [Test, Order(617)]
        public void T617_StepModePrepareSequenceBeforeCommentOutTest()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            {                                                           //with step status Running
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                // Step 0
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 1                                               //This section will be commented out in the next test   //
                if (tc.Step(1, true, "Step 1"))                         //                                                      //
                {                                                       //                                                      //
                    tc.StepCompleteWhen(true);                          //                                                      //
                }                                                       //                                                      //
                // Step 2                                               //                                                      //
                if (tc.Step(2, true, "Step 2"))                         //                                                      //
                {                                                       //                                                      //
                    tc.StepCompleteWhen(true);                          //                                                      //
                }                                                       //This section will be commented out in the next test   //
                // Step 3
                if (tc.Step(3, true, "Step 3"))
                {
                    tc.StepCompleteWhen(false);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }

                if (cycle <= 3)
                {
                    tc.UpdateCurrentStepDetails();
                    Assert.AreEqual(cycle,                              //Check if StepId changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        tc._sequencer._currentStepId.Synchron);         //and first 3 steps are completed immediately as they contain Await() method with value true
                    Assert.AreEqual("(>Step " +                         //Check if StepDescription changes in each of the first 3 cycles as StepIn() method is called in each cycle
                        cycle.ToString() + "<)",                        //and first 3 steps are completed immediately as they contain Await() method with value true
                        tc._sequencer._currentStepDescription.Synchron);
                    Assert.AreEqual(20,                                 //Check if current step status is ReadyToRun.
                        tc._sequencer._currentStepStatus.Synchron);     //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
                }
            }, endCondition: () => cycle >= numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId changes to 3. 
                tc._sequencer._currentStepId.Synchron);                 //But as no StepIn() method was called, sequence should stay in step 0                  
            Assert.AreEqual("Step 3",                                   //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has 
                tc._sequencer._currentStepDescription.Synchron);        //already been performed, the step logic is executed. 
            Assert.AreEqual(30,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(618)]
        public void T618_StepModeCommentOutTest()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in error,
            {                                                           //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                // Step 0
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                //// Step 1                                             //This section was uncommented in the previous test     //
                //if (tc.Step(1, true, "Step 1"))                       //                                                      //
                //{                                                     //                                                      //
                //    tc.StepCompleteWhen(true);                        //                                                      //
                //}                                                     //                                                      //
                //// Step 2                                             //                                                      //
                //if (tc.Step(2, true, "Step 2"))                       //                                                      //
                //{                                                     //                                                      //
                //    tc.StepCompleteWhen(true);                        //                                                      //
                //}                                                     //This section was uncommented in the previous test     //
                // Step 3
                if (tc.Step(3, true, "Step 3"))
                {
                    tc.StepCompleteWhen(false);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            }, endCondition: () => cycle >= numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId stays at value 3 as before
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 3=>1",
                tc.GetTextOfTheMostImportantMessage());                 //Check if messenger return the expected error message
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(40,                                         //Check if the sequencer error is of the type UidOrderChangedDuringExecution
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(619)]
        public void T619_StepModeRestoreSequenceAfterCommentOutError()
        {

            cycle = 0;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer

            tc.SequencerSingleCycleRun(() =>
            {
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
        }

        [Test, Order(620)]
        public void T620_StepModePrepareSequenceBeforeUncommentTest()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in StepId 3, with StepDescription "Step 3"
            {                                                           //with step status Running
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                // Step 0
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                //// Step 1                                             //This section will be uncommented in the next test     //
                //if (tc.Step(1, true, "Step 1"))                       //                                                      //
                //{                                                     //                                                      //
                //    tc.StepCompleteWhen(true);                        //                                                      //
                //}                                                     //                                                      //
                //// Step 2                                             //                                                      //
                //if (tc.Step(2, true, "Step 2"))                       //                                                      //
                //{                                                     //                                                      //
                //    tc.StepCompleteWhen(true);                        //                                                      //
                //}                                                     //This section will be uncommented in the next test     //
                // Step 3
                if (tc.Step(3, true, "Step 3"))
                {
                    tc.StepCompleteWhen(false);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            }, endCondition: () => cycle >= numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId changes to 3. 
                tc._sequencer._currentStepId.Synchron);                 //But as no StepIn() method was called, sequence should stay in step 0                  
            Assert.AreEqual("Step 3",                                   //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has 
                tc._sequencer._currentStepDescription.Synchron);        //already been performed, the step logic is executed. 
            Assert.AreEqual(30,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(621)]
        public void T621_StepModeUncommentTest()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in error,
            {                                                           //ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                // Step 0
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                // Step 1                                               //This section was commented out in the previous test   //
                if (tc.Step(1, true, "Step 1"))                         //                                                      //
                {                                                       //                                                      //
                    tc.StepCompleteWhen(true);                          //                                                      //
                }                                                       //                                                      //
                // Step 2                                               //                                                      //
                if (tc.Step(2, true, "Step 2"))                         //                                                      //
                {                                                       //                                                      //
                    tc.StepCompleteWhen(true);                          //                                                      //
                }                                                       //This section was commented out in the previous test   //
                // Step 3
                if (tc.Step(3, true, "Step 3"))
                {
                    tc.StepCompleteWhen(false);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            }, endCondition: () => cycle >= numberOfSteps);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes to 1. 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc._sequencer._currentStepDescription.Synchron);        //Check if StepDescription changes from value "Step 3" to the expected error message
            Assert.AreEqual("ERROR, STEP ORDER CHANGED DURING STEP EXECUTION FROM: 1=>3",
                tc.GetTextOfTheMostImportantMessage());                 //Check if messenger returns the expected error message
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(40,                                         //Check if the sequencer error is of the type UidOrderChangedDuringExecution
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(630)]
        public void T630_StepModePrepareForRequestStepFromLowerToHigher()
        {
            numberOfSteps = 30;
            cycle = 0;
            reqStep = 20;
            reqStepNotExists = 300;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance

            tc.SequencerSingleCycleRun(() =>
            {
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
            //After running this method, sequencer should stay in StepId 1, with StepDescription "(>Step 1<)"
            tc.SequencerSingleCycleRun(() =>                            //with step status Running
            {
                tc.StepIn();
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId stays at 0. 
                tc._sequencer._currentStepId.Synchron);                 //But as no StepIn() method was called, sequence should stay in step 0                  
            Assert.AreEqual("(>Step 1<)",                               //Check if StepDescription changes from "Initial step" to "(>Initial step<)". As the StepId uniqueness control control has 
                tc._sequencer._currentStepDescription.Synchron);        //already been performed, the step logic is executed. 
            Assert.AreEqual(20,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(631)]
        public void T631_StepModeRequestStepFromLowerToHigher()
        {
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
                                                                        //After running this method, sequencer should stay in StepId reqStep, with StepDescription "(>Step reqStep<)"
            tc.SequencerSingleCycleRun(() =>                            //with step status ReadyToRun
            {
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                tc.RequestStep(reqStep);
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId changes to reqStep. 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step " + reqStep.ToString() + "<)",      //Check if StepDescription changes to "(>Step reqStep<)". As the StepId uniqueness control control has 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(632)]
        public void T632_StepModePrepareForRequestStepFromHigherToLower()
        {
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance
                                                                        //After running this method, sequencer should stay in StepId 1, with StepDescription "(>Step 1<)"
            tc.SequencerSingleCycleRun(() =>
            {
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays at 0. 
                tc._sequencer._currentStepId.Synchron);                 //As no StepIn() method was called, sequence should stay in step 0                  
            Assert.AreEqual("(>Step 0<)",                               //Check if StepDescription changes from "Step 0" to "(>Step 0<)". 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status is ReadyToRun.
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(633)]
        public void T633_StepModeRequestStepFromHigherToLowerTriggered()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in StepId reqStep + 5, 
            {                                                           //with StepDescription "Step " + reqStep + 5, with step status Running
                                                                        //RequestStep() method is triggered to the step reqStep.
                                                                        //Execution of the RequestStep() is going performed in the next cycle as it is "jump backwards" case
                tc.SetStepMode();                                       //This set sequencer into the step mode
                cycle++;
                tc.UpdateCurrentStepDetails();
                if (tc._sequencer._currentStepStatus.Synchron == 20)
                {
                    tc.StepIn();
                }
                for (ushort i = 0; i < reqStep + 4; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                if (tc.Step((short)(reqStep + 5), true, "Step " + (reqStep + 5).ToString()))
                {
                    tc.RequestStep(reqStep);
                }

                for (ushort i = (ushort)(reqStep + 5); i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            }, endCondition: () => cycle >= reqStep + 5);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep + 5,                                //Check if StepId changes to reqStep + 5
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + (reqStep + 5).ToString(),         //Check if StepDescription changes to "Step " + reqStep + 5
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(634)]
        public void T634_StepModeRequestStepFromHigherToLowerExecuted()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId reqStep
            {                                                           //with StepDescription "Step " + reqStep, with step status ReadyToRun
                                                                        //as RequestStep() method has been triggered in the previous Plc cycle
                tc.SetStepMode();                                       //This set sequencer into the step mode
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId changes to reqStep 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step " + reqStep.ToString() + "<)",      //Check if StepDescription changes to "(>Step + reqStep<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(635)]
        public void T635_StepModeRequestStepToNotExistingStepFirstCycle()
        {
            //After running this method, sequencer should stay in StepId reqStep, with StepDescription "Step "+reqStep
            tc.SequencerSingleCycleRun(() =>                            //with step status Running. Request step to the reqStepNotExists has not yet been performed. 
            {                                                           //Searching for reqStepNotExists, if such a step exists after calling method RequestStep(), it should be
                                                                        //found in this first PLC cycle. If such s step exists before calling method RequestStep(), it should be found
                                                                        //in second PLC cycle. If such a step does not exists, it should be discovered in the third PLC cycle
                                                                        //after calling OpenSequence() method
                tc.SetStepMode();                                       //This set sequencer into the step mode
                tc.RequestStep(reqStepNotExists);
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId stays reqStep as before
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step " + reqStep.ToString() + "<)",      //Check if StepDescription stays in "Step " + reqStep as before
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status stays in ReadyToRun as before
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(636)]
        public void T636_StepModeRequestStepToNotExistingStepSecondCycle()
        {
            //After running this method, sequencer should stay in StepId reqStep, with StepDescription "Step "+reqStep
            tc.SequencerSingleCycleRun(() =>                            //with step status Running. Request step to the reqStepNotExists has not yet been performed.
            {                                                           //Searching for reqStepNotExists, if such a step exists after calling method RequestStep(), it should be
                                                                        //found in previous PLC cycle. If such s step exists before calling method RequestStep(), it should be found
                                                                        //in this second PLC cycle. If such a step does not exists, it should be discovered in the third PLC cycle
                                                                        //after calling OpenSequence() method
                tc.SetStepMode();                                       //This set sequencer into the step mode
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId stays reqStep as before
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step " + reqStep.ToString() + "<)",      //Check if StepDescription stays in "Step " + reqStep as before
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step status stays in ReadyToRun as before
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(637)]
        public void T637_StepModeRequestStepToNotExistingStepThirdCycle()
        {
            //After running this method, sequencer should return an error "REQUESTED UID DOES NOT EXIST"
            tc.SequencerSingleCycleRun(() =>                            //Request step to the reqStepNotExists should be processed in this PLC cycle.
            {                                                           //Searching for reqStepNotExists should finished. If such a step does not exists, 
                                                                        //it should be discovered in the this third PLC cycle after calling OpenSequence() method
                tc.SetStepMode();                                       //This set sequencer into the step mode
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(reqStep,                                    //Check if StepId changes to reqStep 
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("REQUESTED STEP_ID: " +                     //Check if StepDescription changes to the expected error message
                reqStepNotExists.ToString() + " DOES NOT EXIST",
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(50,                                         //Check if the sequencer error is of the type RequestedUidDoesNotExist
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(638)]
        public void T638_StepModeRestoreAfterRequestStepToNotExistingStepError()
        {
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance
            tc.SetCurrentStep(initStepId, initStepDescription);         //Set the StepId so as the StepDescription to the current step of the sequencer

            tc.SequencerSingleCycleRun(() =>
            {
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
        }

        [Test, Order(640)]
        public void T640_StepModeRequestStepWhilePreviousRequestStepHasnotBeenYetProcessed()
        {
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
                                                                        //After running this method, sequencer should stay in error as second RequestStep() method has is 
            tc.SequencerSingleCycleRun(() =>                            //going to be called, while previous is not yet perfromed()
            {
                for (ushort i = 0; i < reqStep + 5; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                tc.RequestStep(reqStep);                                //First RequestStep() method call. It is not going to be executed in this PLC cycle, as it is "jump backwards" case
                for (short i = (short)(reqStep + 5); i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                tc.RequestStep((short)(reqStep + 10));                 //Second RequestStep() method call. 
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual("REQUESTED STEP_ID: " +                     //Check if StepDescription changes to the expected error message
                (reqStep + 10).ToString() +
                " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: " +
                 reqStep.ToString() + " HAS NOT BEEN YET PERFORMED!",
                 tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual("REQUESTED STEP_ID: " +                     //Check if messenger returns the expected error message
                (reqStep + 10).ToString() +
                " HAS BEEN REQUIRED, WHILE PREVIOUS REQUESTED STEP_ID: " +
                 reqStep.ToString() + " HAS NOT BEEN YET PERFORMED!",
                 tc.GetTextOfTheMostImportantMessage());
            Assert.AreEqual(true,                                       //Check if sequence returns error 
                tc.SequencerHasError());
            Assert.AreEqual(60,                                         //Check if the sequencer error is of the type SeveralRequestStep
                tc.GetSequencerErrorId());                              //noerror := 0, NotUniqueStepId:= 10, StepIdHasBeenChanged:= 20, OrderOfTheStepHasBeenChanged:= 40, StepWithRequestedIdDoesNotExists:= 50,SeveralRequestStep:= 60
            Assert.AreEqual(50,                                         //Check if step status is Error
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(641)]
        public void T641_StepModePrepareForDisableStepTestFirstCycle()
        {
            numberOfSteps = 10;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance

            tc._sequencer._stateChangeFrom.Synchron = -1;
            tc._sequencer._stateChangeTo.Synchron = -1;

            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 0, with StepDescription ""(>Step 0<)"
            {                                                           //Step status should be ReadyToRun
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays at 0
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 0<)",                               //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(642)]
        public void T642_StepModePrepareForDisableStepTestNext2Cycles()
        {
            tc.StepIn();
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After running this method, sequencer should stay in StepId 1, with StepDescription "(>Step 1<)"
            {                                                           //Step status should be ReadyToRun
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
                cycle++;
            }, endCondition: () => cycle >= 2);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 1<)",                               //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(-1,
                tc._sequencer._stateChangeFrom.Synchron);
            Assert.AreEqual(-1,
                tc._sequencer._stateChangeTo.Synchron);
        }

        [Test, Order(643)]
        public void T643_StepModePrepareForDisabledStepTest()
        {
            tc._sequencer._onStateChangeCount.Synchron = 0;
            tc._sequencer._stateChangeFrom.Synchron = -1;
            tc._sequencer._stateChangeTo.Synchron = -1;
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 1, with StepDescription "(*Step 1*)"
            {                                                           //Step status should be Disabled
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                tc.Step(1, false, "Step 1");                            //Step() method call with disabled value
                tc.UpdateCurrentStepDetails();
                Assert.AreEqual(10,                                     //Check if current step status changes to Disabled
                    tc._sequencer._currentStepStatus.Synchron);         //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50

                for (ushort i = 2; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(*Step 1*)",                               //Check if StepDescription changes from "(>Step 0<)" to "(*Step 1*)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(10,                                         //Check if current step status changes to Disabled
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(0,
                tc._sequencer._onStateChangeCount.Synchron);
            Assert.AreEqual(-1,
                tc._sequencer._stateChangeFrom.Synchron);
            Assert.AreEqual(-1,
                tc._sequencer._stateChangeTo.Synchron);
        }

        [Test, Order(644)]
        public void T644_StepModeStepInCallOnDisabledStep()
        {
            tc.StepIn();
            cycle = 0;
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 2, with StepDescription "(>Step 2<)"
            {                                                           //Step status should be ReadyToRun
                                                                        //Step 1 is skipped
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }

                tc.Step(1, false, "Step 1");

                for (ushort i = 2; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
                cycle++;
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId changes from 0 to 2
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 2<)",                               //Check if StepDescription changes from "(>Step 0<)" to "(>Step 2<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to Disabled
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(0,
                tc._sequencer._onStateChangeCount.Synchron);
            Assert.AreEqual(-1,
               tc._sequencer._stateChangeFrom.Synchron);
            Assert.AreEqual(-1,
                tc._sequencer._stateChangeTo.Synchron);
        }

        [Test, Order(645)]
        public void T645_StepModeStepInAfterStepInCallOnDisabledStep()
        {
            tc.StepIn();
            cycle = 0;
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 2, with StepDescription "Step 2"
            {                                                           //Step status should be Running
                                                                        
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }

                tc.Step(1, false, "Step 1");

                for (ushort i = 2; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
                cycle++;
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId stays at 2
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 2",                                   //Check if StepDescription changes from "(>Step 2<)" to "Step 2"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step status changes to Disabled
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(1,
                tc._sequencer._onStateChangeCount.Synchron);
            Assert.AreEqual(0,
               tc._sequencer._stateChangeFrom.Synchron);
            Assert.AreEqual(2,
                tc._sequencer._stateChangeTo.Synchron);
        }

        [Test, Order(670)]
        public void T670_StepModePrepareForStepForwardBackwardTestFirstCycle()
        {
            numberOfSteps = 30;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance

            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 0, with StepDescription ""(>Step 0<)"
            {                                                           //Step status should be ReadyToRun
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays at 0
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 0<)",                               //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(671)]
        public void T671_StepModePrepareForStepForwardBackwardTestSecondCycle()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 1, with StepDescription ""(>Step 1<)"
            {                                                           //Step status should be ReadyToRun
                tc.StepIn();
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 1<)",                               //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(672)]
        public void T672_StepModeStepForward()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 2, with StepDescription ""(>Step 2<)"
            {                                                           //Step status should be ReadyToRun
                tc.StepForward();
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId changes from 1 to 2
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 2<)",                               //Check if StepDescription changes from "(>Step 1<)" to "(>Step 2<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(673)]
        public void T673_StepModeRequestStepToTheLastStepOfTheSequence()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId numberOfSteps, that is the last step of the sequence
            {                                                           //with StepDescription "(>Step + numberOfSteps<)"
                                                                        //Step status should be ReadyToRun
                tc.RequestStep((short)numberOfSteps);
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps,                              //Check if StepId changes from 2 to numberOfSteps
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step " + numberOfSteps.ToString()        //Check if StepDescription changes from "(>Step 2<)" to "(>Step + numberOfSteps<)"
                + "<)", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status stays at ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(674)]
        public void T674_StepModeStepForward()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId numberOfSteps, that is the last step of the sequence
            {                                                           //with StepDescription "(>Step + numberOfSteps<)" as it is last step of the sequence and step forwards is not possible
                                                                        //Step status should be ReadyToRun
                tc.StepForward();
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps,                              //Check if StepId stays at numberOfSteps
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step " + numberOfSteps.ToString()        //Check if StepDescription stays "(>Step + numberOfSteps<)"
                + "<)", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status stays at ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(675)]
        public void T675_StepModeStepBackward()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId numberOfSteps - 1, that is the before last step 
            {                                                           //of the sequence, with StepDescription "(>Step + numberOfSteps - 1<)" as it is before last step of the sequence
                                                                        //Step status should be ReadyToRun
                tc.StepBackward();
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps - 1,                          //Check if StepId changes from numberOfSteps to  numberOfSteps - 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step " +                                 //Check if StepDescription changes from "(>Step + numberOfSteps<)" to  "(>Step + numberOfSteps - 1<)"
                (numberOfSteps - 1).ToString() + "<)",
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status stays at ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(676)]
        public void T676_StepModeRequestBeforeFirstStepOfTheSequence()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 1, that is the before first step 
            {                                                           //of the sequence, with StepDescription "(>Step 1<)" with step status ReadyToRun
                tc.RequestStep(1);
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from numberOfSteps - 1 to 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 1<)",                               //Check if StepDescription changes from "(>Step + numberOfSteps - 1<)" to  "(>Step 1<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status stays at ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(677)]
        public void T677_StepModeStepBackward()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 0, that is the first step 
            {                                                           //of the sequence, with StepDescription "(>Step 0<)" with step status ReadyToRun
                tc.StepBackward();
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId changes from 1 to 0
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 0<)",                               //Check if StepDescription changes from "(>Step 1<)" to  "(>Step 0<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status stays at ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(678)]
        public void T678_StepModeStepBackward()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 0, that is the first step 
            {                                                           //of the sequence, so step backwards is not posssible. StepDescription should be "(>Step 0<)" 
                                                                        //and step status ReadyToRun 
                tc.StepBackward();
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId changes from 1 to 0
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 0<)",                               //Check if StepDescription changes from "(>Step 1<)" to  "(>Step 0<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status stays at ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(680)]
        public void T680_StepModePrepareForStepInTest()
        {
            numberOfSteps = 30;
            tc.SequencerSingleCycleRun(() => tc.SetStepMode());         //This set sequencer into the step mode
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance

            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 0, with StepDescription ""(>Step 0<)"
            {                                                           //Step status should be ReadyToRun
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId stays at 0
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 0<)",                               //Check if StepDescription changes from "Step 0" to "(>Step 0<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(681)]
        public void T681_StepModeStepIn()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 0, with StepDescription ""(>Step 0<)"
            {                                                           //Step status should be ReadyToRun
                tc.StepIn();
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes from 0 to 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 1<)",                               //Check if StepDescription changes from "(>Step 0<)" to "(>Step 1<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(682)]
        public void T682_StepModeStepInIntoTheStepInsideWhichRequestStepToTheLastStepOfTheSequence()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId numberOfSteps, that is the last step of the sequence
            {                                                           //with StepDescription "(>Step + numberOfSteps<)"
                                                                        //Step status should be ReadyToRun
                tc.StepIn();
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.RequestStep((short)numberOfSteps);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps,                              //Check if StepId changes from 1 to numberOfSteps
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step " + numberOfSteps.ToString()        //Check if StepDescription changes from "(>Step 1<)" to "(>Step + numberOfSteps<)"
                + "<)", tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step status stays at ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(683)]
        public void T683_StepModeStepInAfterRequestStepToTheLastStepOfTheSequenceWasCalled()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId numberOfSteps
            {                                                           //with StepDescription "(>Step + numberOfSteps<)" with step status Done as CloseSequence() method has been called
                tc.StepIn();
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.RequestStep((short)numberOfSteps);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(numberOfSteps,                              //Check if StepId changes from 1 to numberOfSteps
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step " + numberOfSteps.ToString(),         //Check if StepDescription changes from "(>Step 1<)" to "(>Step + numberOfSteps<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(40,                                         //Check if current step changes to Done
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(684)]
        public void T684_StepModeStepInFromLastStepOfTheSequence()
        {
            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should canges to StepId 0
            {                                                           //with StepDescription "(>Step 0<)" with step status ReadyToRun as StapIn() method has been called
                                                                        //in last step of the sequence
                if (tc.Step(0, true, "Step 0"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.RequestStep((short)numberOfSteps);
                }
                for (ushort i = 1; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(0,                                          //Check if StepId changes from 1 to numberOfSteps
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 0<)",                               //Check if StepDescription changes from "(>Step 1<)" to "(>Step + numberOfSteps<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step changes to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Order(690)]
        public void T690_PrepareSequenceBeforeSwitchStepModeOnDuringStepExecution()
        {
            numberOfSteps = 30;
            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance

            tc.SequencerSingleCycleRun(() =>                            //After running this method, sequencer should stay in StepId 1, with StepDescription "Step 1"
            {                                                           //Step status should be Running

                tc.SetCyclicMode();                                     //This set sequence to cyclic mode
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))
                {
                    tc.StepCompleteWhen(false);
                }
                for (ushort i = 2; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            });
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(1,                                          //Check if StepId changes to 1
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 1",                                   //Check if StepDescription changes to "Step 1" 
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step changes to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
        }

        [Test, Timeout(20000), Order(691)]
        public void T691_SwitchStepModeOnDuringStepExecution()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of 2 
            {                                                           //StepDescription should have value of "(>Step 2<)" and step status should be ReadyToRun
                if (tc.Step(0, true, "Initial step"))
                {
                    tc.StepCompleteWhen(true);
                }
                if (tc.Step(1, true, "Step 1"))
                {
                    cycle++;
                    if (cycle >= 5)
                    {
                        tc.SetStepMode();
                    }
                    tc.StepCompleteWhen(cycle >= 10);
                }
                for (ushort i = 2; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            }, endCondition: () => cycle >= 10);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId changes from 1 to 2
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 2<)",                               //Check if StepDescription changes from "Step 1" "(>Step 2<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step changes from Running to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(10, cycle);                                 //Just for Shure
        }

        [Test, Order(692)]
        public void T692_SwitchStepModePrepareForStepForwardFromRunningStep()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of 2 
            {                                                           //StepDescription should have value of "Step 2" and step status should be Running
                tc.StepIn();
                if (tc.Step(0, true, "Initial step")) { }
                if (tc.Step(1, true, "Step 1")) { }
                if (tc.Step(2, true, "Step 2"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                if (tc.Step(3, true, "Step 3"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            }, endCondition: () => cycle >= 10);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId stays 2
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 2",                                   //Check if StepDescription changes from "(>Step 2<) to "Step 2""
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step changes from Running to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(10, cycle);                                 //Just for Shure
        }

        [Test, Order(693)]
        public void T693_SwitchStepModeStepForwardFromRunningStep()
        {
            cycle = 0;
            plcCycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of 3 
            {                                                           //StepDescription should have value of "(>Step 3<)" and step status should be ReadyToRun
                if(plcCycle== 0)
                {
                    tc.StepForward();
                }
                plcCycle++;
                if (tc.Step(0, true, "Initial step")) { }
                if (tc.Step(1, true, "Step 1")) { }
                if (tc.Step(2, true, "Step 2"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                if (tc.Step(3, true, "Step 3"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            }, endCondition: () => (cycle >= 10 | plcCycle >= 10));
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId changes from 2 to 3
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 3<)",                               //Check if StepDescription changes from "Step 2" to "(>Step 3<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step changes from Running to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(0, cycle);                                  //No step entry was performed
            Assert.AreEqual(10, plcCycle);                              //just for shure
        }

        [Test, Order(694)]
        public void T694_SwitchStepModePrepareForStepBackwardFromRunningStep()
        {
            cycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of 3 
            {                                                           //StepDescription should have value of "Step 3" and step status should be Running
                tc.StepIn();
                if (tc.Step(0, true, "Initial step")) { }
                if (tc.Step(1, true, "Step 1")) { }
                if (tc.Step(2, true, "Step 2"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                if (tc.Step(3, true, "Step 3"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            }, endCondition: () => cycle >= 10);
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(3,                                          //Check if StepId stays 3
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 3",                                   //Check if StepDescription changes from "(>Step 3<) to "Step 3""
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step changes from Running to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(10, cycle);                                 //Just for Shure
        }

        [Test, Order(695)]
        public void T695_SwitchStepModeStepBackwardFromRunningStep()
        {
            cycle = 0;
            plcCycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of 2 
            {                                                           //StepDescription should have value of "(>Step 2<)" and step status should be ReadyToRun
                if(plcCycle==0)
                {
                    tc.StepBackward();
                }
                plcCycle++;
                if (tc.Step(0, true, "Initial step")) { }
                if (tc.Step(1, true, "Step 1")) { }
                if (tc.Step(2, true, "Step 2"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                if (tc.Step(3, true, "Step 3"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            }, endCondition: () => (cycle >= 10 | plcCycle >= 10));
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId changes from 3 to 2
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("(>Step 2<)",                               //Check if StepDescription changes from "Step 3" to "(>Step 2<)"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(20,                                         //Check if current step changes from Running to ReadyToRun
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(0, cycle);                                  //No step entry was performed
            Assert.AreEqual(10, plcCycle);                              //just for shure
        }

        [Test, Order(696)]
        public void T696_SwitchStepModeStepOffFromReadyToRun()
        {
            cycle = 0;
            plcCycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of 2 
            {                                                           //StepDescription should have value of "(>Step 2<)" and step status should be ReadyToRun
                plcCycle++;
                tc.SetCyclicMode();
                if (tc.Step(0, true, "Initial step")) { }
                if (tc.Step(1, true, "Step 1")) { }
                if (tc.Step(2, true, "Step 2"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                if (tc.Step(3, true, "Step 3"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            }, endCondition: () => (cycle >= 10 | plcCycle >= 10));
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId stays at 2
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 2",                                   //Check if StepDescription changes from "(>Step 2<)" to "Step 2"
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step changes from ReadyToRun to Running
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(10, cycle);                                 //Step entry was performed
            Assert.AreEqual(10, plcCycle);                              //just for shure
        }

        [Test, Order(697)]
        public void T697_SwitchStepModeStepOnFromRunning()
        {
            cycle = 0;
            plcCycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of 2 
            {                                                           //StepDescription should have value of "Step 2" and step status should be Running
                plcCycle++;
                tc.SetStepMode();
                if (tc.Step(0, true, "Initial step")) { }
                if (tc.Step(1, true, "Step 1")) { }
                if (tc.Step(2, true, "Step 2"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                if (tc.Step(3, true, "Step 3"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            }, endCondition: () => (cycle >= 10 | plcCycle >= 10));
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId stays at 2 as before
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 2",                                   //Check if StepDescription stays at "Step 2" as before
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step stays at Running as before
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(10, cycle);                                 //Step entry was performed
            Assert.AreEqual(10, plcCycle);                              //just for shure
        }

        [Test, Order(698)]
        public void T698_SwitchStepModeStepOffFromRunning()
        {
            cycle = 0;
            plcCycle = 0;
            tc.SequencerRunUntilEndConditionIsMet(action: () =>         //After execution of this method, actual StepId should have value of 2 
            {                                                           //StepDescription should have value of "Step 2" and step status should be Running
                plcCycle++;
                tc.SetCyclicMode();
                if (tc.Step(0, true, "Initial step")) { }
                if (tc.Step(1, true, "Step 1")) { }
                if (tc.Step(2, true, "Step 2"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                if (tc.Step(3, true, "Step 3"))
                {
                    cycle++;
                    tc.StepCompleteWhen(cycle >= 20);
                }
                for (ushort i = 4; i < numberOfSteps; i++)
                {
                    tc.Step((short)i, true, "Step " + i.ToString());
                }
            }, endCondition: () => (cycle >= 10 | plcCycle >= 10));
            tc.UpdateCurrentStepDetails();
            Assert.AreEqual(2,                                          //Check if StepId stays at 2 as before
                tc._sequencer._currentStepId.Synchron);
            Assert.AreEqual("Step 2",                                   //Check if StepDescription stays at "Step 2" as before
                tc._sequencer._currentStepDescription.Synchron);
            Assert.AreEqual(30,                                         //Check if current step stays at Running as before
                tc._sequencer._currentStepStatus.Synchron);             //None := 0 , Disabled:= 10 , ReadyToRun:= 20 , Running:= 30 , Done:= 40, Error := 50
            Assert.AreEqual(10, cycle);                                 //Step entry was performed
            Assert.AreEqual(10, plcCycle);                              //just for shure
        }

        [Test, Order(699)]
        public void T699_OnStateChangeWhenChangingModes()
        {
            //This test runs the whole sequence several times in cyclic mode then swith to step mode and performs StepIn several times, and then switch back to cyclic mode 
            //and check if the number of the OnStateChange method calls, so as the PostStepComplete and PostSequenceComplete method calls was as expected.
            cycle = 0;
            plcCycle = 0;
            numberOfSteps = 11;
            ushort cyclicCycles = 13;
            ushort stepInEvents = 17;

            tc.SingleCycleRun(() => tc.Restore());                      //Restore sequencer to its initial state, reset all step counters, timers and all additional values
            tc.SetSequenceAsChecked();                                  //Set sequence as checked, so no StepId uniqueness control is performed on next sequence execution
            tc.SetNumberOfSteps(numberOfSteps);                         //Set numberOfSteps to the PLC instance
            tc._sequencer._onStateChangeCount.Synchron = 0;
            tc._sequencer._onCompleteStepCount.Synchron = 0;
            tc._sequencer._onSequenceCompleteCount.Synchron = 0;
            //Cyclic mode
            tc.SetCyclicMode();                                         //This set sequence to cyclic mode
            tc.SequencerMultipleCyclesRun(() =>                         //Performs cyclicCycles of the complete sequence. OnStateChange should be called cyclicCycles * numberOfSteps + numberOfSteps - 1 times.
            {                                                           
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    if(tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                    cycle++;
                }
            }, cyclicCycles);
            ulong onStateChangeCount = tc._sequencer._onStateChangeCount.Synchron;
            Assert.AreEqual(cycle * (numberOfSteps +1) -1, onStateChangeCount);
            //Step mode
            tc.SequencerSingleCycleRun(() =>
            {
                tc.SetStepMode();                                       //This set sequence to step mode
                tc.Step(0,false,"");
            });
            tc.SequencerMultipleCyclesRun(() =>                         //After running this method, sequencer should stay in StepId 1, with StepDescription "Step 1"
            {                                                           //Step status should be Running
                tc.StepIn();
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                    cycle++;
                }
            }, stepInEvents);

            Assert.AreEqual(onStateChangeCount + stepInEvents,
                tc._sequencer._onStateChangeCount.Synchron);

            //Cyclic mode
            tc.SetCyclicMode();                                         //This set sequence to cyclic mode
            tc.SequencerMultipleCyclesRun(() =>                         //After running this method, sequencer should stay in StepId 1, with StepDescription "Step 1"
            {                                                           //Step status should be Running
                for (ushort i = 0; i < numberOfSteps; i++)
                {
                    if (tc.Step((short)i, true, "Step " + i.ToString()))
                    {
                        tc.StepCompleteWhen(true);
                    }
                }
                if (tc.Step((short)numberOfSteps, true, "Step " + numberOfSteps.ToString()))
                {
                    tc.SequenceComplete();
                    cycle++;
                }
            }, cyclicCycles);
            Assert.AreEqual(cycle * (numberOfSteps + 1) - 1, tc._sequencer._onStateChangeCount.Synchron);

            ulong PostStepCompleteCount = 2 * (ulong)cyclicCycles * numberOfSteps + (ulong)Math.Ceiling((decimal)(stepInEvents / (numberOfSteps + 1))) * numberOfSteps;
            Assert.AreEqual(PostStepCompleteCount, tc._sequencer._onCompleteStepCount.Synchron);

            ulong PostSequenceCompleteCount = 2 * (ulong)cyclicCycles + (ulong)Math.Ceiling((decimal)(stepInEvents / (numberOfSteps + 1)));
            Assert.AreEqual(PostSequenceCompleteCount, tc._sequencer._onSequenceCompleteCount.Synchron);
        }

    }
}