using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2015;

public class Day14(Day day) : Solution(day)
{
    public override long PartOne()
    {
        List<Reindeer> reindeers = [];

        foreach (var line in Input)
            reindeers.Add(ParseReindeer(line));

        return CalculateMaxTravelled(reindeers, 2503);
    }

    public override long PartTwo()
    {
        List<Reindeer> reindeers = [];

        foreach (var line in Input)
            reindeers.Add(ParseReindeer(line));

        CalculateTotalTravelledAfter(reindeers, 2503);

        return reindeers.Select(x => x.Points()).Max();
    }

    // Solution for part 1
    static int CalculateMaxTravelled(List<Reindeer> reindeers, int travelTime)
    {
        List<int> travelTimes = [];

        foreach (var reindeer in reindeers)
            travelTimes.Add(reindeer.CalculateTotalTravelledAfter(travelTime));

        return travelTimes.Max();
    }

    // Solution for part 2
    static int CalculateTotalTravelledAfter(List<Reindeer> reindeers, int travelTime)
    {
        for (int i = 0; i < travelTime; i++)
        {
            foreach (var reindeer in reindeers)
            {
                reindeer.Travel();
            }

            AddPointToReindeerInTheLead(reindeers);
        }

        return reindeers.Select(x => x.TotalTraveled).Max();
    }

    static void AddPointToReindeerInTheLead(List<Reindeer> reindeers)
    {
        var maxValue = reindeers.Select(x => x.TotalTraveled).Max();
        var maxReinders = reindeers.Where(x => x.TotalTraveled == maxValue).ToList();

        foreach (var reindeer in maxReinders)
            reindeer.AddPoint();
    }

    static Reindeer ParseReindeer(string line)
    {
        var name = line.Split(' ').First();
        var numberMatches = RegexHelper.ExtractPositiveNumbersRegex().Matches(line);
        _ = int.TryParse(numberMatches[0].Value, out int speed);
        _ = int.TryParse(numberMatches[1].Value, out int time);
        _ = int.TryParse(numberMatches[2].Value, out int rest);

        return new Reindeer(name, speed, time, rest);
    }
}

class Reindeer(string name, int speed, int time, int rest)
{
    public string Name = name;
    public int Speed = speed;
    public int FlyTime = time;
    private int _internalFlyTimer = 0;
    public int RestTime = rest;
    private int _internalRestTimer = 0;
    public bool IsResting = false;
    private int _points = 0;
    private int _totalTravelled = 0;
    public int TotalTraveled => _totalTravelled;

    public void AddPoint() => _points++;
    public int Points() => _points;

    public void Travel()
    {
        if (!IsResting)
        {
            _totalTravelled += Speed;
            _internalFlyTimer++;
            if (_internalFlyTimer == FlyTime)
            {
                _internalFlyTimer = 0;
                IsResting = true;
            }

            return;
        }

        if (IsResting)
        {
            _internalRestTimer++;
            if (_internalRestTimer == RestTime)
            {
                _internalRestTimer = 0;
                IsResting = false;
            }

            return;
        }
    }

    public int CalculateTotalTravelledAfter(int travelTime)
    {
        var total = 0;
        var timer = 0;
        while (timer <= travelTime)
        {
            if (!IsResting)
            {
                total += Fly();
                timer += FlyTime;
            }

            if (IsResting)
            {
                Rest();
                timer += RestTime;
            }
        }

        return total;
    }

    int Fly()
    {
        var distance = 0;
        for (int i = 0; i < FlyTime; i++)
        {
            distance += Speed;
        }
        IsResting = true;

        return distance;
    }

    void Rest()
    {
        var restTimer = 0;
        while (restTimer < RestTime)
        {
            restTimer++;
        }
        IsResting = false;
    }
}