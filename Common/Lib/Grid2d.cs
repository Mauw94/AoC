namespace AoC.Common.Lib;

public class Grid2d<T>(int width, int height)
    where T : IPoint
{
    // Accessing 2D arrays as [row, column] where row is the y value and column is the x value.
    private readonly T[,] _grid = new T[height, width];

    public T GetPoint(int x, int y) => _grid[y, x];
    public void SetPoint(int x, int y, T point) => _grid[y, x] = point;

    public int Width => _grid.GetLength(0);
    public int Height => _grid.GetLength(1);

    public List<(int x, int y)> GetNeighbours(int currX, int currY, bool includeDiagonal = false)
    {
        List<(int x, int y)> neighbours = [];
        neighbours.Add((currX + 1, currY));
        neighbours.Add((currX, currY + 1));
        neighbours.Add((currX - 1, currY));
        neighbours.Add((currX, currY - 1));

        if (includeDiagonal)
        {
            neighbours.Add((currX + 1, currY + 1));
            neighbours.Add((currX - 1, currY - 1));
            neighbours.Add((currX - 1, currY + 1));
            neighbours.Add((currX + 1, currY - 1));
        }

        return neighbours.Where(n => n.x >= 0 && n.y >= 0 && n.x < Width && n.y < Height).ToList();
    }
}

public interface IPoint
{
}
