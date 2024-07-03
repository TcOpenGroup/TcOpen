using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class TcoEthercatMasterDeviceTopologyViewModel : RenderableViewModel
    {
        public IVortexObject Device { get; private set; }
        public override object Model
        {
            get => Device;
            set => Device = value as IVortexObject;
        }
    }
}
