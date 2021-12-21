
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
        
        public TcoDataTestsTwinController Plc { get; } = TcoDataTests.Entry.TcoDataTests;
    }
}
