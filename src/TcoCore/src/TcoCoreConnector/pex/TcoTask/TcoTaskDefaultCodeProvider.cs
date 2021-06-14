using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using TcOpen.Inxton.Swift;

namespace TcoCore.Swift
{
    public class TcoTaskDefaultCodeProvider : ICodeProvider
    {
        public TcoTaskDefaultCodeProvider(IsTask _task)
        {
            task = _task;
        }

        IsTask task;

        public string Code(params object[] args)
        {
            return $"{task.Symbol}.Invoke().Done";
        }
    }
}
