using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcSlaveBase_82538BE2ServiceViewModel : RenderableViewModel
    {               
        public IVortexObject Component { get; private set; }
        public override object Model { get => Component; set => Component = value as IVortexObject; }
    }
}
