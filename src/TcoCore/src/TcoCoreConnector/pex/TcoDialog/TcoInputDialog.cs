using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Dialogs;
using Vortex.Connector;

namespace TcoCore
{
    public partial class TcoInputDialog : IsDialog
    {
        public new void ShowAgainIfInvoked()
        {
            this.Read();
            if ((eDialogAnswer)this._answer.LastValue == eDialogAnswer.Invoked)
            {
                this._restoreRequest.Synchron = true;
            }
        }
    }
}
