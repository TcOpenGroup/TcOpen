using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcSlaveTerminalBase_947E5A46TopologyTerminalViewModel : RenderableViewModel
    {               
        public IVortexObject Terminal { get; private set; }
        public override object Model { get => Terminal; set => Terminal = value as IVortexObject; }
    }
}
