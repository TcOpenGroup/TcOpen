using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vortex.Connector;

namespace TcOpen.Inxton.Swift
{
    public class Sequence
    {
        public Sequence() { }

        readonly IList<Step> steps = new List<Step>();

        /// <summary>
        /// Gets steps of this sequence.
        /// </summary>
        public IEnumerable<Step> Steps => steps;

        /// <summary>
        /// Adds step to the this sequence.
        /// </summary>
        /// <returns>Newly added step.</returns>
        public Step AddStep(IVortexElement origin = null)
        {
            var newStep = new Step(origin);
            steps.Add(newStep);
            return newStep;
        }

        public StringBuilder EmitCode(StringBuilder sb)
        {
            int stepId = 0;
            foreach (var step in Steps)
            {
                step.EmitCode(sb, ++stepId);
            }

            return sb;
        }
    }
}
