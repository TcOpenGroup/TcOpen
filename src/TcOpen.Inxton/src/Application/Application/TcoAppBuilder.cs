namespace TcOpen.Inxton
{
    using TcOpen.Inxton.Abstractions.Data;
    using TcOpen.Inxton.Abstractions.Logging;
    using TcOpen.Inxton.Abstractions.Security;
    using TcOpen.Inxton.App.Logging;
    using TcOpen.Inxton.Threading;
    using Vortex.Connector;

    /// <summary>
    /// TcOpen application configuration builder.
    /// </summary>
    public class TcoAppBuilder
    {
        /// <summary>
        /// Creates new instance of <see cref="TcoApplication"/>
        /// </summary>
        internal TcoAppBuilder(TcoAppDomain domain)
        {
            Domain = domain;
        }
     
        private TcoAppDomain Domain { get; }

        /// <summary>
        /// Sets the logger for this application.
        /// </summary>
        /// <param name="logger">Instance of a logger.</param>
        /// <returns>Application builder.</returns>
        public TcoAppBuilder SetUpLogger(ITcoLogger logger)
        {
            Domain.Logger = logger;
            return this;
        }

        /// <summary>
        /// Sets the dispatcher for this application.
        /// </summary>
        /// <param name="logger">Dispatcher implementation</param>
        /// <returns>Application builder.</returns>
        public TcoAppBuilder SetDispatcher(IDispatcher dispatcher)
        {
            Dispatcher.SetDispatcher(dispatcher);
            return this;
        }

        /// <summary>
        /// Sets the authentication service for the application.
        /// </summary>
        /// <param name="authenticationService">Authentication service</param>
        /// <returns>AppBuilder</returns>
        public TcoAppBuilder SetSecurity(IAuthenticationService authenticationService)
        {
            SecurityProvider.Create(authenticationService);  
            return this;
        }

        /// <summary>
        /// Sets the logging for the 'Edit' -> 'Online' value change.
        /// </summary>
        /// <param name="obj">Root twin object</param>
        /// <returns>AppBuilder</returns>
        public TcoAppBuilder SetEditValueChangeLogging(IVortexObject obj)
        {
            foreach (var valtag in obj.GetValueTags())
            {
                valtag.EditValueChange = LoggingHelpers.EditValueChange;
            }
            
            return this;
        }

       
    }
}
 