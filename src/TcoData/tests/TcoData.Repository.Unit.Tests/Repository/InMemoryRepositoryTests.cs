using System;
using NUnit.Framework;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.InMemory;

namespace TcoDataUnitTests
{
    [TestFixture()]
    public class InMemoryRepositoryTests : RepositoryBaseTests
    {
        public override void Init()
        {
            this.repository = Repository.Factory<DataTestObject>(
                new InMemoryRepositorySettings<DataTestObject>()
            );
            this.repository_altered_structure = Repository.Factory<DataTestObjectAlteredStructure>(
                new InMemoryRepositorySettings<DataTestObjectAlteredStructure>()
            );
            this.repository.OnCreate = (id, data) =>
            {
                data._Created = DateTimeProviders.DateTimeProvider.Now;
                data._Modified = DateTimeProviders.DateTimeProvider.Now;
            };
            this.repository.OnUpdate = (id, data) =>
            {
                data._Modified = DateTimeProviders.DateTimeProvider.Now;
            };
        }
    }
}
