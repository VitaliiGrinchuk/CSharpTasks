using System.Text;

namespace CSharpTasks.Tasks.Task1;

public static class StringCompressor
{
    public static string Compress(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Length == 0)
        {
            return string.Empty;
        }

        ValidateInput(input);

        StringBuilder result = new();

        char currentCharacter = input[0];
        int count = 1;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == currentCharacter)
            {
                count++;
                continue;
            }

            AppendGroup(result, currentCharacter, count);

            currentCharacter = input[i];
            count = 1;
        }

        AppendGroup(result, currentCharacter, count);

        return result.ToString();
    }

    private static void AppendGroup(
        StringBuilder result,
        char character,
        int count)
    {
        result.Append(character);

        if (count > 1)
        {
            result.Append(count);
        }
    }

    private static void ValidateInput(string input)
    {
        foreach (char character in input)
        {
            if (character is < 'a' or > 'z')
            {
                throw new ArgumentException(
                    "Input string must contain only lowercase Latin letters.",
                    nameof(input));
            }
        }
    }
}
