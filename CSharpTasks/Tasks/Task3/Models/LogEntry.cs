namespace CSharpTasks.Tasks.Task3.Models;

public sealed class LogEntry
{
    public required DateOnly Date { get; init; }

    public required string Time { get; init; }

    public required string Level { get; init; }

    public required string CallingMethod { get; init; }

    public required string Message { get; init; }
}