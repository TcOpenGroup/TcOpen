using TcoData;
using TcoDataTests;
using TcOpen.Inxton.Data.Wpf;
using TcOpen.Inxton.Data.MongoDb;

namespace Sandbox.TcoData.Wpf
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            var parameters = new MongoDbRepositorySettings<PlainData>("mongodb://localhost:27017", "TestDataBase", "TestCollection");
            var repository = Repository.Factory<PlainData>(parameters);
            //var x = new DataViewModel<PlainData>(repository, new TcoDataManager());
            Plc.MAIN.sandbox.DataManager.InitializeRepository(repository);
        }



        public TcoDataTestsTwinController Plc { get; } = TcoDataTests.Entry.TcoDataTests;
    }
}
