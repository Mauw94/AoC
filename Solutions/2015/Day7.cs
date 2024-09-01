using AoC.Common;

namespace AoC.Solutions._2015;

public class Day7(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var wireValues = new Dictionary<string, ushort>();
        var instructions = new Dictionary<string, string>();

        foreach (var line in Input)
        {
            var parts = line.Split(" -> ");
            instructions[parts[1]] = parts[0];
        }

        var result = GetValue("a", instructions, wireValues);

        return result;
    }

    static ushort GetValue(string wire, Dictionary<string, string> instructions, Dictionary<string, ushort> wireValues)
    {
        if (ushort.TryParse(wire, out ushort value))
            return value;

        if (wireValues.ContainsKey(wire))
            return wireValues[wire];

        var instruction = instructions[wire];
        var parts = instruction.Split(' ');

        if (parts.Length == 1)
        {
            value = GetValue(parts[0], instructions, wireValues);
        }
        else if (parts.Length == 2 && parts[0] == "NOT")
        {
            value = (ushort)~GetValue(parts[1], instructions, wireValues);
        }
        else if (parts[1] == "AND")
        {
            value = (ushort)(GetValue(parts[0], instructions, wireValues) & GetValue(parts[2], instructions, wireValues));
        }
        else if (parts[1] == "OR")
        {
            value = (ushort)(GetValue(parts[0], instructions, wireValues) | GetValue(parts[2], instructions, wireValues));
        }
        else if (parts[1] == "LSHIFT")
        {
            value = (ushort)(GetValue(parts[0], instructions, wireValues) << int.Parse(parts[2]));
        }
        else if (parts[1] == "RSHIFT")
        {
            value = (ushort)(GetValue(parts[0], instructions, wireValues) >> int.Parse(parts[2]));
        }

        wireValues[wire] = value;

        return value;
    }

    public override long PartTwo()
    {
        var wireValues = new Dictionary<string, ushort>();
        var instructions = new Dictionary<string, string>();

        foreach (var line in Input)
        {
            var parts = line.Split(" -> ");
            instructions[parts[1]] = parts[0];
        }

        var result = GetValue("a", instructions, wireValues);
        instructions["b"] = result.ToString();
        wireValues.Clear();
        result = GetValue("a", instructions, wireValues);

        return result;
    }
}