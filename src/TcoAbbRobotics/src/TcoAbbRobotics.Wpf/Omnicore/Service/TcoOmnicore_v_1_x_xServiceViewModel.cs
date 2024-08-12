using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoAbbRobotics
{
    public class TcoOmnicore_v_1_x_xServiceViewModel
        : TcoAbbRoboticsBaseServiceViewModel<TcoOmnicore_v_1_x_x>
    {
        public TcoOmnicore_v_1_x_xServiceViewModel()
            : base() { }
    }

    public class TcoOmnicore_v_1_x_xViewModel : TcoOmnicore_v_1_x_xServiceViewModel { }
}
