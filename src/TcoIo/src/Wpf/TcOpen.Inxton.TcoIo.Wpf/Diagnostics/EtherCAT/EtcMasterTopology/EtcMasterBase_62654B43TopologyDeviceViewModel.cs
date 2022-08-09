using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcMasterBase_62654B43TopologyDeviceViewModel : RenderableViewModel
    {               
        public IVortexObject Device { get; private set; }
        public override object Model { get => Device; set => Device = value as IVortexObject; }
    }
}
