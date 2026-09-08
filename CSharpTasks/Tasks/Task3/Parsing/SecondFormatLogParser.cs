using System.Globalization;
using CSharpTasks.Tasks.Task3.Models;

namespace CSharpTasks.Tasks.Task3.Parsing;

public sealed class SecondFormatLogParser : ILogParser
{
    public bool TryParse(string line, out LogEntry? entry)
    {
        entry = null;

        if (string.IsNullOrWhiteSpace(line))
        {
            return false;
        }

        string[] parts = line.Split('|');

        if (parts.Length != 5)
        {
            return false;
        }

        string dateAndTime = parts[0].Trim();
        string levelValue = parts[1].Trim();
        string callingMethod = parts[3].Trim();
        string message = parts[4].Trim();

        int separatorIndex = dateAndTime.IndexOf(' ');

        if (separatorIndex <= 0)
        {
            return false;
        }

        string dateValue = dateAndTime[..separatorIndex];
        string timeValue = dateAndTime[(separatorIndex + 1)..];

        if (!DateOnly.TryParseExact(
                dateValue,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateOnly date))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(timeValue) ||
            string.IsNullOrWhiteSpace(callingMethod) ||
            string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        if (!LogLevelNormalizer.TryNormalize(
                levelValue,
                out string level))
        {
            return false;
        }

        entry = new LogEntry
        {
            Date = date,
            Time = timeValue,
            Level = level,
            CallingMethod = callingMethod,
            Message = message
        };

        return true;
    }
}