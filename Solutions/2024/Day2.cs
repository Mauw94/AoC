using System.Net;
using AoC.Common;

namespace AoC.Solutions._2024;

public class Day2(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var totalSafe = 0;
        foreach (var line in Input)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            if (IsValid(numbers))
                totalSafe++;
        }

        return totalSafe;
    }

    public override long PartTwo()
    {
        var totalSafe = 0;
        foreach (var line in Input)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            if (IsValid(numbers))
            {
                totalSafe++;
                continue;
            }

            totalSafe += PruneFaulty(numbers) ? 1 : 0;
        }

        return totalSafe;
    }

    static bool IsValid(List<int> numbers)
    {
        bool isIncreasing = numbers[0] < numbers[^1];

        for (int i = 0; i < numbers.Count; i++)
        {
            if (i + 1 == numbers.Count) return true;
            if (NextNumberIsValid(numbers[i], numbers[i + 1], isIncreasing) && IsBetweenBounds(numbers[i], numbers[i + 1]))
                continue;
            else
                return false;
        }

        return true;
    }

    static bool PruneFaulty(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            var pruned = numbers.Where((item, index) => index != i).ToList();
            if (IsValid(pruned)) return true;
        }

        return false;
    }

    static bool NextNumberIsValid(int left, int right, bool isIncreasing)
    {
        return isIncreasing ? left < right : left > right;
    }

    static bool IsBetweenBounds(int left, int right)
    {
        var diff = Math.Abs(left - right);
        return diff >= 1 && diff <= 3;
    }
}