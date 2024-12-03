using System.Text.RegularExpressions;
using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2024;

public class Day3(Day day) : Solution(day)
{
    public override long PartOne()
    {
        return MullItOver(Input);
    }

    public override long PartTwo()
    {
        return MullItOverP2(Input);
    }

    static int MullItOver(List<string> input)
    {
        var total = 0;
        foreach (var line in input)
        {
            var matches = RegexHelper.ExtractValidMulInstructions().Matches(line);

            foreach (Match match in matches)
            {
                var numbers = RegexHelper.ExtractAllNumbersRegex()
                    .Matches(match.Value)
                    .Select(n => int.Parse(n.Value))
                    .ToList();

                total += numbers.Factor();
            }
        }

        return total;
    }

    static int MullItOverP2(List<string> input)
    {
        var total = 0;
        var doIgnore = false;

        foreach (var line in input)
        {
            var matches = RegexHelper.ExtractValidMulInstructionsP2().Matches(line);

            foreach (Match match in matches)
            {
                if (match.Value == "don't()") { doIgnore = true; continue; }
                if (match.Value == "do()") { doIgnore = false; continue; }
                if (doIgnore) continue;

                var numbers = RegexHelper.ExtractAllNumbersRegex()
                    .Matches(match.Value)
                    .Select(n => int.Parse(n.Value))
                    .ToList();

                total += numbers.Factor();
            }
        }

        return total;
    }
}