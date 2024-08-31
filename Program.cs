using AoC.Common;

var solutions = new List<Solution>
{
    // new Day1(Day.Create(2015, 1)),
    // new Day2(Day.Create(2015, 2)),
    // new Day3(Day.Create(2015, 3)),
    // new Day4(Day.Create(2015, 4)),
    new Day5(Day.Create(2015, 5)),
};

foreach (var solution in solutions)
    solution.Run();