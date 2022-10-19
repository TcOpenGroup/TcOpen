using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcoCognexVision
{
   
    public class TcoDataman_v_5_x_xViewModel : RenderableViewModel
    {
        public TcoDataman_v_5_x_xViewModel()
        {
           
        }

        public TcoDataman_v_5_x_x Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as TcoDataman_v_5_x_x; } }
    }

    public class TcoDataman_v_5_x_xServiceViewModel : TcoDataman_v_5_x_xViewModel
    {

    }
}
