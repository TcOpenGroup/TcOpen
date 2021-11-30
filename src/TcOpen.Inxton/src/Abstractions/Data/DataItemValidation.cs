namespace TcOpen.Inxton.Data
{
    /// <summary>
    /// Provides basic information about the validation state.
    /// </summary>
    public class DataItemValidation
    {
        /// <summary>
        /// Creates new instance of <see cref="DataItemValidation"/>
        /// </summary>
        /// <param name="error">Error message providing details about failed validation.</param>
        /// <param name="failed">Indicates that the valiation failed.</param>
        public DataItemValidation(string error, bool failed)
        {
            Error = error;
            Failed = failed;
        }

        /// <summary>
        /// Gets failed when the validation failed.
        /// </summary>
        public bool Failed { get; }
        
        /// <summary>
        /// Get validation error description.
        /// </summary>
        public string Error { get; }
    }
}
