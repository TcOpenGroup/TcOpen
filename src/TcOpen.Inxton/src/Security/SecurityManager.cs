using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security
{
    ///<summary>
    ///     Provides management of user access. 
    ///     To setup security manager you need an IRepository where you will store the user data 
    ///     
    /// <code>
    ///     SecurityManager.Create(new DefaultUserDataRepository<UserData>()); //you can use RavenDB,Mongo,Json repository for user data persistence.
    ///     
    ///     //grab the service
    ///     IAuthenticationService authService = SecurityProvider.Get.AuthenticationService;
    ///     
    ///     //create a user
    ///     
    ///     var userName = "Admin";
    ///     var password = "AdminPassword";
    ///     var roles = new string[] { "Administrator" };
    ///     authService.UserRepository.Create(userName, new UserData(userName, password, roles.ToList()));  
    ///     
    ///     //login created user
    ///     authService.AuthenticateUser("Admin", "AdminPassword");
    /// </code>
    ///  
    /// To limit execution of methods for privileged user use <see cref="   "/>
    ///</summary>       
    public class SecurityManager : ISecurityManager, ISecurityManagerUserInfo
    {

        /// <summary>
        /// Gets the name of current user.
        /// </summary>
        public string UserName { get { return this.Principal.Identity.Name; } }

        private SecurityManager(IRepository<UserData> repository)
        {
            UserRepository = repository;
            Service = new AuthenticationService(repository);
            Principal = new AppIdentity.AppPrincipal();
            SecurityProvider.Create(Service);

            if (System.Threading.Thread.CurrentPrincipal?.GetType() != typeof(AppIdentity.AppPrincipal))
            {
                System.Threading.Thread.CurrentPrincipal = Principal;
                AppDomain.CurrentDomain.SetThreadPrincipal(Principal);
            }
        }

        /// <summary>
        /// Creates authentication service with given user data repository.
        /// </summary>
        /// <param name="repository">User data repository <see cref="IRepository{UserData}"/></param>
        /// <returns>Authentication service for this application <see cref="IAuthenticationService"/>Authentication service for this application.</returns>
        public static IAuthenticationService Create(IRepository<UserData> repository)
        {
            if (_manager == null)
            {
                _manager = new SecurityManager(repository);
            }
            
            return _manager.Service;
        }

        /// <summary>
        /// Creates authentication service with default user data repository <see cref="DefaultUserDataRepository{UserData}"/>
        /// </summary>
        /// <param name="usersFolder">User file storage folder.</param>
        /// <returns>Authentication service for this application <see cref="IAuthenticationService"/>Authentication service for this application.</returns>
        public static IAuthenticationService CreateDefault(string usersFolder = @"C:\INXTON\USERS\")
        {
            if (_manager == null)
            {
                _manager = new SecurityManager(new DefaultUserDataRepository<UserData>(usersFolder));
            }

            return _manager.Service;
        }

        /// <summary>
        /// *Creates authentication* service with default user data repository <see cref="DefaultUserDataRepository{UserData}"/>        
        /// </summary>        
        ///<remarks>   
        /// > [!TIP]
        /// > You can create your own user repository using <see cref="IRepository{UserData}"/>        
        /// > [!IMPORTANT]
        /// > Default repository is designed for handling limited number of users and it should not be used in shared scenarios.
        /// > If you would like to use shared user repository consider implementation of an appropriate <see cref="IRepository{UserData}"/> implementation.
        ///</remarks>
        /// <returns>Authentication service for this application <see cref="IAuthenticationService"/>Authentication service for this application.</returns>

        public static IAuthenticationService CreateDefault()
        {
            if (_manager == null)
            {
                _manager = new SecurityManager(new DefaultUserDataRepository<UserData>());
            }

            return _manager.Service;
        }

        public AppIdentity.AppPrincipal Principal { get; private set; }

        public IAuthenticationService Service { get; private set; }

        private static SecurityManager _manager;
        public static SecurityManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    throw new SecurityManagerNotInitializedException("Security manager was not created.");
                }

                return _manager;
            }
        }

        List<Role> availableRoles = new List<Role>() { new Role("Administrator", "AdminGroup") };

        public IEnumerable<Role> AvailableRoles
        {
            get
            {
                return availableRoles;
            }
        }

        public IEnumerable<string> AvailableGroups() => availableRoles.Select(o => o.DefaultGroup).Distinct();

        /// <summary>
        /// Get the existing role or add the rule if not present in the system.
        /// </summary>
        /// <param name="role">Role to create or retrieve.</param>
        /// <returns>Requested role</returns>
        public Role GetOrCreateRole(Role role)
        {
            if (!this.availableRoles.Contains(role))
            {
                this.availableRoles.Add(role);
            }

            return role;
        }

        public IRepository<UserData> UserRepository { get; }

        /// <summary>
        /// Gets weather the security manager is initialized.
        /// </summary>
        public static bool IsInitialized { get { return _manager != null; } }
    }

    public class SecurityManagerNotInitializedException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="SecurityManagerNotInitializedException" /> class.</summary>
        public SecurityManagerNotInitializedException()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="SecurityManagerNotInitializedException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        public SecurityManagerNotInitializedException(string message) : base(message)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="SecurityManagerNotInitializedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified. </param>
        public SecurityManagerNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="SecurityManagerNotInitializedException" /> class with serialized data.</summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />. </exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="System.Exception.HResult" /> is zero (0). </exception>
        [SecuritySafeCritical]
        protected SecurityManagerNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
