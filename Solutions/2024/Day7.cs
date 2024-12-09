using AoC.Common;
using AoC.Common.Lib;

namespace AoC.Solutions._2024;

public class Day7(Day day) : Solution(day)
{
    static readonly List<string> operators = ["+", "*", "|"];

    public override long PartOne()
    {
        var equations = ParseEquations(Input);
        return CalculateTotalCorrectEquations(equations);
    }

    public override long PartTwo()
    {
        return 0;
    }

    static long CalculateTotalCorrectEquations(List<(long calibrationResult, List<long> calibrations)> equations)
    {
        var total = 0L;

        foreach (var (calibrationResult, calibrations) in equations)
        {
            var expressions = GenerateExpressions(calibrations);
            if (IsCalibrationCorrect(calibrationResult, expressions)) total += calibrationResult;
        }

        return total;
    }

    static bool IsCalibrationCorrect(long expectedResult, List<string> expressions)
    {
        foreach (var expression in expressions)
        {
            if (expectedResult == EvaluateExpression(expression))
                return true;
        }

        return false;
    }

    static long EvaluateExpression(string expression)
    {
        var tokens = expression.Split(' ');
        var result = long.Parse(tokens[0]);

        for (int i = 1; i < tokens.Length; i += 2)
        {
            var op = tokens[i];
            var nextNumber = long.Parse(tokens[i + 1]);

            switch (op)
            {
                case "+":
                    result += nextNumber;
                    break;
                case "*":
                    result *= nextNumber;
                    break;
                case "|":
                    var intermediateResult = result.ToString() + nextNumber.ToString();
                    result = long.Parse(intermediateResult);
                    break;
            }
        }

        return result;
    }

    static List<string> GenerateExpressions(List<long> numbers)
    {
        List<string> results = [];
        Generate(numbers, operators, 0, numbers[0].ToString(), results);
        return results;
    }

    static void Generate(List<long> numbers, List<string> operators, int index, string currentExpr, List<string> results)
    {
        if (index == numbers.Count - 1) { results.Add(currentExpr); return; }

        foreach (var op in operators)
        {
            var newExpression = currentExpr + " " + op + " " + numbers[index + 1];
            Generate(numbers, operators, index + 1, newExpression, results);
        }
    }

    static List<(long, List<long>)> ParseEquations(List<string> input)
    {
        List<(long, List<long>)> result = [];

        foreach (var line in input)
        {
            var numbers = RegexHelper.ExtractAllNumbersRegex().Matches(line); ;
            var calibration = 0L;
            var equations = new List<long>();

            for (int i = 0; i < numbers.Count; i++)
            {
                if (i == 0) calibration = long.Parse(numbers[i].Value);
                else equations.Add(long.Parse(numbers[i].Value));
            }

            result.Add((calibration, equations));
        }

        return result;
    }
}