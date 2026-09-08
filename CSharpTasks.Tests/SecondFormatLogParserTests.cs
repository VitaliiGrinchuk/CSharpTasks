using CSharpTasks.Tasks.Task3.Parsing;

namespace CSharpTasks.Tests;

public class SecondFormatLogParserTests
{
    private readonly SecondFormatLogParser _parser = new();

    [Fact]
    public void TryParse_ValidLine_ReturnsTrue()
    {
        string line =
            "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

        bool result =
            _parser.TryParse(line, out var entry);

        Assert.True(result);
        Assert.NotNull(entry);
    }

    [Fact]
    public void TryParse_ValidLine_ParsesAllFields()
    {
        string line =
            "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

        _parser.TryParse(line, out var entry);

        Assert.NotNull(entry);

        Assert.Equal(
            new DateOnly(2025, 3, 10),
            entry.Date);

        Assert.Equal(
            "15:14:51.5882",
            entry.Time);

        Assert.Equal(
            "INFO",
            entry.Level);

        Assert.Equal(
            "MobileComputer.GetDeviceId",
            entry.CallingMethod);

        Assert.Equal(
            "Код устройства: '@MINDEO-M40-D-410244015546'",
            entry.Message);
    }

    [Fact]
    public void TryParse_ErrorLevel_PreservesError()
    {
        string line =
            "2025-03-10 15:14:51.5882| ERROR|11|Database.Connect| Connection failed";

        _parser.TryParse(line, out var entry);

        Assert.NotNull(entry);
        Assert.Equal("ERROR", entry.Level);
    }

    [Fact]
    public void TryParse_InvalidDate_ReturnsFalse()
    {
        string line =
            "2025-99-99 15:14:51.5882| INFO|11|Test.Method| Test";

        bool result =
            _parser.TryParse(line, out var entry);

        Assert.False(result);
        Assert.Null(entry);
    }

    [Fact]
    public void TryParse_MissingMethod_ReturnsFalse()
    {
        string line =
            "2025-03-10 15:14:51.5882| INFO|11|| Test";

        bool result =
            _parser.TryParse(line, out var entry);

        Assert.False(result);
        Assert.Null(entry);
    }

    [Fact]
    public void TryParse_InvalidStructure_ReturnsFalse()
    {
        string line = "This is not a valid log";

        bool result =
            _parser.TryParse(line, out var entry);

        Assert.False(result);
        Assert.Null(entry);
    }
}
