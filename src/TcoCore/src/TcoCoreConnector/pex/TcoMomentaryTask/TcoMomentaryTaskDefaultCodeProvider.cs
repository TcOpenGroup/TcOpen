using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using TcOpen.Inxton.Swift;

namespace TcoCore.Swift
{
    public class TcoMomentaryTaskDefaultCodeProvider : ICodeProvider
    {
        public TcoMomentaryTaskDefaultCodeProvider(IsTask _task)
        {
            task = _task;
        }

        IsTask task;

        public string Code(params object[] args)
        {            
            if(args.Length > 0)
            {
                bool result;
                if (bool.TryParse(args[0].ToString(), out result))
                {
                    return result ? $"{task.Symbol}.On()" : $"{task.Symbol}.Off()";
                }                                    
            }

            return "// Wrong type of number of arguments.";
        }
    }
}
