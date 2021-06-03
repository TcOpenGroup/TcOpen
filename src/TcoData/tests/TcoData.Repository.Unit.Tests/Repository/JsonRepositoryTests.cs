using NUnit.Framework;
using System.IO;
using System.Reflection;
using TcoData.Repository.Json;
using TcOpen.Inxton.Data.Json;

namespace TcoDataUnitTests
{
    [TestFixture()]
    public class JsonFileRepositoryTests : RepositoryBaseTests
    { 
        string outputDir = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, "JsonRepositoryTestOutputDir");
     
        public override void Init()
        {
            if (Directory.Exists(outputDir))
            {
                Directory.Delete(outputDir, true);
            }

            this.repository = Repository.Factory<DataTestObject>(new JsonRepositorySettings<DataTestObject>(outputDir));
            this.repository_altered_structure = Repository.Factory<DataTestObjectAlteredStructure>(new JsonRepositorySettings<DataTestObjectAlteredStructure>(outputDir));
        }
    }
}