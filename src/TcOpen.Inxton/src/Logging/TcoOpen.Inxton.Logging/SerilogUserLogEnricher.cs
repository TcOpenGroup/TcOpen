namespace TcOpen.Inxton.Logging
{
    using System;
    using Serilog.Context;
    using TcOpen.Inxton.Security;

    /// <summary>
    /// Provides a way to add current user into logs
    /// To use it, craete a single instance of SerilogUserLogEnricher, and call methods `UserLoggedIn` and `UserLoggedOut` when users change.
    ///
    /// To use it enrtich your serilog with context
    /// <code>
    /// SerilogAdapter(new Serilog.LoggerConfiguration()
    ///                         .Enrich.FromLogContext()
    ///                                    ...
    ///    </code>
    ///
    /// To display the current user, use the {UserData} in your template.
    /// <code>
    ///     string outputTemplate = "[{UserData}] {Message:lj}{NewLine}{Exception}",
    /// <code>
    ///
    ///
    /// </summary>
    ///
    public class SerilogUserLogEnricher
    {
        public static readonly string UserLoggingPropertyName = "UserData";

        private IDisposable UserProperty { get; set; }

        public void UserLoggedIn(IUser user)
        {
            UserProperty?.Dispose();
            UserProperty = LogContext.PushProperty(UserLoggingPropertyName, user.UserName, true);
        }

        public void UserLoggedOut()
        {
            UserProperty?.Dispose();
            UserProperty = LogContext.PushProperty(UserLoggingPropertyName, "No-user", true);
        }
    }
}
