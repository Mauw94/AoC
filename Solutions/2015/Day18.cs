using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2015;

class Light(bool isOn) : IPoint
{
    public bool IsOn = isOn;
}

public class Day18(Day day) : Solution(day)
{

    public override long PartOne()
    {
        var grid = ParseGrid(Input);

        VisualizeGrid(grid);
        ExecuteSteps(grid, 100, visualizeGrid: false);
        return CountTotalLightsOn(grid);
    }

    public override long PartTwo()
    {
        var grid = ParseGrid(Input);
        var cornerLights = new List<(int x, int y)>
        {
            (0, 0),
            (0, grid.Height - 1),
            (grid.Width - 1, 0),
            (grid.Width - 1, grid.Height - 1)
        };

        ToggleCornerLights(grid, cornerLights);

        ExecuteSteps(grid, 100, visualizeGrid: false, isPart2: true);
        return CountTotalLightsOn(grid);
    }

    static void ToggleCornerLights(Grid2d<Light> grid, List<(int x, int y)> cornerLights)
    {
        foreach (var (x, y) in cornerLights)
        {
            grid.GetPoint(x, y).IsOn = true;
        }
    }

    static void ExecuteSteps(Grid2d<Light> grid, int steps, bool visualizeGrid = false, bool isPart2 = false)
    {
        for (int i = 0; i < steps; i++)
        {
            var lightsToToggle = GetLightsToToggle(grid);
            ToggleLights(grid, lightsToToggle, isPart2);

            if (visualizeGrid)
                VisualizeGrid(grid);
        }
    }

    static List<(int x, int y)> GetLightsToToggle(Grid2d<Light> grid)
    {
        var pointsToToggle = new List<(int x, int y)>();

        for (int y = 0; y < grid.Height; y++)
            for (int x = 0; x < grid.Width; x++)
            {
                var neighbours = grid.GetNeighbours(x, y, includeDiagonal: true);

                if (grid.GetPoint(x, y).IsOn && !RangeNeighboursOn(grid, neighbours, (2, 3)))
                {
                    pointsToToggle.Add((x, y));
                }
                else if (!grid.GetPoint(x, y).IsOn && ExactlyNeighboursOn(grid, neighbours, 3))
                {
                    pointsToToggle.Add((x, y));
                }
            }

        return pointsToToggle;
    }

    static void ToggleLights(Grid2d<Light> grid, List<(int x, int y)> pointsToToggle, bool isPart2 = false)
    {
        foreach (var (x, y) in pointsToToggle)
        {
            if (isPart2)
            {
                if (x == 0 && y == 0) continue;
                if (x == 0 && y == grid.Height - 1) continue;
                if (x == grid.Width - 1 && y == grid.Height - 1) continue;
                if (x == grid.Width - 1 && y == 0) continue;
            }

            grid.GetPoint(x, y).IsOn = !grid.GetPoint(x, y).IsOn;
        }
    }

    static bool ExactlyNeighboursOn(Grid2d<Light> grid, List<(int x, int y)> neighbours, int maxNeighboursOn)
    {
        var amountOfNeighboursOn = 0;

        foreach (var (x, y) in neighbours)
        {
            if (grid.GetPoint(x, y).IsOn)
                amountOfNeighboursOn++;
        }

        return amountOfNeighboursOn == maxNeighboursOn;
    }

    static bool RangeNeighboursOn(Grid2d<Light> grid, List<(int x, int y)> neighbours, (int min, int max) range)
    {
        var amountOfNeighboursOn = 0;

        foreach (var (x, y) in neighbours)
        {
            if (grid.GetPoint(x, y).IsOn)
                amountOfNeighboursOn++;
        }

        return amountOfNeighboursOn >= range.min && amountOfNeighboursOn <= range.max;
    }

    static void VisualizeGrid(Grid2d<Light> grid)
    {
        for (int y = 0; y < grid.Height; y++)
        {
            Console.WriteLine();
            for (int x = 0; x < grid.Width; x++)
            {
                if (grid.GetPoint(x, y).IsOn)
                    Console.Write('#');
                else
                    Console.Write('.');
            }
        }
        Console.WriteLine("\n");
    }

    static int CountTotalLightsOn(Grid2d<Light> grid)
    {
        var totalOn = 0;
        for (int y = 0; y < grid.Height; y++)
            for (int x = 0; x < grid.Width; x++)
            {
                if (grid.GetPoint(x, y).IsOn) totalOn++;
            }

        return totalOn;
    }

    static Grid2d<Light> ParseGrid(List<string> input)
    {
        var grid = new Grid2d<Light>(input.Count, input[0].Length);

        for (int y = 0; y < input.Count; y++)
            for (int x = 0; x < input[y].Length; x++)
            {
                var isOn = input[y][x] == '#';
                grid.SetPoint(x, y, new Light(isOn));
            }

        return grid;
    }
}
