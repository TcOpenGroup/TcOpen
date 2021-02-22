using NUnit.Framework;
using System.Threading;
using TcoCoreTests;

namespace TcoCoreUnitTests
{

    public class T04_TcoStateTests
    {

        TcoContextTest tc = ConnectorFixture.Connector.MAIN._TcoContextTest_A;

        [SetUp]
        public void Setup()
        {
        }


        [Test, Order(400)]
        public void T400_IdentitiesTest()
        {
            TcoObjectTest to = tc._TcoObjectTest_A;
            TcoStateTest ts = to._TcoStateTest_A;
            TcoTaskTest tt = ts._TcoTaskTest_A;

            tc._CallMyPlcInstance.Synchron = true;

            Thread.Sleep(300);
            tc._CallMyPlcInstance.Synchron = false;

            tc.ReadOutCycleCounters();

            Assert.Greater(tc._startCycles.Synchron, 0);
            Assert.Greater(tc._endCycles.Synchron, 0);

            to.ReadOutIdentities();
            ts.ReadOutIdentities();
            tt.ReadOutIdentities();

            Assert.AreEqual(tc._MyIdentity.Synchron, to._MyContextIdentity.Synchron);
            Assert.AreEqual(tc._MyIdentity.Synchron, ts._MyContextIdentity.Synchron);
            Assert.AreEqual(tc._MyIdentity.Synchron, tt._MyContextIdentity.Synchron);

            Assert.AreNotEqual(tc._MyIdentity.Synchron, to._MyIdentity.Synchron);
            Assert.AreNotEqual(tc._MyIdentity.Synchron, ts._MyIdentity.Synchron);
            Assert.AreNotEqual(tc._MyIdentity.Synchron, tt._MyIdentity.Synchron);

            Assert.AreNotEqual(to._MyIdentity.Synchron, ts._MyIdentity.Synchron);
            Assert.AreNotEqual(to._MyIdentity.Synchron, tt._MyIdentity.Synchron);
        }

        [Test, Order(401)]
        public void T401_StateMessage()
        {
            TcoStateTest ts = tc._TcoObjectTest_A._TcoStateTest_A;
            string message = TestHelpers.RandomString(20);

            tc.ContextOpen();
            ts.PostMessage(message);
            tc.ContextClose();

            Assert.AreEqual(message, ts.GetMessage());
        }

        [Test, Order(402)]
        public void T402_ChangeState()
        {
            TcoStateTest ts = tc._TcoObjectTest_A._TcoStateTest_A;

            short ccc = ts._OnStateChangeCounter.Synchron;
            short prevState;
            short newState = TestHelpers.RandomNumber(20,100);

            ts.ReadOutState();

            prevState = ts._MyState.Synchron;

            tc.ContextOpen();

            ts.TriggerChangeState(newState);
            ts.ReadOutState();

            tc.ContextClose();

            Assert.AreEqual(newState, ts._MyState.Synchron);
            Assert.AreEqual(ccc+1, ts._OnStateChangeCounter.Synchron);
            Assert.AreEqual("My state has been change from " + prevState.ToString() + " to the new state " + newState.ToString() + ".", ts.GetMessage());
        }

        [Test, Order(403)]
        public void T403_OnStateChange()
        {
            TcoStateTest ts = tc._TcoObjectTest_A._TcoStateTest_A;
            string message = TestHelpers.RandomString(20);
            short ccc = ts._OnStateChangeCounter.Synchron;
            short prevState;
            short newState = TestHelpers.RandomNumber(20, 100);

            tc.SingleCycleRun(() =>
            {
                ts.PostMessage(message);
            });

            Assert.AreEqual(message, ts.GetMessage());

            ts.ReadOutState();
            prevState = ts._MyState.Synchron;

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeState(prevState);
            });
            ts.ReadOutState();

            Assert.AreEqual(prevState, ts._MyState.Synchron);
            Assert.AreEqual(ccc, ts._OnStateChangeCounter.Synchron);
            Assert.AreEqual(message, ts.GetMessage());

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeState(newState);
            });
            ts.ReadOutState();

            Assert.AreEqual(newState, ts._MyState.Synchron);
            Assert.AreEqual(ccc + 1, ts._OnStateChangeCounter.Synchron);
            Assert.AreEqual("My state has been change from " + prevState.ToString() + " to the new state " + newState.ToString() + ".", ts.GetMessage());
        }

        [Test, Order(404)]
        public void T404_Restore()
        {
            TcoStateTest ts = tc._TcoObjectTest_A._TcoStateTest_A;

            short newState = TestHelpers.RandomNumber(20, 100);

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeState(newState);
                ts.TriggerRestore();
            });

            ts.ReadOutState();

            Assert.AreNotEqual(newState, ts._MyState.Synchron);
            Assert.AreEqual(-1, ts._MyState.Synchron);
        }

        [Test, Order(405)]
        public void T405_ChangeStateWithObjectRestore()
        {
            TcoStateTest ts = tc._TcoObjectTest_A._TcoStateTest_A;
            short prevState;
            short newState = TestHelpers.RandomNumber(20, 100);
            
            ts.ReadOutState();
            prevState = ts._MyState.Synchron;

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeState(newState);
                ts.CallTaskInstancies();
                ts.ReadOutState();
            });

            Assert.AreEqual(newState, ts._MyState.Synchron);
            Assert.AreEqual("My state has been change from " + prevState.ToString() + " to the new state " + newState.ToString() + ".", ts.GetMessage());
            Assert.IsFalse(ts._TcoTaskTest_A._IsBusy.Synchron);
            Assert.IsFalse(ts._TcoTaskTest_B._IsBusy.Synchron);


            tc.SingleCycleRun(() =>
            {
                ts.TriggerTaskInvoke();
                ts.CallTaskInstancies();
                ts.ReadOutState();
            });

            Assert.IsTrue(ts._TcoTaskTest_A._IsBusy.Synchron);
            Assert.IsTrue(ts._TcoTaskTest_B._IsBusy.Synchron);

            prevState = ts._MyState.Synchron;
            newState = TestHelpers.RandomNumber(101, 200);

            tc.SingleCycleRun(() =>
            {
                ts.TriggerChangeStateWithObjectRestore(newState);
                ts.CallTaskInstancies();
                ts.ReadOutState();
            });

            Assert.AreEqual(newState, ts._MyState.Synchron);
            Assert.AreEqual("My state has been change from " + prevState.ToString() + " to the new state " + newState.ToString() + ".", ts.GetMessage());
            Assert.IsFalse(ts._TcoTaskTest_A._IsBusy.Synchron);
            Assert.IsFalse(ts._TcoTaskTest_B._IsBusy.Synchron);


        }

        [Test, Order(406)]
        public void T406_CheckAutoRestoreProperties()
        {
            TcoStateTest ts;
            TcoTaskTest tt_a;
            TcoTaskTest tt_b;

            ts = tc._TcoObjectTest_A._TcoStateTest_A;
            tt_a = ts._TcoTaskTest_A;
            tt_b = ts._TcoTaskTest_B;
            ts.ReadOutAutoRestoreProperties();
            tt_a.ReadOutAutoRestoreProperties();
            tt_b.ReadOutAutoRestoreProperties();
            Assert.IsFalse(ts._AutoRestoreByMyParentEnabled.Synchron);
            Assert.IsTrue(ts._AutoRestoreToMyChildsEnabled.Synchron);
            Assert.IsTrue(tt_a._AutoRestoreByMyParentEnabled.Synchron);
            Assert.IsTrue(tt_b._AutoRestoreByMyParentEnabled.Synchron);

            ts = tc._TcoObjectTest_A._TcoStateTest_B;
            tt_a = ts._TcoTaskTest_A;
            tt_b = ts._TcoTaskTest_B;
            ts.ReadOutAutoRestoreProperties();
            tt_a.ReadOutAutoRestoreProperties();
            tt_b.ReadOutAutoRestoreProperties();
            Assert.IsFalse(ts._AutoRestoreByMyParentEnabled.Synchron);
            Assert.IsFalse(ts._AutoRestoreToMyChildsEnabled.Synchron);
            Assert.IsFalse(tt_a._AutoRestoreByMyParentEnabled.Synchron);
            Assert.IsFalse(tt_b._AutoRestoreByMyParentEnabled.Synchron);


            ts = tc._TcoObjectTest_B._TcoStateTest_A;
            tt_a = ts._TcoTaskTest_A;
            tt_b = ts._TcoTaskTest_B;
            ts.ReadOutAutoRestoreProperties();
            tt_a.ReadOutAutoRestoreProperties();
            tt_b.ReadOutAutoRestoreProperties();
            Assert.IsFalse(ts._AutoRestoreByMyParentEnabled.Synchron);
            Assert.IsTrue(ts._AutoRestoreToMyChildsEnabled.Synchron);
            Assert.IsTrue(tt_a._AutoRestoreByMyParentEnabled.Synchron);
            Assert.IsTrue(tt_b._AutoRestoreByMyParentEnabled.Synchron);

            ts = tc._TcoObjectTest_B._TcoStateTest_B;
            tt_a = ts._TcoTaskTest_A;
            tt_b = ts._TcoTaskTest_B;
            ts.ReadOutAutoRestoreProperties();
            tt_a.ReadOutAutoRestoreProperties();
            tt_b.ReadOutAutoRestoreProperties();
            Assert.IsFalse(ts._AutoRestoreByMyParentEnabled.Synchron);
            Assert.IsFalse(ts._AutoRestoreToMyChildsEnabled.Synchron);
            Assert.IsFalse(tt_a._AutoRestoreByMyParentEnabled.Synchron);
            Assert.IsFalse(tt_b._AutoRestoreByMyParentEnabled.Synchron);
        }

        [Test, Order(407)]
        public void T407_AutoRestoreOnStateChange()
        {
            TcoStateTest ts_a, ts_b;
            TcoStateAutoRestoreTest tst_a, tst_b;
            short rc_a, rc_b, ps_a, ps_b, tps_a, tps_b, ns_a, ns_b, cc_a, cc_b;
            ulong cv_a, cv_b;

            ts_a = tc._TcoObjectTest_A._TcoStateTest_A;
            ts_b = tc._TcoObjectTest_A._TcoStateTest_B;
            tst_a = ts_a._TcoStateTest_A;
            tst_b = ts_b._TcoStateTest_A;

            //Ensure repeatibility of this test
            tc.SingleCycleRun(() =>
            {
                ts_a.CleanUp();
                ts_b.CleanUp();
                tst_a.CleanUp();
                tst_b.CleanUp();
            });

            ts_a.ReadOutAutoRestoreProperties();
            Assert.IsTrue(ts_a._AutoRestoreToMyChildsEnabled.Synchron);

            ts_b.ReadOutAutoRestoreProperties();
            Assert.IsFalse(ts_b._AutoRestoreToMyChildsEnabled.Synchron);

            ts_a.ReadOutState();
            ps_a = ts_a._MyState.Synchron;

            ts_b.ReadOutState();
            ps_b = ts_b._MyState.Synchron;

            tst_a._CountsPerStep.Synchron = 5;
            tst_b._CountsPerStep.Synchron = 7;

            cv_a = 100;
            cv_b = 100;

            tst_a._CounterValue.Synchron = cv_a;
            tst_b._CounterValue.Synchron = cv_b;

            rc_a = tst_a._RestoreCounter.Synchron;
            rc_b = tst_b._RestoreCounter.Synchron;

            tc.AddEmptyCycle();

            tc.SingleCycleRun(() =>
            {
                tst_a.ExecutionBody();
                tst_b.ExecutionBody();
                ts_a.ReadOutState();
                ts_b.ReadOutState();
            });

            //tst_a should be restored, as it is AutoRestorable and it was not called in the previous cycle
            Assert.AreEqual(rc_a + 1, tst_a._RestoreCounter.Synchron);
            //State of the tst_a is set to -1 by calling Restore() method, but in the same PLC cycle it is 
            //set to 0 by calling ExecutionBody
            Assert.AreEqual(0, tst_a._MyState.Synchron);
            //CounterValue of the tst_a is reseted from 100 to 0 in the State -1 in the ExecutionBody()
            //as Restore() method was called before due to ExecutionBody was not called in the previous PLC cycle.
            //In the same PLC cycle it is State changes from -1 to 0 and in State 0 CounterValue is incremented by 1
            //so at the end of this PLC cycle it should have value of 1
            Assert.AreEqual(1, tst_a._CounterValue.Synchron);

            //tst_b should not be restored, as it is not AutoRestorable so does not matter if it was or was not called in the previous PLC cycle
            Assert.AreEqual(rc_b, tst_b._RestoreCounter.Synchron);
            //State of the tst_b is not affected
            Assert.AreEqual(0, tst_b._MyState.Synchron);
            //CounterValue of the tst_b is not reseted and in State 0 CounterValue is incremented by 1
            //so at the end of this PLC cycle it should have value of cv_b + 1
            Assert.AreEqual(cv_b + 1, tst_b._CounterValue.Synchron);

            //State of the parents of both childs remain same
            Assert.AreEqual(ps_a, ts_a._MyState.Synchron);
            Assert.AreEqual(ps_b, ts_b._MyState.Synchron);

            rc_a = tst_a._RestoreCounter.Synchron;
            rc_b = tst_b._RestoreCounter.Synchron;
            cv_a = tst_a._CounterValue.Synchron;
            cv_b = tst_b._CounterValue.Synchron;

            tc.SingleCycleRun(() =>
            {
                tst_a.TriggerRestore();
                tst_b.TriggerRestore();
            });

            Assert.AreEqual(rc_a + 1, tst_a._RestoreCounter.Synchron);
            Assert.AreEqual(rc_b + 1, tst_b._RestoreCounter.Synchron);
            //States of both tst_a so as tst_b should change to -1 as Retore() method was called
            Assert.AreEqual(-1, tst_a._MyState.Synchron);
            Assert.AreEqual(-1, tst_b._MyState.Synchron);
            //Counter values of both tst_a so as tst_b should stay untouched as ExecutionBody() was not called yet
            Assert.AreEqual(cv_a , tst_a._CounterValue.Synchron);
            Assert.AreEqual(cv_b , tst_b._CounterValue.Synchron);


            tc.SingleCycleRun(() =>
            {
                tst_a.ExecutionBody();
                tst_b.ExecutionBody();
            });

            //States of both tst_a so as tst_b should change from - 1 to 0 
            Assert.AreEqual(0, tst_a._MyState.Synchron);
            Assert.AreEqual(0, tst_b._MyState.Synchron);
            //CounterValue of the tst_a is reseted from the previous value to 0 in the State -1 in the ExecutionBody()
            //In the same PLC cycle it is State changes from -1 to 0 and in State 0 CounterValue is incremented by 1
            //so at the end of this PLC cycle it should have value of 1
            Assert.AreEqual(1, tst_a._CounterValue.Synchron);
            //CounterValue of the tst_b is reseted from the previous value to 0 in the State -1 in the ExecutionBody()
            //In the same PLC cycle it is State changes from -1 to 0 and in State 0 CounterValue is incremented by 1
            //so at the end of this PLC cycle it should have value of 1
            Assert.AreEqual(1, tst_b._CounterValue.Synchron);

            tps_a = 0;
            tps_b = 0;
            tc.RunUntilEndConditionIsMet(() =>
            {
                tst_a.ExecutionBody();
                tst_b.ExecutionBody();
                tps_a = tst_a._MyState.Synchron;
                tps_b = tst_b._MyState.Synchron;
            }, () => tps_a >= 10 && tps_b >= 10);


            cc_a = ts_a._OnStateChangeCounter.Synchron;
            ns_a = TestHelpers.RandomNumber(20, 100);
            Assert.AreNotEqual(ps_a, ns_a);

            cc_b = ts_b._OnStateChangeCounter.Synchron;
            ns_b = TestHelpers.RandomNumber(20, 100);
            Assert.AreNotEqual(ps_b, ns_b);

            cv_a = tst_a._CounterValue.Synchron;
            cv_b = tst_b._CounterValue.Synchron;

            tc.SingleCycleRun(() =>
            {
                ts_a.TriggerChangeState(ns_a);
                ts_b.TriggerChangeState(ns_b);
                tst_a.ExecutionBody();
                tst_b.ExecutionBody();
                ts_a.ReadOutState();
                ts_b.ReadOutState();
            });

            //Parent state has changed to ns_a
            Assert.AreEqual(ns_a, ts_a._MyState.Synchron);
            Assert.AreEqual(cc_a + 1, ts_a._OnStateChangeCounter.Synchron);
            //Child state has been restored
            Assert.AreEqual(0, tst_a._MyState.Synchron);
            //Child counter has been reseted
            Assert.AreEqual(1, tst_a._CounterValue.Synchron);


            //Parent state has changed to ns_b
            Assert.AreEqual(ns_b, ts_b._MyState.Synchron);
            Assert.AreEqual(cc_b + 1, ts_b._OnStateChangeCounter.Synchron);
            //Child state has not been restored, so it should be same as before, or in the case if one call of the Execution body causes State increment it could be greather 
            Assert.GreaterOrEqual(tps_b, tst_b._MyState.Synchron);
            //Child counter has not been reseted
            Assert.AreEqual(cv_b + 1, tst_b._CounterValue.Synchron);

            //Ensure repeatibility of this test
            tc.SingleCycleRun(() =>
            {
                ts_a.CleanUp();
                ts_b.CleanUp();
                tst_a.CleanUp();
                tst_b.CleanUp();
            });
        }
    }
}