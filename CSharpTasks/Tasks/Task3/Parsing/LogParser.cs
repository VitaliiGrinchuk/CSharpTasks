using CSharpTasks.Tasks.Task3.Models;

namespace CSharpTasks.Tasks.Task3.Parsing;

public sealed class LogParser
{
    private readonly IReadOnlyList<ILogParser> _parsers;

    public LogParser(IEnumerable<ILogParser> parsers)
    {
        ArgumentNullException.ThrowIfNull(parsers);

        _parsers = parsers.ToList();

        if (_parsers.Count == 0)
        {
            throw new ArgumentException(
                "At least one log parser must be provided.",
                nameof(parsers));
        }
    }

    public bool TryParse(string line, out LogEntry? entry)
    {
        foreach (ILogParser parser in _parsers)
        {
            if (parser.TryParse(line, out entry))
            {
                return true;
            }
        }

        entry = null;
        return false;
    }
}
