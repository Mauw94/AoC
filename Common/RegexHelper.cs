
using System.Text.RegularExpressions;

namespace AoC.Common;

public static partial class RegexHelper
{
    [GeneratedRegex(@"\d+")]
    public static partial Regex ExtractNumbersRegex();
}
