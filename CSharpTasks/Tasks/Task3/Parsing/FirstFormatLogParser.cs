using System.Globalization;
using System.Text.RegularExpressions;
using CSharpTasks.Tasks.Task3.Models;

namespace CSharpTasks.Tasks.Task3.Parsing;

public sealed class FirstFormatLogParser : ILogParser
{
    private static readonly Regex LogRegex = new(
        @"^(?<date>\d{2}\.\d{2}\.\d{4})\s+" +
        @"(?<time>\d{2}:\d{2}:\d{2}(?:\.\d+)?)\s+" +
        @"(?<level>[A-Z]+)\s+" +
        @"(?<message>.+)$",
        RegexOptions.Compiled);

    public bool TryParse(string line, out LogEntry? entry)
    {
        entry = null;

        if (string.IsNullOrWhiteSpace(line))
        {
            return false;
        }

        Match match = LogRegex.Match(line);

        if (!match.Success)
        {
            return false;
        }

        if (!DateOnly.TryParseExact(
                match.Groups["date"].Value,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateOnly date))
        {
            return false;
        }

        if (!LogLevelNormalizer.TryNormalize(
                match.Groups["level"].Value,
                out string level))
        {
            return false;
        }

        entry = new LogEntry
        {
            Date = date,
            Time = match.Groups["time"].Value,
            Level = level,
            CallingMethod = "DEFAULT",
            Message = match.Groups["message"].Value
        };

        return true;
    }
}