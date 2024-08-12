using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class TcoEcatSlaveStateViewModel : RenderableViewModel
    {
        public TcoEcatSlaveStateViewModel() { }

        public TcoEcatSlaveState Component { get; private set; }
        public override object Model
        {
            get => Component;
            set { Component = value as TcoEcatSlaveState; }
        }
    }

    public class TcoEcatSlaveStateServiceViewModel : TcoEcatSlaveStateViewModel { }
}
