using AoC.Common;
using AoC.Solutions._2015;

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
    new Day10(Day.Create(2015, 10)),
};

foreach (var solution in solutions)
    solution.Run();