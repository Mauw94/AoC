using AoC.Common;

namespace AoC.Solutions._2015;

public class Day11(Day day) : Solution(day)
{
    public override long PartOne()
    {
        var nextPassword = GetNextPassword(Input.First());
        Console.WriteLine(nextPassword);

        return nextPassword.Length;
    }

    public override long PartTwo()
    {
        var nextPassword = GetNextPassword(Input.First());
        Console.WriteLine(nextPassword);

        return nextPassword.Length;
    }

    static string GetNextPassword(string password)
    {
        do
        {
            password = IncrementPassword(password);
        }
        while (!IsValidPassword(password));

        return password;
    }

    static string IncrementPassword(string password)
    {
        char[] chars = password.ToCharArray();
        for (int i = chars.Length - 1; i >= 0; i--)
        {
            if (chars[i] == 'z')
            {
                chars[i] = 'a';
            }
            else
            {
                chars[i]++;
                break;
            }
        }

        return new string(chars);
    }

    static bool IsValidPassword(string password)
    {
        if (!ContainsStraight(password))
            return false;

        if (!ContainsTwoPairs(password))
            return false;

        if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
            return false;

        return true;
    }

    static bool ContainsStraight(string password)
    {
        for (int i = 0; i < password.Length - 2; i++)
        {
            if (password[i] + 1 == password[i + 1] && password[i + 1] + 1 == password[i + 2])
                return true;
        }

        return false;
    }

    static bool ContainsTwoPairs(string password)
    {
        int pairCount = 0;
        for (int i = 0; i < password.Length - 1; i++)
        {
            if (password[i] == password[i + 1])
            {
                pairCount++;
                i++;
            }
        }

        return pairCount >= 2;
    }
}