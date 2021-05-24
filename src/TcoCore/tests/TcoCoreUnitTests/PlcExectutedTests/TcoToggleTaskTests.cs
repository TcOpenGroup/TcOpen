using NUnit.Framework;
using System.Threading;
using TcoCoreTests;

namespace TcoCoreUnitTests.PlcExecutedTests
{

    public class T00_TcoToggleTaskTests
    {
        bool state = false;

        TcoToggleTaskTestContext tc = ConnectorFixture.Connector.MAIN._tcoToggleTaskTestContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            tc._done.Synchron = false;
            tc._bool.Synchron = true;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.Reinit, () => tc._done.Synchron);
            tc._done.Synchron = false;
        }

        [SetUp]
        public void Setup()
        {
            tc._done.Synchron = false;
            state = !state;
        }

        [TearDown]
        public void TearDown()
        {
            tc._done.Synchron = false;
        }

        [Test, Order(002)]
        public void T002_CheckInitStates()
        {
            tc._done.Synchron = false;
            tc._bool.Synchron = true;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.Reinit, () => tc._done.Synchron);
            Assert.AreEqual(tc._bool.Synchron,tc._sut._state.Synchron);

            tc._bool.Synchron = false;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.Reinit, () => tc._done.Synchron);
            Assert.AreEqual(tc._bool.Synchron, tc._sut._state.Synchron);
        }

        [Test, Order(003)]
        public void T003_Message()
        {
            string message = "Test error message";
            tc._string .Synchron = message;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.Message, () => tc._done.Synchron);
            Assert.AreEqual(message, tc._sut._messenger._mime.Text.Synchron);                                 //Check if message apears in the mime.
        }

        [Test, Order(004)]
        public void T004_TriggerToggleWhileRunNotCalled()
        {
            state = tc._sut._state.Synchron;
            tc.ExecuteProbeRun(2, 0);
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerToggleWhileRunNotCalled, () => tc._done.Synchron);
            Assert.AreEqual("Run() method is not called cyclically.", tc._sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(state, tc._sut._state.Synchron);
        }

        [Test, Order(005)]
        public void T005_TriggerToggleWhileDisabled()
        {
            state = tc._sut._state.Synchron;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerToggleWhileDisabled, () => tc._done.Synchron);
            Assert.AreEqual("Toggletask cannot be triggered  as its Enabled property is FALSE.", tc._sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(state, tc._sut._state.Synchron);
        }

        [Test, Order(006)]
        public void T006_TriggerToggleWhileEnabled()
        {
            state = tc._sut._state.Synchron;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerToggleWhileEnabled, () => tc._done.Synchron);
            Assert.AreEqual(string.Empty, tc._sut._messenger._mime.Text.Synchron);
            Assert.AreNotEqual(state, tc._sut._state.Synchron);
        }

        [Test, Order(007)]
        public void T007_TriggerOnWhileRunNotCalled()
        {
            tc._sut.SetState(false);
            state = tc._sut._state.Synchron;
            tc.ExecuteProbeRun(2, 0);
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerOnWhileRunNotCalled, () => tc._done.Synchron);
            Assert.AreEqual("Run() method is not called cyclically.", tc._sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(state, tc._sut._state.Synchron);
        }

        [Test, Order(008)]
        public void T008_TriggerOnWhileDisabled()
        {
            tc._sut.SetState(false);
            state = tc._sut._state.Synchron;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerOnWhileDisabled, () => tc._done.Synchron);
            Assert.AreEqual("Toggletask cannot be set to TRUE as its Enabled property is FALSE.", tc._sut._messenger._mime.Text.Synchron);
            Assert.IsFalse(tc._sut._state.Synchron);
        }

        [Test, Order(009)]
        public void T009_TriggerOnWhileEnabled()
        {
            tc._sut.SetState(false);
            state = tc._sut._state.Synchron;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerOnWhileEnabled, () => tc._done.Synchron);
            Assert.AreEqual(string.Empty, tc._sut._messenger._mime.Text.Synchron);
            Assert.IsTrue(tc._sut._state.Synchron);
        }

        [Test, Order(010)]
        public void T010_TriggerOffWhileRunNotCalled()
        {
            tc._sut.SetState(true);
            tc.ExecuteProbeRun(2, 0);
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerOffWhileRunNotCalled, () => tc._done.Synchron);
            Assert.AreEqual("Run() method is not called cyclically.", tc._sut._messenger._mime.Text.Synchron);
            Assert.IsTrue(tc._sut._state.Synchron);
        }

        [Test, Order(011)]
        public void T011_TriggerOffWhileDisabled()
        {
            tc._sut.SetState(true);
            state = tc._sut._state.Synchron;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerOffWhileDisabled, () => tc._done.Synchron);
            Assert.AreEqual("Toggletask cannot be set to FALSE as its Enabled property is FALSE.", tc._sut._messenger._mime.Text.Synchron);
            Assert.IsTrue(tc._sut._state.Synchron);
        }

        [Test, Order(012)]
        public void T012_TriggerOffWhileEnabled()
        {
            tc._sut.SetState(true);
            state = tc._sut._state.Synchron;
            tc.ExecuteProbeRun((int)eTcoToggleTaskTests.TriggerOffWhileEnabled, () => tc._done.Synchron);
            Assert.AreEqual(string.Empty, tc._sut._messenger._mime.Text.Synchron);
            Assert.IsFalse(tc._sut._state.Synchron);
        }

    }
}

