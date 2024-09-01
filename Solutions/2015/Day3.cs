using AoC.Common;

namespace AoC.Solutions._2015;

public class Day3(Day day) : Solution(day)
{
    record Point(int X, int Y);

    public override long PartOne()
    {
        var points = new HashSet<Point>()
        {
            new(0,0)
        };

        var position = (x: 0, y: 0);
        var directions = Input.SelectMany(x => x).ToList();

        foreach (var dir in directions)
        {
            position = Move(position, dir);
            points.Add(new Point(position.x, position.y));
        }

        return points.Count;
    }

    static (int x, int y) Move((int x, int y) position, char direction)
    {
        return direction switch
        {
            '>' => (position.x + 1, position.y),
            '<' => (position.x - 1, position.y),
            '^' => (position.x, position.y + 1),
            'v' => (position.x, position.y - 1),
            _ => position // No movement
        };
    }

    public override long PartTwo()
    {
        var points = new HashSet<Point>
        {
            new(0, 0)
        };

        var santaPosition = (x: 0, y: 0);
        var robotPosition = (x: 0, y: 0);

        var directions = Input.SelectMany(x => x).ToList();

        for (var i = 0; i < directions.Count; i++)
        {
            var dir = directions[i];
            var isSanta = i % 2 == 0;


            if (isSanta)
            {
                santaPosition = Move(santaPosition, dir);
                points.Add(new Point(santaPosition.x, santaPosition.y));
            }
            else
            {
                robotPosition = Move(robotPosition, dir);
                points.Add(new Point(robotPosition.x, robotPosition.y));
            }
        }

        return points.Count;
    }
}