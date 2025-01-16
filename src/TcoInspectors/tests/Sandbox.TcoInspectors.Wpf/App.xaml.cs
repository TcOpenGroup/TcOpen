using TcoInspectors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcoInspectorsTests;
using Vortex.Adapters.Connector.Tc3.Adapter;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.TcoCore.Wpf;
using Vortex.Presentation.Wpf;
using TcOpen.Inxton.Local.Security.Wpf;
using System.IO;
using TcOpen.Inxton.Security;

namespace Sandbox.TcoInspectors.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
                       
            

            Plc.Connector.BuildAndStart().ReadWriteCycleDelay = 75;

            #region DialogProxyServiceInitialization
            TcOpen.Inxton.TcoAppDomain.Current.Builder
            #endregion
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get)
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { App.Plc.MAIN._exampleContext }));
            //.SetSecurity(SecurityManager.CreateDefault());


            Directory.EnumerateFiles(@"C:\INXTON\USERS\").ToList().ForEach(File.Delete);
            Directory.EnumerateFiles(@"C:\INXTON\GROUP\").ToList().ForEach(File.Delete);
            var userDataRepo = new DefaultUserDataRepository<UserData>();
            var groups = new DefaultGroupDataRepository<GroupData>();
            var roleGroupManager = new RoleGroupManager(groups);

            roleGroupManager.CreateGroup("OperatorGroup");
            roleGroupManager.AddRoleToGroup("OperatorGroup", "Operator");
            roleGroupManager.AddRoleToGroup("OperatorGroup", "can_terminate_inspection");
            roleGroupManager.AddRoleToGroup("OperatorGroup", "can_override_inspection");

            SecurityManager.Create(userDataRepo, roleGroupManager);
            SecurityManager.Manager.GetOrCreateRole(new Role("Operator", "OperatorGroup"));
           
            var userName = "Operator";
            var password = "OperatorPassword";
            
            userDataRepo.Create(userName, new UserData(userName, string.Empty, password, new string[] { "OperatorGroup" }, "Operator", string.Empty) { CanUserChangePassword = true });

            LazyRenderer.Get.CreateSecureContainer = (permissions) => new PermissionBox { Permissions = permissions, SecurityMode = SecurityModeEnum.Disabled };

      
            SecurityManager.Manager.Service.AuthenticateUser(userName, password);

            //SecurityManager.Manager.Service.DeAuthenticateCurrentUser();
        }

        private static string AMS_ID = Environment.GetEnvironmentVariable("Tc3Target");

        private static volatile object mutex = new object();

        private static TcoInspectorsTestsTwinController _plc;

        /// <summary>
        /// Gets Plc twin for this application.
        /// </summary>
        public static TcoInspectorsTestsTwinController Plc
        {
            get
            {
                if (_plc == null)
                {
                    lock (mutex)
                    {
                        if (_plc == null)
                        {
                            _plc = CreateTwin();
                        }
                    }
                }

                return _plc;
            }
        }

        /// <summary>
        /// Creates twin connector for runtime or design mode.
        /// </summary>
        /// <returns>Plc twin</returns>
        private static TcoInspectorsTestsTwinController CreateTwin()
        {
            if (!IsInDesign)
            {
                return new TcoInspectorsTestsTwinController(Tc3ConnectorAdapter.Create(AMS_ID, 852, true));
            }
            else
            {
                return new TcoInspectorsTestsTwinController(new Vortex.Connector.ConnectorAdapter(typeof(Vortex.Connector.DummyConnectorFactory)), new object[] { string.Empty });
            }
        }

        /// <summary>
        /// Gets true when running in design mode
        /// </summary>
        private static bool IsInDesign
        {
            get
            {
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }
    }
}
