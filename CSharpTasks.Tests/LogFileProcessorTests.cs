using CSharpTasks.Tasks.Task3;
using CSharpTasks.Tasks.Task3.Parsing;

namespace CSharpTasks.Tests;

public class LogFileProcessorTests
{
    [Fact]
    public void Process_MixedInput_SeparatesValidAndInvalidLines()
    {
        string testDirectory = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString());

        Directory.CreateDirectory(testDirectory);

        string inputPath =
            Path.Combine(testDirectory, "input.txt");

        string outputPath =
            Path.Combine(testDirectory, "output.txt");

        string problemsPath =
            Path.Combine(testDirectory, "problems.txt");

        try
        {
            string[] inputLines =
            [
                "10.03.2025 15:14:49.523 INFORMATION Test message",
                "2025-03-10 15:14:51.5882| INFO|11|Test.Method| Second message",
                "invalid log line"
            ];

            File.WriteAllLines(
                inputPath,
                inputLines);

            ILogParser[] parsers =
            [
                new FirstFormatLogParser(),
                new SecondFormatLogParser()
            ];

            LogParser logParser =
                new(parsers);

            LogFileProcessor processor =
                new(logParser);

            processor.Process(
                inputPath,
                outputPath,
                problemsPath);

            string[] outputLines =
                File.ReadAllLines(outputPath);

            string[] problemLines =
                File.ReadAllLines(problemsPath);

            Assert.Equal(2, outputLines.Length);
            Assert.Single(problemLines);

            Assert.Equal(
                "10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tTest message",
                outputLines[0]);

            Assert.Equal(
                "10-03-2025\t15:14:51.5882\tINFO\tTest.Method\tSecond message",
                outputLines[1]);

            Assert.Equal(
                "invalid log line",
                problemLines[0]);
        }
        finally
        {
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(
                    testDirectory,
                    recursive: true);
            }
        }
    }
}