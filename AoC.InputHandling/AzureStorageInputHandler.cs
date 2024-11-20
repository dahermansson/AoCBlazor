using AoC.InputHandling.Interfaces;
using Azure.Storage.Files.Shares;
using Microsoft.Extensions.Logging;

namespace AoC.InputHandling;

public class AzureStorageInputHandler(IAOCDownloadService aocService, ShareServiceClient shareServiceClient, AzureStorageInputHandlerOptions azureStorageInputHandlerOptions, ILogger<AzureStorageInputHandler> logger) : IInputHandler
{
    private string Share { get; } = azureStorageInputHandlerOptions.Share;
    public async Task<string> GetInput(int year, int day)
    {
        var directory = await MakeShoureDirectoryExists($"{year}"); 
        var file = directory.GetFileClient($"{day}.txt");
        
        if(!await file.ExistsAsync())
        {
            var downloadedInput = await aocService.DownloadInput(year, day);
            if(downloadedInput == string.Empty)
                return string.Empty;
            logger.LogInformation("Uplodaing input year: {year} day: {day}", year, day);
            var inputToStore = System.Text.Encoding.ASCII.GetBytes(downloadedInput.TrimEnd('\n').ReplaceLineEndings());
            using var uploadStream = new MemoryStream(inputToStore);
            await file.CreateAsync(inputToStore.Length);
            await file.UploadRangeAsync(new Azure.HttpRange(0, uploadStream.Length), uploadStream);
        }

        logger.LogInformation("Fetching inputFile year: {year} day: {day}", year, day);
        var download = await file.DownloadAsync();
        using MemoryStream downloadStream = new();
        
        download.Value.Content.CopyTo(downloadStream);
        return System.Text.Encoding.ASCII.GetString(downloadStream.GetBuffer(), 0, (int)downloadStream.Length);
    }
    private async Task<ShareDirectoryClient> MakeShoureDirectoryExists(string directory)
    {
        var shareClient = shareServiceClient.GetShareClient(Share);
        await shareClient.CreateIfNotExistsAsync();
        var directoryClient = shareClient.GetDirectoryClient(directory);
        await directoryClient.CreateIfNotExistsAsync();
        return directoryClient;
    }
    
}
