using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2015;

public class Day12(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var numbers = ExtractNumbers(Input);

        return numbers.Sum();
    }

    public override long PartTwo()
    {
        var jsonNode = JsonNode.Parse(Input.First());
        var total = SumExcludingRed(jsonNode);

        return total;
    }

    static List<int> ExtractNumbers(List<string> input)
    {
        List<int> numbers = [];

        foreach (var line in input)
        {
            var matches = RegexHelper.ExtractAllNumbersRegex().Matches(line);
            foreach (Match match in matches)
            {
                numbers.Add(int.Parse(match.Value));
            }
        }

        return numbers;
    }

    static int SumExcludingRed(JsonNode? node)
    {
        if (node is null)
            return 0;

        if (node is JsonValue value && value.TryGetValue(out int number))
            return number;

        int sum = 0;

        if (node is JsonArray array)
        {
            foreach (var item in array)
            {
                sum += SumExcludingRed(item);
            }
        }
        else if (node is JsonObject obj)
        {
            if (!ContainsRed(obj))
            {
                foreach (var prop in obj)
                {
                    sum += SumExcludingRed(prop.Value);
                }
            }
        }

        return sum;
    }

    static bool ContainsRed(JsonObject obj)
    {
        foreach (var prop in obj)
        {
            if (prop.Value is JsonValue value && value.TryGetValue(out string? strValue) && strValue == "red")
            {
                return true;
            }
        }

        return false;
    }
}