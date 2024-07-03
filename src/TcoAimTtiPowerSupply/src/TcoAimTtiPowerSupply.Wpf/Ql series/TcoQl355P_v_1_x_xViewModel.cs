using System;
using System.Linq;
using Vortex.Presentation.Wpf;

namespace TcoAimTtiPowerSupply
{
    public class TcoQl355P_v_1_x_xViewModel : RenderableViewModel
    {
        public TcoQl355P_v_1_x_xViewModel() { }

        public TcoQl355P_v_1_x_x Component { get; private set; }
        public override object Model
        {
            get => Component;
            set { Component = value as TcoQl355P_v_1_x_x; }
        }

        public bool ShowGetCommand { get; set; }
    }

    public class TcoQl355P_v_1_x_xServiceViewModel : TcoQl355P_v_1_x_xViewModel { }
}
