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

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Entry.TcoInspectorsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            container = Entry.TcoInspectorsTests.MAIN._inspectionGroupTests;
        }

        [SetUp]
        public void SetUp()
        {
            foreach (var dinsp in container._diis)
            {
                var plain = dinsp.CreatePlainerType();
                dinsp.FlushPlainToOnline(plain);
            }
            
            container.ExecuteProbeRun(1, 0);
        }


        [Test]   
        public void inspection_group_must_fail()
        {
            container._diis[0]._data.RequiredStatus.Synchron = true;
            container._dii_inspectedValues[0].Synchron = true;

            container._diis[1]._data.RequiredStatus.Synchron = true;
            container._dii_inspectedValues[1].Synchron = false;
            
            container.ExecuteProbeRun((int)eInspectionGroupTests.Inspect);

            Assert.AreEqual((short)eOverallResult.Failed, container._sut._result.Result.Synchron);
        }

        [Test]
        public void inspection_group_must_pass()
        {
            container._diis[0]._data.RequiredStatus.Synchron = true;
            container._dii_inspectedValues[0].Synchron = true;

            container._diis[1]._data.RequiredStatus.Synchron = false;
            container._dii_inspectedValues[1].Synchron = false;



            container.ExecuteProbeRun((int)eInspectionGroupTests.Inspect);

            Assert.AreEqual((short)eOverallResult.Failed, container._sut._result.Result.Synchron);
        }
    }
}
