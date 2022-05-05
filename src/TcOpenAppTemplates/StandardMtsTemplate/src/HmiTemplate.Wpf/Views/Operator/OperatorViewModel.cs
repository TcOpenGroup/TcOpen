using MainPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HmiTemplate.Wpf.Views.Operator
{
    public class OperatorViewModel
    {
        public OperatorViewModel()
        {
            MainPlc.MAIN._technology._automatAllTask.ExecuteDialog = () => 
            {
                return MessageBox.Show(HmiTemplate.Wpf.Properties.strings.AutomatAllWarning, "Automat", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;                    
            };

            MainPlc.MAIN._technology._groundAllTask.ExecuteDialog = () =>
            {
                return MessageBox.Show(HmiTemplate.Wpf.Properties.strings.GroundAllWarning, "Automat", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
            };

            MainPlc.MAIN._technology._automatAllTask.Roles = Roles.technology_automat_all;
            MainPlc.MAIN._technology._groundAllTask.Roles = Roles.technology_ground_all;
        }

        public MainPlcTwinController MainPlc { get { return App.MainPlc; } }       
    }
}
