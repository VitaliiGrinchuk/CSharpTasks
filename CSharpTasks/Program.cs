using CSharpTasks.Tasks.Task1;
using CSharpTasks.Tasks.Task2;
using CSharpTasks.Tasks.Task3;
using CSharpTasks.Tasks.Task3.Parsing;

while (true)
{
    Console.WriteLine();
    Console.WriteLine("CSharp Tasks");
    Console.WriteLine("============");
    Console.WriteLine("1 - String compression/decompression");
    Console.WriteLine("2 - Thread-safe server");
    Console.WriteLine("3 - Log file standardization");
    Console.WriteLine("0 - Exit");
    Console.WriteLine();

    Console.Write("Select task: ");

    string? choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        case "1":
            RunTask1();
            break;

        case "2":
            RunTask2();
            break;

        case "3":
            RunTask3();
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Unknown command.");
            break;
    }
}

static void RunTask1()
{
    Console.Write("Enter a string: ");

    string? input = Console.ReadLine();

    if (input is null)
    {
        Console.WriteLine("Input was not provided.");
        return;
    }

    try
    {
        string compressed =
            StringCompressor.Compress(input);

        string decompressed =
            StringDecompressor.Decompress(compressed);

        Console.WriteLine();
        Console.WriteLine($"Original:     {input}");
        Console.WriteLine($"Compressed:   {compressed}");
        Console.WriteLine($"Decompressed: {decompressed}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Invalid input: {ex.Message}");
    }
}

static void RunTask2()
{
    const int operationsCount = 1000;

    Console.WriteLine(
        $"Starting {operationsCount} concurrent write operations...");

    int initialCount = CountServer.GetCount();

    Parallel.For(
        0,
        operationsCount,
        _ => CountServer.AddToCount(1));

    int finalCount = CountServer.GetCount();

    Console.WriteLine($"Initial count: {initialCount}");
    Console.WriteLine($"Final count:   {finalCount}");
    Console.WriteLine(
        $"Added:         {finalCount - initialCount}");
}

static void RunTask3()
{
    ILogParser[] parsers =
    [
        new FirstFormatLogParser(),
        new SecondFormatLogParser()
    ];

    LogParser logParser = new(parsers);
    LogFileProcessor processor = new(logParser);

    string inputPath =
        Path.Combine(AppContext.BaseDirectory, "input.txt");

    string outputPath =
        Path.Combine(AppContext.BaseDirectory, "output.txt");

    string problemsPath =
        Path.Combine(AppContext.BaseDirectory, "problems.txt");

    try
    {
        processor.Process(
            inputPath,
            outputPath,
            problemsPath);

        Console.WriteLine(
            "Log processing completed successfully.");

        Console.WriteLine();
        Console.WriteLine($"Input:    {inputPath}");
        Console.WriteLine($"Output:   {outputPath}");
        Console.WriteLine($"Problems: {problemsPath}");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine(
            $"Input file was not found: {inputPath}");
    }
    catch (UnauthorizedAccessException ex)
    {
        Console.WriteLine(
            $"Access error: {ex.Message}");
    }
    catch (IOException ex)
    {
        Console.WriteLine(
            $"File processing error: {ex.Message}");
    }
}