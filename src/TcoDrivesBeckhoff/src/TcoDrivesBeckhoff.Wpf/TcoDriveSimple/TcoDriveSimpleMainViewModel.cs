using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcoDrivesBeckhoff
{
   
    public class TcoDriveSimpleMainViewModel : RenderableViewModel
    {
        public TcoDriveSimpleMainViewModel()
        {

        }

        public TcoDriveSimple Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as TcoDriveSimple; } }
    }
}
