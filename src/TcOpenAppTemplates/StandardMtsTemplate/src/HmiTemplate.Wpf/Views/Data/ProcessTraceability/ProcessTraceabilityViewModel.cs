using MainPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoData;

namespace HmiTemplate.Wpf.Views.Data.ProcessTraceability
{
    public class ProcessTraceabilityViewModel
    {
        public TcoDataExchangeDisplayViewModel ProcessData
        {
            get
            {
                return new TcoDataExchangeDisplayViewModel() { Model = App.MainPlc.MAIN._technology._processTraceability };
            }           
        }
    }
}
