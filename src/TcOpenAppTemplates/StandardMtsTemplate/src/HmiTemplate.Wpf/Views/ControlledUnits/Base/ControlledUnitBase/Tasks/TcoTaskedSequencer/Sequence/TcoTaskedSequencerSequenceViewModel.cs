using System.Collections.Generic;
using Vortex.Presentation.Wpf;
using TcoCore;

namespace MainPlc
{
    public class TcoTaskedSequencerSequenceViewModel : RenderableViewModel
    {
        public TcoTaskedSequencerSequenceViewModel()
        {

        }

        TcoTaskedSequencer component = new TcoTaskedSequencer();
        public TcoTaskedSequencer Component
        {
            get => component;
            private set
            {
                if (component == value)
                {
                    return;
                }

                SetProperty(ref component, value);
            }
        }

        public override object Model { get => Component; set => Component = (TcoTaskedSequencer)value; }

        public IEnumerable<object> _parallelTasks = new List<object>();
        public IEnumerable<object> ParallelTasks
        {
            get
            {
                if (Component != null && Component.GetChildren() != null)
                {
                    _parallelTasks = Component.GetChildren<TcoSequencerBase>();
                }

                return _parallelTasks;
            }
        }


        public object StepController { get { return this.Component._modeController; } }
    }
}
