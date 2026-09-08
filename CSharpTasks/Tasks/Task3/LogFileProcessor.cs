using System.Text;
using CSharpTasks.Tasks.Task3.Formatting;
using CSharpTasks.Tasks.Task3.Models;
using CSharpTasks.Tasks.Task3.Parsing;

namespace CSharpTasks.Tasks.Task3;

public sealed class LogFileProcessor
{
    private readonly LogParser _parser;

    public LogFileProcessor(LogParser parser)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }

    public void Process(
        string inputPath,
        string outputPath,
        string problemsPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(inputPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(outputPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(problemsPath);

        using StreamReader reader = new(
            inputPath,
            Encoding.UTF8);

        using StreamWriter outputWriter = new(
            outputPath,
            false,
            Encoding.UTF8);

        using StreamWriter problemsWriter = new(
            problemsPath,
            false,
            Encoding.UTF8);

        string? line;

        while ((line = reader.ReadLine()) is not null)
        {
            if (_parser.TryParse(line, out LogEntry? entry) &&
                entry is not null)
            {
                string formattedLine =
                    LogEntryFormatter.Format(entry);

                outputWriter.WriteLine(formattedLine);
            }
            else
            {
                problemsWriter.WriteLine(line);
            }
        }
    }
}