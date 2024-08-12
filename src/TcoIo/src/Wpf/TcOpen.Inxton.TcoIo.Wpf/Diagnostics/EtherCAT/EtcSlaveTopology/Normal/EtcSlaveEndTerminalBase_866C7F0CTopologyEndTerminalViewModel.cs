using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcSlaveEndTerminalBase_866C7F0CTopologyEndTerminalViewModel : RenderableViewModel
    {
        public IVortexObject Terminal { get; private set; }
        public override object Model
        {
            get => Terminal;
            set => Terminal = value as IVortexObject;
        }
    }
}
