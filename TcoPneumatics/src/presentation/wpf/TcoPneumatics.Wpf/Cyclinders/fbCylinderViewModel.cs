using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcoPneumatics
{
    public class fbCylinderViewModel : RenderableViewModel
    {
        public fbCylinderViewModel()
        {
            
        }

        public fbCylinder Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as fbCylinder; } }        
    }
}
