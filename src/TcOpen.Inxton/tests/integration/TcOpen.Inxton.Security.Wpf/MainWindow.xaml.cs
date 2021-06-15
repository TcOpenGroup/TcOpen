using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcOpen.Inxton.Abstractions.Security;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.Security.Wpf;

namespace integration.Security.Wpf.netcore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IAuthenticationService authService;
        public MainWindow()
        {
            Directory.EnumerateFiles(@"C:\INXTON\USERS\").ToList().ForEach(File.Delete);

            SecurityManager.Create(new DefaultUserDataRepository<UserData>());

            authService = SecurityProvider.Get.AuthenticationService;
            var userName = "Admin";
            var password = "AdminPassword";
            var roles = new string[] { "Administrator" };
            authService.UserRepository.Create(userName, new UserData(userName, password, roles.ToList())
);

            userName = "Operator";
            password = "OperatorPassword";
            roles = new string[] { "Operator" };

            authService.UserRepository.Create(userName, new UserData(userName, password, roles.ToList())
);

            authService.DeAuthenticateCurrentUser();

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            authService.AuthenticateUser("Admin", "AdminPassword");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            authService.AuthenticateUser("Operator", "OperatorPassword");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            authService.DeAuthenticateCurrentUser();
            ;
        }

        private void LoginOnDifferentThread_Click(object sender, RoutedEventArgs e) => Task.Run(() => Button_Click(sender, e));

        private void LogoutOnDifferentThread_Click(object sender, RoutedEventArgs e) => Task.Run(() => Button_Click_2(sender, e));

        private void loginDialogue(object sender, RoutedEventArgs e)
        {
            var d = new LoginWindow();
            d.ShowDialog();
        }
    }
}
