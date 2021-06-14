using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Abstractions.Security;

namespace TcOpen.Inxton.Security
{
    ///<summary>Provides management of user access.</summary>       
    public class SecurityManager
    {
        private SecurityManager(IRepository<UserData> repository)
        {
            Service = new AuthenticationService(repository);
            Principal = new VortexIdentity.VortexPrincipal();
            SecurityProvider.Create(Service);

            if (System.Threading.Thread.CurrentPrincipal?.GetType() != typeof(VortexIdentity.VortexPrincipal))
            {
                System.Threading.Thread.CurrentPrincipal = Principal;
                AppDomain.CurrentDomain.SetThreadPrincipal(Principal);
            }
        }

        /// <summary>
        /// Creates authentication service with given user data repository.
        /// </summary>
        /// <param name="repository">User data reposotory <see cref="IRepository{UserData}"/></param>
        /// <returns>Autentication service for this application <see cref="IAuthenticationService"/>Authentication service for this application.</returns>
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
        /// <returns>Autentication service for this application <see cref="IAuthenticationService"/>Authentication service for this application.</returns>
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
        /// > Default repository is designed for handling limited number of users and it should not be used in shared scenarions.
        /// > If you would like to use shared user repository consider implmentation of an appropriate <see cref="IRepository{UserData}"/> implenentation.
        ///</remarks>
        /// <returns>Autentication service for this application <see cref="IAuthenticationService"/>Authentication service for this application.</returns>

        public static IAuthenticationService CreateDefault()
        {
            if (_manager == null)
            {
                _manager = new SecurityManager(new DefaultUserDataRepository<UserData>());
            }

            return _manager.Service;
        }

        public VortexIdentity.VortexPrincipal Principal { get; private set; }

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

        public Role GetRole(Role role)
        {
            if (!this.availableRoles.Contains(role))
            {
                this.availableRoles.Add(role);
            }

            return role;
        }
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
