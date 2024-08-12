using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoSequencerObserverViewModel : RenderableViewModelBase
    {
        public void UpdateStepsTable()
        {
            Observer.Read();
            var steps = Observer
                ._steps.Take(Observer._stepsCount.LastValue)
                .Select(p =>
                {
                    var plain = p.CreatePlainerType();
                    plain.CopyCyclicToPlain(p);
                    plain.IsActive = plain.Order == this.Observer._currentStepOrder.LastValue;
                    return plain;
                });
            Steps = steps;
        }

        public IEnumerable<PlainStepDetails> Steps { get; set; }
        public TcoSequencerObserver Observer { get; set; }
        public override object Model
        {
            get => Observer;
            set { Observer = value as TcoSequencerObserver; }
        }
    }
}
