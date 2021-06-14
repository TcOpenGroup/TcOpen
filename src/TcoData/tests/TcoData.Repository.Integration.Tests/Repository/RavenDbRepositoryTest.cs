using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.RavenDb;

namespace TcoDataUnitTests
{
    public class RavenDbRepositoryTest : RepositoryBaseTests
    {
        public override void Init()
        {
            if (this.repository == null)
            {
                //var parameters = new RavenDbTestRepositorySettings<DataTestObject>();
                //var parametersAltered = new RavenDbTestRepositorySettings<DataTestObjectAlteredStructure>();
                var parameters = new RavenDbRepositorySettings<DataTestObject>(new string[]{"http://localhost:8080"}, "TestDataBase", "", "credentials");
                var parametersAltered = new RavenDbRepositorySettings<DataTestObjectAlteredStructure>(new string[] { "http://localhost:8080" }, "TestDataBase", "", "");
                
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
