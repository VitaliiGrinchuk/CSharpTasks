using CSharpTasks.Tasks.Task2;

namespace CSharpTasks.Tests;

public class CountServerTests
{
    public CountServerTests()
    {
        CountServer.Reset();
    }

    [Fact]
    public void GetCount_Initially_ReturnsZero()
    {
        int result = CountServer.GetCount();

        Assert.Equal(0, result);
    }

    [Fact]
    public void AddToCount_PositiveValue_IncreasesCount()
    {
        CountServer.AddToCount(10);

        int result = CountServer.GetCount();

        Assert.Equal(10, result);
    }

    [Fact]
    public void AddToCount_MultipleValues_ReturnsCorrectSum()
    {
        CountServer.AddToCount(10);
        CountServer.AddToCount(20);
        CountServer.AddToCount(30);

        int result = CountServer.GetCount();

        Assert.Equal(60, result);
    }

    [Fact]
    public void AddToCount_NegativeValue_DecreasesCount()
    {
        CountServer.AddToCount(100);
        CountServer.AddToCount(-25);

        int result = CountServer.GetCount();

        Assert.Equal(75, result);
    }

    [Fact]
    public void AddToCount_ConcurrentWriters_DoesNotLoseUpdates()
    {
        const int writersCount = 1000;

        Parallel.For(
            0,
            writersCount,
            _ => CountServer.AddToCount(1));

        int result = CountServer.GetCount();

        Assert.Equal(writersCount, result);
    }

    [Fact]
    public async Task ConcurrentReadersAndWriters_ReturnCorrectFinalValue()
    {
        const int writersCount = 500;
        const int readersCount = 500;

        Task[] writers = Enumerable
            .Range(0, writersCount)
            .Select(_ => Task.Run(
                () => CountServer.AddToCount(1)))
            .ToArray();

        Task[] readers = Enumerable
            .Range(0, readersCount)
            .Select(_ => Task.Run(
                () => CountServer.GetCount()))
            .ToArray();

        await Task.WhenAll(writers.Concat(readers));

        Assert.Equal(writersCount, CountServer.GetCount());
    }
}
