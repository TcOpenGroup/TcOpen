using TcoDataTests;
using TcOpen.Inxton.Data.InMemory;

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
