using CSharpTasks.Tasks.Task3.Formatting;
using CSharpTasks.Tasks.Task3.Models;

namespace CSharpTasks.Tests;

public class LogEntryFormatterTests
{
    [Fact]
    public void Format_ValidEntry_ReturnsTabSeparatedString()
    {
        LogEntry entry = new()
        {
            Date = new DateOnly(2025, 3, 10),
            Time = "15:14:49.523",
            Level = "INFO",
            CallingMethod = "DEFAULT",
            Message = "Test message"
        };

        string result =
            LogEntryFormatter.Format(entry);

        Assert.Equal(
            "10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tTest message",
            result);
    }

    [Fact]
    public void Format_UsesExactlyFiveFields()
    {
        LogEntry entry = new()
        {
            Date = new DateOnly(2025, 3, 10),
            Time = "15:14:49.523",
            Level = "INFO",
            CallingMethod = "DEFAULT",
            Message = "Message"
        };

        string result =
            LogEntryFormatter.Format(entry);

        string[] fields = result.Split('\t');

        Assert.Equal(5, fields.Length);
    }

    [Fact]
    public void Format_NullEntry_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => LogEntryFormatter.Format(null!));
    }
}