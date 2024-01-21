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
    public class TcoIrc5_v_1_x_xServiceViewModel : TcoAbbRoboticsBaseServiceViewModel<TcoIrc5_v_1_x_x>
    {

        public TcoIrc5_v_1_x_xServiceViewModel():base()
        {
           

        }
    }
    public class TcoIrc5_v_1_x_xViewModel : TcoIrc5_v_1_x_xServiceViewModel
    { }

}
