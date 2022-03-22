using HmiTemplate.Wpf.Properties;
using HmiTemplate.Wpf.Views.Data.ProcessSettings;
using HmiTemplate.Wpf.Views.Data.TechnologicalSettings;
using HmiTemplate.Wpf.Views.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Local.Security.Wpf;
using Vortex.Presentation.Wpf;

namespace HmiTemplate.Wpf.Data
{
    public class DataViewModel : MenuControlViewModel
    {
        public DataViewModel()
        {           
            this.AddCommand(typeof(ProcessSettingsView), strings.ProcessData);
            this.AddCommand(typeof(TechnologicalSettingsView), strings.TechData);            
        }
    }
}
