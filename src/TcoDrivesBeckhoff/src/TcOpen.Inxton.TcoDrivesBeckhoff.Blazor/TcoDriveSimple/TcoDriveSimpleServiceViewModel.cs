using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation;

namespace TcoDrivesBeckhoff
{
    public class TcoDriveSimpleServiceViewModel : RenderableViewModelBase
    {
        public TcoDriveSimpleServiceViewModel()
        {

        }

        public TcoDriveSimple Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as TcoDriveSimple; } }
    }
}
