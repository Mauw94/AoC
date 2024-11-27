using System.Text;
using AoC.Common;

namespace AoC.Solutions._2015;

public class Day10(Day day) : Solution(day)
{
    public override long PartOne()
    {
        return LookAndSay(Input.First(), 40).Length;
    }

    public override long PartTwo()
    {
        return LookAndSay(Input.First(), 50).Length;
    }

    static string LookAndSay(string input, int iterations)
    {
        string current = input;

        for (int i = 0; i < iterations; i++)
        {
            current = GenerateNext(current);
        }

        return current;
    }

    static string GenerateNext(string input)
    {
        StringBuilder result = new();
        int count = 1;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == input[i - 1])
                count++;
            else
            {
                result.Append(count);
                result.Append(input[i - 1]);
                count = 1;
            }
        }

        result.Append(count);
        result.Append(input[^1]);

        return result.ToString();
    }
}