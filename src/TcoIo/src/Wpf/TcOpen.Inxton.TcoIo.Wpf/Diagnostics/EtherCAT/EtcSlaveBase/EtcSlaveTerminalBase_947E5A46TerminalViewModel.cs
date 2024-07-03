using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcSlaveTerminalBase_947E5A46TerminalViewModel : RenderableViewModel
    {
        public IVortexObject Terminal { get; private set; }
        public override object Model
        {
            get => Terminal;
            set => Terminal = value as IVortexObject;
        }
    }
}
