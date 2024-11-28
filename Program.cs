using AoC.Common;
using AoC.Solutions._2015;

// dotnet run
if (args.Length == 0)
{

    var solutions = new List<Solution>
{
    // new Day1(Day.Create(2015, 1)),
    // new Day2(Day.Create(2015, 2)),
    // new Day3(Day.Create(2015, 3)),
    // new Day4(Day.Create(2015, 4)),
    // new Day5(Day.Create(2015, 5)),
    // new Day6(Day.Create(2015, 6)),
    // new Day7(Day.Create(2015, 7)),
    // new Day8(Day.Create(2015, 8)),
    // new Day9(Day.Create(2015, 9)),
    // new Day10(Day.Create(2015, 10)),
    // new Day11(Day.Create(2015, 11)),
    // new Day12(Day.Create(2015, 12)),
    new Day13(Day.Create(2015, 13)),
};

    foreach (var solution in solutions)
        solution.Run();
}

// dotnet run gen_day {day} {year}
else if (args[0] == "gen_day" && args.Length == 3)
{
    if (int.TryParse(args[1], out int day) && int.TryParse(args[2], out int year))
    {
        Console.WriteLine($"Generating Day{day}.cs and input {day}.txt files.");
        await DayUtils.Generate(day, year);
    }
    else
    {
        Console.WriteLine("Invalid arguments. Day and year must be integers.");
    }
}
// dotnet run day {day} {year}
else if (args[0] == "day" && args.Length == 3)
{
    if (int.TryParse(args[1], out int day) && int.TryParse(args[2], out int year))
    {
        Console.WriteLine($"Running day {day} for year {year}.");
        DayUtils.Run(day, year);
    }
    else
    {
        Console.WriteLine("Invalid arguments. Day and year must be integers.");
    }
}
else
{
    Console.WriteLine("Unknown command or invalid arguments.");
}