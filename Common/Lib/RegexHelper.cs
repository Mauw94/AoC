
using System.Text.RegularExpressions;

namespace AoC.Common.Lib;

public static partial class RegexHelper
{
    [GeneratedRegex(@"\d+")]
    public static partial Regex ExtractPositiveNumbersRegex();

    [GeneratedRegex(@"[+-]?\d+")]
    public static partial Regex ExtractAllNumbersRegex();

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    public static partial Regex ExtractValidMulInstructions();

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)|do(n't)?\(\)")]
    public static partial Regex ExtractValidMulInstructionsP2();
}
