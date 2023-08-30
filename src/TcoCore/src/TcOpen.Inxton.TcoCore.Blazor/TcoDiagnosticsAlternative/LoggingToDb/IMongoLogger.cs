namespace TcoCore.TcoDiagnosticsAlternative.LoggingToDb
{
    public interface IMongoLogger
    {
        void LogMessage(PlainTcoMessage message);
    }
}