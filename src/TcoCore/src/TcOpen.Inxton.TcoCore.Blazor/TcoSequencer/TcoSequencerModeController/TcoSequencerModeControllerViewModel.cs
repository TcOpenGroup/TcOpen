using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoSequencerModeControllerViewModel : RenderableViewModelBase
    {
        public TcoSequencerModeControllerViewModel() { }

        public TcoSequencerModeController TcoSequencerModeController { get; private set; }

        public override object Model
        {
            get => TcoSequencerModeController;
            set { TcoSequencerModeController = value as TcoSequencerModeController; }
        }
    }
}
