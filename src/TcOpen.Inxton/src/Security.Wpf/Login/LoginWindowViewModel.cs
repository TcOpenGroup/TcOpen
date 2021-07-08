using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using TcOpen.Inxton.Input;
using Vortex.Presentation;

namespace TcOpen.Inxton.Security.Wpf
{
    internal class LoginWindowViewModel : BindableBase
    {
        public LoginWindowViewModel()
        {
            if(System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                SecurityManager.CreateDefault();
            }

            LoginCommand = new RelayCommand(a => Login(UserName, a));
            LogoutCommand = new RelayCommand(a => Logout());
            CancelCommand = new RelayCommand(a => CloseTrigger = true);
            ChangePasswordCommand = new RelayCommand(a => ChangePassword(a));
            ChangeAuthorizationTokenCommand = new RelayCommand(a => ChangeAuthorizationToken(),
                                                               x => SecurityManager.Manager.Service.ExternalAuthorization != null);
        }

        private void ChangeAuthorizationToken()
        {
            var answer = MessageBox.Show("Would you like to change your authorization token?", "Authorization token change.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                Security.SecurityManager.Manager.Service.ExternalAuthorization.WillChangeToken = true;
                MessageBox.Show("Read your authorization token and press OK", "Authorization token change.", MessageBoxButton.OK, MessageBoxImage.Information);
                Security.SecurityManager.Manager.Service.ExternalAuthorization.WillChangeToken = false;

                if (!string.IsNullOrEmpty(Security.SecurityManager.Manager.Service.ExternalAuthorization.AuthorizationErrorMessage))
                {
                    MessageBox.Show(Security.SecurityManager.Manager.Service.ExternalAuthorization.AuthorizationErrorMessage, "Authorization error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Authorization token changed successfully.", "Authorization", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (userName == value)
                {
                    return;
                }

                SetProperty(ref userName, value);
            }
        }

        PasswordBox password;
        public PasswordBox Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value)
                {
                    return;
                }

                SetProperty(ref password, value);
            }
        }

        string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status == value)
                {
                    return;
                }

                SetProperty(ref status, value);
            }
        }


        public bool CanUserChangePassword
        {
            get
            {
                return SecurityManager.Manager.Principal.Identity.CanUserChangePassword;
            }
        }

        private void ChangePassword(object args)
        {
            try
            {
                var a = args as PwdsChange;
                SecurityManager.Manager.Service.ChangePassword(SecurityManager.Manager.Principal.Identity.Name, a.OldPwd.Password, a.Pb1.Password, a.Pb2.Password);
                CloseTrigger = true;
            }
            catch (Exception ex)
            {

                Status = $"Password change failed: {ex.Message}";
            }

        }

        private void Login(string userName, object pwd)
        {
            try
            {
                Status = string.Empty;
                SecurityManager.Manager.Service.AuthenticateUser(userName, ((PasswordBox)pwd).Password);
                CloseTrigger = true;
            }
            catch (Exception ex)
            {
                Status = "Authorization failed.";
            }

        }

        private void Logout()
        {
            SecurityManager.Manager.Service.DeAuthenticateCurrentUser();
            CloseTrigger = true;
        }

        public RelayCommand LoginCommand { get; private set; }

        public RelayCommand LogoutCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ChangePasswordCommand { get; private set; }

        public RelayCommand ChangeAuthorizationTokenCommand { get; private set; }

        bool closeTrigger;
        public bool CloseTrigger
        {
            get
            {
                if (closeTrigger)
                {
                    closeTrigger = false;
                    return true;
                }

                return closeTrigger;
            }
            set
            {
                if (closeTrigger == value)
                {
                    return;
                }

                SetProperty(ref closeTrigger, value);
            }
        }
    }

    public class HideWindowBehavior : Behavior<Window>
    {
        public bool CloseTrigger
        {
            get { return (bool)GetValue(CloseTriggerProperty); }
            set { SetValue(CloseTriggerProperty, value); }
        }

        public static readonly DependencyProperty CloseTriggerProperty =
            DependencyProperty.Register("CloseTrigger", typeof(bool), typeof(HideWindowBehavior), new PropertyMetadata(false, OnCloseTriggerChanged));

        private static void OnCloseTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as HideWindowBehavior;

            if (behavior != null)
            {
                behavior.OnCloseTriggerChanged();
            }
        }

        private void OnCloseTriggerChanged()
        {
            // when closetrigger is true, close the window
            if (this.CloseTrigger)
            {
                this.AssociatedObject.Hide();
            }
        }
    }
}
