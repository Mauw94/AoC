namespace AoC.Common;

public static class InputLoading
{
    public static async Task<List<string>> Load(Day day)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var inputFolderPath = Path.Combine(currentDirectory, "Inputs");
        var filePath = Path.Combine(inputFolderPath, $"{day.Year}/{day.DayNr}.txt");

        if (!File.Exists(filePath))
            throw new ArgumentException($"File {filePath} does not exist.");

        var content = await File.ReadAllLinesAsync(filePath);

        return [.. content];
    }
}
