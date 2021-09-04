using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoSequencerViewModel : RenderableViewModelBase
    {
        public TcoSequencerViewModel()
        {

        }

        public TcoSequencer TcoSequencer { get; private set; }

        public override object Model { get => TcoSequencer; set { TcoSequencer = value as TcoSequencer; } }
    }
}
