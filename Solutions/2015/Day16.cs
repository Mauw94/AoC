using AoC.Common;

namespace AoC.Solutions._2015;

public class Day16(Day day) : Solution(day)
{
    public override long PartOne()
    {
        List<Aunt> aunts = [];

        foreach (var line in Input)
            aunts.Add(ParseAunt(line));

        return FindCorrectAunt(aunts, false);
    }

    public override long PartTwo()
    {
        List<Aunt> aunts = [];

        foreach (var line in Input)
            aunts.Add(ParseAunt(line));

        return FindCorrectAunt(aunts, true);
    }

    static int FindCorrectAunt(List<Aunt> aunts, bool part2)
    {

        Dictionary<string, int> target = [];
        target.Add("children", 3);
        target.Add("cats", 7);
        target.Add("samoyeds", 2);
        target.Add("pomeranians", 3);
        target.Add("akitas", 0);
        target.Add("vizslas", 0);
        target.Add("goldfish", 5);
        target.Add("trees", 3);
        target.Add("cars", 2);
        target.Add("perfumes", 1);

        foreach (var aunt in aunts)
        {
            if (Match(aunt, target, part2))
                return aunt.Number;
        }

        throw new Exception("Did not find an aunt.");
    }

    static bool Match(Aunt aunt, Dictionary<string, int> target, bool part2)
    {
        foreach (var val in target)
        {
            if (!aunt.Values.ContainsKey(val.Key))
                continue;

            var auntVal = aunt.Values[val.Key];
            var targetVal = val.Value;

            if (part2)
            {
                if (val.Key == "cats" || val.Key == "trees")
                {
                    if (auntVal <= targetVal)
                        return false;
                }
                else if (val.Key == "pomeranian" || val.Key == "goldfish")
                {
                    if (auntVal >= targetVal)
                        return false;
                }
                else if (auntVal != targetVal)

                {
                    return false;
                }
            }
            else
            {
                if (auntVal != targetVal)
                    return false;
            }
        }

        return true;
    }

    static Aunt ParseAunt(string line)
    {
        var parts = line.Split(["Sue ", ": ", ", "], StringSplitOptions.RemoveEmptyEntries);
        var number = int.Parse(parts[0]);

        var aunt = new Aunt { Number = number };

        for (int i = 1; i < parts.Length - 1; i += 2)
        {
            var property = parts[i];
            var val = int.Parse(parts[i + 1]);

            switch (property)
            {
                case "children": aunt.Values[property] = val; break;
                case "cats": aunt.Values[property] = val; break;
                case "samoyeds": aunt.Values[property] = val; break;
                case "pomeranians": aunt.Values[property] = val; break;
                case "akitas": aunt.Values[property] = val; break;
                case "vizslas": aunt.Values[property] = val; break;
                case "goldfish": aunt.Values[property] = val; break;
                case "trees": aunt.Values[property] = val; break;
                case "cars": aunt.Values[property] = val; break;
                case "perfumes": aunt.Values[property] = val; break;
            }
        }

        return aunt;
    }
}

class Aunt
{
    public int Number { get; set; }
    public Dictionary<string, int> Values { get; set; } = [];
}