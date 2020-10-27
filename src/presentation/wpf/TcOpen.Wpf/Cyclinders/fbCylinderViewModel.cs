using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcOpen
{
    public class fbCylinderViewModel : RenderableViewModel
    {
        public fbCylinderViewModel()
        {
            MoveToWorkCommand = new RelayCommand(a => { this.Component._manualMoveToWork.Cyclic = true; this.Component._manualMoveToHome.Cyclic = false; });
            MoveToHomeCommand = new RelayCommand(a => { this.Component._manualMoveToHome.Cyclic = true; this.Component._manualMoveToWork.Cyclic = false; });
        }

        public fbCylinder Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as fbCylinder; } }

        public RelayCommand MoveToWorkCommand { get; }
        public RelayCommand MoveToHomeCommand { get; }
    }
}
