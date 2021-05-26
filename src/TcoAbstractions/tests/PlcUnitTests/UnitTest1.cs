using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoAbstractionsTests;
using Tc.Prober.Runners;

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
