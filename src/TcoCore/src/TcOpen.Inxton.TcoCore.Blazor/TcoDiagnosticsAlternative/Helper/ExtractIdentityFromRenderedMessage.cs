using System;
using System.Text.RegularExpressions;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Helper
{
    public static class IdentityExtractor
    {
        public static ulong? ExtractIdentityFromRenderedMessage(string renderedMessage)
        {
            var regex = new Regex(@"Identity: (\d+)", RegexOptions.Compiled);
            var match = regex.Match(renderedMessage);
            if (match.Success)
            {
                var identityString = match.Groups[1].Value;
                if (ulong.TryParse(identityString, out ulong identity))
                {
                    return identity;
                }
                else
                {
                    Console.WriteLine($"Failed to parse identity: {identityString}");  // Debug log
                    return null;
                }
            }
            return null;
        }
    }
}
