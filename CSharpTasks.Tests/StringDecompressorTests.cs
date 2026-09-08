using CSharpTasks.Tasks.Task1;

namespace CSharpTasks.Tests;

public class StringDecompressorTests
{
    [Fact]
    public void Decompress_ValidCompressedString_ReturnsOriginalString()
    {
        string input = "a3b2c3d2e";

        string result = StringDecompressor.Decompress(input);

        Assert.Equal("aaabbcccdde", result);
    }

    [Fact]
    public void Decompress_StringWithoutCounts_ReturnsSameString()
    {
        string input = "abcde";

        string result = StringDecompressor.Decompress(input);

        Assert.Equal("abcde", result);
    }

    [Fact]
    public void Decompress_MultiDigitCount_ReturnsCorrectString()
    {
        string input = "a12";

        string result = StringDecompressor.Decompress(input);

        Assert.Equal(new string('a', 12), result);
    }

    [Fact]
    public void Decompress_EmptyString_ReturnsEmptyString()
    {
        string result = StringDecompressor.Decompress(string.Empty);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Decompress_InvalidFormat_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(
            () => StringDecompressor.Decompress("3a"));
    }

    [Fact]
    public void Decompress_ExplicitCountOfOne_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(
            () => StringDecompressor.Decompress("a1"));
    }

    [Fact]
    public void Decompress_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => StringDecompressor.Decompress(null!));
    }
}
