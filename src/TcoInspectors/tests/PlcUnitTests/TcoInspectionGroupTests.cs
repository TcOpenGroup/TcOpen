using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoInspectorsTests;
using Tc.Prober.Runners;
using TcoInspectors;
using System.Threading.Tasks;
using System.Threading;
using System;

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
            container._sut._overallResult.Result.Synchron = 0;            
        }


        [Test]   
        public void inspection_group_must_fail()
        {
            set_to_fail();

            container.ExecuteProbeRun((int)eInspectionGroupTests.Inspect);

            Assert.AreEqual(eOverallResult.Failed, (eOverallResult)container._sut._overallResult.Result.Synchron);
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

            Assert.AreEqual((short)eOverallResult.NoAction, container._sut._overallResult.Result.Synchron);
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
        }

        [Test]
        public void inspect_previously_failed_group_passed()
        {            
            this.set_to_pass();

            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Failed;
            
            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            InspectorContainer._retryState.Synchron = expectedState;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);
            Assert.AreEqual(eOverallResult.Failed, InspectorContainer._sut.ResultAsEnum);           
        }

        [Test]
        public void inspect_previously_failed_group_failed()
        {
            this.set_to_fail();

            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Failed;

            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            InspectorContainer._retryState.Synchron = expectedState;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);
            Assert.AreEqual(eOverallResult.Failed, InspectorContainer._sut.ResultAsEnum);
        }

        [Test]
        public void inspect_previously_passed_group_passed()
        {
            this.set_to_pass();

            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Passed;

            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            InspectorContainer._retryState.Synchron = expectedState;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);
            Assert.AreEqual(eOverallResult.Passed, InspectorContainer._sut.ResultAsEnum);
        }

        [Test]
        public void inspect_previously_passed_group_failed()
        {
            this.set_to_fail();

            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Passed;

            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            InspectorContainer._retryState.Synchron = expectedState;

            InspectorContainer.ExecuteProbeRun((int)eInspectorTests.Inspect);
            Assert.AreEqual(eOverallResult.Failed, InspectorContainer._sut.ResultAsEnum);
        }

        [Test]
        public void inspect_failed_description_group_failed()
        {
            TimeSpan passTime = new TimeSpan(0, 0, 0, 0, 500);
            TimeSpan failTime = new TimeSpan(0, 0, 0, 0, 1000);
            string fDescII_0 = "Failure Description II 0";
            string fDescII_1 = "Failure Description II 1";
            string fDescAI_0 = "Failure Description AI 0";
            string fDescAI_1 = "Failure Description AI 1";
            string fDescDI_0 = "Failure Description DI 0";
            string fDescDI_1 = "Failure Description DI 1";
            string errCodeII_0 = "1010";
            string errCodeII_1 = "1020";
            string errCodeAI_0 = "2010";
            string errCodeAI_1 = "2020";
            string errCodeDI_0 = "3010";
            string errCodeDI_1 = "3020";
            this.set_to_fail();
            this.container._diis[0].InspectorData.FailureDescription.Synchron = fDescII_0;
            this.container._diis[0].InspectorData.ErrorCode.Synchron = errCodeII_0;
            this.container._diis[0].InspectorData.PassTime.Synchron = passTime;
            this.container._diis[0].InspectorData.FailTime.Synchron = failTime;

            this.container._diis[1].InspectorData.FailureDescription.Synchron = fDescII_1;
            this.container._diis[1].InspectorData.ErrorCode.Synchron = errCodeII_1;
            this.container._diis[1].InspectorData.PassTime.Synchron = passTime;
            this.container._diis[1].InspectorData.FailTime.Synchron = failTime;

            this.container._dais[0].InspectorData.FailureDescription.Synchron = fDescAI_0;
            this.container._dais[0].InspectorData.ErrorCode.Synchron = errCodeAI_0;
            this.container._dais[0].InspectorData.PassTime.Synchron = passTime;
            this.container._dais[0].InspectorData.FailTime.Synchron = failTime;

            this.container._dais[1].InspectorData.FailureDescription.Synchron = fDescAI_1;
            this.container._dais[1].InspectorData.ErrorCode.Synchron = errCodeAI_1;
            this.container._dais[1].InspectorData.PassTime.Synchron = passTime;
            this.container._dais[1].InspectorData.FailTime.Synchron = failTime;

            this.container._ddis[0].InspectorData.FailureDescription.Synchron = fDescDI_0;
            this.container._ddis[0].InspectorData.ErrorCode.Synchron = errCodeDI_0;
            this.container._ddis[0].InspectorData.PassTime.Synchron = passTime;
            this.container._ddis[0].InspectorData.FailTime.Synchron = failTime;

            this.container._ddis[1].InspectorData.FailureDescription.Synchron = fDescDI_1;
            this.container._ddis[1].InspectorData.ErrorCode.Synchron = errCodeDI_1;
            this.container._ddis[1].InspectorData.PassTime.Synchron = passTime;
            this.container._ddis[1].InspectorData.FailTime.Synchron = failTime;

            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Passed;

            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            InspectorContainer._retryState.Synchron = expectedState;

            InspectorContainer.ExecuteProbeRun((int)eInspectionGroupTests.UpdateComprehensiveResultDescriptions, 
                () => {
                    Task.Delay(failTime).Wait();
                    InspectorContainer._sut._tcoInspectorDialogue._dialogTerminate.Synchron = true;
                    return true;
            });

            Assert.AreEqual(eOverallResult.Failed, InspectorContainer._sut.ResultAsEnum);
            Assert.AreEqual(fDescII_1 + ";" + fDescAI_1 + ";" + fDescDI_1 + ";", InspectorContainer._sut._overallResult.Failures.Synchron);
            Assert.AreEqual(errCodeII_1 + ";" + errCodeAI_1 + ";" + errCodeDI_1 + ";", InspectorContainer._sut._overallResult.ErrorCodes.Synchron);
        }

        [Test]
        public void should_inspect_with_retries_normalization()
        {
            this.set_to_fail();

            var expected = 3;

            container._diis[0]._data.NumberOfAllowedRetries.Synchron = 3;            
            container._diis[1]._data.NumberOfAllowedRetries.Synchron = 0;            
            container._dais[0]._data.NumberOfAllowedRetries.Synchron = 100;            
            container._dais[1]._data.NumberOfAllowedRetries.Synchron = 5;            
            container._ddis[0]._data.NumberOfAllowedRetries.Synchron = 4;            
            container._ddis[1]._data.NumberOfAllowedRetries.Synchron = 8;

            InspectorContainer._overallResult.Result.Synchron = (short)eOverallResult.Passed;

            var initialState = InspectorContainer._coordinator._state.Synchron;
            var expectedState = (short)(initialState - 10);
            InspectorContainer._retryState.Synchron = expectedState;

            InspectorContainer.ExecuteProbeRun((int)eInspectionGroupTests.InspectWithNormalizedNumberOfRetries);

            Assert.AreEqual(expected, container._diis[0]._data.NumberOfAllowedRetries.Synchron);
            Assert.AreEqual(expected, container._diis[1]._data.NumberOfAllowedRetries.Synchron);
            Assert.AreEqual(expected, container._dais[0]._data.NumberOfAllowedRetries.Synchron);
            Assert.AreEqual(expected, container._dais[1]._data.NumberOfAllowedRetries.Synchron);
            Assert.AreEqual(expected, container._ddis[0]._data.NumberOfAllowedRetries.Synchron);
            Assert.AreEqual(expected, container._ddis[1]._data.NumberOfAllowedRetries.Synchron);

        }
    }
}
