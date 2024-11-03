using AoC.InputHandling.Interfaces;
using Microsoft.Extensions.Logging;

namespace AoC.InputHandling;

public record LocalInputHandlerOptions(string Path);

public class LocalInputHandler(IAOCDownloadService aocService, LocalInputHandlerOptions localInputHandlerOptions, ILogger<LocalInputHandler> logger) : IInputHandler
{
    private string RootPath {get; } = localInputHandlerOptions.Path;
    public async Task<string> GetInput(int year, int day)
    {
        MakeShoureDirectoryExists(Path.Combine(RootPath, year.ToString()));
        
        var filePath = Path.Combine(RootPath, year.ToString(), $"{day}.txt");
        if(!File.Exists(filePath))
        {
            var downloadedInput = await aocService.DownloadInput(year, day);
            if(downloadedInput == string.Empty)
                logger.LogInformation("Writing {} as an empy file, copy https://adventofcode.com/{year}/day/{day}/input to the created empty file", filePath, year, day);
            await File.WriteAllTextAsync(filePath, downloadedInput.TrimEnd('\n').ReplaceLineEndings());
        }
        return await File.ReadAllTextAsync(filePath);
    }
    private static void MakeShoureDirectoryExists(string directory)
    {
        if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }
    
}
