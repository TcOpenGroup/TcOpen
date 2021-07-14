using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Swift;

namespace TcoCore
{
    public interface IsTask
    {
        string Symbol { get; }
        string HumanReadable { get; }
        ICodeProvider CodeProvider { get; }
        RecordTaskActionDelegate RecordTaskAction { get; set; }
    }
}
