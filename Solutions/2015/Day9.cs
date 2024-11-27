using AoC.Common;

namespace AoC.Solutions._2015;

public class Day9(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var distances = ParseInputToGraph();
        var locations = distances.Keys.Select(x => x.Item1).Distinct().ToList();

        var permutations = GetPermutations(locations, locations.Count);
        var shortestDistance = int.MaxValue;

        foreach (var perm in permutations)
        {
            var distance = CalculateDistance(perm, distances);

            shortestDistance = Math.Min(shortestDistance, distance);
        }

        return shortestDistance;
    }

    public override long PartTwo()
    {
        var distances = ParseInputToGraph();
        var locations = distances.Keys.Select(x => x.Item1).Distinct().ToList();

        var permutations = GetPermutations(locations, locations.Count);
        var longestDistance = int.MinValue;

        foreach (var perm in permutations)
        {
            var distance = CalculateDistance(perm, distances);

            longestDistance = Math.Max(longestDistance, distance);
        }

        return longestDistance;
    }

    private Dictionary<(string, string), int> ParseInputToGraph()
    {
        var distances = new Dictionary<(string, string), int>();

        foreach (var line in Input)
        {
            var parts = line.Split([" to ", " = "], StringSplitOptions.None);

            if (parts.Length == 3)
            {
                var city1 = parts[0].Trim();
                var city2 = parts[1].Trim();
                var distance = int.Parse(parts[2].Trim());

                distances[(city1, city2)] = distance;
                distances[(city2, city1)] = distance;
            }
        }

        return distances;
    }

    private static IEnumerable<List<T>> GetPermutations<T>(List<T> list, int length)
    {
        if (length == 1) return list.Select(t => new List<T> { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat([t2]).ToList());
    }

    private static int CalculateDistance(List<string> route, Dictionary<(string, string), int> distances)
    {
        var distance = 0;

        for (int i = 0; i < route.Count - 1; i++)
        {
            var pair = (route[i], route[i + 1]);
            distance += distances[pair];
        }

        return distance;
    }
}