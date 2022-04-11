using MainPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmiTemplate.Wpf.Views.Data.TechnologicalSettings
{
    public class TechnologicalSettingsViewModel
    {
        public TechnologicalDataManager TechnologicalData
        {
            get
            {
                return App.MainPlc.MAIN._technology._technologySettings;
            }           
        }
    }
}
