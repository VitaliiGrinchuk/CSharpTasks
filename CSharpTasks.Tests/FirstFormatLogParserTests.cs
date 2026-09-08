using CSharpTasks.Tasks.Task3.Parsing;

namespace CSharpTasks.Tests;

public class FirstFormatLogParserTests
{
    private readonly FirstFormatLogParser _parser = new();

    [Fact]
    public void TryParse_ValidLine_ReturnsTrue()
    {
        string line =
            "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";

        bool result = _parser.TryParse(line, out var entry);

        Assert.True(result);
        Assert.NotNull(entry);
    }

    [Fact]
    public void TryParse_ValidLine_ParsesAllFields()
    {
        string line =
            "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";

        _parser.TryParse(line, out var entry);

        Assert.NotNull(entry);

        Assert.Equal(
            new DateOnly(2025, 3, 10),
            entry.Date);

        Assert.Equal(
            "15:14:49.523",
            entry.Time);

        Assert.Equal(
            "INFO",
            entry.Level);

        Assert.Equal(
            "DEFAULT",
            entry.CallingMethod);

        Assert.Equal(
            "Версия программы: '3.4.0.48729'",
            entry.Message);
    }

    [Fact]
    public void TryParse_Warning_NormalizesToWarn()
    {
        string line =
            "10.03.2025 15:14:49.523 WARNING Test message";

        _parser.TryParse(line, out var entry);

        Assert.NotNull(entry);
        Assert.Equal("WARN", entry.Level);
    }

    [Fact]
    public void TryParse_InvalidDate_ReturnsFalse()
    {
        string line =
            "99.99.9999 15:14:49.523 INFORMATION Test";

        bool result =
            _parser.TryParse(line, out var entry);

        Assert.False(result);
        Assert.Null(entry);
    }

    [Fact]
    public void TryParse_InvalidLevel_ReturnsFalse()
    {
        string line =
            "10.03.2025 15:14:49.523 CRITICAL Test";

        bool result =
            _parser.TryParse(line, out var entry);

        Assert.False(result);
        Assert.Null(entry);
    }

    [Fact]
    public void TryParse_EmptyLine_ReturnsFalse()
    {
        bool result =
            _parser.TryParse(string.Empty, out var entry);

        Assert.False(result);
        Assert.Null(entry);
    }
}
