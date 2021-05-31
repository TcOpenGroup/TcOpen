namespace TcOpen.Inxton.Abstractions.Logging
{
    /// <summary>
    /// Provides abstraction for arbitrary logger implementation.
    /// </summary>
    public interface ITcoLogger
    {
        /// <summary>
        /// Logs debug message. This should ne used only for debugging purpose and should not be active in production environment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringTemplate">Message template</param>
        /// <param name="payload">Object for structured logging</param>        
        void Debug<T>(string stringTemplate, T payload = default(T));

        /// <summary>
        /// Logs verbose message level. Use for detailed information collection. This level should be activated in particular situations 
        /// and should not be active as a rule in production environment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringTemplate">Message template</param>
        /// <param name="payload">Object for structured logging</param>
        void Verbose<T>(string stringTemplate, T payload = default(T));

        /// <summary>
        /// Logs information message level. User for logging information that is of interest, such as user actions 
        /// (log-in / log-out, value change, manual command execution, etc.)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringTemplate">Message template</param>
        /// <param name="payload">Object for structured logging</param>
        void Information<T>(string stringTemplate, T payload = default(T));

        /// <summary>
        /// Logs warning message level. Use to preserve information about possible problem or to emphasize an information that
        /// the user should be aware of.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringTemplate">Message template</param>
        /// <param name="payload">Object for structured logging</param>
        void Warning<T>(string stringTemplate, T payload = default(T));

        /// <summary>
        /// Logs error message level. Use to preserve information an error (e.g. exception)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringTemplate">Message template</param>
        /// <param name="payload">Object for structured logging</param>
        void Error<T>(string stringTemplate, T payload = default(T));

        /// <summary>
        /// Logs fatal message level. Use to preserve information a fatal error (hardware problem, storage space, essential network connectivity, etc.)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringTemplate">Message template</param>
        /// <param name="payload">Object for structured logging</param>
        void Fatal<T>(string stringTemplate, T payload = default(T));
    }    
}
