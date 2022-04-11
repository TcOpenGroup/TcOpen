using MainPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmiTemplate.Wpf.Views.Operator
{
    public class OperatorViewModel
    {
        public OperatorViewModel()
        {
            
        }

        public MainPlcTwinController MainPlc { get { return App.MainPlc; } }       
    }
}
