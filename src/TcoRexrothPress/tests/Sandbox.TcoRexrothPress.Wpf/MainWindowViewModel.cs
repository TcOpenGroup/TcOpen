using TcoRexrothPressTests;

namespace Sandbox.TcoRexrothPress.Wpf
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel() { }

        public TcoRexrothPressTestsTwinController TcoRexrothPressTestPlc { get; } =
            Entry.TcoRexrothPressTestsPlc;
    }
}
