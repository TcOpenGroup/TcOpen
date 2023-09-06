using System;
using System.Collections.Generic;

namespace TcoCore.TcoDiagnosticsAlternative.LoggingToDb
{
    public interface IMongoLogger
    {
        void LogMessage(PlainTcoMessage message);
        bool MessageExistsInDatabase(PlainTcoMessage message);
        List<PlainTcoMessage> ReadMessages();
        void UpdateMessages(ulong identity, DateTime timeStamp, DateTime timeStampAcknowledged, bool pinned);
    }
}