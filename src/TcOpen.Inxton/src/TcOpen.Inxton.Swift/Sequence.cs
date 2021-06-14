using System;
using System.Collections.Generic;
using System.Linq;

namespace TcOpen.Inxton.Swift
{
    public class Sequence
    {
        public Sequence()
        {

        }

        readonly IList<Step> steps = new List<Step>();

        /// <summary>
        /// Gets steps of this sequence.
        /// </summary>
        public IEnumerable<Step> Steps => steps;

        /// <summary>
        /// Adds step to the this sequence.
        /// </summary>
        /// <returns>Newly added step.</returns>
        public object AddStep()
        {
            var newStep = new Step();
            steps.Add(newStep);
            return newStep;
        }
    }
}
