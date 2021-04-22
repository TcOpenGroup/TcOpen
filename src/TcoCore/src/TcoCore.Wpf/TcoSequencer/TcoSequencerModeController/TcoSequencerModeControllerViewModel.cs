using Vortex.Presentation.Wpf;

namespace TcoCore
{
    public class TcoSequencerModeControllerViewModel : RenderableViewModel
    {
        public TcoSequencerModeControllerViewModel()
        {
            
        }

        public TcoSequencerModeController TcoSequencerModeController { get; private set; }

        public override object Model { get => TcoSequencerModeController; set { TcoSequencerModeController = value as TcoSequencerModeController; } }

        
    }
}
