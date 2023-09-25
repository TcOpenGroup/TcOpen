using System;
using System.Collections.Generic;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcoCore.TcoDiagnosticsAlternative.LoggingToDb
{
    public interface IMongoLogger
    {
        void LogMessage(PlainTcoMessage message);
        bool MessageExistsInDatabase(PlainTcoMessage message);
        List<PlainTcoMessageExtended> ReadMessages();
        PlainTcoMessage GetSimilarMessage(PlainTcoMessage message);
        void UpdateMessages(ulong identity, DateTime timeStamp, DateTime timeStampAcknowledged, bool pinned);
    }
}