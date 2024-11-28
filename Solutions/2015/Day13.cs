using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2015;

public class Day13(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var happiness = ParseHappiness(Input);
        var people = happiness.Keys.Select(x => x.Item1).Distinct().ToList();
        var maxHappiness = ListExtensions.GetPermutations(people, people.Count).Max(arrangement => CalculateHappiness(arrangement, happiness));

        return maxHappiness;
    }

    public override long PartTwo()
    {
        var happiness = ParseHappiness(Input);
        var people = happiness.Keys.Select(x => x.Item1).Distinct().ToList();

        foreach (var person in people)
        {
            happiness[("You", person)] = 0;
            happiness[(person, "You")] = 0;
        }
        people.Add("You");

        var maxHappiness = ListExtensions.GetPermutations(people, people.Count).Max(arrangement => CalculateHappiness(arrangement, happiness));

        return maxHappiness;
    }

    static int CalculateHappiness(IEnumerable<string> arrangement, Dictionary<(string, string), int> happiness)
    {
        int totalHappiness = 0;
        var people = arrangement.ToList();

        for (int i = 0; i < people.Count; i++)
        {
            var left = people[i];
            var right = people[(i + 1) % people.Count];
            totalHappiness += happiness[(left, right)] + happiness[(right, left)];
        }

        return totalHappiness;
    }

    static Dictionary<(string, string), int> ParseHappiness(List<string> input)
    {
        Dictionary<(string, string), int> happiness = [];

        foreach (var line in input)
        {
            var parsedLine = ParseNameAndHappiness(line);
            happiness.Add(parsedLine.Item1, parsedLine.Item2);
        }

        return happiness;
    }

    static ((string, string), int) ParseNameAndHappiness(string line)
    {
        bool gain = line.Contains("gain");
        var happiness = RegexHelper.ExtractAllNumbersRegex().Matches(line);
        line = line.TrimEnd('.');
        var words = line.Split(' ');

        var happinessValue = int.Parse(happiness.First().Value);
        if (!gain)
            happinessValue = -happinessValue;

        return ((words.First(), words.Last()), happinessValue);
    }
}