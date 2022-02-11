using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoInspectorsTests;
using Tc.Prober.Runners;
using TcoInspectors;
using System.Threading.Tasks;
using System.Threading;

namespace TcoInspectorsUnitTests
{
    public abstract class TcoInspectorTests
    {
        // protected dynamic InspectorContainer { get; set; }

        protected TcoInspectorsTests.TcoInspectorTests InspectorContainer { get; set; }

        public abstract void OneTimeSetup();
               
        public abstract void SetUp();
      
        protected abstract void set_to_fail_below_threshold();
        protected abstract dynamic set_to_fail_above_threshold();
        protected abstract dynamic set_to_pass_at_bottom_threshold();
        protected abstract dynamic set_to_pass_at_mid();
        protected abstract void set_to_pass_at_top_threshold();
        protected abstract dynamic set_introduce_jitter();

        [Test]
        public void inspect_must_fail_below_threshold()
        {
            set_to_fail_below_threshold();

            InspectorContainer.ExecuteProbeRun(1, 0);

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);
        }

        [Test]
        public void inspect_must_fail_above_threshold()
        {
            set_to_fail_above_threshold();

            InspectorContainer.ExecuteProbeRun(1, 0);

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);
        }

        [Test]
        public void inspect_must_pass_at_bottom_threshold()
        {
            set_to_pass_at_bottom_threshold();

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Passed, InspectorContainer.Inspector.ResultAsEnum);
        }

        [Test]
        public void inspect_must_pass_at_top_threshold()
        {
            set_to_pass_at_top_threshold();

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Passed, InspectorContainer.Inspector.ResultAsEnum);
        }

        [Test]
        public void inspect_bypassed()
        {
            InspectorContainer.Inspector.InspectorData.IsByPassed.Synchron = true;

            InspectorContainer.ExecuteProbeRun(1, 0);

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Bypassed, InspectorContainer.Inspector.ResultAsEnum);
        }

        [Test]
        public void inspect_excluded_passed()
        {
            var actualSignal = this.set_to_pass_at_bottom_threshold();
            InspectorContainer.Inspector.InspectorData.IsExcluded.Synchron = true;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Excluded, InspectorContainer.Inspector.ResultAsEnum);

            Assert.AreEqual(actualSignal, InspectorContainer.Inspector.InspectorData.DetectedStatus.Synchron);
        }

        [Test]
        public void inspect_excluded_failed()
        {
            var actualSignal = this.set_to_fail_above_threshold();
            InspectorContainer.Inspector.InspectorData.IsExcluded.Synchron = true;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Excluded, InspectorContainer.Inspector.ResultAsEnum);

            Assert.AreEqual(actualSignal, InspectorContainer.Inspector.InspectorData.DetectedStatus.Synchron);
        }

        [Test]
        public void inspect_get_result_passed()
        {
            this.set_to_pass_at_mid();

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            InspectorContainer.ExecuteProbeRun(1, (int)eInspectorTests.GetResult);

            Assert.AreEqual((short)eInspectorResult.Passed, InspectorContainer._result.Synchron);
        }

        [Test]
        public void inspect_get_result_failed()
        {
            this.set_to_fail_below_threshold();

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            InspectorContainer.ExecuteProbeRun(1, (int)eInspectorTests.GetResult);

            Assert.AreEqual((short)eInspectorResult.Failed, InspectorContainer._result.Synchron);
        }

        [Test]
        public void inspect_pass_time_more_than_file_time_must_fail()
        {
            set_to_pass_at_mid();
            InspectorContainer.Inspector.InspectorData.PassTime.Synchron = System.TimeSpan.FromMilliseconds(1000);
            InspectorContainer.Inspector.InspectorData.FailTime.Synchron = System.TimeSpan.FromMilliseconds(500);

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);
        }

        [Test]
        public void inspect_jitter_signal_must_fail()
        {
            InspectorContainer.Inspector.InspectorData.PassTime.Synchron = System.TimeSpan.FromMilliseconds(250);
            InspectorContainer.Inspector.InspectorData.FailTime.Synchron = System.TimeSpan.FromMilliseconds(700);

            var run = true;
            var task = Task.Run(() => { while (run) { Thread.Sleep(10); set_introduce_jitter(); } });

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);
            run = false;
            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);

            while (!task.IsCompleted) { }

            Assert.IsTrue(task.IsCompleted);
        }

        [Test]
        public void inspect_with_over_inspection_test()
        {
            this.set_to_fail_below_threshold();
            InspectorContainer.Inspector.InspectorData.NumberOfAllowedRetries.Synchron = 10;

            for (int i = 0; i < 9; i++)
            {
                InspectorContainer.ExecuteProbeRun(1, 0);
                InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);
            }

            Assert.AreEqual(9, InspectorContainer.Inspector.InspectorData.RetryAttemptsCount.Synchron);

            InspectorContainer.ExecuteProbeRun(1, 0);
            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);

            this.set_to_pass_at_mid();
            InspectorContainer.ExecuteProbeRun(1, 0);
            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);
            InspectorContainer.ExecuteProbeRun(1, (int)eInspectorTests.GetResult);

            Assert.AreEqual(eInspectorResult.Inconclusive, InspectorContainer.Inspector.ResultAsEnum);
            Assert.AreEqual(true, InspectorContainer._isOverIspected.Synchron);

        }

        [Test]
        public void inspect_store_over_all_no_action_and_inspect_failed_test()
        {
            this.set_to_fail_below_threshold();
            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.NoAction;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, InspectorContainer._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_store_over_all_failed_and_inspect_failed_test()
        {
            this.set_to_pass_at_mid();
            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Failed;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Passed, InspectorContainer.Inspector.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, InspectorContainer._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_store_over_all_in_progress_and_inspect_failed_test()
        {
            this.set_to_fail_below_threshold();
            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.InProgress;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, InspectorContainer._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_store_over_all_in_failed_and_inspect_bypassed_test()
        {
            this.set_to_fail_above_threshold();

            InspectorContainer.Inspector.InspectorData.IsByPassed.Synchron = true;
            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Failed;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Bypassed, InspectorContainer.Inspector.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, InspectorContainer._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_store_over_all_in_failed_and_inspect_excluded_test()
        {
            this.set_to_fail_above_threshold();

            InspectorContainer.Inspector.InspectorData.IsExcluded.Synchron = true;
            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Failed;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Excluded, InspectorContainer.Inspector.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, InspectorContainer._overallResult.Result.Synchron);
        }

        [Test]
        public void inspect_on_fail_failed_carry_on_test()
        {
            this.set_to_fail_above_threshold();
            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Failed;
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailCarryOn);
            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, InspectorContainer._overallResult.Result.Synchron);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_carry_on_test()
        {
            this.set_to_pass_at_mid();
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailCarryOn);
            Assert.AreEqual(eInspectorResult.Passed, InspectorContainer.Inspector.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_failed_terminate_test()
        {
            this.set_to_fail_above_threshold();

            var expectedState = 8005;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailTerminate);
            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_terminate_test()
        {
            this.set_to_pass_at_mid();
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailTerminate);
            Assert.AreEqual(eInspectorResult.Passed, InspectorContainer.Inspector.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_retry_test()
        {
            this.set_to_pass_at_mid();
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailRetry);
            Assert.AreEqual(eInspectorResult.Passed, InspectorContainer.Inspector.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_failed_retry_test()
        {
            this.set_to_fail_above_threshold();
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            InspectorContainer._retryState.Synchron = expectedState;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailRetry);
            Assert.AreEqual(eInspectorResult.Failed, InspectorContainer.Inspector.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }
    }
}
