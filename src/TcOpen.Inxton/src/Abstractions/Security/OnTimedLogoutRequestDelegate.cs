namespace TcOpen.Inxton.Security
{
    /// <summary>
    /// Determines whether user can be logged out automatically.
    /// </summary>
    /// <returns>True when the user can be logged out automatically.</returns>
    public delegate bool OnTimedLogoutRequestDelegate();
}
