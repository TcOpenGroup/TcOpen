using MainPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmiTemplate.Wpf.Views.Data.ProcessSettings
{
    public class ProcessSettingsViewModel
    {
        public ProcessDataManager ProcessData
        {
            get
            {
                return App.MainPlc.MAIN._technology._processSettings;
            }           
        }
    }
}
