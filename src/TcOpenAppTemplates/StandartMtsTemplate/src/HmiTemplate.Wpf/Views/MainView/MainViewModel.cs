using HmiTemplate.Wpf.Properties;
using HmiTemplate.Wpf.Views.Data.ProcessSettings;
using HmiTemplate.Wpf.Views.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Local.Security.Wpf;
using Vortex.Presentation.Wpf;

namespace HmiTemplate.Wpf.Views.MainView
{
    public class MainViewModel : MenuControlViewModel
    {
        public MainViewModel()
        {
            this.OpenCommand(this.AddCommand(typeof(OperatorView), strings.Operator));
            this.AddCommand(typeof(ProcessSettingsView), strings.Data); 
            this.AddCommand(typeof(UserManagementView), strings.UserManagement);
        }
    }
}
