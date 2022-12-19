
using TcoDataTests;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.Data;

namespace Sandbox.TcoData.Wpf
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {



        }

        public void ExternalInvokeSearchTest()
        {
            Plc.MAIN.sandbox.DataManager.DataExchangeOperations.FilterByID = "TEST";
            Plc.MAIN.sandbox.DataManager.DataExchangeOperations.InvokeSearch();
        }
        
        public TcoDataTestsTwinController Plc { get; } = TcoDataTests.Entry.TcoDataTests;
    }
}
