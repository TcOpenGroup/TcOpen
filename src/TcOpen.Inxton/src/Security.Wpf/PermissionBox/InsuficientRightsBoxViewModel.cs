using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TcOpen.Inxton.Input;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class InsufficientRightsBoxViewModel : INotifyPropertyChanged
    {
        public InsufficientRightsBoxViewModel()
        {
            OpenLoginWindowCommand = new RelayCommand(a => OpenLoginWindow(), b => true);
        }

        FrameworkElement protectedContent;
        public FrameworkElement ProtectedContent
        {
            get => protectedContent;
            internal set
            {
                if (protectedContent == value)
                {
                    return;
                }

                protectedContent = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(ProtectedContent))
                );
            }
        }

        public UserAccessor UserInfo
        {
            get { return UserAccessor.Instance; }
        }

        public RelayCommand OpenLoginWindowCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OpenLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
    }
}
