using System.Security.Cryptography;
using System.Text;

using AoC.Common;

namespace AoC.Solutions._2015;

public class Day4(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var result = 0;
        var isHashFound = false;

        while (!isHashFound)
        {
            result += 1;
            var newHash = Input[0] + result.ToString();
            var hash = ComputeMD5Hash(newHash);
            if (StartsWithXZeros(hash, 5))
                isHashFound = true;
        }

        return result;
    }

    static string ComputeMD5Hash(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = MD5.HashData(inputBytes);
        var sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }

        return sb.ToString();

    }

    static bool StartsWithXZeros(string hash, int amountOfZeros) => hash.Take(amountOfZeros).All(c => c == '0');

    public override long PartTwo()
    {
        var result = 0;
        var isHashFound = false;

        while (!isHashFound)
        {
            result += 1;
            var newHash = Input[0] + result.ToString();
            var hash = ComputeMD5Hash(newHash);
            if (StartsWithXZeros(hash, 6))
                isHashFound = true;
        }

        return result;
    }
}