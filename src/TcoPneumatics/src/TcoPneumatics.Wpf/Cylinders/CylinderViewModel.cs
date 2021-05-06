using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcoPneumatics
{
   
    public class CylinderManualViewModel : RenderableViewModel
    {
        public CylinderManualViewModel()
        {

        }

        public Cylinder Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as Cylinder; } }
    }
}
