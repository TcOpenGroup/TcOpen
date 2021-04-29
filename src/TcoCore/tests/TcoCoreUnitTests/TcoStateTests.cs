using NUnit.Framework;
using System.Threading;
using TcoCoreTests;

namespace TcoCoreUnitTests
{

    public class T04_TcoStateTests
    {

        TcoContextTest tc = ConnectorFixture.Connector.MAIN._tcoContextTest_A;

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(400)]
        public void T400_IdentitiesTest()
        {
            TcoObjectTest to = tc._tcoObjectTest_A;                                                     //to is the child of the tc
            TcoStateTest ts = to._tcoStateTest_A;                                                       //ts is the child of the to and grandchild of the tc
            TcoTaskTest tt = ts._tcoTaskTest_A;                                                         //tt is the child of the ts and great-grandchild of the tc

            tc._callMyPlcInstance.Synchron = true;                                                      //Switch on the cyclical execution of the tc instance 

            Thread.Sleep(300);                                                                          //Time of the cyclical execution of the test instance
            tc._callMyPlcInstance.Synchron = false;                                                     //Switch off the cyclical execution of the tc instance 

            tc.ReadOutCycleCounters();                                                                  //Read out actual cycle counters values into the test instance

            Assert.Greater(tc._startCycles.Synchron, 0);                                                //StartCycleCounter should be greather than zero as test instance was running at least 300ms.
            Assert.Greater(tc._endCycles.Synchron, 0);                                                  //EndCycleCounter should be greather than zero as test instance was running at least 300ms.

            to.ReadOutIdentities();                                                                     //Readout identities into the test instance
            ts.ReadOutIdentities();                                                                     //Readout identities into the test instance
            tt.ReadOutIdentities();                                                                     //Readout identities into the test instance

            Assert.AreEqual(tc._myIdentity.Synchron, to._myContextIdentity.Synchron);                   //Identity of the child's context (to) is the same as the identity of the parent(tc)
            Assert.AreEqual(tc._myIdentity.Synchron, ts._myContextIdentity.Synchron);                   //Identity of the grandchild's context (ts) is the same as the identity of the grandparent(tc)
            Assert.AreEqual(tc._myIdentity.Synchron, tt._myContextIdentity.Synchron);                   //Identity of the great-grandchild's context (tt) is the same as the identity of the great-grandparent(tc)

            Assert.AreNotEqual(tc._myIdentity.Synchron, to._myIdentity.Synchron);                       //Identity of the child(to) is different than the identity of its parent(tc), as they are both unique objects.
            Assert.AreNotEqual(tc._myIdentity.Synchron, ts._myIdentity.Synchron);                       //Identity of the grandchild(ts) is different than the identity of its grandparent(tc), as they are both unique objects.
            Assert.AreNotEqual(tc._myIdentity.Synchron, tt._myIdentity.Synchron);                       //Identity of the great-grandchild(tt) is different than the identity of its great-grandparent(tc), as they are both unique objects.

            Assert.AreNotEqual(to._myIdentity.Synchron, ts._myIdentity.Synchron);                       //Identity of the grandchild(ts) is different than the identity of its parent(to), as they are both unique objects.
            Assert.AreNotEqual(to._myIdentity.Synchron, tt._myIdentity.Synchron);                       //Identity of the great-grandchild(tt) is different than the identity of its grandparent(to), as they are both unique objects.
        }

        [Test, Order(401)]
        public void T401_StateMessage()
        {
            TcoStateTest ts = tc._tcoObjectTest_A._tcoStateTest_A;
            string message = TestHelpers.RandomString(20);

            tc.ContextOpen();
            ts.PostMessage(message);                                                                    //Force the error message to the task instence
            tc.ContextClose();

            Assert.AreEqual(message, ts.GetMessage());                                                  //Check if message apears in the mime.
        }

        [Test, Order(402)]
        public void T402_ChangeState()
        {
            TcoStateTest ts = tc._tcoObjectTest_A._tcoStateTest_A;

            short initState;
            short newState;
            short ccc = ts._onStateChangeCounter.Synchron;                                              //Store the actual value of the calls of the method OnStateChange().

            ts.ReadOutState();

            initState = ts._myState.Synchron;                                                           //Store the previous state of the ts
            newState = TestHelpers.RandomNumber((short)(initState + 1), (short)(5 * (initState + 1)));  //Generate new random value of the new state.
            Assert.AreNotEqual(initState, newState);                                                    //New state should be different as the initial state.

            tc.ContextOpen();

            ts.TriggerChangeState(newState);                                                            //Change state of the ts to the new value newState.
            ts.ReadOutState();

            tc.ContextClose();

            Assert.AreEqual(newState, ts._myState.Synchron);                                            //Check if the state of the ts has been changed to the newState.                                                           
            Assert.AreEqual(ccc + 1, ts._onStateChangeCounter.Synchron);                                  //OnStateChange() method should be called just once, as the only one change of the state has been performed on the ts.
            Assert.AreEqual("My state has been change from " + initState.ToString()                     //Check if overiden method OnStateChange() generate expected message.
                + " to the new state " + newState.ToString() + ".", ts.GetMessage());
        }

        [Test, Order(403)]
        public void T403_OnStateChange()
        {
            TcoStateTest ts = tc._tcoObjectTest_A._tcoStateTest_A;
            string message = TestHelpers.RandomString(20);
            short initState;
            short newState;
            short ccc = ts._onStateChangeCounter.Synchron;                                              //Store the actual value of the calls of the method OnStateChange().

            tc.SingleCycleRun(() =>
            {
                ts.PostMessage(message);                                                                //Force test instance to post randomly generated message.
            });

            Assert.AreEqual(message, ts.GetMessage());                                                  //Check if the randomly generated message has been appeared in mime.

            ts.ReadOutState();

            initState = ts._myState.Synchron;                                                           //Store the previous state of the ts
            newState = TestHelpers.RandomNumber((short)(initState + 1), (short)(5 * (initState + 1)));  //Generate new random value of the new state.
            Assert.AreNotEqual(initState, newState);                                                    //New state should be different as the initial state.

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeState(initState);                                                       //Change state initiated to the same value as the actual state was before. From initState to initState.
            });
            ts.ReadOutState();

            Assert.AreEqual(initState, ts._myState.Synchron);                                           //State of the ts should stay the same.
            Assert.AreEqual(ccc, ts._onStateChangeCounter.Synchron);                                    //OnStateChange() method should not call, as there was no real change of the state of the ts.
            Assert.AreEqual(message, ts.GetMessage());                                                  //Message should stay the same, as there was no real change of the state of the ts and therefor no OnStateChange() method should be performed.

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeState(newState);                                                        //Change state initiated to the different value as before. From initState to newState.
            });
            ts.ReadOutState();

            Assert.AreEqual(newState, ts._myState.Synchron);                                            //Check if the state of the ts has been changed from initState to newState.
            Assert.AreEqual(ccc + 1, ts._onStateChangeCounter.Synchron);                                //OnStateChange() method should be called just once, as the only one change of the state has been performed on the ts.
            Assert.AreEqual("My state has been change from " + initState.ToString()                     //Check if overiden method OnStateChange() generate expected message and overwrite the message randomly generated.
                + " to the new state " + newState.ToString() + ".", ts.GetMessage());
        }

        [Test, Order(404)]
        public void T404_Restore()
        {
            TcoStateTest ts = tc._tcoObjectTest_A._tcoStateTest_A;

            short newState = TestHelpers.RandomNumber(20, 100);                                         //Generate new random value of the newState
            short ccc = ts._onStateChangeCounter.Synchron;                                              //Store the actual value of the calls of the method OnStateChange().

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeState(newState);                                                        //Initiate change the state of the ts to the new value newState
            });
            ts.ReadOutState();

            Assert.AreEqual(newState, ts._myState.Synchron);                                            //Check if the state of the ts has been changed to the newState
            Assert.AreEqual(ccc + 1, ts._onStateChangeCounter.Synchron);                                //OnStateChange() method should be called just once, as the only one change of the state has been performed on the ts.

            ccc = ts._onStateChangeCounter.Synchron;                                                    //Store the actual value of the calls of the method OnStateChange().
            tc.SingleCycleRun(() =>
            {
                ts.TriggerRestore();                                                                    //By calling the Restore() method state of the ts should change to -1
            });
            ts.ReadOutState();

            Assert.AreEqual(-1, ts._myState.Synchron);                                                  //Check if Restore() method change the state of the ts to -1.
            Assert.AreEqual(ccc + 1, ts._onStateChangeCounter.Synchron);                                //OnStateChange() method should be called just once, as the only one change of the state has been performed on the ts.
        }

        [Test, Order(405)]
        public void T405_ChangeStateWithObjectRestore()
        {
            TcoStateTest ts = tc._tcoObjectTest_A._tcoStateTest_A;
            short initState;
            short newState;
            short ccc = ts._onStateChangeCounter.Synchron;                                              //Store the actual value of the calls of the method OnStateChange().

            ts.ReadOutState();
            initState = ts._myState.Synchron;                                                           //Store the previous state of the ts
            newState = TestHelpers.RandomNumber((short)(initState + 1), (short)(5 * (initState + 1)));  //Generate new random value of the new state.

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeState(newState);                                                        //Initiate change the state of the ts to the new value newState
                ts.CallTaskInstancies();                                                                //Call instancies of the tasks that are childs of the ts
                ts.ReadOutState();
            });

            Assert.AreEqual(newState, ts._myState.Synchron);                                            //Check if the state of the ts has been changed from initState to newState.
            Assert.AreEqual(ccc + 1, ts._onStateChangeCounter.Synchron);                                //OnStateChange() method should be called just once, as the only one change of the state has been performed on the ts.
            Assert.IsFalse(ts._tcoTaskTest_A._isBusy.Synchron);                                         //Task A should be in Idle state, as it was not started
            Assert.IsFalse(ts._tcoTaskTest_B._isBusy.Synchron);                                         //Task B should be in Idle state, as it was not started


            tc.SingleCycleRun(() =>
            {
                ts.TriggerTaskInvoke();                                                                 //Calling the Invoke() methods on the both instancies of the task, should get them into the Request state
                ts.CallTaskInstancies();                                                                //Following call of theirs body should get them into the Executing state.
                ts.ReadOutState();
            });

            Assert.IsTrue(ts._tcoTaskTest_A._isBusy.Synchron);                                          //Task A should be in the Execution state.
            Assert.IsTrue(ts._tcoTaskTest_B._isBusy.Synchron);                                          //Task B should be in the Execution state.

            initState = ts._myState.Synchron;                                                           //Store the previous state of the ts
            newState = TestHelpers.RandomNumber((short)(initState + 1), (short)(5 * (initState + 1)));  //Generate new random value of the new state.

            ccc = ts._onStateChangeCounter.Synchron;                                                    //Store the actual value of the calls of the method OnStateChange().
            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeStateWithObjectRestore(newState);                                       //Changing state of the ts with manual object restore using fluent syntax: ts.ChangeState(newState).ObjectRestore(TaskA).ObjectRestore(TaskB);
                ts.CallTaskInstancies();
                ts.ReadOutState();
            });

            Assert.AreEqual(newState, ts._myState.Synchron);                                            //Check if the state of the ts has been changed to the newState
            Assert.AreEqual(ccc + 1, ts._onStateChangeCounter.Synchron);                                //OnStateChange() method should be called just once, as the only one change of the state has been performed on the ts.

            Assert.IsFalse(ts._tcoTaskTest_A._isBusy.Synchron);                                         //Task A should be in Idle state, as it was restored.
            Assert.IsFalse(ts._tcoTaskTest_B._isBusy.Synchron);                                         //Task B should be in Idle state, as it was restored.


        }

        [Test, Order(406)]
        public void T406_CheckAutoRestoreProperties()
        {
            TcoStateTest ts;
            TcoTaskTest tt_a;
            TcoTaskTest tt_b;

            //First case tc._tcoObjectTest_A._tcoStateTest_A
            ts = tc._tcoObjectTest_A._tcoStateTest_A;                                                   //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tcoTaskTest_A;                                                                   //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tcoTaskTest_B;                                                                   //tt_b(Tco_Task) is a child object of the ts(TcoState)
            ts.ReadOutAutoRestoreProperties();                                                          //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent
            tt_a.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            tt_b.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            Assert.IsFalse(ts._autoRestoreByMyParentEnabled.Synchron);                                  //Check if ts is not AutoRestorable, if it has EnableAutoRestore property enabled and if its childs have inherited the value of this property.
            Assert.IsTrue(ts._autoRestoreToMyChildsEnabled.Synchron);                                   //Values of this properties are given by definition part of the test instance.
            Assert.IsTrue(tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.IsTrue(tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Second case tc._tcoObjectTest_A._tcoStateTest_B
            ts = tc._tcoObjectTest_A._tcoStateTest_B;                                                   //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tcoTaskTest_A;                                                                   //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tcoTaskTest_B;                                                                   //tt_b(Tco_Task) is a child object of the ts(TcoState)
            ts.ReadOutAutoRestoreProperties();                                                          //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent
            tt_a.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            tt_b.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            Assert.IsFalse(ts._autoRestoreByMyParentEnabled.Synchron);                                  //Check if ts is not AutoRestorable, if it has EnableAutoRestore property disabled and if its childs have inherited the value of this property.
            Assert.IsFalse(ts._autoRestoreToMyChildsEnabled.Synchron);                                  //Values of this properties are given by definition part of the test instance.
            Assert.IsFalse(tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.IsFalse(tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Third case tc._tcoObjectTest_B._tcoStateTest_A
            ts = tc._tcoObjectTest_B._tcoStateTest_A;                                                   //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tcoTaskTest_A;                                                                   //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tcoTaskTest_B;                                                                   //tt_b(Tco_Task) is a child object of the ts(TcoState)
            ts.ReadOutAutoRestoreProperties();                                                          //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent
            tt_a.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            tt_b.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            Assert.IsFalse(ts._autoRestoreByMyParentEnabled.Synchron);                                  //Check if ts is not AutoRestorable, if it has EnableAutoRestore property enabled and if its childs have inherited the value of this property.
            Assert.IsTrue(ts._autoRestoreToMyChildsEnabled.Synchron);                                   //Values of this properties are given by definition part of the test instance.
            Assert.IsTrue(tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.IsTrue(tt_b._autoRestoreByMyParentEnabled.Synchron);

            //Fourth case tc._tcoObjectTest_A._tcoStateTest_B
            ts = tc._tcoObjectTest_B._tcoStateTest_B;                                                   //ts(TcoState) is a parent object for tt_a(Tco_Task) and tt_b(Tco_Task)
            tt_a = ts._tcoTaskTest_A;                                                                   //tt_a(Tco_Task) is a child object of the ts(TcoState)
            tt_b = ts._tcoTaskTest_B;                                                                   //tt_b(Tco_Task) is a child object of the ts(TcoState)
            ts.ReadOutAutoRestoreProperties();                                                          //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent
            tt_a.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            tt_b.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the child
            Assert.IsFalse(ts._autoRestoreByMyParentEnabled.Synchron);                                  //Check if ts is not AutoRestorable, if it has EnableAutoRestore property disabled and if its childs have inherited the value of this property.
            Assert.IsFalse(ts._autoRestoreToMyChildsEnabled.Synchron);                                  //Values of this properties are given by definition part of the test instance.
            Assert.IsFalse(tt_a._autoRestoreByMyParentEnabled.Synchron);
            Assert.IsFalse(tt_b._autoRestoreByMyParentEnabled.Synchron);
        }

        [Test, Order(407)]
        public void T407_AutoRestoreOnStateChange()
        {
            //ts_a has EnableAutoRestore enabled, so his child tst_a should be AutoRestorable and on change of his parent state, it should restore itself.
            //ts_b has EnableAutoRestore disabled, so his child tst_b should not be AutoRestorable and on change of his parent state, it should not restore itself.

            TcoStateTest ts_a, ts_b;
            TcoStateAutoRestoreTest tst_a, tst_b;
            short rc_a, rc_b, is_a, is_b, tis_a, tis_b, ns_a, ns_b, cc_a, cc_b;
            ulong cv_a, cv_b;

            ts_a = tc._tcoObjectTest_A._tcoStateTest_A;                                                 //ts_a(TcoState) is a parent object for tst_a(TcoState).
            ts_b = tc._tcoObjectTest_A._tcoStateTest_B;                                                 //ts_b(TcoState) is a parent object for tst_b(TcoState).

            tst_a = ts_a._tcoStateTest_A;                                                               //tst_a(TcoState) is a child object of the ts_a(TcoState).
            tst_b = ts_b._tcoStateTest_B;                                                               //tst_b(TcoState) is a child object of the ts_b(TcoState).


            //Ensure repeatibility of this test
            tc.SingleCycleRun(() =>                                                                     //Cleanup change the state to 0, reset the OnStateChange counter and reset the AutoRestore properties (inherited from the parent so as dedicated to the childrens)
            {
                ts_a.CleanUp();
                ts_b.CleanUp();
                tst_a.CleanUp();
                tst_b.CleanUp();
            });

            ts_a.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent.
            Assert.IsTrue(ts_a._autoRestoreToMyChildsEnabled.Synchron);                                 //Check if State A has EnabledAutoRestore property enabled.

            ts_b.ReadOutAutoRestoreProperties();                                                        //Readout auto restore properties (inherited from MY parent so as dedicated to MY childrens) of the parent.
            Assert.IsFalse(ts_b._autoRestoreToMyChildsEnabled.Synchron);                                //Check if State A has EnabledAutoRestore property disabled.

            ts_a.ReadOutState();
            is_a = ts_a._myState.Synchron;                                                              //Store the value of the inital state of the State A

            ts_b.ReadOutState();
            is_b = ts_b._myState.Synchron;                                                              //Store the value of the inital state of the State B

            tst_a._countsPerStep.Synchron = 5;
            tst_b._countsPerStep.Synchron = 7;

            cv_a = 100;
            cv_b = 100;

            tst_a._counterValue.Synchron = cv_a;
            tst_b._counterValue.Synchron = cv_b;

            rc_a = tst_a._restoreCounter.Synchron;
            rc_b = tst_b._restoreCounter.Synchron;

            tc.AddEmptyCycle();                                                                         //Empty cycle causes Restore() method call on the AutoRestorable State, in this caset tst_a

            tc.SingleCycleRun(() =>
            {
                tst_a.ExecutionBody();
                tst_b.ExecutionBody();
                ts_a.ReadOutState();
                ts_b.ReadOutState();
            });

            Assert.AreEqual(rc_a + 1, tst_a._restoreCounter.Synchron);                                  //tst_a should be restored, as it is AutoRestorable and it was not called in the previous cycle
            Assert.AreEqual(0, tst_a._myState.Synchron);                                                //State of the tst_a is set to -1 by calling Restore() method, but in the same PLC cycle it is set to 0 by calling ExecutionBody.
            Assert.AreEqual(1, tst_a._counterValue.Synchron);                                           //CounterValue of the tst_a is reset from 100 to 0 in the State -1 in the ExecutionBody(), as Restore() method was called before due to ExecutionBody not called in the previous PLC cycle.
                                                                                                        //In the same PLC cycle its State changes from -1 to 0 and in State 0 CounterValue is incremented by 1 so at the end of this PLC cycle it should have the value of 1.
            Assert.AreEqual(rc_b, tst_b._restoreCounter.Synchron);                                      //tst_b should not be restored, as it is not AutoRestorable so does not matter if it was or was not called in the previous PLC cycle

            Assert.AreEqual(0, tst_b._myState.Synchron);                                                //State of the tst_b should not be affected
            Assert.AreEqual(cv_b + 1, tst_b._counterValue.Synchron);                                    //CounterValue of the tst_b is not reset and in State 0 CounterValue is incremented by 1 so at the end of this PLC cycle it should have the value of cv_b + 1.


            Assert.AreEqual(is_a, ts_a._myState.Synchron);                                              //State of the parent (ts_a) of the AutoRestorable child (tst_a) should remain the same as before.
            Assert.AreEqual(is_b, ts_b._myState.Synchron);                                              //State of the parent (ts_b) of the nonAutoRestorable child (tst_b) should remain the same as before.

            rc_a = tst_a._restoreCounter.Synchron;                                                      //Store the value of the counter of the Restore() method call.           
            rc_b = tst_b._restoreCounter.Synchron;                                                      //Store the value of the counter of the Restore() method call.
            cv_a = tst_a._counterValue.Synchron;                                                        //Store the value of the CounterVlaue of the AutoRestorable state (tst_a).
            cv_b = tst_b._counterValue.Synchron;                                                        //Store the value of the CounterVlaue of the nonAutoRestorable state (tst_b).

            tc.SingleCycleRun(() =>
            {
                tst_a.TriggerRestore();                                                                 //Call the Restore() method on the AutoRestorable state instance (tst_a).
                tst_b.TriggerRestore();                                                                 //Call the Restore() method on the nonAutoRestorable state instance (tst_b).
            });

            Assert.AreEqual(rc_a + 1, tst_a._restoreCounter.Synchron);                                  //Check if the Restore() method was called ony once.
            Assert.AreEqual(rc_b + 1, tst_b._restoreCounter.Synchron);                                  //Check if the Restore() method was called ony once.
            Assert.AreEqual(-1, tst_a._myState.Synchron);                                               //Check if the state of tst_a has been change to -1 as the Retore() method was called.
            Assert.AreEqual(-1, tst_b._myState.Synchron);                                               //Check if the state of tst_b has been change to -1 as the Retore() method was called.
            Assert.AreEqual(cv_a, tst_a._counterValue.Synchron);                                       //Check if the counter value of tst_a stay unchanged as the ExecutionBody() was not called yet.
            Assert.AreEqual(cv_b, tst_b._counterValue.Synchron);                                       //Check if the counter value of tst_b stay unchanged as the ExecutionBody() was not called yet.


            tc.SingleCycleRun(() =>
            {
                tst_a.ExecutionBody();                                                                  //Calling the execution body of the tst_a.
                tst_b.ExecutionBody();                                                                  //Calling the execution body of the tst_b.
            });

            Assert.AreEqual(0, tst_a._myState.Synchron);                                                //State of the tst_a should change from -1 to 0. 
            Assert.AreEqual(0, tst_b._myState.Synchron);                                                //State of the tst_b should change from -1 to 0. 
            Assert.AreEqual(1, tst_a._counterValue.Synchron);                                           //CounterValue of the tst_a is reset from 100 to 0 in the State -1 in the ExecutionBody(), as Restore() method was called before due to ExecutionBody not called in the previous PLC cycle.
                                                                                                        //In the same PLC cycle its State changes from -1 to 0 and in State 0 CounterValue is incremented by 1 so at the end of this PLC cycle it should have the value of 1.
            Assert.AreEqual(1, tst_b._counterValue.Synchron);                                           //CounterValue of the tst_b is reset from 100 to 0 in the State -1 in the ExecutionBody(), as Restore() method was called before due to ExecutionBody not called in the previous PLC cycle.
                                                                                                        //In the same PLC cycle its State changes from -1 to 0 and in State 0 CounterValue is incremented by 1 so at the end of this PLC cycle it should have the value of 1.
            tis_a = 0;
            tis_b = 0;

            tc.RunUntilEndConditionIsMet(() =>
            {
                tst_a.ExecutionBody();                                                                  //Calling the execution body of the tst_a.
                tst_b.ExecutionBody();                                                                  //Calling the execution body of the tst_b.
                tis_a = tst_a._myState.Synchron;                                                        //Readout state of the tst_a.
                tis_b = tst_b._myState.Synchron;                                                        //Readout state of the tst_b.
            }, () => tis_a >= 10 && tis_b >= 10);                                                       //End condition- both task are in state 10 or greather


            cc_a = ts_a._onStateChangeCounter.Synchron;                                                 //Store the actual value of the calls of the method OnStateChange() of the ts_a.
            ns_a = TestHelpers.RandomNumber((short)(is_a + 1), (short)(5 * (is_a + 1)));                //Generate new random value of the new state of the ts_a.

            cc_b = ts_b._onStateChangeCounter.Synchron;                                                 //Store the actual value of the calls of the method OnStateChange() of the ts_b.
            ns_b = TestHelpers.RandomNumber((short)(is_b + 1), (short)(5 * (is_b + 1)));                //Generate new random value of the new state of the ts_b.

            cv_a = tst_a._counterValue.Synchron;                                                        //Store tha actual counter value of the tst_a.
            cv_b = tst_b._counterValue.Synchron;                                                        //Store tha actual counter value of the tst_b.

            tc.SingleCycleRun(() =>
            {
                ts_a.TriggerChangeState(ns_a);                                                          //Initiate change state on the parent object ts_a, that should cause Restore() call on the child object tst_a.
                ts_b.TriggerChangeState(ns_b);                                                          //Initiate change state on the parent object ts_b, that should cause Restore() call on the child object tst_b.
                tst_a.ExecutionBody();                                                                  //Calling the execution body of the tst_a.
                tst_b.ExecutionBody();                                                                  //Calling the execution body of the tst_b.
                ts_a.ReadOutState();
                ts_b.ReadOutState();
            });
            //AutoRestorable child
            Assert.AreEqual(ns_a, ts_a._myState.Synchron);                                              //Check if the state of the parent ts_a has been changed to the new value ns_a.
            Assert.AreEqual(cc_a + 1, ts_a._onStateChangeCounter.Synchron);                             //OnStateChange() method should be called just once, as the only one change of the state has been performed on the ts_a.
            Assert.AreEqual(0, tst_a._myState.Synchron);                                                //Check if the state of the child tst_a has been restored to -1 by calling Restore() method, but in the same PLC cycle it is set to 0 by calling ExecutionBody.
            Assert.AreEqual(1, tst_a._counterValue.Synchron);                                           //Check if CounterValue of the tst_a is reset to 0 in the State -1 in the ExecutionBody(), as Restore() method was called before due to change state of the parent ts_a.
                                                                                                        //In the same PLC cycle its State changes from -1 to 0 and in State 0 CounterValue is incremented by 1 so at the end of this PLC cycle it should have the value of 1.


            //NonAutoRestorable child
            Assert.AreEqual(ns_b, ts_b._myState.Synchron);                                              //Check if the state of the parent ts_b has been changed to the new value ns_b.
            Assert.AreEqual(cc_b + 1, ts_b._onStateChangeCounter.Synchron);                             //OnStateChange() method should be called just once, as the only one change of the state has been performed on the ts_b.
            Assert.GreaterOrEqual(tis_b, tst_b._myState.Synchron);                                      //Check if the state of the child tst_b has not been restored as this child is not AutoRestorable. The state of the tst_b
                                                                                                        //should be the same as before, or in the case if one call of the Execution body causes State increment it could be greather.
            Assert.AreEqual(cv_b + 1, tst_b._counterValue.Synchron);                                    //Check if CounterValue has not been reseted.

            //Ensure repeatibility of this test
            tc.SingleCycleRun(() =>                                                                     //Cleanup change the state to 0, reset the OnStateChange counter and reset the AutoRestore properties (inherited from the parent so as dedicated to the childrens)
            {
                ts_a.CleanUp();
                ts_b.CleanUp();
                tst_a.CleanUp();
                tst_b.CleanUp();
            });
        }
    }
}