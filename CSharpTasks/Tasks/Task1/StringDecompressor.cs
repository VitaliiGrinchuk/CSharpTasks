using System.Text;

namespace CSharpTasks.Tasks.Task1;

public static class StringDecompressor
{
    public static string Decompress(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Length == 0)
        {
            return string.Empty;
        }

        StringBuilder result = new();

        int index = 0;

        while (index < input.Length)
        {
            char character = input[index];

            if (character is < 'a' or > 'z')
            {
                throw new FormatException(
                    $"Expected a lowercase Latin letter at position {index}.");
            }

            index++;

            int count = ReadCount(input, ref index);

            result.Append(character, count);
        }

        return result.ToString();
    }

    private static int ReadCount(string input, ref int index)
    {
        if (index >= input.Length || !char.IsDigit(input[index]))
        {
            return 1;
        }

        int startIndex = index;

        while (index < input.Length && char.IsDigit(input[index]))
        {
            index++;
        }

        ReadOnlySpan<char> numberSpan =
            input.AsSpan(startIndex, index - startIndex);

        if (!int.TryParse(numberSpan, out int count))
        {
            throw new FormatException(
                "The compressed string contains an invalid group length.");
        }

        if (count < 2)
        {
            throw new FormatException(
                "Group length must be at least 2 when explicitly specified.");
        }

        return count;
    }
}
