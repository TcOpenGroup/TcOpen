using System;
using System.Collections.Generic;

namespace TcoCore.TcoDiagnosticsAlternative.LoggingToDb
{
    public interface IMongoLogger
    {
        void LogMessage(PlainTcoMessage message);
        bool MessageExistsInDatabase(ulong identity, DateTime timeStamp);
        List<PlainTcoMessage> ReadMessages();
        void UpdateMessage(ulong identity, DateTime timeStamp, DateTime timeStampAcknowledged);
    }
}