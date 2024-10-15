using AoC.Common;

namespace AoC.Solutions._2015;

public class Day8(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var total = 0;
        foreach (var line in Input)
        {
            total += line.Length - GetActualStringLength(line);
        }

        return total;
    }

    public override long PartTwo()
    {
        var total = 0;
        foreach (var line in Input)
        {
            total += GetEscapedStringLength(line) - line.Length;
        }

        return total;
    }

    static int GetActualStringLength(string line)
    {
        var content = line.Substring(1, line.Length - 2);
        var actualLength = 0;

        for (int i = 0; i < content.Length; i++)
        {
            if (content[i] == '\\')
            {
                if (content[i + 1] == 'x')
                    i += 3;
                else
                    i++;
            }
            actualLength++;
        }

        return actualLength;
    }

    static int GetEscapedStringLength(string line)
    {

        var escapedLength = 2;
        foreach (char c in line)
        {
            if (c == '\\' || c == '\"')
                escapedLength += 2;
            else
                escapedLength++;
        }

        return escapedLength;
    }
}