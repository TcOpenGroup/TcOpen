using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Swift
{
    public interface ICodeRecorder
    {
        void Record(ICodeProvider codeProvider, params object[] args);
    }
}
