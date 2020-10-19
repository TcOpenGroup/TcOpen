using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcOpen
{
    public class FB_CylinderAlternativeViewModel : RenderableViewModel
    {
        public FB_CylinderAlternativeViewModel()
        {
            MoveToWorkCommand = new RelayCommand(a => { this.Component._manualMoveToWork.Cyclic = true; this.Component._manualMoveToHome.Cyclic = false; });
            MoveToHomeCommand = new RelayCommand(a => { this.Component._manualMoveToHome.Cyclic = true; this.Component._manualMoveToWork.Cyclic = false; });
        }

        public FB_CylinderAlternative Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as FB_CylinderAlternative; } }

        public RelayCommand MoveToWorkCommand { get; }
        public RelayCommand MoveToHomeCommand { get; }
    }
}
