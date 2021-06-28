using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Swift;
using Vortex.Connector;

namespace TcoDrivesBeckhoff
{
    public partial class TcoDriveSimple
    {
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this._moveAbsoluteTask.CodeProvider = new MoveAbsoluteTaskCodeProvider(this);
            this._moveRelativeTask.CodeProvider = new MoveRelativeTaskCodeProvider(this);
            this._moveModuloTask.CodeProvider = new MoveModuloTaskCodeProvider(this);
            this._moveVelocityTask.CodeProvider = new MoveVelocityTaskCodeProvider(this);
        }       
    }
}

