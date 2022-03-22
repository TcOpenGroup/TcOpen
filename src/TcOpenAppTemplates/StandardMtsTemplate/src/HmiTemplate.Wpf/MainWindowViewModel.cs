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

        public IEnumerable<object> Sequencers 
        { 
            get 
            {
                List<object> retval = new List<object>();
                retval.AddRange(MainPlc.MAIN._technology._cu00x.GetChildren<TcoTaskedSequencer>());
                retval.AddRange(MainPlc.MAIN._technology._cu00x.GetChildren<TcoTaskedService>());

                return retval;
            } 
        }

    }
}
