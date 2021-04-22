using Vortex.Presentation.Wpf;

namespace TcoCore
{
    public class TcoSequencerViewModel : RenderableViewModel
    {
        public TcoSequencerViewModel()
        {
            
        }

        public TcoSequencer TcoSequencer { get; private set; }

        public override object Model { get => TcoSequencer; set { TcoSequencer = value as TcoSequencer; } }

        
    }
}
