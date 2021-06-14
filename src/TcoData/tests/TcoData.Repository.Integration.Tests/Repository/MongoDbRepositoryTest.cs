using TcOpen.Inxton.MongoDb;

namespace TcoDataUnitTests
{
    /**
     * I'm using shared project bcs of these reasons 
     * - Avoid duplication of `DataTestObject` 
     * - When you reference  the assembly with `RepositoryBaseTests` and inherit it in this project, unit test will not be found be NUnit
     * - I need a build time constant to avoid errors with `AlteredStructureTest` - it needs a type from `TcoData.Repository.Unit.Tests` which I don't have.
    **/
    public class MongoDbRepositoryTest : RepositoryBaseTests
    {
        public override void Init()
        {
            if(this.repository == null)
            {
                var a = new DataTestObject();
#pragma warning disable CS0618 // Type or member is obsolete
                var parameters = new MongoDbRepositorySettings<DataTestObject>("mongodb://localhost:27017", "TestDataBase", "TestCollection");
                var parametersAltered = new MongoDbRepositorySettings<DataTestObjectAlteredStructure>("mongodb://localhost:27017", "TestDataBase", "TestCollection");
#pragma warning restore CS0618 // Type or member is obsolete
                this.repository = Repository.Factory(parameters);

                this.repository_altered_structure = Repository.Factory(parametersAltered);
            }

            foreach (var item in this.repository.GetRecords("*"))
            {
                repository.Delete(item._EntityId);
            } 
        }
    }
}
