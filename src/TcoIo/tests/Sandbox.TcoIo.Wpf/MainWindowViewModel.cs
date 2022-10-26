using TcoIoTests;

namespace Sandbox.TcoIo.Wpf
{
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        public TcoIoTestsTwinController TcoIoTests { get; } = Entry.TcoIoTests;       
    }
}
