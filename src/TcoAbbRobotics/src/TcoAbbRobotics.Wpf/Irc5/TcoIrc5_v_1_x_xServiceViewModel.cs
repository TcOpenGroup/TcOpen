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
using RelayCommand = TcOpen.Inxton.Input.RelayCommand;


namespace TcoAbbRobotics
{
    public class TcoIrc5_v_1_x_xServiceViewModel : RenderableViewModel
    {

        public TcoIrc5_v_1_x_xServiceViewModel()
        {
           

        }


      
        public TcoIrc5_v_1_x_x Component { get; private set; }

       

        public override object Model { get => this.Component; set { this.Component = value as TcoIrc5_v_1_x_x; } }

        
    }

}
