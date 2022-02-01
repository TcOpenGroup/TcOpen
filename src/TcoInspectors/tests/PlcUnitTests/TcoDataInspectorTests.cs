using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoInspectorsTests;
using Tc.Prober.Runners;
using TcoInspectors;
using System.Threading.Tasks;
using System.Threading;

namespace TcoInspectorsUnitTests
{
    public class TcoDataInspectorTests : TcoInspectorTests
    {        
        private TcoInspectorsTests.TcoDataInspectorTests container;

        [OneTimeSetUp]
        public override void OneTimeSetup()
        {
            Entry.TcoInspectorsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            container = Entry.TcoInspectorsTests.MAIN._dataInspectorTests;
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
            container._sut._data.RequiredStatus.Synchron = "this is some data to compare";
            container._inspectedValue.Synchron = "data to compare and fail";            
        }

        protected override dynamic set_to_fail_above_threshold()
        {
            container._sut._data.RequiredStatus.Synchron = "this is some data to compare";
            container._inspectedValue.Synchron = "this is some data to compare and fail";

            return container._inspectedValue.Synchron;
        }

        protected override dynamic set_to_pass_at_bottom_threshold()
        {
            container._sut._data.RequiredStatus.Synchron = "this is some data to compare and pass";
            container._inspectedValue.Synchron = "this is some data to compare and pass";
            return container._inspectedValue.Synchron;
        }

        protected override dynamic set_to_pass_at_mid()
        {
            container._sut._data.RequiredStatus.Synchron = "this is some data to compare and pass";
            container._inspectedValue.Synchron = "this is some data to compare and pass";
            return container._inspectedValue.Synchron;
        }

        protected override void set_to_pass_at_top_threshold()
        {
            container._sut._data.RequiredStatus.Synchron = "this is some data to compare and pass";
            container._inspectedValue.Synchron = "this is some data to compare and pass";
        }

        protected override dynamic set_introduce_jitter()
        {
            container._sut._data.RequiredStatus.Synchron = "this is some data to compare and pass";
            container._inspectedValue.Synchron = "this is some data to compare and pass";

            var ok = "this is some data to compare and pass";
            var nok = "this is some data to compare and fail";

            container._inspectedValue.Synchron = container._inspectedValue.Synchron == "this is some data to compare and pass" ? nok : ok;

            return container._inspectedValue.Synchron;
        }

    }
}
