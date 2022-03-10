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
            Observer.Read();            
            Steps = Observer._steps.Take(Observer._stepsCount.LastValue).Select(p => 
                        { 
                            var plain = p.CreatePlainerType(); 
                            plain.CopyCyclicToPlain(p); 
                            plain.IsActive = plain.Order == this.Observer._currentStepOrder.LastValue; 
                            return plain; 
                        });           
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
