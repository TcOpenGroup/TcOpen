using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoInspectorsTests;
using Tc.Prober.Runners;
using TcoInspectors;
using System.Threading.Tasks;
using System.Threading;

namespace TcoInspectorsUnitTests
{
    public class TcoDigitalInspectorTests
    {
        DigitalInspectorTests container;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Entry.TcoInspectorsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            container = Entry.TcoInspectorsTests.MAIN._digitalInspectorTests;
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
            container.inspectedValue.Synchron = actualSignal;

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
            container.inspectedValue.Synchron = actualSignal;
            
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
            container.inspectedValue.Synchron = actualSignal;
            container._sut._data.IsExcluded.Synchron = true;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Excluded, container._sut.ResultAsEnum);

            Assert.AreEqual(actualSignal, container._sut._data.DetectedStatus.Synchron);
        }

        [Test]
        public void inspect_get_result_passed()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container.inspectedValue.Synchron = true;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            container.ExecuteProbeRun(1, (int)eDigitalInspectorTests.GetResult);

            Assert.AreEqual((short)eInspectorResult.Passed, container._result.Synchron);           
        }

        [Test]
        public void inspect_get_result_failed()
        {
            container._sut._data.RequiredStatus.Synchron = false;
            container.inspectedValue.Synchron = true;

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            container.ExecuteProbeRun(1, (int)eDigitalInspectorTests.GetResult);

            Assert.AreEqual((short)eInspectorResult.Failed, container._result.Synchron);
        }

        [Test]       
        public void inspect_pass_time_more_than_file_time_must_fail()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container.inspectedValue.Synchron = true;
            container._sut._data.PassedTime.Synchron = System.TimeSpan.FromMilliseconds(1000);
            container._sut._data.FailedTime.Synchron = System.TimeSpan.FromMilliseconds(500);            

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);

            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);            
        }

        [Test]
        public void inspect_jitter_signal_must_fail()
        {
            container._sut._data.RequiredStatus.Synchron = true;          
            container._sut._data.PassedTime.Synchron = System.TimeSpan.FromMilliseconds(250);
            container._sut._data.FailedTime.Synchron = System.TimeSpan.FromMilliseconds(700);

            var run = true;
            var task = Task.Run(() => { while (run) { Thread.Sleep(10); container.inspectedValue.Synchron = !container.inspectedValue.Synchron; } });

            container.ExecuteProbeRun((int)eDigitalInspectorTests.Inspect);
            run = false;
            Assert.AreEqual(eInspectorResult.Failed, container._sut.ResultAsEnum);

            while (!task.IsCompleted) { }

            Assert.IsTrue(task.IsCompleted);
        }
    }
}
