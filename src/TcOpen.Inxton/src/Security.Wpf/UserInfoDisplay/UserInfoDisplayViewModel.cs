using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Security.Wpf.Internal;

namespace TcOpen.Inxton.Security.Wpf.UserInfoDisplay
{
    public class UserInfoDisplayViewModel
    {
        public UserInfoDisplayViewModel()
        {
            OpenLoginWindowCommand = new RelayCommand(a => OpenLoginWindow());
            LogOutCommand = new RelayCommand(a => Logout());
        }

        public UserAccessor UserInfo
        {
            get
            {
                return UserAccessor.Instance;
            }
        }

        public RelayCommand OpenLoginWindowCommand { get; private set; }

        public RelayCommand LogOutCommand { get; private set; }

        public void OpenLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        public void Logout()
        {
            TcOpen.Inxton.Security.SecurityManager.Manager.Service.DeAuthenticateCurrentUser();
        }
    }
}
