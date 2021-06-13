using TcOpen.Inxton.RavenDb;

namespace TcoDataUnitTests
{
    public class RavenDbRepositoryTest : RepositoryBaseTests
    {
        public override void Init()
        {
            if (this.repository == null)
            {
                var parameters = new RavenDbTestRepositorySettings<DataTestObject>();
                var parametersAltered = new RavenDbTestRepositorySettings<DataTestObjectAlteredStructure>();
                
                this.repository = new RavenDbRepository<DataTestObject>(parameters);
                this.repository_altered_structure = new RavenDbRepository<DataTestObjectAlteredStructure>(parametersAltered);
            }

            foreach (var item in this.repository.GetRecords("*"))
            {
                repository.Delete(item._EntityId);
            }
        }
    }
}
