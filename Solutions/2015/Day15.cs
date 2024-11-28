using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2015;

public class Day15(Day day) : Solution(day)
{
    public override long PartOne()
    {
        List<Ingredient> ingredients = [];

        foreach (var line in Input)
            ingredients.Add(ParseIngredient(line));

        return HighestScoringCookie(ingredients, false);
    }

    public override long PartTwo()
    {
        List<Ingredient> ingredients = [];

        foreach (var line in Input)
            ingredients.Add(ParseIngredient(line));

        return HighestScoringCookie(ingredients, true);
    }

    static int HighestScoringCookie(List<Ingredient> ingredients, bool hasToMeetCalorieRequirement)
    {
        List<int> scores = [];
        var combinations = GenerateCombinations(100, ingredients.Count);

        foreach (var combination in combinations)
        {
            if (!hasToMeetCalorieRequirement)
                scores.Add(CalculateScore(combination, ingredients));
            else
            {
                if (MeetsCalorieRequirement(combination, ingredients))
                {
                    scores.Add(CalculateScore(combination, ingredients));
                }
            }
        }

        return scores.Max();
    }

    static bool MeetsCalorieRequirement(int[] combination, List<Ingredient> ingredients)
    {
        int calories = 0;
        for (int i = 0; i < combination.Length; i++)
            calories += combination[i] * ingredients[i].Calories;

        return calories == 500;
    }

    static int CalculateScore(int[] combination, List<Ingredient> ingredients)
    {
        int capacity = 0, durability = 0, flavor = 0, texture = 0;

        for (int i = 0; i < combination.Length; i++)
        {
            capacity += combination[i] * ingredients[i].Capacity;
            durability += combination[i] * ingredients[i].Durability;
            flavor += combination[i] * ingredients[i].Flavor;
            texture += combination[i] * ingredients[i].Texture;
        }

        capacity = Math.Max(0, capacity);
        durability = Math.Max(0, durability);
        texture = Math.Max(0, texture);
        flavor = Math.Max(0, flavor);

        return capacity * durability * flavor * texture;
    }

    static IEnumerable<int[]> GenerateCombinations(int total, int count)
    {
        if (count == 1)
            yield return new[] { total };
        else
        {
            for (int i = 0; i <= total; i++)
            {
                foreach (var combination in GenerateCombinations(total - i, count - 1))
                    yield return new[] { i }.Concat(combination).ToArray();
            }
        }
    }

    static Ingredient ParseIngredient(string line)
    {
        var numberMatches = RegexHelper.ExtractAllNumbersRegex().Matches(line);
        _ = int.TryParse(numberMatches[0].Value, out int capacity);
        _ = int.TryParse(numberMatches[1].Value, out int durability);
        _ = int.TryParse(numberMatches[2].Value, out int flavor);
        _ = int.TryParse(numberMatches[3].Value, out int texture);
        _ = int.TryParse(numberMatches[4].Value, out int calories);

        return new Ingredient(capacity, durability, flavor, texture, calories);
    }
}

class Ingredient(int capacity, int durability, int flavor, int texture, int calories)
{
    public int Capacity = capacity;
    public int Durability = durability;
    public int Flavor = flavor;
    public int Texture = texture;
    public int Calories = calories;
}