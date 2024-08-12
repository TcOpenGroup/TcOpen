using NUnit.Framework;
using NUnit.Framework.Constraints;
using Tc.Prober.Runners;
using TcoAbstractionsTests;

namespace TcoAbstractionsUnitTests
{
    public class fbUnitTestExampleTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Entry.TcoAbstractionsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
        }
    }
}
