using System.IO;
using TcoDataTests;
using TcOpen.Inxton.Data.Json;

namespace TcoDataUnitTests
{
    public class TcoDataExchangeJsonTest : TcoDataExchangeBaseTest
    {
        public override void Init()
        {
            var executingPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location
            );
            DataFolder = Path.Combine(executingPath, "_data");
            Repository = new JsonRepository<PlainstProcessData>(
                new JsonRepositorySettings<PlainstProcessData>(DataFolder)
            );
            ClearFolder(DataFolder);
        }

        private void ClearFolder(string dataDir)
        {
            foreach (var file in Directory.GetFiles(dataDir))
            {
                File.Delete(file);
            }
        }
    }
}
