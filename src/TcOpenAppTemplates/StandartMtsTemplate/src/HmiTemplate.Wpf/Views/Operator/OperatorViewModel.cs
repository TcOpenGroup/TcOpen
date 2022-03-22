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

        public CUBaseSpotViewModel Cu { get; } = new CUBaseSpotViewModel() { Model = App.MainPlc.MAIN._technology._cu00x };
    }
}
