
namespace Sandbox.TcoElements.Wpf
{
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public TcoElementsTests.TcoElementsTestsTwinController TcoElementsTestsPlc { get; } = TcoElementsTests.Entry.TcoElementsTests;
    }
}
