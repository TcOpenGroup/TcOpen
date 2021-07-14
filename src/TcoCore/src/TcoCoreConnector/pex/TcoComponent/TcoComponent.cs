using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public partial class TcoComponent
    {
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            
        }

        private IEnumerable<IsTask> _tasks;
        public IEnumerable<IsTask> Tasks 
        {
            get { if (_tasks == null) _tasks = this.GetDescendants<IsTask>(); return _tasks; }
        }

        public object StatusControl
        {
            get
            {
                object status  = this.GetKids().Where(p => p.Symbol.EndsWith("_status")).FirstOrDefault();
                return status != null ? status : new object();
            }
        }

        public object ConfigControl
        {
            get
            {
                object status = this.GetKids().Where(p => p.Symbol.EndsWith("_config")).FirstOrDefault();
                return status != null ? status : new object();
            }
        }
    }
}
