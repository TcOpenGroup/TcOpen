using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TcoData.Repository.Json;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.Json;

namespace TcoDataUnitTests
{
    [TestFixture()]
    public class JsonFileRepositoryTests : RepositoryBaseTests
    {
        string outputDir = Path.Combine(
            new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName,
            "JsonRepositoryTestOutputDir"
        );

        public override void Init()
        {
            if (Directory.Exists(outputDir))
            {
                Directory.Delete(outputDir, true);
            }

            this.repository = Repository.Factory<DataTestObject>(
                new JsonRepositorySettings<DataTestObject>(outputDir)
            );
            this.repository_altered_structure = Repository.Factory<DataTestObjectAlteredStructure>(
                new JsonRepositorySettings<DataTestObjectAlteredStructure>(outputDir)
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
