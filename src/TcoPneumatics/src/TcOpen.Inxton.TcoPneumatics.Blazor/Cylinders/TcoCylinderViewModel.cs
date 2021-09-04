using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vortex.Presentation;

namespace TcoPneumatics

{
    public class TcoCylinderViewModel : RenderableViewModelBase
    {
        public TcoCylinderViewModel()
        {

        }

        public TcoCylinder Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as TcoCylinder; } }
    }

    public class TcoCylinderServiceViewModel : TcoCylinderViewModel
    {

    }
}

