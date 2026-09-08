using System.Globalization;
using CSharpTasks.Tasks.Task3.Models;

namespace CSharpTasks.Tasks.Task3.Formatting;

public static class LogEntryFormatter
{
    public static string Format(LogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        return string.Join(
            '\t',
            entry.Date.ToString(
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture),
            entry.Time,
            entry.Level,
            entry.CallingMethod,
            entry.Message);
    }
}