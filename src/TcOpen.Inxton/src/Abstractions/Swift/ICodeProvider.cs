using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Swift
{
    public abstract class ICodeProvider
    {
        public abstract string Code(params object[] args);        

        
    }
}
