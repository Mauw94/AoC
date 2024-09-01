namespace AoC.Common;

public static class Extensions
{
    public static HashSet<(int x, int y)> AddToTupleHashSet(this HashSet<(int x, int y)> lights, Point start, Point end)
    {
        for (int i = start.X; i <= end.X; i++)
            for (int j = start.Y; j <= end.Y; j++)
            {
                lights.Add((i, j));
            }

        return lights;
    }

    public static HashSet<(int x, int y)> RemoveFromTupleHashSet(this HashSet<(int x, int y)> lights, Point start, Point end)
    {
        for (int i = start.X; i <= end.X; i++)
            for (int j = start.Y; j <= end.Y; j++)
            {
                lights.Remove((i, j));
            }

        return lights;
    }

    public static HashSet<(int x, int y)> ToggleFromTupleHashSet(this HashSet<(int x, int y)> lights, Point start, Point end)
    {
        for (int i = start.X; i <= end.X; i++)
            for (int j = start.Y; j <= end.Y; j++)
            {
                if (lights.Contains((i, j)))
                    lights.Remove((i, j));
                else
                    lights.Add((i, j));
            }

        return lights;
    }
}