using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TcOpen.Inxton.TcoCore.Wpf;
using Vortex.Connector;
using Vortex.Presentation.Wpf;


namespace TcoCore
{
    public class TcoSequencerObserverViewModel : RenderableViewModel
    {      
        public void UpdateStepsTable()
        {
            Steps = Observer.GetPlainFromOnline<PlainTcoSequencerObserver>()._steps.Where(p => p.ID != 0);
            var activeStep = Steps.Where(p => p.Order == this.Observer._currentStepOrder.LastValue).FirstOrDefault();
            if(activeStep != null)
            {
                activeStep.IsActive = true;
            }
        }
                      
        IEnumerable<PlainStepDetails> steps;
        public IEnumerable<PlainStepDetails> Steps
        {
            get => steps;
            private set
            {
                if (steps == value)
                {
                    return;
                }

                SetProperty(ref steps, value);
            }
        }

        public TcoSequencerObserver Observer { get; set; }
        public override object Model { get => Observer; set { Observer = value as TcoSequencerObserver; } }
    }
}
