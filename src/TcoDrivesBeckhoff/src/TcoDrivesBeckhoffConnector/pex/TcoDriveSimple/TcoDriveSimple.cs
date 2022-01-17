using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Swift;
using Vortex.Connector;
using TcoCore;

namespace TcoDrivesBeckhoff
{   
    public partial class TcoDriveSimple
    {
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {            
            this._stopTask.LogPayloadDecoration = () => this.GetPlainFromOnline();
            this._haltTask.LogPayloadDecoration = () => this.GetPlainFromOnline();
            this._setPositionTask.LogPayloadDecoration = () => this._setPositionTask.GetPlainFromOnline();
            this._soEResetTask.LogPayloadDecoration = () => this.GetPlainFromOnline();
            this._resetTask.LogPayloadDecoration = () => this.GetPlainFromOnline();
            this._homeTask.LogPayloadDecoration = () => this._homeTask.GetPlainFromOnline();
            this._moveAbsoluteTask.LogPayloadDecoration = () => this._moveAbsoluteTask.GetPlainFromOnline();
            this._moveRelativeTask.LogPayloadDecoration = () => this._moveRelativeTask.GetPlainFromOnline();
            this._moveModuloTask.LogPayloadDecoration = () => this._moveModuloTask.GetPlainFromOnline();
            this._moveVelocityTask.LogPayloadDecoration = () => this._moveVelocityTask.GetPlainFromOnline();

            this._moveAbsoluteTask.CodeProvider = new MoveAbsoluteTaskCodeProvider(this);
            this._moveRelativeTask.CodeProvider = new MoveRelativeTaskCodeProvider(this);
            this._moveModuloTask.CodeProvider = new MoveModuloTaskCodeProvider(this);
            this._moveVelocityTask.CodeProvider = new MoveVelocityTaskCodeProvider(this);
        }       
    }
}



