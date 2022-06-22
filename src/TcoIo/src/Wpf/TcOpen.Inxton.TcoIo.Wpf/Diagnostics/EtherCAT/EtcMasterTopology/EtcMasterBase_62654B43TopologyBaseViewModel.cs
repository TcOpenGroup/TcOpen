using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcMasterBase_62654B43TopologyBaseViewModel : RenderableViewModel
    {               
        public IVortexObject Component { get; private set; }
        public override object Model { get => Component; set => Component = value as IVortexObject; }
    }
}
