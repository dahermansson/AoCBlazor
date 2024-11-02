using AoC.InputHandling.Interfaces;

namespace AoC.InputHandling;

public class LocalInputHandler(IAOCDownloadService aocService) : IInputHandler
{
    private string BasePath {get; init;} = @"c:\temp\aoc\input";
    public async Task<string> GetInput(int year, int day)
    {
        MakeShoureDirectoryExists(Path.Combine(BasePath, year.ToString()));
        
        var filePath = Path.Combine(BasePath, year.ToString(), $"{day}.txt");
        if(!File.Exists(filePath))
            await File.WriteAllTextAsync(filePath, (await aocService.DownloadInput(year, day)).TrimEnd('\n').ReplaceLineEndings());
            
        return await File.ReadAllTextAsync(filePath);
    }
    private static void MakeShoureDirectoryExists(string directory)
    {
        if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }
    
}
