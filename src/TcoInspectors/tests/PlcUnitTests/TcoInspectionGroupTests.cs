using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoInspectorsTests;
using Tc.Prober.Runners;
using TcoInspectors;
using System.Threading.Tasks;
using System.Threading;

namespace TcoInspectorsUnitTests
{
    public class TcoInspectionGroupTests
    {
        TcoInspectorsTests.TcoInspectionGroupTests container;
        TcoInspectorsTests.TcoInspectionGroupTests InspectorContainer;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Entry.TcoInspectorsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            container = Entry.TcoInspectorsTests.MAIN._inspectionGroupTests;
            InspectorContainer = container;           
        }

        [SetUp]
        public void SetUp()
        {
            foreach (var dinsp in container._diis)
            {
                var plain = dinsp.CreatePlainerType();
                dinsp.FlushPlainToOnline(plain);
            }

            foreach (var dinsp in container._dais)
            {
                var plain = dinsp.CreatePlainerType();
                dinsp.FlushPlainToOnline(plain);
            }

            foreach (var dinsp in container._ddis)
            {
                var plain = dinsp.CreatePlainerType();
                dinsp.FlushPlainToOnline(plain);
            }

            container.ExecuteProbeRun(1, 0);
            InspectorContainer._overallResult.Result.Synchron = 0;
            container._sut._sharedResult.Result.Synchron = 0;
        }


        [Test]   
        public void inspection_group_must_fail()
        {
            set_to_fail();

            container.ExecuteProbeRun((int)eInspectionGroupTests.Inspect);

            Assert.AreEqual((short)eOverallResult.Failed, container._sut._sharedResult.Result.Synchron);
        }

        private void set_to_fail()
        {
            container._diis[0]._data.PassTime.Synchron = System.TimeSpan.FromSeconds(1);
            container._diis[0]._data.FailTime.Synchron = System.TimeSpan.FromSeconds(2);
            container._diis[0]._data.RequiredStatus.Synchron = true;
            container._dii_inspectedValues[0].Synchron = true;

            container._diis[1]._data.RequiredStatus.Synchron = true;
            container._dii_inspectedValues[1].Synchron = false;

            container._dais[0]._data.RequiredMin.Synchron = 9;
            container._dai_inspectedValues[0].Synchron = 10;
            container._dais[0]._data.RequiredMax.Synchron = 12;

            container._dais[1]._data.RequiredMin.Synchron = 11;
            container._dai_inspectedValues[1].Synchron = 10;
            container._dais[1]._data.RequiredMax.Synchron = 12;

            container._ddis[0]._data.RequiredStatus.Synchron = "mehh";
            container._ddi_inspectedValues[0].Synchron = "mehh";

            container._ddis[1]._data.RequiredStatus.Synchron = "buu";
            container._ddi_inspectedValues[1].Synchron = "mehh";
        }

        [Test]
        public void inspection_group_must_pass()
        {
            set_to_pass();

            container.ExecuteProbeRun((int)eInspectionGroupTests.Inspect);

            Assert.AreEqual((short)eOverallResult.NoAction, container._sut._sharedResult.Result.Synchron);
        }

        private void set_to_pass()
        {
            container._diis[0]._data.RequiredStatus.Synchron = true;
            container._dii_inspectedValues[0].Synchron = true;

            container._diis[1]._data.RequiredStatus.Synchron = false;
            container._dii_inspectedValues[1].Synchron = false;

            container._dais[0]._data.RequiredMin.Synchron = 100;
            container._dai_inspectedValues[0].Synchron = 110;
            container._dais[0]._data.RequiredMax.Synchron = 120;

            container._dais[1]._data.RequiredMin.Synchron = 10;
            container._dai_inspectedValues[1].Synchron = 11;
            container._dais[1]._data.RequiredMax.Synchron = 12;

            container._ddis[0]._data.RequiredStatus.Synchron = "mehh";
            container._ddi_inspectedValues[0].Synchron = "mehh";

            container._ddis[1]._data.RequiredStatus.Synchron = "buu";
            container._ddi_inspectedValues[1].Synchron = "buu";
        }

        [Test]
        public void inspect_on_fail_failed_carry_on_test()
        {
            this.set_to_fail();           
            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Failed;
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailCarryOn);
            Assert.AreEqual(eOverallResult.Failed, InspectorContainer._sut.ResultAsEnum);
            Assert.AreEqual((short)eOverallResult.Failed, InspectorContainer._overallResult.Result.Synchron);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_carry_on_test()
        {
            this.set_to_pass();                      
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailCarryOn);
            Assert.AreEqual(eOverallResult.NoAction, InspectorContainer._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_failed_terminate_test()
        {
            this.set_to_fail();

            var expectedState = 8005;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailTerminate);
            Assert.AreEqual(eOverallResult.Failed, InspectorContainer._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_terminate_test()
        {
            this.set_to_pass();
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailTerminate);
            Assert.AreEqual(eOverallResult.NoAction, InspectorContainer._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_passed_retry_test()
        {
            this.set_to_pass();
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = initialState + 1;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailRetry);
            Assert.AreEqual(eOverallResult.NoAction, InspectorContainer._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }

        [Test]
        public void inspect_on_fail_failed_retry_test()
        {
            this.set_to_fail();
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            InspectorContainer._retryState.Synchron = expectedState;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.OnFailRetry);
            Assert.AreEqual(eOverallResult.Failed, InspectorContainer._sut.ResultAsEnum);

            Assert.AreEqual(expectedState, InspectorContainer._coordinator._state.Synchron);
        }
    }
}
