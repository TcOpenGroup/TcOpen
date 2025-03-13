using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Helper
{
    public class ExtractMessageFromTemplateText
    {
        public static List<MongoDbLogItem> ExtractMessageWithoutBraces(List<MongoDbLogItem> messages)
        {
            foreach (var message in messages)
            {
                message.MessageTemplate.Text = Regex.Replace(message?.MessageTemplate?.Text, @"\{.*?\}", "").Trim();
            }

            return messages; 
        }

        public static string ExtractMessageWithoutBraces(string messageTemplateText)
        {
            return System.Text.RegularExpressions.Regex.Replace(messageTemplateText, @"\{.*?\}", "").Trim();
        }
    }
}
