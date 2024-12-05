namespace AoC.Common.Lib;

public static class ListExtensions
{
    public static IEnumerable<List<T>> GetPermutations<T>(List<T> list, int length)
    {
        if (length == 1) return list.Select(t => new List<T> { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat([t2]).ToList());
    }

    public static long Factor(this List<long> list)
    {
        long factor = 1;
        foreach (var item in list)
            factor *= item;

        return factor;
    }

    public static int Factor(this List<int> list)
    {
        int factor = 1;
        foreach (var item in list)
            factor *= item;

        return factor;
    }

    public static int GetMiddleItem(this List<int> list)
    {
        return list.Count % 2 == 0 ? list[list.Count / 2 - 1] : list[list.Count / 2];
    }

    public static void Swap<T>(this List<T> list, int index1, int index2)
    {
        if (index1 < 0 || index1 >= list.Count || index2 < 0 || index2 >= list.Count)
            throw new ArgumentOutOfRangeException("Index out of range in the Swap extension method.");

        (list[index2], list[index1]) = (list[index1], list[index2]);
    }
}