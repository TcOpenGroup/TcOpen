using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoInspectorsTests;
using Tc.Prober.Runners;
using TcoInspectors;
using System.Threading.Tasks;
using System.Threading;

namespace TcoInspectorsUnitTests
{
    public class TcoAnalogueInspectorTests : TcoInspectorTests
    {        
        private TcoInspectorsTests.TcoAnalogueInspectorTests container;

        [OneTimeSetUp]
        public override void OneTimeSetup()
        {
            Entry.TcoInspectorsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            container = Entry.TcoInspectorsTests.MAIN._analogueInspectorTests;
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
            container._sut._data.RequiredMin.Synchron = 10.0;
            container._inspectedValue.Synchron = 9.0;
            container._sut._data.RequiredMax.Synchron = 11.0;
        }

        protected override double set_to_fail_above_threshold()
        {
            container._sut._data.RequiredMin.Synchron = 10.0;
            container._inspectedValue.Synchron = 11.1;
            container._sut._data.RequiredMax.Synchron = 11.0;

            return container._inspectedValue.Synchron;
        }

        protected override double set_to_pass_at_bottom_threshold()
        {
            container._sut._data.RequiredMin.Synchron = 10.0;
            container._inspectedValue.Synchron = 10.0;
            container._sut._data.RequiredMax.Synchron = 11.0;
            return container._inspectedValue.Synchron;
        }

        protected override double set_to_pass_at_mid()
        {
            container._sut._data.RequiredMin.Synchron = 9.0;
            container._inspectedValue.Synchron = 10.0;
            container._sut._data.RequiredMax.Synchron = 11.0;
            return container._inspectedValue.Synchron;
        }

        protected override void set_to_pass_at_top_threshold()
        {
            container._sut._data.RequiredMin.Synchron = 10.0;
            container._inspectedValue.Synchron = 11.0;
            container._sut._data.RequiredMax.Synchron = 11.0;
        }

        protected override double set_introduce_jitter()
        {
            container._sut._data.RequiredMin.Synchron = 10.0;
            container._inspectedValue.Synchron = 9.0;
            container._sut._data.RequiredMax.Synchron = 11.0;

            if (container._inspectedValue.Synchron == container._sut._data.RequiredMin.Synchron)
            {
                container._inspectedValue.Synchron = 11.0;
            }
            else if (container._inspectedValue.Synchron == container._sut._data.RequiredMax.Synchron)
            {
                container._inspectedValue.Synchron = 9.0;
            }
            else
            {
                container._inspectedValue.Synchron = 10.0;
            }

            return container._inspectedValue.Synchron;
        }

    }
}
