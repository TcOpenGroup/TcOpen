using System;
using System.Collections.Generic;

namespace TcoCore.TcoDiagnosticsAlternative.LoggingToDb
{
    public interface IMongoLogger
    {
        void LogMessage(PlainTcoMessage message);
        bool MessageExistsInDatabase(ulong identity);
        List<PlainTcoMessage> ReadMessages();
        void SaveNewMessages(ulong identity, DateTime timeStamp, DateTime timeStampAcknowledged, bool pinned);
    }
}