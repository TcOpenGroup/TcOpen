using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcSlaveTerminalBase_947E5A46ServiceViewModel : RenderableViewModel
    {               
        public IVortexObject Component { get; private set; }
        public override object Model { get => Component; set => Component = value as IVortexObject; }
    }
}
