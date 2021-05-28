namespace TcOpen.Inxton
{
    using TcOpen.Inxton.Abstractions.Logging;
   
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
    }
}
 