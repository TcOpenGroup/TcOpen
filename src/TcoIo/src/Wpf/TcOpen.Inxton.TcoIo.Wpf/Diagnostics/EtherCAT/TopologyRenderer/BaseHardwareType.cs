using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.TcoIo.Wpf
{
    public class BaseHardwareType
    {
        public bool IsEtcMasterBase{ get; set; }
        public bool IsEtcSlaveBoxBase { get; set; }
        public bool IsEtcSlaveTerminalBase { get; set; }

        public BaseHardwareType()
        {
            IsEtcMasterBase = false;
            IsEtcSlaveBoxBase = false;
            IsEtcSlaveTerminalBase = false;
        }
    }
}
