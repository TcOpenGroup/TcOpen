using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcoPneumatics
{
   
    public class fbCylinderManualViewModel : RenderableViewModel
    {
        public fbCylinderManualViewModel()
        {

        }

        public fbCylinder Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as fbCylinder; } }
    }
}
