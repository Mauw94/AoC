using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2024;

public class Day6(Day day) : Solution(day)
{
    private record Point(bool HasObstacle) : IPoint;

    public override long PartOne()
    {
        var (grid, guardPos) = ParseGrid(Input);

        return CountUniquePositions(grid, guardPos);
    }

    public override long PartTwo()
    {
        var (grid, guardPos) = ParseGrid(Input);

        return CountBlockingPositions(grid, guardPos);
    }

    static HashSet<(int, int)> GetAvailableBlockers(Grid2d<Point> grid, (int x, int y) guardPos)
    {
        HashSet<(int x, int y)> availableBlockers = [];

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                if (!grid.GetPoint(x, y).HasObstacle)
                    availableBlockers.Add((x, y));

                if (x == guardPos.x && y == guardPos.y) continue;
            }
        }

        return availableBlockers;
    }

    static int CountBlockingPositions(Grid2d<Point> grid, (int x, int y) guardPos)
    {
        var isStuck = false;
        var maxSteps = 10000;
        var steps = 0;
        var direction = Direction.Up;
        var originalPos = guardPos;
        HashSet<(int x, int y)> blockingPositions = [];

        foreach (var (x, y) in GetAvailableBlockers(grid, guardPos))
        {
            grid.SetPoint(x, y, new Point(HasObstacle: true));

            while (!IsOffGrid(grid, guardPos) && !isStuck)
            {
                (guardPos, direction) = MoveGuard(grid, guardPos, direction);

                steps++;
                if (steps == maxSteps) { isStuck = true; }
            }

            if (isStuck)
            {
                blockingPositions.Add((x, y));
                isStuck = false;
            }

            steps = 0;
            guardPos = originalPos;
            direction = Direction.Up;
            grid.SetPoint(x, y, new Point(HasObstacle: false));
        }

        return blockingPositions.Count;
    }

    static int CountUniquePositions(Grid2d<Point> grid, (int x, int y) guardPos)
    {
        var visitedPositions = new HashSet<(int, int)>();
        var direction = Direction.Up;

        while (!IsOffGrid(grid, guardPos))
        {
            (guardPos, direction) = MoveGuard(grid, guardPos, direction);
            visitedPositions.Add(guardPos);
        }

        return visitedPositions.Count;
    }

    static bool IsOffGrid(Grid2d<Point> grid, (int x, int y) curPos)
    {
        return curPos.x < 0 || curPos.x >= grid.Width || curPos.y < 0 || curPos.y >= grid.Height;
    }

    static ((int x, int y), Direction direction1) MoveGuard(Grid2d<Point> grid, (int x, int y) curPos, Direction direction)
    {
        var newDirection = direction;
        switch (direction)
        {
            case Direction.Up:
                if (grid.IsInbounds(curPos.x, curPos.y - 1) && grid.GetPoint(curPos.x, curPos.y - 1).HasObstacle)
                {
                    newDirection = Direction.Right;
                    return (curPos, newDirection);
                }
                curPos.y--;
                break;
            case Direction.Down:
                if (grid.IsInbounds(curPos.x, curPos.y + 1) && grid.GetPoint(curPos.x, curPos.y + 1).HasObstacle)
                {
                    newDirection = Direction.Left;
                    return (curPos, newDirection);
                }
                curPos.y++;
                break;
            case Direction.Left:
                if (grid.IsInbounds(curPos.x - 1, curPos.y) && grid.GetPoint(curPos.x - 1, curPos.y).HasObstacle)
                {
                    newDirection = Direction.Up;
                    return (curPos, newDirection);
                }
                curPos.x--;
                break;
            case Direction.Right:
                if (grid.IsInbounds(curPos.x + 1, curPos.y) && grid.GetPoint(curPos.x + 1, curPos.y).HasObstacle)
                {
                    newDirection = Direction.Down;
                    return (curPos, newDirection);
                }
                curPos.x++;
                break;
        }

        return (curPos, newDirection);
    }

    static (Grid2d<Point> grid, (int x, int y) guardPos) ParseGrid(List<string> input)
    {
        var grid = new Grid2d<Point>(input.Count, input[0].Length);
        (int x, int y) guardPos = (0, 0);

        for (int y = 0; y < input.Count; y++)
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '^') guardPos = (x, y);
                grid.SetPoint(x, y, new Point(HasObstacle: input[y][x] == '#'));
            }

        return (grid, guardPos);
    }
}