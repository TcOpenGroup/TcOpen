using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation;

namespace TcoPneumatics
{
   
    public class TcoCylinderViewModel2 : RenderableViewModelBase
    {
        public TcoCylinderViewModel2()
        {
           
        }

        public TcoCylinder Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as TcoCylinder; } }
    }

    
}
