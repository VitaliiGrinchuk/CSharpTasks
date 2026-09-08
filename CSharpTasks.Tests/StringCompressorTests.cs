using CSharpTasks.Tasks.Task1;

namespace CSharpTasks.Tests;

public class StringCompressorTests
{
    [Fact]
    public void Compress_ValidString_ReturnsCompressedString()
    {
        string input = "aaabbcccdde";

        string result = StringCompressor.Compress(input);

        Assert.Equal("a3b2c3d2e", result);
    }

    [Fact]
    public void Compress_StringWithoutRepeatedCharacters_ReturnsSameString()
    {
        string input = "abcde";

        string result = StringCompressor.Compress(input);

        Assert.Equal("abcde", result);
    }

    [Fact]
    public void Compress_LongGroup_SupportsCountsGreaterThanNine()
    {
        string input = "aaaaaaaaaaaa";

        string result = StringCompressor.Compress(input);

        Assert.Equal("a12", result);
    }

    [Fact]
    public void Compress_EmptyString_ReturnsEmptyString()
    {
        string result = StringCompressor.Compress(string.Empty);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Compress_InvalidCharacters_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => StringCompressor.Compress("aaaBBB"));
    }

    [Fact]
    public void Compress_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => StringCompressor.Compress(null!));
    }
}
