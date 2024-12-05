using System.Reflection;

namespace AoC.Common;

public static class DayUtils
{
    public static async Task Generate(int day, int year)
    {
        await GenerateInputFile(day, year);
        await GenerateClassFile(day, year);
    }

    private static async Task GenerateInputFile(int day, int year)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var inputFolderPath = Path.Combine(currentDirectory, $"Inputs/{year}");

        if (!Directory.Exists(inputFolderPath))
            Directory.CreateDirectory(inputFolderPath);

        var inputFilePath = Path.Combine(inputFolderPath + $"/{day}.txt");
        var aocInput = await GetAoCInput(day, year);

        File.WriteAllText(inputFilePath, aocInput);
    }

    private static async Task<string> GetAoCInput(int day, int year)
    {
        var url = $"https://adventofcode.com/{year}/day/{day}/input";

        using HttpClient client = new();
        client.DefaultRequestHeaders.Add("Cookie", $"session={GetSessionCookie()}");

        var response = await client.GetAsync(url);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadAsStringAsync()
            : throw new Exception($"Failed to fetch input: {response.ReasonPhrase}.");
    }

    private static string GetSessionCookie()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(currentDirectory, "session.txt");

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Session cookie file is not found.");

        return File.ReadAllText(filePath);
    }

    private static async Task GenerateClassFile(int day, int year)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var templateFolderPath = Path.Combine(currentDirectory, "Common/Template");
        var filePath = Path.Combine(templateFolderPath, "day.txt");

        if (!File.Exists(filePath))
            throw new ArgumentException($"File {filePath} does not exist.");

        var content = await File.ReadAllTextAsync(filePath);
        content = content.Replace("[Year]", year.ToString());
        content = content.Replace("[Day_Nr]", day.ToString());

        var solutionFolderPath = Path.Combine(currentDirectory, $"Solutions/{year}");
        if (!Directory.Exists(solutionFolderPath))
            Directory.CreateDirectory(solutionFolderPath);

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