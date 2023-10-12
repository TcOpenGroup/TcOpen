using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TcoCore;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping
{
    public static class MessageCategoryMapper
    {
        private static readonly Dictionary<string, eMessageCategory> CategoryMapping = new Dictionary<string, eMessageCategory>
    {
        { "Verbose", eMessageCategory.Trace },
        { "Debug", eMessageCategory.Debug },
        { "Information", eMessageCategory.Info },
        { "TimedOut", eMessageCategory.TimedOut },
        { "Notification", eMessageCategory.Notification },
        { "Warning", eMessageCategory.Warning },
        { "Error", eMessageCategory.Error },
        { "ProgrammingError", eMessageCategory.ProgrammingError },
        { "Critical", eMessageCategory.Critical },
        { "Fatal", eMessageCategory.Fatal },
        { "Catastrophic", eMessageCategory.Catastrophic }
    };

        private static readonly Dictionary<eMessageCategory, string> IconMapping = new Dictionary<eMessageCategory, string>
    {
        { eMessageCategory.Trace, "&#9900;" },
        { eMessageCategory.Debug, "&#9900;" },
        { eMessageCategory.Info, "&#10082;" },
        { eMessageCategory.TimedOut, "&#9775;" },
        { eMessageCategory.Notification, "&#9835;" },
        { eMessageCategory.Warning, "&#9888;" },
        { eMessageCategory.Error, "&#9762;" },
        { eMessageCategory.ProgrammingError, "&#9762;" },
        { eMessageCategory.Critical, "&#10006;" },
        { eMessageCategory.Fatal, "&#9760;" },
        { eMessageCategory.Catastrophic, "&#9760;" }
    };

        private static readonly Dictionary<eMessageCategory, string> ColorMapping = new Dictionary<eMessageCategory, string>
    {
        { eMessageCategory.Trace, "bg-trace" },
        { eMessageCategory.Debug, "bg-debug" },
        { eMessageCategory.Info, "bg-info" },
        { eMessageCategory.TimedOut, "bg-timedout" },
        { eMessageCategory.Notification, "bg-notification" },
        { eMessageCategory.Warning, "bg-warning" },
        { eMessageCategory.Error, "bg-error" },
        { eMessageCategory.ProgrammingError, "bg-programmingerror" },
        { eMessageCategory.Critical, "bg-critical" },
        { eMessageCategory.Fatal, "bg-fatal" },
        { eMessageCategory.Catastrophic, "bg-catastrophic" }
    };

        public static string GetBackgroundColorForCategory(string level)
        {
            if (CategoryMapping.TryGetValue(level, out var category) && ColorMapping.TryGetValue(category, out var color))
            {
                return color;
            }
            return "white"; // Default color if level is not found
        }

        public static string GetIconForLevel(string levelString)
        {
            if (CategoryMapping.TryGetValue(levelString, out var category) && IconMapping.TryGetValue(category, out var icon))
            {
                return icon;
            }
            return string.Empty; // Default icon if level is not found
        }

        public static eMessageCategory MapLevelToMessageCategory(string levelString)
        {
            if (CategoryMapping.TryGetValue(levelString, out var category))
            {
                return category;
            }
            return eMessageCategory.All; // Default category if level is not found
        }

        public static string MapMessageCategoryToLevel(eMessageCategory category)
        {
            return CategoryMapping.FirstOrDefault(x => x.Value == category).Key;
        }

        public static List<string> GetAllLevelsGreaterThanOrEqualTo(eMessageCategory category)
        {
            int categoryValue = (int)category;
            return CategoryMapping
                .Where(pair => (int)pair.Value >= categoryValue)
                .Select(pair => pair.Key)
                .ToList();
        }

    }
}
