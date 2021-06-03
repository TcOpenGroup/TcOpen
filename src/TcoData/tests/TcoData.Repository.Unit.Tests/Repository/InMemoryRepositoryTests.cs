using NUnit.Framework;
using TcOpen.Inxton.Data.InMemory;

namespace TcoDataUnitTests
{
    [TestFixture()]
    public class InMemoryRepositoryTests : RepositoryBaseTests
    {        
        
        public override void Init()
        {
            this.repository = Repository.Factory<DataTestObject>(new InMemoryRepositorySettings<DataTestObject>());
            this.repository_altered_structure = Repository.Factory<DataTestObjectAlteredStructure>(new InMemoryRepositorySettings<DataTestObjectAlteredStructure>());
        }
    }
}