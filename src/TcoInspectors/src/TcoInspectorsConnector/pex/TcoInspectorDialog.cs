using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using TcOpen.Inxton.Dialogs;
using Vortex.Connector;

namespace TcoInspectors
{
    public partial class TcoInspectorDialog : IsDialog
    {
        public new void ShowAgainIfInvoked()
        {
            this.Read();
            if (this._isInvoked.LastValue)
            {
                this._restoreRequest.Synchron = true;
            }
        }
    }
}
