using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Local.Security.Wpf;

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
            var userDataRepo = new DefaultUserDataRepository<UserData>();
            SecurityManager.Create(userDataRepo);

            authService = SecurityProvider.Get.AuthenticationService;

            var userName = "Admin";
            var password = "AdminPassword";
            var roles = new string[] { "Administrator" };
            userDataRepo.Create(userName, new UserData(userName, password, roles.ToList()));

            userName = "Operator";
            password = "OperatorPassword";
            roles = new string[] { "Operator" };

            userDataRepo.Create(userName, new UserData(userName, password, roles.ToList()));

            authService.ExternalAuthorization = new ComPortTokenProvider("COM3", 9600, 8, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.None);

            authService.DeAuthenticateCurrentUser();

            InitializeComponent();
        }

        private void LoginAdmin(object sender, RoutedEventArgs e)
        {
            authService.AuthenticateUser("Admin", "AdminPassword");
        }

        private void LoginOperator(object sender, RoutedEventArgs e)
        {
            authService.AuthenticateUser("Operator", "OperatorPassword");
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            authService.DeAuthenticateCurrentUser();
            ;
        }

        private void LoginOnDifferentThread_Click(object sender, RoutedEventArgs e) => Task.Run(() => LoginAdmin(sender, e));

        private void LogoutOnDifferentThread_Click(object sender, RoutedEventArgs e) => Task.Run(() => Logout(sender, e));

        private void loginDialogue(object sender, RoutedEventArgs e)
        {
            var d = new LoginWindow();
            d.ShowDialog();
        }
    }
}
