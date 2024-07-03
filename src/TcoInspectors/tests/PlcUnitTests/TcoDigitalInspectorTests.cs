using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Tc.Prober.Runners;
using TcoInspectors;
using TcoInspectorsTests;

namespace TcoInspectorsUnitTests
{
    public class TcoDigitalInspectorTests : TcoInspectorTests
    {
        private TcoInspectorsTests.TcoDigitalInspectorTests container;

        [OneTimeSetUp]
        public override void OneTimeSetup()
        {
            Entry.TcoInspectorsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            container = Entry.TcoInspectorsTests.MAIN._digitalInspectorTests;
            InspectorContainer = container;
        }

        [SetUp]
        public override void SetUp()
        {
            var plain = container._sut._data.CreatePlainerType();
            container._sut._data.FlushPlainToOnline(plain);
            container.ExecuteProbeRun(1, 0);
        }

        protected override void set_to_fail_below_threshold()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = false;
        }

        protected override dynamic set_to_fail_above_threshold()
        {
            container._sut._data.RequiredStatus.Synchron = false;
            container._inspectedValue.Synchron = true;

            return container._inspectedValue.Synchron;
        }

        protected override dynamic set_to_pass_at_bottom_threshold()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
            return container._inspectedValue.Synchron;
        }

        protected override dynamic set_to_pass_at_mid()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
            return container._inspectedValue.Synchron;
        }

        protected override void set_to_pass_at_top_threshold()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;
        }

        protected override dynamic set_introduce_jitter()
        {
            container._sut._data.RequiredStatus.Synchron = true;
            container._inspectedValue.Synchron = true;

            container._inspectedValue.Synchron = !container._inspectedValue.Synchron;

            return container._inspectedValue.Synchron;
        }
    }
}
