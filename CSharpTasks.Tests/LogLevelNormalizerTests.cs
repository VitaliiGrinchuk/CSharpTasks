using CSharpTasks.Tasks.Task3;

namespace CSharpTasks.Tests;

public class LogLevelNormalizerTests
{
    [Theory]
    [InlineData("INFORMATION", "INFO")]
    [InlineData("INFO", "INFO")]
    [InlineData("WARNING", "WARN")]
    [InlineData("WARN", "WARN")]
    [InlineData("ERROR", "ERROR")]
    [InlineData("DEBUG", "DEBUG")]
    public void TryNormalize_ValidLevel_ReturnsExpectedValue(
        string input,
        string expected)
    {
        bool result =
            LogLevelNormalizer.TryNormalize(
                input,
                out string normalized);

        Assert.True(result);
        Assert.Equal(expected, normalized);
    }

    [Theory]
    [InlineData("CRITICAL")]
    [InlineData("TRACE")]
    [InlineData("")]
    public void TryNormalize_InvalidLevel_ReturnsFalse(
        string input)
    {
        bool result =
            LogLevelNormalizer.TryNormalize(
                input,
                out string normalized);

        Assert.False(result);
        Assert.Equal(string.Empty, normalized);
    }
}