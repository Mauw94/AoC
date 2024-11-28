
using System.Text.RegularExpressions;

namespace AoC.Common.Lib;

public static partial class RegexHelper
{
    [GeneratedRegex(@"\d+")]
    public static partial Regex ExtractPositiveNumbersRegex();

    [GeneratedRegex(@"[+-]?\d+")]
    public static partial Regex ExtractAllNumbersRegex();
}
