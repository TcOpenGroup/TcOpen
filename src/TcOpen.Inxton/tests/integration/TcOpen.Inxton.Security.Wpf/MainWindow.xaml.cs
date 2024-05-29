using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Local.Security.Wpf;
using TcOpen.Inxton.Local.Security.Readers;

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
            Directory.EnumerateFiles(@"C:\INXTON\GROUP\").ToList().ForEach(File.Delete);
            var userDataRepo = new DefaultUserDataRepository<UserData>();
            var groups = new DefaultGroupDataRepository<GroupData>();
            var roleGroupManager = new RoleGroupManager(groups);
            
            roleGroupManager.CreateGroup("OperatorGroup");
            roleGroupManager.AddRoleToGroup("OperatorGroup", "Operator");

            SecurityManager.Create(userDataRepo, roleGroupManager);
            SecurityManager.Manager.GetOrCreateRole(new Role("Operator", "OperatorGroup"));
            authService = SecurityProvider.Get.AuthenticationService;



            var userName = "Admin";
            var password = "AdminPassword";
            //newUser = new UserData("admin", string.Empty, "admin", new string[] { "AdminGroup" }, "Administrator", string.Empty);
            userDataRepo.Create(userName, new UserData(userName, string.Empty, password, new string[] { "AdminGroup" }, "Administrator", string.Empty) { CanUserChangePassword = true });

            userName = "Operator";
            password = "OperatorPassword";

            //userDataRepo.Create(userName, new UserData(userName, password, roles.ToList()) { CanUserChangePassword = true });
            userDataRepo.Create(userName, new UserData(userName, string.Empty, password, new string[] { "OperatorGroup" }, "Operator", string.Empty) { CanUserChangePassword = true });

            //  authService.ExternalAuthorization = ExternalTokenAuthorization.CreateComReader("COM3", deauthenticateWhenSame: true);

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
