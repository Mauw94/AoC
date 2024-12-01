using AoC.Common;

namespace AoC.Solutions._2024;

public class Day1(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var (first, second) = ParseLists(Input);
        return CalculateTotalDistanceApart([.. first.OrderBy(x => x)], [.. second.OrderBy(x => x)]);
    }

    public override long PartTwo()
    {
        var (first, second) = ParseLists(Input);
        return CalculateSimilarityScore(first, second);
    }

    static int CalculateTotalDistanceApart(List<int> first, List<int> second)
    {
        var totalDistanceApart = 0;

        for (int i = 0; i < first.Count; i++)
        {
            var distanceApart = first[i] - second[i];
            if (distanceApart < 0) distanceApart *= -1;

            totalDistanceApart += distanceApart;
        }

        return totalDistanceApart;
    }

    static int CalculateSimilarityScore(List<int> first, List<int> second)
    {
        var totalSimilarity = 0;

        for (int i = 0; i < first.Count; i++)
        {
            var appearances = second.Where(x => x == first[i]).Count();
            totalSimilarity += appearances * first[i];
        }

        return totalSimilarity;
    }

    static (List<int> first, List<int> second) ParseLists(List<string> input)
    {
        List<int> first = [];
        List<int> second = [];

        foreach (var line in input)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            first.Add(int.Parse(numbers[0]));
            second.Add(int.Parse(numbers[1]));
        }

        return (first, second);
    }
}