using Vortex.Presentation.Wpf;

namespace TcoCore
{
    public class TcoSequencerBaseViewModel : RenderableViewModel
    {
        public TcoSequencerBaseViewModel() { }

        public TcoSequencerBase TcoSequencer { get; private set; }

        public override object Model
        {
            get => TcoSequencer;
            set { TcoSequencer = value as TcoSequencerBase; }
        }
    }
}
