using System.Diagnostics;

namespace AoC.Common;

public abstract class Solution
{
    public Solution(Day day)
    {
        _day = day;
        Task.Run(LoadInput).Wait();
    }

    protected List<string> Input = [];
    private bool IsInputLoaded => Input.Count > 0;
    private readonly Day _day;

    public async Task LoadInput()
    {
        if (IsInputLoaded) return;

        Input = await InputLoading.Load(_day);
    }

    public abstract long PartOne();
    public abstract long PartTwo();

    public void Run()
    {
        Console.WriteLine($"--- YEAR \t{_day.Year} ---");
        Console.WriteLine($"--- DAY \t{_day.DayNr} ---");

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var resultPartOne = PartOne();
        stopwatch.Stop();
        Console.WriteLine($"Result part one: {resultPartOne}");
        Console.WriteLine($"Ran in: {stopwatch.ElapsedMilliseconds} ms");
        stopwatch.Reset();

        stopwatch.Start();
        var resultPartTwo = PartTwo();
        stopwatch.Stop();
        Console.WriteLine($"Result part two: {resultPartTwo}");
        Console.WriteLine($"Ran in: {stopwatch.ElapsedMilliseconds} ms");
    }
}