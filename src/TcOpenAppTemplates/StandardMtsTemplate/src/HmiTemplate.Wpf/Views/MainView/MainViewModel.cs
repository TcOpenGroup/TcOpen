using HmiTemplate.Wpf.Data;
using HmiTemplate.Wpf.Properties;
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
            this.Title = "TECHNOLOGY";
            this.OpenCommand(this.AddCommand(typeof(OperatorView), strings.Operator));
            this.AddCommand(typeof(DataView), strings.Data); 
            this.AddCommand(typeof(UserManagementView), strings.UserManagement);           
            this.OpenLoginWindowCommand = new TcOpen.Inxton.Input.RelayCommand(a => OpenLoginWindow());
            this.LogOutWindowCommand = new TcOpen.Inxton.Input.RelayCommand(a => TcOpen.Inxton.TcoAppDomain.Current.AuthenticationService.DeAuthenticateCurrentUser() );
        }

        public TcOpen.Inxton.Input.RelayCommand OpenLoginWindowCommand { get; private set; }
        public TcOpen.Inxton.Input.RelayCommand LogOutWindowCommand { get; private set; }

        public void OpenLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
    }
}
