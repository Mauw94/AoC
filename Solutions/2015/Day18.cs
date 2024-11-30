using AoC.Common;

namespace AoC.Solutions._2015;

public class Day18(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var grid = ParseGrid(Input);

        ExecuteSteps(grid, 100);
        return CountTotalLightsOn(grid);
    }

    public override long PartTwo()
    {
        var grid = ParseGrid(Input);
        var cornerLights = new List<(int x, int y)>
        {
            (0, 0),
            (0, grid.GetLength(0) - 1),
            (grid.GetLength(0) - 1, 0),
            (grid.GetLength(0) - 1, grid.GetLength(0) - 1)
        };

        ToggleCornerLights(grid, cornerLights);

        ExecuteSteps(grid, 100, visualizeGrid: true, isPart2: true);
        return CountTotalLightsOn(grid);
    }

    static void ToggleCornerLights(GridP[,] grid, List<(int x, int y)> cornerLights)
    {
        foreach (var (x, y) in cornerLights)
            grid[y, x].IsOn = true;
    }


    static void ExecuteSteps(GridP[,] grid, int steps, bool visualizeGrid = false, bool isPart2 = false)
    {
        for (int i = 0; i < steps; i++)
        {
            var lightsToToggle = GetLightsToToggle(grid);
            ToggleLights(grid, lightsToToggle, isPart2: true);

            if (visualizeGrid)
                VisualizeGrid(grid);
        }
    }

    static void VisualizeGrid(GridP[,] grid)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            Console.WriteLine();
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x].IsOn)
                    Console.Write('#');
                else
                    Console.Write('.');
            }
        }
        Console.WriteLine("\n");
    }

    static int CountTotalLightsOn(GridP[,] grid)
    {
        var totalOn = 0;
        for (int y = 0; y < grid.GetLength(0); y++)
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x].IsOn) totalOn++;
            }

        return totalOn;
    }

    static List<(int x, int y)> GetLightsToToggle(GridP[,] grid)
    {
        var pointsToToggle = new List<(int x, int y)>();

        for (int y = 0; y < grid.GetLength(0); y++)
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                var neighbours = GetNeighbours(x, y, grid.GetLength(0), grid.GetLength(1));

                if (grid[y, x].IsOn && !RangeNeighboursOn(grid, neighbours, (2, 3)))
                {
                    pointsToToggle.Add((x, y));
                }
                else if (!grid[y, x].IsOn && ExactlyNeighboursOn(grid, neighbours, 3))
                {
                    pointsToToggle.Add((x, y));
                }
            }

        return pointsToToggle;
    }

    static void ToggleLights(GridP[,] grid, List<(int x, int y)> pointsToToggle, bool isPart2 = false)
    {
        foreach (var (x, y) in pointsToToggle)
        {
            if (isPart2)
            {
                if (x == 0 && y == 0) continue;
                if (x == 0 && y == grid.GetLength(0) - 1) continue;
                if (x == grid.GetLength(0) - 1 && y == grid.GetLength(0) - 1) continue;
                if (x == grid.GetLength(0) - 1 && y == 0) continue;
            }

            grid[y, x].IsOn = !grid[y, x].IsOn;
        }
    }

    static bool ExactlyNeighboursOn(GridP[,] grid, List<(int x, int y)> neighbours, int maxNeighboursOn)
    {
        var amountOfNeighboursOn = 0;

        foreach (var (x, y) in neighbours)
        {
            if (grid[y, x].IsOn)
                amountOfNeighboursOn++;
        }

        return amountOfNeighboursOn == maxNeighboursOn;
    }

    static bool RangeNeighboursOn(GridP[,] grid, List<(int x, int y)> neighbours, (int min, int max) range)
    {
        var amountOfNeighboursOn = 0;

        foreach (var (x, y) in neighbours)
        {
            if (grid[y, x].IsOn)
                amountOfNeighboursOn++;
        }

        return amountOfNeighboursOn >= range.min && amountOfNeighboursOn <= range.max;
    }

    static GridP[,] ParseGrid(List<string> input)
    {
        var grid = new GridP[input.Count, input[0].Length];

        for (int y = 0; y < input.Count; y++)
            for (int x = 0; x < input[y].Length; x++)
            {
                var isOn = input[y][x] == '#';
                var point = new GridP(x, y, isOn);
                grid[y, x] = point;
            }

        return grid;
    }

    static List<(int x, int y)> GetNeighbours(int x, int y, int maxX, int maxY)
    {
        var neighbours = new List<(int x, int y)>
        {
            (x - 1, y),
            (x + 1, y),
            (x - 1, y - 1),
            (x + 1, y - 1),
            (x - 1, y + 1),
            (x + 1, y + 1),
            (x, y - 1),
            (x, y + 1)
        };

        return neighbours
            .Where(n => n.x >= 0 && n.y >= 0 && n.x < maxX && n.y < maxY)
            .ToList();
    }
}

record struct GridP(int X, int Y, bool IsOn);