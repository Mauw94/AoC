using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2024;



public class Day4(Day day) : Solution(day)
{
    private record Point(char Letter) : IPoint;

    public override long PartOne()
    {
        var grid = ParseGrid(Input);
        // VisualizeGrid(grid);

        return CountXMAS(grid);
    }

    public override long PartTwo()
    {
        var grid = ParseGrid(Input);

        return CountXMASPart2(grid);
    }

    static int CountXMASPart2(Grid2d<Point> grid)
    {
        var total = 0;

        for (int y = 0; y < grid.Height; y++)
            for (int x = 0; x < grid.Width; x++)
            {
                if (grid.GetPoint(x, y).Letter == 'A')
                {
                    if (CheckStandardXmas(grid, x, y)
                        || CheckReversedXmas(grid, x, y)
                        || CheckStandXmasUpsideDown(grid, x, y)
                        || CheckReversedXmasUpsideDown(grid, x, y))
                        total++;
                }
            }

        return total;
    }

    static bool CheckStandardXmas(Grid2d<Point> grid, int curX, int curY)
    {
        if (CheckTopLeft(grid, curX, curY, 'M')
            && CheckBottomLeft(grid, curX, curY, 'M')
            && CheckTopRight(grid, curX, curY, 'S')
            && CheckBottomRight(grid, curX, curY, 'S'))
            return true;

        return false;
    }

    static bool CheckReversedXmas(Grid2d<Point> grid, int curX, int curY)
    {
        if (CheckTopLeft(grid, curX, curY, 'S')
                && CheckBottomLeft(grid, curX, curY, 'S')
                && CheckTopRight(grid, curX, curY, 'M')
                && CheckBottomRight(grid, curX, curY, 'M'))
            return true;

        return false;
    }

    static bool CheckStandXmasUpsideDown(Grid2d<Point> grid, int curX, int curY)
    {
        if (CheckTopLeft(grid, curX, curY, 'M')
           && CheckBottomLeft(grid, curX, curY, 'S')
           && CheckTopRight(grid, curX, curY, 'M')
           && CheckBottomRight(grid, curX, curY, 'S'))
            return true;

        return false;
    }

    static bool CheckReversedXmasUpsideDown(Grid2d<Point> grid, int curX, int curY)
    {
        if (CheckTopLeft(grid, curX, curY, 'S')
                && CheckBottomLeft(grid, curX, curY, 'M')
                && CheckTopRight(grid, curX, curY, 'S')
                && CheckBottomRight(grid, curX, curY, 'M'))
            return true;

        return false;
    }

    static bool CheckTopLeft(Grid2d<Point> grid, int curX, int curY, char letter)
    {
        if (curX - 1 < 0) return false;
        if (curY - 1 < 0) return false;

        if (grid.GetPoint(curX - 1, curY - 1).Letter == letter) return true;

        return false;
    }

    static bool CheckBottomLeft(Grid2d<Point> grid, int curX, int curY, char letter)
    {
        if (curX - 1 < 0) return false;
        if (curY + 1 >= grid.Height) return false;

        if (grid.GetPoint(curX - 1, curY + 1).Letter == letter) return true;

        return false;
    }

    static bool CheckTopRight(Grid2d<Point> grid, int curX, int curY, char letter)
    {
        if (curX + 1 >= grid.Width) return false;
        if (curY - 1 < 0) return false;

        if (grid.GetPoint(curX + 1, curY - 1).Letter == letter) return true;

        return false;
    }

    static bool CheckBottomRight(Grid2d<Point> grid, int curX, int curY, char letter)
    {
        if (curX + 1 >= grid.Width) return false;
        if (curY + 1 >= grid.Width) return false;

        if (grid.GetPoint(curX + 1, curY + 1).Letter == letter) return true;

        return false;
    }

    static int CountXMAS(Grid2d<Point> grid)
    {
        var total = 0;

        for (int y = 0; y < grid.Height; y++)
            for (int x = 0; x < grid.Width; x++)
            {
                if (grid.GetPoint(x, y).Letter == 'X')
                {
                    total += ScanLeftToRight(grid, x, y) ? 1 : 0;
                    total += ScanRightToLeft(grid, x, y) ? 1 : 0;
                    total += ScanDownToUp(grid, x, y) ? 1 : 0;
                    total += ScanUpToDown(grid, x, y) ? 1 : 0;
                    total += ScanDiagonalRightDown(grid, x, y) ? 1 : 0;
                    total += ScanDiagonalRightUp(grid, x, y) ? 1 : 0;
                    total += ScanDiagonalLeftDown(grid, x, y) ? 1 : 0;
                    total += ScanDiagonalLeftUp(grid, x, y) ? 1 : 0;
                }
            }

        return total;
    }

    static bool ScanLeftToRight(Grid2d<Point> grid, int curX, int curY)
    {
        if (curX + 3 >= grid.Width) return false;
        if (grid.GetPoint(curX + 1, curY).Letter == 'M'
            && grid.GetPoint(curX + 2, curY).Letter == 'A'
            && grid.GetPoint(curX + 3, curY).Letter == 'S')
            return true;

        return false;
    }

    static bool ScanRightToLeft(Grid2d<Point> grid, int curX, int curY)
    {
        if (curX - 3 < 0) return false;
        if (grid.GetPoint(curX - 1, curY).Letter == 'M'
            && grid.GetPoint(curX - 2, curY).Letter == 'A'
            && grid.GetPoint(curX - 3, curY).Letter == 'S')
            return true;

        return false;
    }

    static bool ScanDownToUp(Grid2d<Point> grid, int curX, int curY)
    {
        if (curY - 3 < 0) return false;
        if (grid.GetPoint(curX, curY - 1).Letter == 'M'
            && grid.GetPoint(curX, curY - 2).Letter == 'A'
            && grid.GetPoint(curX, curY - 3).Letter == 'S')
            return true;

        return false;
    }

    static bool ScanUpToDown(Grid2d<Point> grid, int curX, int curY)
    {
        if (curY + 3 >= grid.Height) return false;
        if (grid.GetPoint(curX, curY + 1).Letter == 'M'
            && grid.GetPoint(curX, curY + 2).Letter == 'A'
            && grid.GetPoint(curX, curY + 3).Letter == 'S')
            return true;

        return false;
    }

    static bool ScanDiagonalRightDown(Grid2d<Point> grid, int curX, int curY)
    {
        if (curX + 3 >= grid.Width) return false;
        if (curY + 3 >= grid.Height) return false;

        if (grid.GetPoint(curX + 1, curY + 1).Letter == 'M'
            && grid.GetPoint(curX + 2, curY + 2).Letter == 'A'
            && grid.GetPoint(curX + 3, curY + 3).Letter == 'S')
            return true;

        return false;
    }

    static bool ScanDiagonalRightUp(Grid2d<Point> grid, int curX, int curY)
    {
        if (curX + 3 >= grid.Width) return false;
        if (curY - 3 < 0) return false;

        if (grid.GetPoint(curX + 1, curY - 1).Letter == 'M'
            && grid.GetPoint(curX + 2, curY - 2).Letter == 'A'
            && grid.GetPoint(curX + 3, curY - 3).Letter == 'S')
            return true;

        return false;
    }

    static bool ScanDiagonalLeftDown(Grid2d<Point> grid, int curX, int curY)
    {
        if (curX - 3 < 0) return false;
        if (curY + 3 >= grid.Height) return false;

        if (grid.GetPoint(curX - 1, curY + 1).Letter == 'M'
            && grid.GetPoint(curX - 2, curY + 2).Letter == 'A'
            && grid.GetPoint(curX - 3, curY + 3).Letter == 'S')
            return true;

        return false;
    }

    static bool ScanDiagonalLeftUp(Grid2d<Point> grid, int curX, int curY)
    {
        if (curX - 3 < 0) return false;
        if (curY - 3 < 0) return false;

        if (grid.GetPoint(curX - 1, curY - 1).Letter == 'M'
            && grid.GetPoint(curX - 2, curY - 2).Letter == 'A'
            && grid.GetPoint(curX - 3, curY - 3).Letter == 'S')
            return true;

        return false;
    }

    static Grid2d<Point> ParseGrid(List<string> input)
    {
        var grid = new Grid2d<Point>(input.Count, input[0].Length);

        for (int y = 0; y < input.Count; y++)
            for (int x = 0; x < input[y].Length; x++)
            {
                var letter = input[y][x];
                grid.SetPoint(x, y, new Point(letter));
            }

        return grid;
    }

    static void VisualizeGrid(Grid2d<Point> grid)
    {
        for (int y = 0; y < grid.Height; y++)
        {
            Console.WriteLine();
            for (int x = 0; x < grid.Width; x++)
            {
                Console.Write(grid.GetPoint(x, y).Letter);
            }
        }
        Console.WriteLine("\n");
    }
}