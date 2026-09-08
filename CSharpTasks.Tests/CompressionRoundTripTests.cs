using CSharpTasks.Tasks.Task1;

namespace CSharpTasks.Tests;

public class CompressionRoundTripTests
{
    [Theory]
    [InlineData("a")]
    [InlineData("abc")]
    [InlineData("aaabbcccdde")]
    [InlineData("aaaaaaaaaaaa")]
    [InlineData("aabbbbbbbbbbbbccccddde")]
    public void CompressThenDecompress_ReturnsOriginalString(string original)
    {
        string compressed =
            StringCompressor.Compress(original);

        string decompressed =
            StringDecompressor.Decompress(compressed);

        Assert.Equal(original, decompressed);
    }
}
