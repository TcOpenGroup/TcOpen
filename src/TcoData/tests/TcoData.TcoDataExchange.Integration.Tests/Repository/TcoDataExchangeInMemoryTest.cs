using TcoDataTests;
using TcoData.Repository.InMemory;

namespace TcoDataUnitTests
{
    public class TcoDataExchangeInMemoryTest : TcoDataExchangeBaseTest
    {
        public override void Init()
        {
            Repository = new InMemoryRepository<PlainstProcessData>();
        }
       
    }
}
