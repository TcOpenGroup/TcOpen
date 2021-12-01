
using TcoDataTests;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.Data;

namespace Sandbox.TcoData.Wpf
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            var parameters = new MongoDbRepositorySettings<PlainData>("mongodb://localhost:27017", "TestDataBase", "TestCollection");
            var repository = Repository.Factory<PlainData>(parameters);
            repository.OnRecordUpdateValidation = (data) => 
            {
                return new DataItemValidation[]
                    {
                        new DataItemValidation($"'{nameof(data.sampleData.SampleInt)}' must be greater than 0", data.sampleData.SampleInt <= 0),
                        new DataItemValidation($"'{nameof(data.sampleData.SampleInt2)}' must be less than 0", data.sampleData.SampleInt2 > 0)
                    };
             };
            //var x = new DataViewModel<PlainData>(repository, new TcoDataManager());
            Plc.MAIN.sandbox.DataManager.InitializeRepository(repository);
        }
        
        public TcoDataTestsTwinController Plc { get; } = TcoDataTests.Entry.TcoDataTests;
    }
}
