using AoC.Common;

namespace AoC.Solutions._2015;

public class Day5(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var result = 0;

        foreach (var line in Input)
        {
            if (IsNice(line))
                result += 1;
        }

        return result;
    }

    static bool IsNice(string input)
    {
        var forbidden = new List<string>
        {
            "ab",
            "cd",
            "pq",
            "xy"
        };

        var vowels = new Dictionary<char, int>
        {
            { 'a', 0 },
            { 'e', 0 },
            { 'i', 0 },
            { 'o', 0 },
            { 'u', 0 }
        };

        if (forbidden.Any(input.Contains))
            return false;

        foreach (var chr in input)
        {
            if (vowels.ContainsKey(chr))
                vowels[chr] += 1;
        }

        if (vowels.Values.Sum() < 3)
            return false;

        if (!ContainsConsecutiveDuplicates(input))
            return false;

        return true;
    }

    static bool ContainsConsecutiveDuplicates(string input)
    {
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (input[i] == input[i + 1])
                return true;
        }

        return false;
    }

    public override long PartTwo()
    {
        var result = 0;

        foreach (var line in Input)
            if (IsNicePart2(line))
                result += 1;

        return result;
    }

    static bool IsNicePart2(string input) => HasNonOverlappingPair(input) && HasRepeatingWithOneBetween(input);

    static bool HasNonOverlappingPair(string input)
    {
        var seenPairs = new Dictionary<string, int>();

        for (int i = 0; i < input.Length - 1; i++)
        {
            var pair = input.Substring(i, 2);

            if (seenPairs.TryGetValue(pair, out int value))
                if (i - value > 1)
                    return true;

            seenPairs[pair] = i;
        }

        return false;
    }

    static bool HasRepeatingWithOneBetween(string input)
    {
        for (int i = 0; i < input.Length - 2; i++)
        {
            if (input[i] == input[i + 2])
                return true;
        }

        return false;
    }
}