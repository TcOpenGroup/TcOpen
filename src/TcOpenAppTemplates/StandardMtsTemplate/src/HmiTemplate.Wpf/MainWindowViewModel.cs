using MainPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;

namespace HmiTemplate.Wpf
{
    public class MainWindowViewModel
    {
        public MainPlcTwinController MainPlc { get { return App.MainPlc; } }        
    }
}
