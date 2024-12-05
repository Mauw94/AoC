using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2024;

public class Day5(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var (orderingRules, updateLists) = ParseOrderingRulesAndPages(Input);

        // Hard mode for part 1..
        return UpdatePages(orderingRules, updateLists);

        // This is much simpler. Oh well
        // var comparer = new ComparePages(orderingRules);
        // return pages.Where(x => SortedCorrectly(x, comparer)).Sum(x => x[x.Count / 2]);
    }

    public override long PartTwo()
    {
        var (orderingRules, pages) = ParseOrderingRulesAndPages(Input);

        var comparer = new ComparePages(orderingRules);

        return pages.Where(x => !SortedCorrectly(x, comparer)).Sum(x => x.Order(comparer).ElementAt(x.Count / 2));
    }

    static bool SortedCorrectly(List<int> page, ComparePages comparer) => page.SequenceEqual(page.Order(comparer));

    static int UpdatePages(List<(int, int)> orderingRules, List<List<int>> pages)
    {
        var total = 0;
        foreach (var update in pages)
        {
            if (CheckIfInOrder(update, orderingRules))
            {
                total += update.GetMiddleItem();
            }
        }

        return total;
    }

    static bool CheckIfInOrder(List<int> update, List<(int, int)> orderingRules)
    {
        for (int i = 0; i < update.Count; i++)
        {
            var pageNr = update[i];

            if (!IsInOrderAfter(update, orderingRules, pageNr, i).isInOrder
                || !IsInOrderBefore(update, orderingRules, pageNr, i).isInOrder)
                return false;
        }

        return true;
    }

    static (int faultyIndex, bool isInOrder) IsInOrderAfter(List<int> update, List<(int, int)> orderingRules, int pageNr, int i)
    {
        var rulesAfter = orderingRules.Where(o => o.Item1 == pageNr).Select(o => o.Item2).ToList();
        for (int j = i + 1; j < update.Count - i; j++)
        {
            if (!rulesAfter.Contains(update[j])) return (j, false);
        }

        return (0, true);
    }

    static (int faultyIndex, bool isInOrder) IsInOrderBefore(List<int> update, List<(int, int)> orderingRules, int pageNr, int i)
    {
        var rulesBefore = orderingRules.Where(o => o.Item2 == pageNr).Select(o => o.Item1).ToList();
        for (int j = i - 1; j >= 0; j--)
        {
            if (!rulesBefore.Contains(update[j])) return (j, false);
        }

        return (0, true);
    }

    static (List<(int, int)> orderingRules, List<List<int>> pages) ParseOrderingRulesAndPages(List<string> input)
    {
        List<(int, int)> orderingRules = [];
        List<List<int>> pages = [];

        foreach (var line in input)
        {
            if (line.Contains('|'))
            {
                var numbers = line.Split('|').Select(int.Parse).ToList();
                orderingRules.Add((numbers[0], numbers[1]));
            }
            else if (line.Contains(','))
            {
                var ns = line.Split(',').Select(int.Parse).ToList();
                pages.Add(ns);
            }
        }

        return (orderingRules, pages);
    }
}

class ComparePages(List<(int, int)> rules) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (rules.Contains((x, y))) return -1;
        if (rules.Contains((y, x))) return 1;
        return 0;
    }
}