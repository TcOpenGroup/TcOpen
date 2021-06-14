using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Security.Wpf.Internal;

namespace TcOpen.Inxton.Security.Wpf
{
    public class InsufficientRightsBoxViewModel
    {
        public InsufficientRightsBoxViewModel()
        {
            OpenLoginWindowCommand = new RelayCommand(a => OpenLoginWindow());           
        }

        public UserAccessor UserInfo
        {
            get
            {
                return UserAccessor.Instance;
            }
        }

        public RelayCommand OpenLoginWindowCommand { get; private set; }      

        private void OpenLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }       
    }
}
