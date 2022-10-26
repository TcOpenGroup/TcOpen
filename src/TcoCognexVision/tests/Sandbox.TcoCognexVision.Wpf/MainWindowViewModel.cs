using TcoCognexVisionTests;

namespace Sandbox.TcoCognexVision.Wpf
{
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        public TcoCognexVisionTestsTwinController TcoCognexVisionTestPlc { get; } = Entry.TcoCognexVisionTestsPlc;       
    }
}
