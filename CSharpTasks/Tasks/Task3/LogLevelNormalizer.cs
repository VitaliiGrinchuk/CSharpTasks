namespace CSharpTasks.Tasks.Task3;

public static class LogLevelNormalizer
{
    public static bool TryNormalize(
        string level,
        out string normalizedLevel)
    {
        normalizedLevel = level switch
        {
            "INFORMATION" => "INFO",
            "INFO" => "INFO",
            "WARNING" => "WARN",
            "WARN" => "WARN",
            "ERROR" => "ERROR",
            "DEBUG" => "DEBUG",
            _ => string.Empty
        };

        return normalizedLevel.Length > 0;
    }
}
