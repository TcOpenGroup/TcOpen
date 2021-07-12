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
            this._stopTask.LogPayloadDecoration = () => this.CreatePlain();
            this._haltTask.LogPayloadDecoration = () => this.CreatePlain();
            this._setPositionTask.LogPayloadDecoration = () => this._setPositionTask.CreatePlain();
            this._soEResetTask.LogPayloadDecoration = () => this.CreatePlain();
            this._resetTask.LogPayloadDecoration = () => this.CreatePlain();
            this._homeTask.LogPayloadDecoration = () => this._homeTask.CreatePlain();
            this._moveAbsoluteTask.LogPayloadDecoration = () => this._moveAbsoluteTask.CreatePlain();
            this._moveRelativeTask.LogPayloadDecoration = () => this._moveRelativeTask.CreatePlain();
            this._moveModuloTask.LogPayloadDecoration = () => this._moveModuloTask.CreatePlain();
            this._moveVelocityTask.LogPayloadDecoration = () => this._moveVelocityTask.CreatePlain();

            this._moveAbsoluteTask.CodeProvider = new MoveAbsoluteTaskCodeProvider(this);
            this._moveRelativeTask.CodeProvider = new MoveRelativeTaskCodeProvider(this);
            this._moveModuloTask.CodeProvider = new MoveModuloTaskCodeProvider(this);
            this._moveVelocityTask.CodeProvider = new MoveVelocityTaskCodeProvider(this);
        }       
    }
}



