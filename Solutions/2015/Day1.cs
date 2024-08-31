using AoC.Common;

namespace AoC.Solutions._2015;

public class Day1(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var result = 0L;

        foreach (var line in Input)
        {
            foreach (var chr in line)
            {
                if (chr == '(')
                    result += 1;
                if (chr == ')')
                    result -= 1;
            }
        }

        return result;
    }

    public override long PartTwo()
    {
        var result = 0;
        var final = -1;

        var combined = Input.SelectMany(x => x).ToList();

        foreach (var (current, index) in combined.Select((value, i) => (value, i)))
        {
            result += current == '(' ? 1 : current == ')' ? -1 : 0;

            if (result == -1)
            {
                final = index + 1;
                break;
            }
        }

        return final;
    }
}