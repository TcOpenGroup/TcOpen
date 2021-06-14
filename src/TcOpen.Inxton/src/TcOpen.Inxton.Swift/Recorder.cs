using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using Vortex.Connector;

namespace TcOpen.Inxton.Swift
{
    public class Recorder
    {
        public IEnumerable<IsTask> Tasks { get; }

        public Recorder(IVortexObject vortexObject)
        {
            vortexObject.GetDescendants<IsTask>().Where(p => ;
        }

        readonly Sequence sequence = new Sequence();
        public Sequence Sequence => sequence;
    }
}
