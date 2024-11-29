using AoC.Common;

namespace AoC.Solutions._2015;

public class Day17(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var containers = ParseContainerSizes(Input);
        var results = new List<List<int>>();

        FindCombinations(containers, 150, 0, [], results);

        return results.Count;
    }

    public override long PartTwo()
    {
        var containers = ParseContainerSizes(Input);
        var results = new List<List<int>>();

        FindCombinations(containers, 150, 0, [], results);

        var minimum = results.Min(x => x.Count);

        return results.Where(x => x.Count == minimum).Count();
    }


    static void FindCombinations(List<int> containers, int target, int index, List<int> current, List<List<int>> results)
    {
        if (target == 0)
        {
            results.Add([.. current]);
            return;
        }

        if (target < 0 || index == containers.Count) return;

        // Include current container
        current.Add(containers[index]);
        FindCombinations(containers, target - containers[index], index + 1, current, results);

        // Exclude current container
        current.RemoveAt(current.Count - 1);
        FindCombinations(containers, target, index + 1, current, results);
    }

    static List<int> ParseContainerSizes(List<string> input)
    {
        return input.Select(x => int.Parse(x)).ToList();
    }
}