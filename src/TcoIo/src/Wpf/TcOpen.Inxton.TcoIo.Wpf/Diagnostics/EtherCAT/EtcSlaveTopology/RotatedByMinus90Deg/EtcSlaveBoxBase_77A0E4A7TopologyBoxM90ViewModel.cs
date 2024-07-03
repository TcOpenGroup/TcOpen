using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcSlaveBoxBase_77A0E4A7TopologyBoxM90ViewModel : RenderableViewModel
    {
        public IVortexObject Box { get; private set; }
        public override object Model
        {
            get => Box;
            set => Box = value as IVortexObject;
        }
    }
}
