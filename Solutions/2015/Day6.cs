using System.Text.RegularExpressions;
using AoC.Common;
using Point = AoC.Common.Point;

namespace AoC.Solutions._2015;

public class Day6(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var lights = new HashSet<(int x, int y)>();

        foreach (var line in Input)
        {
            var isTurnOn = line.Contains("on");
            var isToggle = line.Contains("toggle");
            var numbers = ExtractNumbers(line);
            var firstTwo = numbers[0..2];
            var lastTwo = numbers[2..];
            var start = new Point(X: firstTwo[0], Y: firstTwo[1]);
            var end = new Point(X: lastTwo[0], Y: lastTwo[1]);

            if (isToggle)
            {
                lights.ToggleFromTupleHashSet(start, end);
            }
            else if (isTurnOn)
            {
                lights.AddToTupleHashSet(start, end);
            }
            else
            {
                lights.RemoveFromTupleHashSet(start, end);
            }

        }

        return lights.Count;
    }


    public override long PartTwo()
    {
        var lights = new Dictionary<(int x, int y), int>();

        foreach (var instruction in Input)
        {
            ExecuteInstruction(lights, instruction);
        }

        return lights.Values.Sum();
    }

    static void ExecuteInstruction(Dictionary<(int x, int y), int> lights, string instruction)
    {
        var turnOn = instruction.StartsWith("turn on");
        var turnoff = instruction.StartsWith("turn off");
        var toggle = instruction.StartsWith("toggle");


        var numbers = ExtractNumbers(instruction);
        var firstTwo = numbers[0..2];
        var lastTwo = numbers[2..];
        var start = new Point(X: firstTwo[0], Y: firstTwo[1]);
        var end = new Point(X: lastTwo[0], Y: lastTwo[1]);

        for (int x = start.X; x <= end.X; x++)
            for (int y = start.Y; y <= end.Y; y++)
            {
                if (turnOn)
                {
                    AddToDictValue(lights, (x, y), 1);
                }
                else if (turnoff)
                {
                    DecreaseValueFromDict(lights, (x, y), 1);
                }
                else if (toggle)
                {
                    AddToDictValue(lights, (x, y), 2);
                }
            }
    }

    static void DecreaseValueFromDict(Dictionary<(int x, int y), int> lights, (int x, int y) key, int value)
    {
        if (lights.ContainsKey(key))
        {
            if (lights[key] - 1 >= 0)
                lights[key] -= value;
        }
    }

    static void AddToDictValue(Dictionary<(int x, int y), int> lights, (int x, int y) key, int value)
    {
        if (lights.ContainsKey(key))
            lights[key] += value;
        else
            lights.Add(key, value);
    }

    static List<int> ExtractNumbers(string input)
    {
        var numbers = new List<int>();
        var matches = RegexHelper.ExtractNumbersRegex().Matches(input);

        foreach (Match match in matches)
            numbers.Add(int.Parse(match.Value));

        return numbers;

    }


}