using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Tc.Prober.Runners;
using TcoInspectors;
using TcoInspectorsTests;

namespace Legacy.TcoInspectorsUnitTests
{
    public class TcoDigitalInspectorTests
    {
        TcoInspectorsTests.LegacyTcoDigitalInspectorTests container;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Entry.TcoInspectorsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            container = Entry.TcoInspectorsTests.MAIN._legacyDigitalInspectorTests;
        }

        [SetUp]
        public void SetUp()
        {
            var plain = container._sut._data.CreatePlainerType();
            container._sut._data.FlushPlainToOnline(plain);
            container.ExecuteProbeRun(1, 0);
        }

        [Test]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void inspect_must_fail(bool required, bool actualSignal)
        {
            container._sut._data.RequiredStatus.Synchron = required;
            container._inspectedValue.Synchron = actualSignal;

            container.ExecuteProbeRun(1, 0);

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);
        }

        [Test]
        [TestCase(true, true)]
        [TestCase(false, false)]
        public void inspect_must_pass(bool required, bool actualSignal)
        {
            container._sut._data.RequiredStatus.Synchron = required;
            container._inspectedValue.Synchron = actualSignal;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Passed, container._sut.ResultAsEnum);
        }

        [Test]
        public void inspect_bypassed()
        {
            container._sut._data.IsByPassed.Synchron = true;

            container.ExecuteProbeRun(1, 0);

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Bypassed, container._sut.ResultAsEnum);
        }

        [Test]
        [TestCase(true, true)]
        [TestCase(false, false)]
        public void inspect_excluded(bool required, bool actualSignal)
        {
            container._sut._data.RequiredStatus.Synchron = required;
            container._inspectedValue.Synchron = actualSignal;
            container._sut._data.IsExcluded.Synchron = true;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Excluded, container._sut.ResultAsEnum);

            Assert.AreEqual(actualSignal, container._sut._data.DetectedStatus.Synchron);
        }

        [Test]
        public void inspect_get_result_passed()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            container.ExecuteProbeRun(1, (int)eDigitalInspectorTests.GetResult);

            Assert.AreEqual((short)eInspectorResult.Passed, container._result.Synchron);
        }

        [Test]
        public void inspect_get_result_failed()
        {
            container._sut._data.RequiredStatus.Synchron = false;
            container._inspectedValue.Synchron = true;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            container.ExecuteProbeRun(1, (int)eDigitalInspectorTests.GetResult);

            Assert.AreEqual((short)eInspectorResult.Failed, container._result.Synchron);
        }

        [Test]
        public void inspect_pass_time_more_than_file_time_must_fail()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
            container._sut._data.PassTime.Synchron = System.TimeSpan.FromMilliseconds(1000);
            container._sut._data.FailTime.Synchron = System.TimeSpan.FromMilliseconds(500);

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);
        }

        [Test]
        public void inspect_jitter_signal_must_fail()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
            container._sut._data.PassTime.Synchron = System.TimeSpan.FromMilliseconds(250);
            container._sut._data.FailTime.Synchron = System.TimeSpan.FromMilliseconds(700);

            var run = true;
            var task = Task.Run(() =>
            {
                while (run)
                {
                    Thread.Sleep(10);
                    container._inspectedValue.Synchron = !container._inspectedValue.Synchron;
                }
            });

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);
            run = false;
            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);

            while (!task.IsCompleted) { }

            Assert.IsTrue(task.IsCompleted);
        }

        [Test]
        public void inspect_with_over_inspection_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = false;
            container._sut._data.NumberOfAllowedRetries.Synchron = 10;

            for (int i = 0; i < 9; i++)
            {
                container.ExecuteProbeRun(1, 0);
                container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);
            }

            Assert.AreEqual(9, container._sut._data.RetryAttemptsCount.Synchron);

            container.ExecuteProbeRun(1, 0);
            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);

            container._inspectedValue.Synchron = true;
            container.ExecuteProbeRun(1, 0);
            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);
            container.ExecuteProbeRun(1, (int)eDigitalInspectorTests.GetResult);

            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);
            Assert.AreEqual(true, container._isOverIspected.Synchron);
        }

        [Test]
        public void inspect_store_over_all_no_action_and_inspect_failed_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = false;
            container._overallResult.Result.Synchron = (short)eOverallResult.NoAction;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, container._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_store_over_all_failed_and_inspect_failed_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
            container._overallResult.Result.Synchron = (short)eOverallResult.Failed;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Passed, container._sut.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, container._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_store_over_all_in_progress_and_inspect_failed_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = false;
            container._overallResult.Result.Synchron = (short)eOverallResult.InProgress;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, container._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_store_over_all_in_failed_and_inspect_bypassed_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._sut._data.IsByPassed.Synchron = true;
            container._inspectedValue.Synchron = false;
            container._overallResult.Result.Synchron = (short)eOverallResult.Failed;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Bypassed, container._sut.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, container._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_store_over_all_in_failed_and_inspect_excluded_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._sut._data.IsExcluded.Synchron = true;
            container._inspectedValue.Synchron = false;
            container._overallResult.Result.Synchron = (short)eOverallResult.Failed;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Excluded, container._sut.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, container._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_on_fail_failed_carry_on_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = false;
            container._overallResult.Result.Synchron = (short)eOverallResult.Failed;
            var initialState = container._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.OnFailCarryOn);
            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, container._overallResult.Result.Synchron);

            Assert.AreEqual(expectedState, container._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_carry_on_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
            var initialState = container._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.OnFailCarryOn);
            Assert.AreEqual(eInspectorResult.Passed, container._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, container._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_failed_terminate_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = false;

            var expectedState = 8005;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.OnFailTerminate);
            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, container._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_terminate_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
            var initialState = container._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.OnFailTerminate);
            Assert.AreEqual(eInspectorResult.Passed, container._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, container._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_retry_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
            var initialState = container._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.OnFailRetry);
            Assert.AreEqual(eInspectorResult.Passed, container._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, container._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_failed_retry_test()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = false;
            var initialState = container._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            container._retryState.Synchron = expectedState;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.OnFailRetry);
            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, container._coordinator._state.Synchron);
        }
    }
}
