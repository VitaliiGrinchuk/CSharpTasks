using CSharpTasks.Tasks.Task3.Models;

namespace CSharpTasks.Tasks.Task3.Parsing;

public interface ILogParser
{
    bool TryParse(string line, out LogEntry? entry);
}