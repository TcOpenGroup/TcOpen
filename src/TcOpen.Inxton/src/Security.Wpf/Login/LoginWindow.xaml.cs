using System;
using System.Windows;
using System.Windows.Input;


namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(string authorisationRequestMessage = "")
        {
            InitializeComponent();
            this.AuthorisationRequest.Text = authorisationRequestMessage;
        }

        private LoginWindowViewModel _context
        {
            get
            {
                return this.DataContext as LoginWindowViewModel;
            }
        }

        private void pb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                try
                {                    
                    this.loginButton.Command.Execute(pb);
                }
                catch (Exception)
                {
                    //++ Ingore
                }                
            }
        }
    }
}
