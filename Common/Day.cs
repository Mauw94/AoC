namespace AoC.Common;

public record Day(int Year, int DayNr)
{
    public static Day Create(int year, int dayNr)
    {
        if (year < 2015 || year > DateTime.Now.Year)
            throw new ArgumentException("Enter a valid year.");

        return new(year, dayNr);
    }
}
