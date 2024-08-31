
namespace AoC.Common;

public class Day2(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var total = 0;

        foreach (var line in Input)
        {
            if (!TryParseDimensions(line, out int length, out int width, out int height))
                throw new ArgumentException("Something went wrong.");

            var side1 = length * width;
            var side2 = width * height;
            var side3 = height * length;

            var surfaceArea = (2 * length * width) + (2 * width * height) + (2 * height * length);
            var smallest = Math.Min(side1, Math.Min(side2, side3));

            total += surfaceArea + smallest;
        }

        return total;
    }

    static bool TryParseDimensions(string input, out int length, out int width, out int height)
    {
        length = width = height = 0;
        var parts = input.Split('x');
        if (parts.Length != 3)
            return false;

        return int.TryParse(parts[0], out length) && int.TryParse(parts[1], out width) && int.TryParse(parts[2], out height);
    }

    public override long PartTwo()
    {
        var total = 0;

        foreach (var line in Input)
        {
            if (!TryParseDimensions(line, out int length, out int width, out int height))
                throw new ArgumentException("Can't parse.");

            var values = new List<int>
            {
                length, width, height
            };
            var smallestTwo = values.OrderBy(x => x).Take(2).ToList();

            var ribbon = smallestTwo[0] + smallestTwo[0] + smallestTwo[1] + smallestTwo[1];
            var bow = length * width * height;

            total += ribbon + bow;
        }

        return total;
    }
}