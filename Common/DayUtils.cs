using System.Reflection;

namespace AoC.Common;

public static class DayUtils
{
    public static async Task Generate(int day, int year)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var templateFolderPath = Path.Combine(currentDirectory, "Common/Template");
        var filePath = Path.Combine(templateFolderPath, "day.txt");

        if (!File.Exists(filePath))
            throw new ArgumentException($"File {filePath} does not exist.");

        var inputFolderPath = Path.Combine(currentDirectory, $"Inputs/{year}/{day}.txt");
        File.Create(inputFolderPath);

        var content = await File.ReadAllTextAsync(filePath);
        content = content.Replace("[Year]", year.ToString());
        content = content.Replace("[Day_Nr]", day.ToString());

        var outputPath = $"{currentDirectory}/Solutions/{year}/Day{day}.cs";
        await File.WriteAllTextAsync(outputPath, content);
    }

    public static void Run(int day, int year)
    {
        var className = $"AoC.Solutions._{year}.Day{day}";
        var type = Assembly.GetExecutingAssembly().GetType(className);

        if (type is null)
        {
            Console.WriteLine($"Class {className} not found.");
            return;
        }

        if (!typeof(IDaySolution).IsAssignableFrom(type))
        {
            Console.WriteLine($"Calss {className} does not implement IDaySolution.");
            return;
        }

        var dayInstance = new Day(year, day);
        var constructor = type.GetConstructor([typeof(Day)]);

        if (constructor is null)
        {
            Console.WriteLine($"Class {className} does not have a constructor that accepts a Day parameter.");
            return;
        }

        var solutionInstance = (Solution)constructor.Invoke([dayInstance]);
        solutionInstance?.Run();
    }
}